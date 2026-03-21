namespace Anatta.Framework.Graphics;

internal class Tween<T> : ITween {
    public Func<T> Getter = null!;
    public Action<T> Setter = null!;
    public bool Restart;
    private T og = default!;
    public T Start = default!;
    public Easing Ease = Easing.None;
    public bool Loop;
    public float Delay;
    public T End = default!;
    public float Duration;
    public float Time;
    public Func<T, T, float, T> Lerp = null!;

    private bool _started;
    public bool Update()
    {
        if (Delay > 0f)
        {
            Delay -= Framework.Time.Delta;
            return false;
        }
        if (!_started)
        {
            
            og = Getter();
            Start = og;
            _started = true;
        }
        if (Duration <= 0f)
        {
            Setter(End);
            return true;
        }
        Time += Framework.Time.Delta;
        float t = Math.Clamp(Time / Duration, 0f, 1f);
        t = _EasingHelper.Evaluate(Ease, t);
        Setter(Lerp(Start, End, t));
        if (Time >= Duration)
        {
            if (Loop)
            {
                Time = 0f;
                if (Restart) {
                    Start = og;
                    Setter(og);
                }
                else {
                    Start = og;
                }
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