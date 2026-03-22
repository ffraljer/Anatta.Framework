using OpenTK.Mathematics;

namespace Anatta.Framework.Graphics.Helpers;

public class GradientFactory {
    public static Gradient Vertical(Colour top, Colour bottom)
        => new Gradient(top, bottom, new Vector2(0, 1));

    public static Gradient Horizontal(Colour left, Colour right)
        => new Gradient(left, right, new Vector2(1, 0));

    public static Gradient Angle(Colour start, Colour end, float degrees)
    {
        float rad = MathHelper.DegreesToRadians(degrees);
        return new Gradient(start, end, new Vector2(MathF.Cos(rad), MathF.Sin(rad)));
    }
}