using OpenTK.Mathematics;
using Anatta.Framework.Graphics;

namespace Anatta.Framework.Interfaces.Graphics;

public interface ISprite : IManageable
{
    public Texture Texture { get; }
    public Vector2 Position { get; set; }
    public Vector2 Scale { get; set; }
    public float Rotation { get; set; }
}