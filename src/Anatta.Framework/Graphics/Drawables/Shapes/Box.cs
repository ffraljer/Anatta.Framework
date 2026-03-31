using Anatta.Framework.Graphics;
using Anatta.Framework.Interfaces.Graphics;
using OpenTK.Mathematics;

namespace Anatta.Framework.Graphics.Drawables.Shapes;

public class Box : Drawable, IHasGradients {

    public override Texture Texture { get; protected set; }

    public Vector2 Size { get; set; } = new Vector2(100);


    public Box() {
        Texture = Texture.WhitePixel;
    }
    
    public override Vector2 GetSize() => Size;
    
    public ColourInfo? Gradient { get; set; } = null;
    public ColourInfo? BorderGradient { get; set; } = null;
}