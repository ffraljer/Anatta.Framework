namespace Anatta.Framework.Graphics;

using OpenTK.Mathematics;

public partial struct Colour
{
    public float R, G, B, A;

    public Colour(byte r, byte g, byte b, byte a = 255)
    {
        R = r;
        G = g;
        B = b;
        A = a;
    }
    public Colour(float r, float g, float b, float a = 255f)
    {
        R = r;
        G = g;
        B = b;
        A = a;
    }
    
    public Colour(string hex)
    {
        if (hex.StartsWith("#"))
            hex = hex.Substring(1);

        if (hex.Length == 3)
        {
            hex = string.Concat(hex[0], hex[0], hex[1], hex[1], hex[2], hex[2]);
        }
        else if (hex.Length == 4)
        {
            hex = string.Concat(
                hex[0], hex[0],
                hex[1], hex[1],
                hex[2], hex[2],
                hex[3], hex[3]
            );
        }

        if (hex.Length == 6)
        {
            R = Convert.ToByte(hex.Substring(0, 2), 16);
            G = Convert.ToByte(hex.Substring(2, 2), 16);
            B = Convert.ToByte(hex.Substring(4, 2), 16);
            A = 255;
        }
        else if (hex.Length == 8)
        {
            R = Convert.ToByte(hex.Substring(0, 2), 16);
            G = Convert.ToByte(hex.Substring(2, 2), 16);
            B = Convert.ToByte(hex.Substring(4, 2), 16);
            A = Convert.ToByte(hex.Substring(6, 2), 16);
        }
        else
        {
            throw new ArgumentException($"invalid hex: {hex}");
        }
    }

    public Vector4 ToVector4()
    {
        return new Vector4(
            R / 255f,
            G / 255f,
            B / 255f,
            A / 255f
        );
    }
   public Color4<Rgba> ToColor4() {
       return new Color4<Rgba>(
                R / 255f,
                G / 255f,
                B / 255f,
                A / 255f
            );
   }
    public static implicit operator Color4<Rgba>(Colour c) => c.ToColor4();
}