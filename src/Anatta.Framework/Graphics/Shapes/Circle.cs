using Anatta.Framework.Graphics.Sprites;
using Anatta.Framework.Graphics.Interfaces;
using OpenTK.Mathematics;

namespace Anatta.Framework.Graphics.Shapes;

public class Circle : Drawable {
    public float Radius { get; set; } = 50f;
    public float Thickness { get; set; } = 0f;
    public ColorInfo BorderColor { get; set; } = Framework.Graphics.Color.White;
    public ColorInfo FillColor { get; set; } = Framework.Graphics.Color.Transparent;

    public override Texture Texture { get; protected set; } = Texture.WhitePixel;

    public override Vector2 GetSize() {
        return new Vector2(Radius * 2f);
    }
}