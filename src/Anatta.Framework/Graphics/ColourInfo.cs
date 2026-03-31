using OpenTK.Mathematics;

namespace Anatta.Framework.Graphics;

public struct ColourInfo
{
    public Colour Top;
    public Colour Bottom;

    public ColourInfo(Colour single) {
        Top = Bottom = single;
    }

    public ColourInfo(Colour top, Colour bottom) {
        Top = top;
        Bottom = bottom;
    }

    public static ColourInfo GradientVertical(Colour top, Colour bottom)
        => new ColourInfo(top, bottom);

    public static ColourInfo GradientHorizontal(Colour left, Colour right)
        => new ColourInfo(left, right);
    
    public Vector4 ToVector4(float t) {
        Colour c = AnimationHelper.Lerp(Top, Bottom, t);
        return new Vector4(c.R / 255f, c.G / 255f, c.B / 255f, c.A / 255f);
    }
}