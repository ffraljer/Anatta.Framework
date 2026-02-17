namespace Anatta.Framework;

public class Time
{
    public float Delta { get; private set; }

    public float Total { get; private set; }

    internal void Update(float delta)
    {
        Delta = delta;
        Total += delta;
    }
}