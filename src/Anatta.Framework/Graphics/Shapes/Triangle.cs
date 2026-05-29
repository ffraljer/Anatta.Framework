using Anatta.Framework.Graphics.Interfaces;
using Anatta.Framework.Graphics.Rendering;
using Anatta.Framework.Graphics.Sprites;
using OpenTK.Mathematics;

namespace Anatta.Framework.Graphics.Shapes; 
public class Triangle : Drawable {
    public float Thickness { get; set; } = 0f;
    public ColourInfo BorderColour { get; set; } = Colour4.White;

    public Triangle() {
    }

    public override Vector2 GetSize() {
        return Scale;
    }
    
    public override RenderCommand BuildRenderCommand(Vector2i screenSize) {
        var size = GetSize();
        var transform = BuildTransform(size, screenSize);
    
        return new RenderCommand {
            Texture = Texture.WhitePixel,
            Transform = transform,
            TintTop = Colour.ToVector4(0f),
            TintBottom = Colour.ToVector4(1f),
            BorderTop = BorderColour.Top.ToVector4(),
            BorderBottom = BorderColour.Bottom.ToVector4(),
            Size = size,
            Triangle = 1f
        };
    }
}
