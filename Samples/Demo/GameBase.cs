using Demo.Screens;
using Fraljer.Anatta.Framework;
using Fraljer.Anatta.Framework.IO;
using Fraljer.Anatta.Framework.Graphics;
using Fraljer.Anatta.Framework.Graphics.Managers;
using OpenTK.Mathematics;
using OpenTK.Graphics.OpenGL4;
using Demo.Resources;

namespace Demo;

public class GameBase : App
{
    private Batcher batch;
    private Manager manager;
    private ScreenManager manager2;
    
    public GameBase(Vector2i tize, string title = "Demo Game") : base(tize, title)
    {
        batch = new();
        manager = new(batch);
        manager2 = new();
    }

    protected override void Initialise()
    {
        Resource.Init("Demo");
        Resource.AddStore(typeof(Ass).Assembly);
        manager2.Push(new MainScreen(manager));
        base.Initialise();
    }

    protected override void Update(float dt)
    {
        base.Update(dt);
        manager?.Update(dt);
        manager2.Update(dt);
    }

    protected override void Draw()
    {
        GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
        manager?.Draw(batch, Size.X, Size.Y);
        manager2.Draw(Size.X, Size.Y);
        base.Draw();
    }
}
