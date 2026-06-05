using Anatta.Framework.Graphics.Sprites;
using Anatta.Framework.Graphics ;
using Anatta.Framework.Graphics.Interfaces;
using OpenTK.Mathematics;

namespace Anatta.Framework;

public class Screen : CompositeDrawable, IDisposable {
    public bool IsActive { get; private set; } = true;
    public virtual void OnEnter() { IsActive = true; }
    public virtual void OnExit() { IsActive = false; }

    public void Add(Drawable item) {
        AddInternal(item);
    }

    public void AddRange(IEnumerable<Drawable> items) {
        foreach (var item in items) {
            AddInternal(item);
        }
    }

    public virtual void Dispose() {
        ClearInternal();
    }
    
    public override Vector2 GetSize() => SpriteManager.ScreenSize;
}