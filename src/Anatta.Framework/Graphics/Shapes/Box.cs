using Anatta.Framework.Graphics.Sprites;
using Anatta.Framework.Graphics.Interfaces;
using OpenTK.Mathematics;

namespace Anatta.Framework.Graphics.Shapes;

public class Box : Drawable {

    public override Texture Texture { get; protected set; } = Texture.WhitePixel;

    public ColorInfo BorderColour { get; set; } = Color.White;
    public float BorderThickness { get; set; } = 0f;

    public Vector2 Size { get; set; } = new Vector2(100);

    public override Vector2 GetSize() => Size;
}