namespace Anatta.Framework.Configuration;

public class Bindable<T> {
    private T _value;
    
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
    
    public Bindable(T defaultValue = default!)
    {
        _value = defaultValue;
    }
    
    public static implicit operator T(Bindable<T> bindable) => bindable.Value;

    public override string ToString() {
        return _value?.ToString() ?? "null";
    }
}