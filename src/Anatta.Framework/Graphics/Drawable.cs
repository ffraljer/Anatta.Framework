using Anatta.Framework.Graphics.Animations;
using Anatta.Framework.Graphics.Helpers;
using Anatta.Framework.Graphics.Interfaces;
using Anatta.Framework.Graphics.Rendering;
using Anatta.Framework.Threading;
using OpenTK.Mathematics;

namespace Anatta.Framework.Graphics;

public abstract class Drawable : IDrawable, IUpdatable {
    public virtual RenderCommand BuildRenderCommand(Vector2i screenSize) => default;
    public Vector2 Position { get; set; }
    public Vector2 Scale { get; set; } = Vector2.One;
    public float CornerRadius { get; set; }
    public IDrawable? Parent { get; set; }
    public float Depth { get; set; } = 1f;
    public bool HandleInput { get; set; }

    public Vector2 DrawPosition {
        get {
            if (Parent == null)
                return Position;

            return Parent.DrawPosition + Position;
        }
    }

    public float Rotation { get; set; }
    public ColourInfo Colour { get; set; } = Colour4.White;

    private Queue<(float triggerTime, Action action)>? _thenActions;

    public Anchors Origin { get; set; } = Anchors.TopLeft;
    public Anchors Anchor { get; set; } = Anchors.TopLeft;

    internal bool IsHovering = false;

    public event Action<IDrawable>? OnClick;
    public event Action<IDrawable>? OnDoubleClick;
    public event Action<IDrawable>? OnHover;
    public Action? OnUpdate;
    private float _clock;
    private float _chainClock;
    private List<Transformation> _transformations = new();
    public event Action<IDrawable>? OnHoverLost;

    private TransformationSequence? _currentSequence;

    public virtual void Update() {
        if (OnUpdate != null)
            OnUpdate?.Invoke();

        _clock += Time.Delta;

        doTransforms();
    }

    private void doTransforms() {
        bool fadeDone = false, moveDone = false, scaleDone = false, rotateDone = false, colourDone = false;

        for (int i = _transformations.Count - 1; i >= 0; i--) {
            var t = _transformations[i];
            if (t.StartTime > _clock || t.EndTime < _clock)
                continue;

            float frac = t.EndTime == t.StartTime
                ? 1f
                : EasingHelper.Ease(t.Easing, _clock - t.StartTime, 0f, 1f, t.EndTime - t.StartTime);

            switch (t.TransformType) {
                case Transformation.Type.Fade when !fadeDone:
                    Colour = new Colour4(Colour.Top.R, Colour.Top.G, Colour.Top.B,
                        AnimationHelper.Lerp(t.FloatStart, t.FloatEnd, frac));
                    fadeDone = true;
                    break;
                case Transformation.Type.Move when !moveDone:
                    Position = AnimationHelper.Lerp(t.VecStart, t.VecEnd, frac);
                    moveDone = true;
                    break;
                case Transformation.Type.Scale when !scaleDone:
                    Scale = AnimationHelper.Lerp(t.VecStart, t.VecEnd, frac);
                    scaleDone = true;
                    break;
                case Transformation.Type.Rotate when !rotateDone:
                    Rotation = AnimationHelper.Lerp(t.FloatStart, t.FloatEnd, frac);
                    rotateDone = true;
                    break;
                case Transformation.Type.Colour when !colourDone:
                    Colour = AnimationHelper.Lerp(t.ColStart, t.ColEnd, frac);
                    colourDone = true;
                    break;
            }
        }

        for (int i = _transformations.Count - 1; i >= 0; i--) {
            var t = _transformations[i];
            if (t.EndTime > _clock) continue;

            switch (t.TransformType) {
                case Transformation.Type.Fade when !fadeDone:
                    Colour = new Colour4(Colour.Top.R, Colour.Top.G, Colour.Top.B, t.FloatEnd);
                    fadeDone = true;
                    break;
                case Transformation.Type.Move when !moveDone:
                    Position = t.VecEnd;
                    moveDone = true;
                    break;
                case Transformation.Type.Scale when !scaleDone:
                    Scale = t.VecEnd;
                    scaleDone = true;
                    break;
                case Transformation.Type.Rotate when !rotateDone:
                    Rotation = t.FloatEnd;
                    rotateDone = true;
                    break;
                case Transformation.Type.Colour when !colourDone:
                    Colour = t.ColEnd;
                    colourDone = true;
                    break;
            }

            _transformations.RemoveAt(i);
        }

        if (_thenActions != null) {
            while (_thenActions.Count > 0 && _clock >= _thenActions.Peek().triggerTime)
                _thenActions.Dequeue().action();
        }

        if (_transformations.Count == 0 && _currentSequence != null && _currentSequence.Loop)
            ApplyTransformationSequence(_currentSequence);
    }

