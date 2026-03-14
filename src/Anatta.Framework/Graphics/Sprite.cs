using Anatta.Framework.Graphics.Animations;
using Anatta.Framework.Graphics.Helpers;
using Anatta.Framework.Interfaces.Graphics;
using OpenTK.Mathematics;

namespace Anatta.Framework.Graphics;

public class Sprite : ISprite, IUpdatable
{
    public Texture Texture { get; }
    public Anchors Origin { get; set; } = Anchors.TopLeft;
    public Anchors Anchor { get; set; } = Anchors.TopLeft;
    public Vector2 Position { get; set; }

    private Vector2 _mousePosition;
    private bool _mousePressed;
    public Vector2 Scale { get; set; } = Vector2.One;
    public float Rotation { get; set; }
    public Vector2 Velocity { get; set; } = Vector2.Zero;
    private List<ITween> tweens = new();

    public event Action<ISprite>? OnClick;
    public Sprite(Texture texture) => Texture = texture;
    public Sprite(string textureName) => Texture = Texture.Load(textureName);

    public void Update()
    {
        Position += Velocity * Time.Delta;

        for (int i = tweens.Count - 1; i >= 0; i--)
        {
            if (tweens[i].Update())
                tweens.RemoveAt(i);
        }
    }
    public void Dispose() => Texture.Dispose();

    /// <param name="position">Position of the Sprite.</param>
    /// <param name="duration">Seconds.</param>
    /// <returns>The current <see cref="ISprite"/> instance, allowing method chaining.</returns>
    public ISprite MoveTo(Vector2 position, float duration, Easing easing = Easing.None, bool loop = false)
    {
        tweens.Add(new Tween<Vector2>
        {
            Loop = loop,
            Getter = () => Position,
            Setter = v => Position = v,
            Start = Position,
            End = position,
            Ease = easing,
            Duration = duration,
            Lerp = AnimationHelper.Lerp
        });

        return this;
    }
    /// <param name="scale">Scale of the Sprite.</param>
    /// <param name="duration">Seconds.</param>
    /// <returns>The current <see cref="ISprite"/> instance, allowing method chaining.</returns>
    public ISprite ScaleTo(Vector2 scale, float duration, Easing easing = Easing.None, bool loop = false)
    {
        tweens.Add(new Tween<Vector2>
        {
            Loop = loop,
            Getter = () => Scale,
            Setter = v => Scale = v,
            End = scale,
            Ease = easing,
            Duration = duration,
            Lerp = AnimationHelper.Lerp
        });

        return this;
    }
    /// <param name="rotation">Degrees.</param>
    /// <param name="duration">Seconds.</param>
    /// <returns>The current <see cref="ISprite"/> instance, allowing method chaining.</returns>
    public ISprite RotateTo(float rotation, float duration, Easing easing = Easing.None, bool loop = false)
    {
        var r = MathHelper.DegreesToRadians(rotation);
        tweens.Add(new Tween<float>
        {
            Loop = loop,
            Getter = () => Rotation,
            Setter = v => Rotation = v,
            Ease = easing,
            Start = Rotation,
            End = r,
            Duration = duration,
            Lerp = AnimationHelper.Lerp
        });

        return this;
    }
    public void ClearTransforms()
    {
        tweens.Clear();
    }
}