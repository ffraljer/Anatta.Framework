using Anatta.Framework.Graphics.Managers;
using Anatta.Framework.Interfaces.Graphics;

namespace Anatta.Framework.Graphics;

public class Screen : IDisposable {
    protected Manager Manager { get; private set; }
    
    public bool IsActive { get; private set; } = true;
    
    public Screen(Manager manager) {

        Manager = manager
                  ?? throw new ArgumentNullException(nameof(manager));
    }

    public virtual void Draw(int width, int height) {
        Manager.Draw(width, height);
    }

    public virtual void Load() { }

    public virtual void Update() { Manager.Update(); }

    public virtual void OnEnter() { IsActive = true; }
    public virtual void OnExit() { IsActive = false; }

    public void Add(IManageable item) {

        Manager.Add(item);
    }

    public void Add(IManageable[] item) {

        Manager.AddRange(item);
    }
    public virtual void Dispose() {

        Manager.Dispose();
    }
}