using OpenTK.Mathematics;
using Fraljer.Anatta.Framework.Graphics;

namespace Fraljer.Anatta.Framework.Interfaces.Graphics;

public interface ISprite : IManageable
{
    public Texture Texture { get; }
    public Vector2 Position { get; set; }
    public Vector2 Scale { get; set; }
    public float Rotation { get; set; }
}