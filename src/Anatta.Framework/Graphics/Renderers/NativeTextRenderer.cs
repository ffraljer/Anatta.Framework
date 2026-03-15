using SixLabors.Fonts;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Drawing.Processing;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.Drawing;
using _Colour = SixLabors.ImageSharp.Color;

namespace Anatta.Framework.Graphics.Renderers; 
public static class NativeTextRenderer {
    public static Texture CreateString(
        FontFace font,
        string text,
        float size,
        Colour colour,
        int padding = 4) {
        
        var data = font.Bytes;
        FontCollection x = new();
        FontFamily y = x.Add(new MemoryStream(data));
        Font z = y.CreateFont(size);

        TextOptions w = new(z) {
            HorizontalAlignment = HorizontalAlignment.Left,
            VerticalAlignment = VerticalAlignment.Top
        };

        FontRectangle xx = TextMeasurer.MeasureBounds(text, w);
        
        int width = (int)MathF.Ceiling(xx.Width) + padding * 2 * 2;
        int height = (int)MathF.Ceiling(xx.Height) + padding * 2 * 2;
        
        float drawX = padding;
        float drawY = padding;

        using Image<Rgba32> yy = new(width, height);
        yy.Mutate(ctx =>
        {
            ctx.Clear(_Colour.Transparent);
            ctx.DrawText(
                text,
                z,
                new _Colour(new System.Numerics.Vector4(colour.R, colour.G, colour.B, 255f)),
                new PointF(drawX, drawY));
        });
    
        byte[] zz = new byte[width * height * 4];
        yy.CopyPixelDataTo(zz);

        return new Texture(width, height, zz);
    }
}
