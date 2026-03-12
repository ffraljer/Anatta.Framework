using OpenTK.Mathematics;

namespace Anatta.Framework.Graphics.Animations;

public static class AnimationHelper {
    public static Vector2 Lerp(Vector2 a, Vector2 b, float t) {
        return a + (b - a) * t;
    }

    public static float Lerp(float a, float b, float t) {
        return a + (b - a) * t;
    }
}
