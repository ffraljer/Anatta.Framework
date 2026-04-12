using Anatta.Framework.Graphics.Sprites;
using Anatta.Framework.Graphics.Interfaces;

namespace Anatta.Framework;

public class Screen : IDisposable {
    protected SpriteManager Manager { get; private set; }
    
    public bool IsActive { get; private set; } = true;
    
    private readonly List<IManageable> _owned = new();
    
    protected IEnumerable<IManageable> InternalChildren {
        set {
            foreach (var item in value)
                Add(item);
        }
    }

    
    public Screen(SpriteManager spriteManager) {

        Manager = spriteManager
                  ?? throw new ArgumentNullException(nameof(spriteManager));
    }

    public virtual void Draw() {
        Manager.Draw();
    }

    public virtual void Load() { }

    public virtual void Update() { Manager.Update(); }

    public virtual void OnEnter() { IsActive = true; }
    public virtual void OnExit() { IsActive = false; Manager.InvalidateInput(); }

    public void Add(IManageable item) {
        _owned.Add(item);
        Manager.Add(item);
    }

    public void Add(IEnumerable<IManageable> items) {
        foreach (var item in items) {
            _owned.Add(item);
            Manager.Add(item);
        }
    }

    public virtual void Dispose() {
        foreach (var item in _owned)
            Manager.Remove(item);

        _owned.Clear();
    }
}