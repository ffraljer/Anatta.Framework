using Fraljer.Anatta.Framework.Graphics.Managers;
using Fraljer.Anatta.Framework.Interfaces.Graphics;
using System.Reflection;

namespace Fraljer.Anatta.Framework.Graphics;

public class Screen : IDisposable {
    
    protected Manager Manager { get; private set; }
    protected Batcher? Batcher => Manager.Batcher;
    
    public bool IsActive { get; private set; } = true;
    
    public Screen(Manager manager) {

        Manager = manager
                  ?? throw new ArgumentNullException(nameof(manager));
    }

    public virtual void Draw(int width, int height) {
        var b = Batcher;
        if (b == null)
            throw new InvalidOperationException("batcher is null.");

        b.Begin(width, height);
        foreach (var s in Manager.GetAll())
            b.Draw(s);

        b.End();
    }

    protected Batcher BatcherManager => (Batcher)
        typeof(Manager)
            .GetField("batcher", BindingFlags.NonPublic | BindingFlags.Instance)
            ?.GetValue(Manager); // kill the green squiggly line

    public virtual void Load() { }

    public virtual void Update(float delta) { Manager.Update(delta); }

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