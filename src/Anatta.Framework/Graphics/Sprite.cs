using Anatta.Framework.Graphics.Animations;
using Anatta.Framework.Graphics.Helpers;
using Anatta.Framework.Interfaces.Graphics;
using OpenTK.Mathematics;

namespace Anatta.Framework.Graphics;

public class Sprite : _Sprite
{
    public override Texture Texture { get; protected set; }
    
    public Vector2 Velocity { get; set; } = Vector2.Zero;
    public Sprite(Texture texture) => Texture = texture;
    public Sprite(string textureName) => Texture = Texture.Load(textureName);
    public override void Dispose() => Texture.Dispose();
}