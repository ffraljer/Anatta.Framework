namespace Anatta.Framework.Graphics;

internal static class _EasingHelper {
    //taken from fraljer client lol
    public static float Evaluate(Easing easing, float t) {
        return easing switch {
            Easing.None => t,
            Easing.InSine => 1 - MathF.Cos((t * MathF.PI) / 2),
            Easing.OutSine => MathF.Sin((t * MathF.PI) / 2),
            Easing.InOutSine => -(MathF.Cos(MathF.PI * t) - 1) / 2,
            Easing.InQuad => t * t,
            Easing.OutQuad => 1 - (1 - t) * (1 - t),
            Easing.InOutQuad => t < 0.5f
                ? 2 * t * t
                : 1 - MathF.Pow(-2 * t + 2, 2) / 2,
            Easing.InCubic => t * t * t,
            Easing.OutCubic => 1 - MathF.Pow(1 - t, 3),
            Easing.InOutCubic => t < 0.5f
                ? 4 * t * t * t
                : 1 - MathF.Pow(-2 * t + 2, 3) / 2,
            Easing.InQuart => t * t * t * t,
            Easing.OutQuart => 1 - MathF.Pow(1 - t, 4),
            Easing.InOutQuart => t < 0.5f
                ? 8 * t * t * t * t
                : 1 - MathF.Pow(-2 * t + 2, 4) / 2,
            Easing.InQuint => t * t * t * t * t,
            Easing.OutQuint => 1 - MathF.Pow(1 - t, 5),
            Easing.InOutQuint => t < 0.5f
                ? 16 * t * t * t * t * t
                : 1 - MathF.Pow(-2 * t + 2, 5) / 2,

            _ => t
        };
    }
}