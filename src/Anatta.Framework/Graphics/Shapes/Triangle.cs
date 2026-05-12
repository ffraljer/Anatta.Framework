using Anatta.Framework.Graphics.Interfaces;
using Anatta.Framework.Graphics.Sprites;
using OpenTK.Mathematics;

namespace Anatta.Framework.Graphics.Shapes; 
public class Triangle : Sprite {
    public float Thickness { get; set; } = 0f;
    public Colour4 BorderColour4 { get; set; } = Colour4.White;
    public Colour4 FillColour4 { get; set; } = Colour4.Transparent;

    public Triangle() : base(Texture.WhitePixel) {
    }

    public override Vector2 GetSize() {
        return Scale;
    }
}
