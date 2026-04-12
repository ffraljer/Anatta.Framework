using OpenTK.Mathematics;

namespace Anatta.Framework.Graphics;

public partial struct ColorInfo
{
    public Color Top;
    public Color Bottom;
    
    public ColorInfo(Color single) {
        Top = Bottom = single;
    }

    public ColorInfo(Color top, Color bottom) {
        Top = top;
        Bottom = bottom;
    }

    public static ColorInfo GradientVertical(Color top, Color bottom)
        => new ColorInfo(top, bottom);

    public static ColorInfo GradientHorizontal(Color left, Color right)
        => new ColorInfo(left, right);
    
    public Vector4 ToVector4(float t) {
        Color c = AnimationHelper.Lerp(Top, Bottom, t);
        return new Vector4(c.R / 255f, c.G / 255f, c.B / 255f, c.A / 255f);
    }
    
    public ColorInfo Darken(float factor = 0.1f)
    {
        return new ColorInfo(
            Top.Darken(factor),
            Bottom.Darken(factor)
        );
    }

    public ColorInfo Lighten(float factor = 0.1f)
    {
        return new ColorInfo(
            Top.Lighten(factor),
            Bottom.Lighten(factor)
        );
    }
    public ColorInfo Alpha(float alpha)
    {
        return new ColorInfo(
            Top.Alpha(alpha),
            Bottom.Alpha(alpha)
        );
    }
}