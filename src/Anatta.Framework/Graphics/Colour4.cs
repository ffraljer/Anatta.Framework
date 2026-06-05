namespace Anatta.Framework.Graphics;

using OpenTK.Mathematics;

public partial struct Colour4
{
    public static bool operator ==(Colour4 a, Colour4 b) => a.R == b.R && a.G == b.G && a.B == b.B && a.A == b.A;
    public static bool operator !=(Colour4 a, Colour4 b) => !(a == b);
    
    public override bool Equals(object? obj) => obj is Colour4 c && this == c;
    public override int GetHashCode() => HashCode.Combine(R, G, B, A);
    
    public float R, G, B, A;

    public Colour4(byte r, byte g, byte b, byte a = 255)
    {
        R = r;
        G = g;
        B = b;
        A = a;
    }
    public Colour4(float r, float g, float b, float a = 255f)
    {
        R = r;
        G = g;
        B = b;
        A = a;
    }
    
    public Colour4(string hex)
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
        return new(
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
   public Colour4 Darken(float factor = 0.1f)
   {
       factor = Math.Clamp(factor, 0f, 1f);
       return new Colour4(
           R * (1 - factor),
           G * (1 - factor),
           B * (1 - factor),
           A
       );
   }

   public Colour4 Lighten(float factor = 0.1f) {
       factor = Math.Clamp(factor, 0f, 1f);
       return new Colour4(
           R + (255 - R) * factor,
           G + (255 - G) * factor,
           B + (255 - B) * factor,
           A
       );
   }
   public Colour4 Alpha(float alpha) {
       return new Colour4(R, G, B, alpha);
   }

   public static implicit operator Color4<Rgba>(Colour4 c) => c.ToColor4();
   public static implicit operator ColourInfo(Colour4 c)
       => new ColourInfo(c);
}