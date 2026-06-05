using Anatta.Framework.Graphics.Interfaces;
using Anatta.Framework.Graphics.Rendering;
using Anatta.Framework.Graphics.Sprites;
using OpenTK.Mathematics;

namespace Anatta.Framework.Graphics.Shapes;

public class Box : Drawable {
    public ColourInfo BorderColour { get; set; } = Colour4.White;
    public float BorderThickness { get; set; } = 0f;

    public Vector2 Size { get; set; } = new Vector2(100);

    public override Vector2 GetSize() => Size;

    public Box() {
    }
    public override RenderCommand BuildRenderCommand(Vector2i screenSize) {
        var size = Size * Scale;
        return new RenderCommand {
            Texture = Texture.WhitePixel,
            TintTop = Colour.Top.ToVector4(),
            TintBottom = Colour.Bottom.ToVector4(),
            BorderTop = BorderColour.Top.ToVector4(),
            BorderBottom = BorderColour.Bottom.ToVector4(),
            Size = size,
            Radius = CornerRadius,
            BoxBorderThickness = BorderThickness,
            Transform = BuildTransform(size, screenSize)
        };
    }
}