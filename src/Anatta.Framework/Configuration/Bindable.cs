namespace Anatta.Framework.Configuration;

public class Bindable<T>(T defaultValue = default!) {
    private T _value = defaultValue;
    
    public T Value
    {
        get => _value;
        set
        {
            _value = value;
            ValueChanged?.Invoke(value);
        }
    }
    
    public event Action<T>? ValueChanged;

    public static implicit operator T(Bindable<T> bindable) => bindable.Value;

    public override string ToString() {
        return _value?.ToString() ?? "null";
    }
}