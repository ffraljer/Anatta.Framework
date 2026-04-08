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
    public float StartTime, EndTime;
    public float Time;
    public Func<T, T, float, T> Lerp = null!;
    private float _duration => EndTime - StartTime;

    private bool _started;
    public bool Update()
    {
        if (!_started) {
            og = Getter();
            _started = true;
        }
        
        Time += Framework.Time.Delta;
        if (Time < StartTime)
            return false;
        float t = Math.Clamp((Time - StartTime) / _duration, 0f, 1f);
        t = _EasingHelper.Evaluate(Ease, t);
        Setter(Lerp(Start, End, t));
        if (Time >= EndTime) {
            if (Loop) {
                Time -= _duration;

                if (Restart) {
                    //og = Getter();
                    Start = Getter();
                    // Setter(Start);
                }
                else {
                    Start = End;
                }

                Setter(Start);

                return false;
            }
            else {
                Setter(End);
                return true;
            }
        }
        return false;
    }
}