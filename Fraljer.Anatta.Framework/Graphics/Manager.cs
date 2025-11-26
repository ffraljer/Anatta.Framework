using OpenTK.Graphics.OpenGL4;
using Fraljer.Anatta.Framework.Interfaces.Graphics;

namespace Fraljer.Anatta.Framework.Graphics;

public class Manager
{
    private readonly List<IManageable>/*I mispelled it.*/ managables = new();
    private readonly Batcher? batcher;
    public Batcher? Batcher => batcher;
    
    public IEnumerable<IManageable> GetAll() => managables;
    
    public Manager(Batcher? batcher)
    {
        this.batcher = batcher
                       ?? throw new ArgumentNullException(nameof(batcher));
    }
    
    
    public void Add(IManageable managed)
    {
        if (managed == null)
            throw new ArgumentNullException(nameof(managed));
        managables.Add(managed);
    }

    public void AddRange(params IManageable[] managedItems)
    {
        if (managedItems == null)
            return;

        foreach (var m in managedItems)
        {
            if (m != null)
                managables.Add(m);
        }
    }
    public void Remove(IManageable managed)
    {
        if (managed == null) return;
        managables.Remove(managed);

    }
    public void Update(float delta)
    {
        foreach (var item in managables)
        {
            if (item is IUpdatable u)
                u.Update(delta);
        }
    }
    
    public void Draw(Batcher? batcher, int screenW, int screenH)
    {
        batcher?.Begin(screenW, screenH);

        foreach (var item in managables)
        {
            batcher?.Draw(item);
        }

        batcher?.End();
    }
    public void Dispose()
    {
        foreach (var item in managables)
        {
            if (item is IDisposable d)
                d.Dispose();
        }

        managables.Clear();

        batcher?.Dispose();
    }
}