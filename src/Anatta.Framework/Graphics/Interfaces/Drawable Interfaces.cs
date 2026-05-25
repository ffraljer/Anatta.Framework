using Anatta.Framework.Graphics.Animations;
using Anatta.Framework.Graphics.Rendering;
using OpenTK.Mathematics;

namespace Anatta.Framework.Graphics.Interfaces;

public interface ITransformable {
    ITransformable MoveTo(Vector2 position, float duration, Easing easing = Easing.None);
    ITransformable ScaleTo(Vector2 scale, float duration, Easing easing = Easing.None);
    ITransformable RotateTo(float rotation, float duration, Easing easing = Easing.None);
    ITransformable Then();
    ITransformable Then(Action action);
}

public interface IDrawable : ITransformable {
    RenderCommand BuildRenderCommand(Vector2i screenSize);
    IDrawable? Parent { get; set; }
    public Vector2 Position { get; set; }
    public Vector2 DrawPosition { get; }
    public Vector2 Scale { get; set; }
    public bool HandleInput { get; set; }
    public float Rotation { get; set; }
    public ColourInfo Colour { get; set; }
    public Anchors Origin { get; set; }
    public Anchors Anchor { get; set; }
    event Action<IDrawable>? OnClick;
    public float Depth { get; set; }
}

public interface ITexturedDrawable : IDrawable
{
    public Texture Texture { get; }
    public float CornerRadius { get; set; }
}