using Anatta.Framework.Interfaces.Graphics;
using OpenTK.Mathematics;

namespace Anatta.Framework.Graphics;

public class Sprite : ISprite, IUpdatable
{
    public Texture Texture { get; }
    public Vector2 Position { get; set; }
    public Vector2 Scale { get; set; } = Vector2.One;
    public float Rotation { get; set; }
    public Vector2 Velocity { get; set; }
    
    public Sprite(Texture texture) => Texture = texture;
    public Sprite(string textureName) => Texture = Texture.Load(textureName);

    public void Update(float deltaTime)
    {
        Position += Velocity * deltaTime;
    }

    public void Draw(Batcher batcher)
    {
        batcher.Draw(this);
    }

    public void Dispose() => Texture.Dispose();
    
}