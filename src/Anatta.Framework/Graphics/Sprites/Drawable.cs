using Anatta.Framework.Graphics.Animations;
using Anatta.Framework.Graphics.Interfaces;
using OpenTK.Mathematics;

namespace Anatta.Framework.Graphics.Sprites;

public abstract class Drawable : ISprite, IUpdatable { 
    // todo: make tweens run in parallel, and somehow still allow the *To() methods to still work.
    public abstract Texture Texture { get; protected set; }
    public Vector2 Position { get; set; }
    public Vector2 Scale { get; set; } = Vector2.One;
    public float CornerRadius { get; set; }

    public Drawable? Parent { get; internal set; }
    
    public Vector2 DrawPosition
    {
        get
        {
            if (Parent == null)
                return Position;

            return Parent.DrawPosition + Position;
        }
    }

    public float Rotation { get; set; }
    public Colour Colour { get; set; } = Colour.White;
    
    private Queue<Action>? _thenActions;

    public Anchors Origin { get; set; } = Anchors.TopLeft;
    public Anchors Anchor { get; set; } = Anchors.TopLeft;
    
    internal bool IsHovering = false;

    public event Action<ISprite>? OnClick;
    public event Action<ISprite>? OnHover;
    public event Action<ISprite>? OnHoverLost;

    private List<ITween> _tweens = new();

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
    }

    public ISprite MoveTo(Vector2 position, float duration, Easing easing = Easing.None, bool loop = false, bool restart = false)
    {
        _tweens.Add(new Tween<Vector2>
        {
            Getter = () => Position,
            Setter = v => Position = v,
            Start = Position,
            End = position,
            Ease = easing,
            StartTime = 0f,
            EndTime = duration,
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
            Ease = easing,
            Restart = restart,
            StartTime = 0f,
            EndTime = duration,
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
            Ease = easing,
            StartTime = 0f,
            EndTime = duration,
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
            Ease = easing,
            Loop = loop,
            StartTime = 0f,
            EndTime = duration,
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
            Ease = easing,
            StartTime = 0f,
            EndTime = duration,
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
    
    public ISprite ApplyTransformationSequence(TransformationSequence sequence) {
        void EnqueueSequence() {
            for (int i = 0; i < sequence.Transformations.Count; i++) {
                var t = sequence.Transformations[i];
                var twin = BuildTween(t);

                dynamic twindyn = twin;
                twindyn.Loop = false;
                twindyn.Restart = false;

                if (i == sequence.Transformations.Count - 1 && sequence.Loop) {
                    Then(() => {
                        TransformSnap(sequence);
                        EnqueueSequence();
                    });
                }

                _tweens.Add(twin);
            }
        }

        EnqueueSequence();
        return this;
    }
	
    private ITween BuildTween(Transformation t) {
        switch (t.TransformType) {
            case Transformation.Type.Move:
                return new Tween<Vector2> {
                    Getter = () => Position,
                    Setter = v => Position = v,
                    Start = t.VecStart,
                    End = t.VecEnd,
                    Ease = t.Easing,
                    StartTime = t.StartTime,
                    EndTime = t.EndTime,
                    Lerp = AnimationHelper.Lerp
                };

            case Transformation.Type.Scale:
                return new Tween<Vector2> {
                    Getter = () => Scale,
                    Setter = v => Scale = v,
                    Start = t.VecStart,
                    End = t.VecEnd,
                    Ease = t.Easing,
                    StartTime = t.StartTime,
                    EndTime = t.EndTime,
                    Lerp = AnimationHelper.Lerp
                };

            case Transformation.Type.Rotate:
                return new Tween<float> {
                    Getter = () => Rotation,
                    Setter = v => Rotation = v,
                    Start = t.FloatStart,
                    End = t.FloatEnd,
                    StartTime = t.StartTime,
                    EndTime = t.EndTime,
                    Ease = t.Easing,
                    Lerp = AnimationHelper.Lerp
                };

            case Transformation.Type.Colour:
                return new Tween<Colour> {
                    Getter = () => Colour,
                    Setter = v => Colour = v,
                    Start = t.ColStart,
                    End = t.ColEnd,
                    StartTime = t.StartTime,
                    EndTime = t.EndTime,
                    Ease = t.Easing,
                    Lerp = AnimationHelper.Lerp
                };

            case Transformation.Type.Fade:
                return new Tween<float> {
                    Getter = () => Colour.A,
                    Setter = v => Colour = new Colour(Colour.R, Colour.G, Colour.B, v),
                    Start = t.FloatStart,
                    End = t.FloatEnd,
                    StartTime = t.StartTime,
                    EndTime = t.EndTime,
                    Ease = t.Easing,
                    Lerp = AnimationHelper.Lerp
                };
        }

        throw new Exception("unknown transformation type");
    }
    
    private void TransformSnap(TransformationSequence sequence) {
        var first = sequence.Transformations[0];

        switch (first.TransformType) {
            case Transformation.Type.Move:
                Position = first.VecStart;
                break;

            case Transformation.Type.Scale:
                Scale = first.VecStart;
                break;

            case Transformation.Type.Rotate:
                Rotation = first.FloatStart;
                break;

            case Transformation.Type.Colour:
                Colour = first.ColStart;
                break;

            case Transformation.Type.Fade:
                Colour = new Colour(Colour.R, Colour.G, Colour.B, first.FloatStart);
                break;
        }
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