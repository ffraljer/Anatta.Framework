using OpenTK.Mathematics;

namespace Anatta.Framework.Graphics;

public partial struct ColourInfo
{
    public Color Top;
    public Color Bottom;
    
    // MAYBE I could replace colours with ColourInfo? MAYBE
    public ColourInfo(Color single) {
        Top = Bottom = single;
    }

    public ColourInfo(Color top, Color bottom) {
        Top = top;
        Bottom = bottom;
    }

    public static ColourInfo GradientVertical(Color top, Color bottom)
        => new ColourInfo(top, bottom);

    public static ColourInfo GradientHorizontal(Color left, Color right)
        => new ColourInfo(left, right);
    
    public Vector4 ToVector4(float t) {
        Color c = AnimationHelper.Lerp(Top, Bottom, t);
        return new Vector4(c.R / 255f, c.G / 255f, c.B / 255f, c.A / 255f);
    }
    
    public Color Darken(float factor = 0.1f)
    {
        factor = Math.Clamp(factor, 0f, 1f);
        return new Color(
            R * (1 - factor),
            G * (1 - factor),
            B * (1 - factor),
            A
        );
    }

    public ColorInfo Lighten(float factor = 0.1f) {
        factor = Math.Clamp(factor, 0f, 1f);
        return new Color(
            R + (255 - R) * factor,
            G + (255 - G) * factor,
            B + (255 - B) * factor,
            A
        );
    }
    public Color Alpha(float alpha) {
        return new Color(R, G, B, alpha);
    }
}