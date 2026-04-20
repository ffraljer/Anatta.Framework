using OpenTK.Mathematics;
using System.Collections;

namespace Anatta.Framework.Graphics.Sprites;

public class Container : CompositeDrawable, IEnumerable<Drawable> {
    public override Texture Texture { get => null!; protected set { } }

    public int Count => Children.Count;
    public Drawable this[int index] => Children[index];

    public virtual void Add(Drawable drawable) => AddInternal(drawable);
    public virtual bool Remove(Drawable drawable) => RemoveInternal(drawable);
    public void Clear() => ClearInternal();

    public IEnumerator<Drawable> GetEnumerator() => Children.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public override Vector2 GetSize() => Vector2.Zero;
}