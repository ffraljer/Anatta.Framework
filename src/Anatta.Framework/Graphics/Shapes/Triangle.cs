using Anatta.Framework.Graphics.Interfaces;
using Anatta.Framework.Graphics.Sprites;
using OpenTK.Mathematics;

namespace Anatta.Framework.Graphics.Shapes; 
public class Triangle : Drawable {
    public float Thickness { get; set; } = 0f;
    public Color BorderColor { get; set; } = Framework.Graphics.Color.White;
    public Color FillColor { get; set; } = Framework.Graphics.Color.Transparent;

    public ColorInfo? Gradient { get; set; } = null;
    public ColorInfo? BorderGradient { get; set; } = null;

    public override Texture Texture { get; protected set; } = Texture.WhitePixel;

    public override Vector2 GetSize() {
        return Scale;
    }
}
