using System;

// Example Usage:
// A class: public ObservableValue<int> Health = new ObservableValue<int>(100);
// Other class: MyClass.Health.OnValueChanged += HealthChanged;

/// <summary>
/// A generic value that can be observed.
/// The OnValueChanged Action gets invoked every time this value gets changed.
/// </summary>
/// <typeparam name="T"></typeparam>
public class ObservableValue<T>
{
    public event Action<T> OnValueChanged;

    private T _value;

    public T Value
    {
        get { return _value; }
        set
        {
            _value = value;
            OnValueChanged.InvokeSafely(_value);
        }
    }

    public ObservableValue(T t)
    {
        _value = t;
    }
}