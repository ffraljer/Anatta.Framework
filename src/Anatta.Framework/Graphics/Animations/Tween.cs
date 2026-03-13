namespace Anatta.Framework.Graphics.Animations;

internal class Tween<T> : ITween {
    public Func<T> Getter = null!;
    public Action<T> Setter = null!;
    public T Start = default!;
    public bool Loop;
    public T End = default!;
    public float Duration;
    public float Time;
    public Func<T, T, float, T> Lerp = null!;

    private bool started;
    public bool Update(float dt)
    {
        if (!started)
        {
            Start = Getter();
            started = true;
        }
        if (Duration <= 0f)
        {
            Setter(End);
            return true;
        }
        Time += dt;
        float t = Math.Clamp(Time / Duration, 0f, 1f);
        Setter(Lerp(Start, End, t));
        if (Time >= Duration)
        {
            if (Loop)
            {
                Time = 0f;
                Setter(Start);
                return false;
            }
            else
            {
                Setter(End);
                return true;
            }
        }
        return false;
    }
}