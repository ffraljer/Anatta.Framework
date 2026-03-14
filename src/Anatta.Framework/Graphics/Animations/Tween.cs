namespace Anatta.Framework.Graphics.Animations;

internal class Tween<T> : ITween {
    public Func<T> Getter = null!;
    public Action<T> Setter = null!;
    public T Start = default!;
    public Easing Ease = Easing.None;
    public bool Loop;
    public T End = default!;
    public float Duration;
    public float pTime;
    public Func<T, T, float, T> Lerp = null!;

    private bool started;
    public bool Update()
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
        pTime += Time.Delta;
        float t = Math.Clamp(pTime / Duration, 0f, 1f);
        t = _ease.Evaluate(Ease, t);
        Setter(Lerp(Start, End, t));
        if (pTime >= Duration)
        {
            if (Loop)
            {
                pTime = 0f;
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