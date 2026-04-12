using OpenTK.Mathematics;
using System.Collections;

namespace Anatta.Framework.Graphics.Sprites;

public class Container : Drawable, IEnumerable<Drawable> {
    private readonly List<Drawable> _children = new();

    public IReadOnlyList<Drawable> Children => _children;
    
    public override Texture Texture
    {
        get => null!;
        protected set { }
    }

    public int Count => _children.Count;

    public Drawable this[int index] => _children[index];
    
    private bool _loaded;

    protected virtual void Load() { }                                                                                       

    internal void EnsureLoaded() {
        if (_loaded) return;

        Load();
        _loaded = true;
    }

    public virtual void Add(Drawable drawable) {
        if (drawable == null)
            throw new ArgumentNullException(nameof(drawable));

        if (drawable == this)
            throw new InvalidOperationException("Cannot add container to itself.");

        drawable.Parent = this;
        _children.Add(drawable);
    }

    public virtual bool Remove(Drawable drawable) {
        if (drawable == null)
            return false;

        if (_children.Remove(drawable))
        {
            drawable.Parent = null;
            return true;
        }

        return false;
    }

    public void Clear() {
        foreach (var c in _children)
            c.Parent = null;

        _children.Clear();
    }

    public override void Update() {
        base.Update();

        foreach (var child in _children)
            child.Update();
    }

    public IEnumerator<Drawable> GetEnumerator() => _children.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public override Vector2 GetSize() {
        return Vector2.Zero;
    }
}