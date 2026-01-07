using Demo.Screens;
using OpenTK.Mathematics;
using OpenTK.Graphics.OpenGL4;
using Demo.Resources;
using Anatta.Framework;
using Anatta.Framework.Graphics.Managers;
using Anatta.Framework.IO;
using Anatta.Framework.Graphics;

namespace Demo;

public class GameBase : App
{
    private Batcher batch;
    private Manager manager;
    private ScreenManager manager2;
    
    public GameBase(Vector2i tize, string title = "Demo Game") : base(tize, title)
    {
        manager = new();
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
        manager?.Draw(Size.X, Size.Y);
        manager2.Draw(Size.X, Size.Y);
        base.Draw();
    }
}
