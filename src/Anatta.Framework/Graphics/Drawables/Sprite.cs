using Anatta.Framework.Graphics;
using OpenTK.Mathematics;

namespace Anatta.Framework.Graphics.Drawables;

public class Sprite : Drawable
{
    public override Texture Texture { get; protected set; }
    
    public Vector2 Velocity { get; set; } = Vector2.Zero;
    public Sprite(Texture texture) => Texture = texture;
    public Sprite(string textureName) => Texture = Texture.Load(textureName);
    public override void Dispose() => Texture.Dispose();
    
    public override Vector2 GetSize() => new Vector2(Texture.Width, Texture.Height);
}