using OpenTK.Mathematics;
using Anatta.Framework.Graphics;

namespace Anatta.Framework.Graphics.Interfaces;

public interface ISprite : IManageable
{
    public Texture Texture { get; }
    public Vector2 Position { get; set; }
    public Vector2 DrawPosition { get; }
    public Vector2 Scale { get; set; }
    public float Rotation { get; set; }
    public float CornerRadius { get; set; }
    public ColorInfo Colour { get; set; }
    public Anchors Origin { get; set; }
    public Anchors Anchor { get; set; }
    
    ISprite MoveTo(Vector2 position, float duration, Easing easing = Easing.None, bool loop = false, bool restart = false);
    ISprite ScaleTo(Vector2 scale, float duration, Easing easing = Easing.None, bool loop = false, bool restart = false);
    ISprite RotateTo(float rotation, float duration, Easing easing = Easing.None, bool loop = false, bool restart = false);
    ISprite Then();
    ISprite Then(Action action);
    event Action<ISprite>? OnClick;
}