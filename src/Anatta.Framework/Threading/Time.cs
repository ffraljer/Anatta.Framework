namespace Anatta.Framework.Threading;

public class Time
{
    public static float Delta { get; private set; }

    public static float Total { get; private set; }

    internal static void Update(float delta)
    {
        Delta = delta;
        Total += delta;
    }
}