    public ITransformable Then() => this;
    
    private float startChain() {
        return _chainClock > _clock ? _chainClock : _clock;
    }

    public void ClearTransforms() {
        _transformations.Clear();
        _chainClock = 0f;
        _thenActions?.Clear();
        _currentSequence = null;
    }

    public ITransformable MoveTo(Vector2 position, float duration, Easing easing = Easing.None) {
        var start = startChain();
        _transformations.Add(new Transformation(
            Transformation.Type.Move, Position, position,
            start, start + duration, easing));
        _chainClock = start + duration;
        return this;
    }

    public ITransformable MoveToX(float x, float duration, Easing easing = Easing.None) => MoveTo(new(x, 0), duration, easing);
    public ITransformable MoveToY(float y, float duration, Easing easing = Easing.None) => MoveTo(new(0, y), duration, easing);

    public ITransformable ScaleTo(Vector2 scale, float duration, Easing easing = Easing.None) {
        var start = startChain();
        _transformations.Add(new Transformation(
            Transformation.Type.Scale, Scale, scale,
            start, start + duration, easing));
        _chainClock = start + duration;
        return this;
    }

    public ITransformable RotateTo(float rotation, float duration, Easing easing = Easing.None) {
        var start = startChain();
        _transformations.Add(new Transformation(
            Transformation.Type.Rotate,
            Rotation, MathHelper.DegreesToRadians(rotation),
            start, start + duration, easing));
        _chainClock = start + duration;
        return this;
    }

    public ITransformable FadeTo(float alpha, float duration, Easing easing = Easing.None) {
        var start = startChain();
        _transformations.Add(new Transformation(
            Transformation.Type.Fade, Colour.Top.A, alpha,
            start, start + duration, easing));
        _chainClock = start + duration;
        return this;
    }

    public ITransformable ColourTo(Colour4 colour4, float duration, Easing easing = Easing.None) {
        var start = startChain();
        _transformations.Add(new Transformation(
            Transformation.Type.Colour, Colour.Top, colour4,
            start, start + duration, easing));
        _chainClock = start + duration;
        return this;
    }

    public IDrawable ApplyTransformationSequence(TransformationSequence sequence) {
        _currentSequence = sequence;
        foreach (var t in sequence.Transformations) {
            _transformations.Add(new Transformation(t.TransformType, t.VecStart, t.VecEnd,
                _clock + t.StartTime, _clock + t.EndTime, t.Easing) {
                FloatStart = t.FloatStart,
                FloatEnd = t.FloatEnd,
                ColStart = t.ColStart,
                ColEnd = t.ColEnd
            });
        }
        return this;
    }

    public virtual void TriggerHover() {
        OnHover?.Invoke(this);
    }

    public virtual void TriggerHoverLost() {
        OnHoverLost?.Invoke(this);
    }

    public virtual void TriggerClick() {
        OnClick?.Invoke(this);
    }
    public virtual void TriggerDoubleClick() {
        OnDoubleClick?.Invoke(this);
    }

    public virtual void Dispose() {
        OnUpdate = null;
    }

    public abstract Vector2 GetSize();

    protected Matrix4 BuildTransform(Vector2 size, Vector2i screenSize) {
        var originOffset = AnchorHelper.ToNormalised(Origin) * size;
        var anchorOffset = AnchorHelper.ToNormalised(Anchor) * new Vector2(screenSize.X, screenSize.Y);

        return
            Matrix4.CreateScale(size.X, size.Y, 1f) *
            Matrix4.CreateTranslation(-originOffset.X, -originOffset.Y, 0f) *
            Matrix4.CreateRotationZ(Rotation) *
            Matrix4.CreateTranslation(DrawPosition.X, DrawPosition.Y, 0f) *
            Matrix4.CreateTranslation(anchorOffset.X, anchorOffset.Y, 0f);
    }

    public ITransformable Then(Action action) {
        if (_thenActions == null)
            _thenActions = new Queue<(float, Action)>();

        _thenActions.Enqueue((_chainClock, action));
        return this;
    }
}