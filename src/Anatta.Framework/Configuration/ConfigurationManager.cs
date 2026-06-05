using System.Text.Json;
using System.Text.Json.Serialization;

namespace Anatta.Framework.Configuration;

[AttributeUsage(AttributeTargets.Field)]
public class ConfigKeyAttribute(string key, object? defaultValue = null) : Attribute {
    public string Key { get; } = key;
    public object? DefaultValue { get; } = defaultValue;
}

public abstract class ConfigurationManager
{
    private readonly string _filePath;
    private bool _initialized;
    
    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        WriteIndented = true,
        Converters = { new JsonStringEnumConverter() }
    };
    
    /// <remarks>Even if the <see cref="Bindable{T}"/> is static, you will still need to instantiate it at least once.</remarks>
    protected ConfigurationManager(string filePath)
    {
        _filePath = filePath;
    }
    
    private readonly Dictionary<string, object> _values = new();

    protected Bindable<T> GetValue<T>(string key, T defaultValue = default!)
    {
        if (_values.TryGetValue(key, out var existing))
        {
            if (existing is Bindable<T> typed)
                return typed;

            throw new InvalidOperationException(
                $"'{key}' was requested as {typeof(T)}, but is {existing.GetType()}");
        }

        var bindable = new Bindable<T>(defaultValue);
        _values[key] = bindable;
        return bindable;
    }
    
    public void Initialize()
    {
        if (_initialized)
            throw new InvalidOperationException("Configuration already initialized.");
        _reg();
        Load();
        _initialized = true;
    }

    protected void SetValue<T>(string key, T value)
    {
        var bindable = GetValue<T>(key);
        bindable.Value = value;
    }

    protected T Get<T>(string key)
    {
        return GetValue<T>(key).Value;
    }

    protected void WriteValue<T>(string key, T value)
    {
        SetValue(key, value);
    }
    
    public void Save()
    {
        var dict = new Dictionary<string, object?>();

        foreach (var (key, value) in _values)
        {
            var valProp = value.GetType().GetProperty("Value");
            dict[key] = valProp?.GetValue(value);
        }

        var json = JsonSerializer.Serialize(dict, new JsonSerializerOptions
        {
            WriteIndented = true,
            Converters = { new JsonStringEnumConverter() }
        });

        File.WriteAllText(_filePath, json);
    }
    private void _reg()
    {
        var fields = GetType().GetFields(
            System.Reflection.BindingFlags.Instance |
            System.Reflection.BindingFlags.Static |
            System.Reflection.BindingFlags.Public |
            System.Reflection.BindingFlags.NonPublic);

        foreach (var field in fields)
        {
            var attr = field.GetCustomAttributes(typeof(ConfigKeyAttribute), false)
                .FirstOrDefault() as ConfigKeyAttribute;

            if (attr == null)
                continue;

            var fieldType = field.FieldType;

            if (!fieldType.IsGenericType || fieldType.GetGenericTypeDefinition() != typeof(Bindable<>))
                throw new InvalidOperationException($"{field.Name} must be Bindable");

            var valueType = fieldType.GetGenericArguments()[0];

            var method = typeof(ConfigurationManager)
                .GetMethod(nameof(GetValue), System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic)!
                .MakeGenericMethod(valueType);

            var bindable = method.Invoke(this, [
                attr.Key,
                attr.DefaultValue ?? (valueType.IsValueType ? Activator.CreateInstance(valueType) : null)
            ]);
            
            _AutoSave(bindable);
            field.SetValue(field.IsStatic ? null : this, bindable);
        }
    }
    public void Load()
    {
        if (!File.Exists(_filePath))
        {
            Save();
            return;
        }

        var json = File.ReadAllText(_filePath);

        var data = JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(json, JsonOptions);
        if (data == null) return;

        foreach (var (key, element) in data)
        {
            if (!_values.TryGetValue(key, out var bindable))
                continue;

            var type = bindable.GetType().GenericTypeArguments[0];

            object? value = type switch
            {
                var t when t.IsEnum => JsonSerializer.Deserialize(element.GetRawText(), type, JsonOptions),
                var t when t == typeof(int) => element.GetInt32(),
                var t when t == typeof(float) => element.GetSingle(),
                var t when t == typeof(double) => element.GetDouble(),
                var t when t == typeof(bool) => element.GetBoolean(),
                var t when t == typeof(string) => element.GetString(),
                _ => JsonSerializer.Deserialize(element.GetRawText(), type, JsonOptions)
            };

            bindable.GetType().GetProperty("Value")?.SetValue(bindable, value);
        }
    }
    private void _AutoSave(object? bindable)
    {
        var eventInfo = bindable?.GetType().GetEvent("ValueChanged");
        if (eventInfo == null) return;

        var handlerType = eventInfo.EventHandlerType!;

        var invokeMethod = handlerType.GetMethod("Invoke")!;
        var paramType = invokeMethod.GetParameters()[0].ParameterType;

        var dynamicHandler = Delegate.CreateDelegate(
            handlerType,
            this,
            typeof(ConfigurationManager)
                .GetMethod(nameof(_SaveDelegate), System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic)!
                .MakeGenericMethod(paramType)
        );

        eventInfo.AddEventHandler(bindable, dynamicHandler);
    }

    private void _SaveDelegate<T>(T _)
    {
        Save();
    }
}