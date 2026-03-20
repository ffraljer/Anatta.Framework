using OpenTK.Mathematics;

namespace Anatta.Framework.Graphics;

public static class AnimationHelper {
    public static Vector2 Lerp(Vector2 a, Vector2 b, float t) {
        return a + (b - a) * t;
    }

    public static float Lerp(float a, float b, float t) {
        return a + (b - a) * t;
    }
    
    public static Colour Lerp(Colour a, Colour b, float t)
    {
        return new Colour(
            MathHelper.Lerp(a.R, b.R, t),
            MathHelper.Lerp(a.G, b.G, t),
            MathHelper.Lerp(a.B, b.B, t),
            MathHelper.Lerp(a.A, b.A, t)
        );
    }
}
