using System.Collections;
using System.Globalization;
using Anatta.Framework.Graphics.Interfaces;
using OpenTK.Mathematics;

namespace Anatta.Framework.Graphics.Shaders;

public class PropertyCollection : IEnumerable<ShaderProperty> {
    private readonly Dictionary<string, ShaderProperty> _properties = new();
    private readonly Shader _owner;

    public bool HasChanged { get; private set; }
    public int Count => _properties.Count;

    public PropertyCollection(Shader owner) {
        _owner = owner;
    }

    public object? this[string name] {
        get => _properties.TryGetValue(name, out var p) ? p.Value : null;
        set {
            if (value == null) return;
            if (!_properties.TryGetValue(name, out var prop)) return;

            object coerced;
            if (value.GetType() != prop.Type) {
                try {
                    coerced = Convert.ChangeType(value, prop.Type, CultureInfo.InvariantCulture);
                }
                catch {
                    return;
                }
            }
            else {
                coerced = value;
            }

            if (prop.Value != null && prop.Value.Equals(coerced)) return;

            prop.Value = coerced;

            if (_owner.HasBegun)
                Dispatch(_owner.Backend!, prop);
            else
                HasChanged = true;
        }
    }

    public void Set() {
        if (!HasChanged) return;
        if (_owner.Backend == null) return;

        foreach (var prop in _properties.Values)
            Dispatch(_owner.Backend, prop);

        HasChanged = false;
    }

    private static void Dispatch(IShader backend, ShaderProperty prop) {
        if (prop.Value == null) return;
        switch (prop.Value) {
            case float f: backend.SetFloat(prop.Name, f); break;
            case int i: backend.SetInt(prop.Name, i); break;
            case bool b: backend.SetBool(prop.Name, b); break;
            case Vector2 v: backend.SetVector2(prop.Name, v); break;
            case Vector3 v: backend.SetVector3(prop.Name, v); break;
            case Vector4 v: backend.SetVector4(prop.Name, v); break;
            case Matrix4 m: backend.SetMatrix4(prop.Name, m); break;
            default:
                throw new NotSupportedException(
                    $"ShaderProperty: {prop.Value.GetType().Name} not supported for '{prop.Name}'");
        }
    }

    public void Add(ShaderProperty prop) => _properties[prop.Name] = prop;

    public void AddRange(ICollection<ShaderProperty> props) {
        foreach (var p in props) _properties[p.Name] = p;
    }

    public bool Remove(string name) => _properties.Remove(name);
    public bool ContainsValue(string name) => _properties.ContainsKey(name);

    public void Clear() {
        _properties.Clear();
        HasChanged = true;
    }

    public IEnumerator<ShaderProperty> GetEnumerator() {
        foreach (var p in _properties.Values) yield return p;
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}