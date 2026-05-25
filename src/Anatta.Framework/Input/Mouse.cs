namespace Anatta.Framework.Input;

public static class Mouse {
    private static readonly HashSet<Button> _current = new();
    private static readonly HashSet<Button> _previous = new();

    public static float X { get; private set; }
    public static float Y { get; private set; }

    public static float DeltaX { get; private set; }
    public static float DeltaY { get; private set; }
    public static System.Numerics.Vector2 CursorPosition { get; private set; }
    public static System.Numerics.Vector2 CursorPositionDelta { get; private set; }

    internal static void BeginFrame() {
        _previous.Clear();
        foreach (var b in _current)
            _previous.Add(b);

        DeltaX = 0;
        DeltaY = 0;

        CursorPosition = new(0);
    }

    internal static void ButtonDown(Button button) {
        _current.Add(button);
    }

    internal static void ButtonUp(Button button) {
        _current.Remove(button);
    }

    internal static void Move(float x, float y) {
        DeltaX = x - X;
        DeltaY = y - Y;

        X = x;
        Y = y;
        CursorPosition = new(x, y);
        CursorPositionDelta = new(x - X, y - Y);
    }

    public static bool IsButtonDown(Button button) {
        return _current.Contains(button);
    }

    public static bool IsButtonPressed(Button button) {
        return _current.Contains(button) && !_previous.Contains(button);
    }

    public static bool IsButtonReleased(Button button) {
        return !_current.Contains(button) && _previous.Contains(button);
    }
    public enum Button
    {
        Left,
        Middle,
        Right,
        X1,
        X2
    }
}