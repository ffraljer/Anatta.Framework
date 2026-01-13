using Anatta.Framework.Graphics.Managers;
using Anatta.Framework.Interfaces.Graphics;
using OpenTK.Windowing.GraphicsLibraryFramework;
using System.Reflection;

namespace Anatta.Framework.Graphics;

public class Screen : IDisposable {
    
    protected Manager Manager { get; private set; }
    
    public bool IsActive { get; private set; } = true;
    
    public Screen(Manager manager) {

        Manager = manager
                  ?? throw new ArgumentNullException(nameof(manager));
    }

    public virtual void Draw(int width, int height) {
        var b = Manager;
        if (b == null)
            throw new InvalidOperationException("batcher is null.");

        foreach (var s in Manager.GetAll())
            b.Draw(width, height);
    }

    protected Batcher BatcherManager => (Batcher)
        typeof(Manager)
            .GetField("batcher", BindingFlags.NonPublic | BindingFlags.Instance)
            ?.GetValue(Manager)!;

    public virtual void Load() { }

    public virtual void Update(float delta, KeyboardState keyboard) { Manager.Update(delta); }

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