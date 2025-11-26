using Fraljer.Anatta.Framework;
using Fraljer.Anatta.Framework.IO;
using Fraljer.Anatta.Framework.Graphics;
using OpenTK.Mathematics;
using OpenTK.Graphics.OpenGL4;

namespace Demo;

public class GameBase : Game
{
    private Batcher batch;
    private Manager manager;
    
    public GameBase(Vector2i tize, string title = "Demo Game") : base(tize, title)
    {
        batch = new();
        manager = new(batch);
    }

    protected override void Initialise()
    {
        Resource.Init("Demo");
        Sprite so = new("sample.png")
        {

        };
        manager.Add(so);
        base.Initialise();
    }

    protected override void Update(float dt)
    {
        base.Update(dt);
        manager?.Update(dt);
    }

    protected override void Draw()
    {
        GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
        manager?.Draw(batch,Size.X, Size.Y);
        base.Draw();
    }
}