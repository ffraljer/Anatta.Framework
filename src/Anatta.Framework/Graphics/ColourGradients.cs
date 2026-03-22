namespace Anatta.Framework.Graphics;

public partial struct Colour {
    private static float Clamp(float value, float min, float max) {
        if (value < min) return min;
        if (value > max) return max;
        return value;
    }
    public static Colour GradientVertical(Colour top, Colour bottom, float t)
    {
        t = Clamp(t, 0f, 1f);
        return new Colour(
            top.R + (bottom.R - top.R) * t,
            top.G + (bottom.G - top.G) * t,
            top.B + (bottom.B - top.B) * t,
            top.A + (bottom.A - top.A) * t
        );
    }

    public static Colour GradientHorizontal(Colour left, Colour right, float t)
    {
        t = Clamp(t, 0f, 1f);
        return new Colour(
            left.R + (right.R - left.R) * t,
            left.G + (right.G - left.G) * t,
            left.B + (right.B - left.B) * t,
            left.A + (right.A - left.A) * t
        );
    }
}