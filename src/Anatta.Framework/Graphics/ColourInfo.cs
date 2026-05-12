using Anatta.Framework.Graphics.Animations;
using OpenTK.Mathematics;

namespace Anatta.Framework.Graphics;

public partial struct ColourInfo
{
    public Colour4 Top;
    public Colour4 Bottom;
    
    public ColourInfo(Colour4 single) {
        Top = Bottom = single;
    }

    public ColourInfo(Colour4 top, Colour4 bottom) {
        Top = top;
        Bottom = bottom;
    }

    public static ColourInfo GradientVertical(Colour4 top, Colour4 bottom)
        => new ColourInfo(top, bottom);

    public static ColourInfo GradientHorizontal(Colour4 left, Colour4 right)
        => new ColourInfo(left, right);
    
    public Vector4 ToVector4(float t) {
        Colour4 c = AnimationHelper.Lerp(Top, Bottom, t);
        return new Vector4(c.R / 255f, c.G / 255f, c.B / 255f, c.A / 255f);
    }
    
    public ColourInfo Darken(float factor = 0.1f)
    {
        return new ColourInfo(
            Top.Darken(factor),
            Bottom.Darken(factor)
        );
    }

    public ColourInfo Lighten(float factor = 0.1f)
    {
        return new ColourInfo(
            Top.Lighten(factor),
            Bottom.Lighten(factor)
        );
    }
    public ColourInfo Alpha(float alpha)
    {
        return new ColourInfo(
            Top.Alpha(alpha),
            Bottom.Alpha(alpha)
        );
    }
}