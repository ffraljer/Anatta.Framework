using Anatta.Framework.Graphics;
using Anatta.Framework.Interfaces.Graphics;
using OpenTK.Mathematics;

namespace Anatta.Framework.Graphics.Drawables;

public abstract class Drawable : ISprite, IUpdatable {
    public abstract Texture Texture { get; protected set; }
    public Vector2 Position { get; set; }
    public Vector2 Scale { get; set; } = Vector2.One;
    public float CornerRadius { get; set; } = 0f;
    public float Rotation { get; set; }
    public Colour Colour { get; set; } = Colour.White;
    
    private Queue<Action>? _thenActions;

    public Anchors Origin { get; set; } = Anchors.TopLeft;
    public Anchors Anchor { get; set; } = Anchors.TopLeft;
    
    internal bool _isHovering = false;

    public event Action<ISprite>? OnClick;
    public event Action<ISprite>? OnHover;
    public event Action<ISprite>? OnHoverLost;

    protected List<ITween> _tweens = new();
    protected float _sequenceTime = 0f;

    public virtual void Update()
    {
        if (_tweens.Count > 0)
        {
            if (_tweens[0].Update()) {

                _tweens.RemoveAt(0);
                if (_thenActions != null && _thenActions.Count > 0)
                    _thenActions.Dequeue()?.Invoke();
            }
        }
    }

    public ISprite Then() => this;

    public void ClearTransforms()
    {
        _tweens.Clear();
        _sequenceTime = 0f;
    }

    public ISprite MoveTo(Vector2 position, float duration, Easing easing = Easing.None, bool loop = false, bool restart = false)
    {
        _tweens.Add(new Tween<Vector2>
        {
            Getter = () => Position,
            Setter = v => Position = v,
            Start = Position,
            End = position,
            Duration = duration,
            Ease = easing,
            Restart = restart,
            Loop = loop,
            Lerp = AnimationHelper.Lerp
        });

        return this;
    }

    public ISprite ScaleTo(Vector2 scale, float duration, Easing easing = Easing.None, bool loop = false, bool restart = false)
    {
        _tweens.Add(new Tween<Vector2>
        {
            Getter = () => Scale,
            Setter = v => Scale = v,
            Start = Scale,
            End = scale,
            Duration = duration,
            Ease = easing,
            Restart = restart,
            Loop = loop,
            Lerp = AnimationHelper.Lerp
        });

        return this;
    }

    public ISprite RotateTo(float rotation, float duration, Easing easing = Easing.None, bool loop = false, bool restart = false)
    {
        var r = MathHelper.DegreesToRadians(rotation);

        _tweens.Add(new Tween<float>
        {
            Getter = () => Rotation,
            Setter = v => Rotation = v,
            Start = Rotation,
            End = r,
            Duration = duration,
            Ease = easing,
            Restart = restart,
            Loop = loop,
            Lerp = AnimationHelper.Lerp
        });

        return this;
    }

    public ISprite ColourTo(Colour colour, float duration, Easing easing = Easing.None, bool loop = false, bool restart = false)
    {
        _tweens.Add(new Tween<Colour>
        {
            Getter = () => Colour,
            Setter = v => Colour = v,
            Start = Colour,
            End = colour,
            Duration = duration,
            Ease = easing,
            Loop = loop,
            Restart = restart,
            Lerp = AnimationHelper.Lerp
        });

        return this;
    }

    public ISprite FadeTo(float alpha, float duration, Easing easing = Easing.None, bool loop = false, bool restart = false)
    {
        _tweens.Add(new Tween<float>
        {
            Getter = () => Colour.A,
            Setter = v => Colour = new Colour(Colour.R, Colour.G, Colour.B, v),
            Start = Colour.A,
            End = alpha,
            Duration = duration,
            Ease = easing,
            Restart = restart,
            Loop = loop,
            Lerp = AnimationHelper.Lerp
        });

        return this;
    }
	internal void TriggerHover() {
		OnHover?.Invoke(this);
	}
	internal void TriggerHoverLost() {
		OnHoverLost?.Invoke(this);
	}
	internal void TriggerClick() {
		OnClick?.Invoke(this);
	}
    public virtual void Dispose()
    {
    }
	
	public abstract Vector2 GetSize();
    
    public ISprite Then(Action action)
    {
        if (_thenActions == null)
            _thenActions = new Queue<Action>();

        _thenActions.Enqueue(action);
        return this;
    }
}