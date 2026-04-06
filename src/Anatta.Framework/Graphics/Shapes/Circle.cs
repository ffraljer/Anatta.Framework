using Anatta.Framework.Graphics.Sprites;
using Anatta.Framework.Graphics.Interfaces;
using OpenTK.Mathematics;

namespace Anatta.Framework.Graphics.Shapes;

public class Circle : Drawable, IHasGradients{
    public float Radius { get; set; } = 50f;
    public float Thickness { get; set; } = 0f;
    public Colour BorderColour { get; set; } = Colour.White;
    public Colour FillColour { get; set; } = Colour.Transparent;
    
    public ColourInfo? Gradient { get; set; } = null;
    public ColourInfo? BorderGradient { get; set; } = null;

    public override Texture Texture { get; protected set; } = Texture.WhitePixel;

    public override Vector2 GetSize() {
        return new Vector2(Radius * 2f);
    }
}