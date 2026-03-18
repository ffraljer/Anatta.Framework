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
}