using Anatta.Framework.Graphics.Interfaces;
using Anatta.Framework.Graphics.Rendering;
using Anatta.Framework.Graphics.Sprites;
using OpenTK.Mathematics;

namespace Anatta.Framework.Graphics.Shapes;

public class Circle : Drawable {
    public float Radius { get; set; } = 50f;
    public float Thickness { get; set; } = 0f;
    public ColourInfo BorderColour { get; set; } = Colour4.White;

    public Circle() {
    }

    public override Vector2 GetSize() {
        return new Vector2(Radius * 2f);
    }

    public override RenderCommand BuildRenderCommand(Vector2i screenSize) {
        var size = GetSize();
        var scaledSize = size * MathF.Max(Scale.X, Scale.Y);

        return new RenderCommand {
            Texture = Texture.WhitePixel,
            TintTop = Colour.Top.ToVector4(),
            TintBottom = Colour.Bottom.ToVector4(),
            BorderTop = BorderColour.Top.ToVector4(),
            BorderBottom = BorderColour.Bottom.ToVector4(),
            CircleRadius = Radius * MathF.Max(Scale.X, Scale.Y),
            CircleThickness = Thickness * MathF.Max(Scale.X, Scale.Y),
            Size = scaledSize,
            Transform = BuildTransform(scaledSize, screenSize)
        };
    }
}