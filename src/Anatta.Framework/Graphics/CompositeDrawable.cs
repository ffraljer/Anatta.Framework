namespace Anatta.Framework.Graphics;

public abstract class CompositeDrawable : Drawable {
    private readonly List<Drawable> _children = new();
    public IReadOnlyList<Drawable> Children => _children;

    private bool _loaded;

    protected virtual void Load() { }

    internal void EnsureLoaded() {
        if (_loaded) return;
        Load();
        _loaded = true;
    }

    protected virtual IEnumerable<Drawable>? InternalChildren {
        set {
            if (value == null) return;
            foreach (var d in value)
                AddInternal(d);
        }
    }

    protected void AddInternal(Drawable drawable) {
        if (drawable == null) throw new ArgumentNullException(nameof(drawable));
        if (drawable == this) throw new InvalidOperationException("Cannot add to itself.");
        drawable.Parent = this;
        _children.Add(drawable);
    }

    protected bool RemoveInternal(Drawable drawable) {
        if (drawable == null) return false;
        if (_children.Remove(drawable)) {
            drawable.Parent = null;
            return true;
        }

        return false;
    }

    protected void ClearInternal() {
        foreach (var c in _children) c.Parent = null;
        _children.Clear();
    }

    public override void Update() {
        base.Update();
        foreach (var child in _children)
            child.Update();
    }

    public override void TriggerClick() {
        base.TriggerClick();
        OnMouseClick();
    }

    public override void TriggerDoubleClick() {
        base.TriggerDoubleClick();
        OnMouseDoubleClick();
    }

    public override void TriggerHover() {
        base.TriggerHover();
        OnMouseHoverOn();
    }

    public override void TriggerHoverLost() {
        base.TriggerHoverLost();
        OnMouseHoverOff();
    }

    public virtual void OnMouseClick() { }
    public virtual void OnMouseDoubleClick() { }
    public virtual void OnMouseHoverOn() { }
    public virtual void OnMouseHoverOff() { }
}