using System.Reflection;
using Demo.Screens;
using OpenTK.Mathematics;
using OpenTK.Graphics.OpenGL;
using Demo.Resources;
using Anatta.Framework;
using Anatta.Framework.Graphics.Managers;
using Anatta.Framework.IO;
using Anatta.Framework.Sound;
using Anatta.Framework.Graphics;

namespace Demo;

public class GameBase : Application
{
    private Manager manager;
    public static GameBase Instance;
    private ScreenManager manager2;

    public GameBase(Vector2i tize, bool UseSDL, string title = "Demo Game") : base(tize, title, UseSDL)
    {
        manager = new();
        manager2 = new();
        Instance = this;
        Audio.Binding = Audio.Bindings.Bass;


    }

    protected override void Initialise()
    {
        //Resource.Init("Demo");
        Resource.AddStore(new AssemblyStore(typeof(_Resource).Assembly, "Demo.Resources"));
        manager2.Push(new MainScreen(manager));
        base.Initialise();
    }

    protected override void Update()
    {
        manager?.Update();
        manager2.Update();
    }

    protected override void Draw()
    {
        GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
        manager?.Draw(Size.X, Size.Y);
        manager2.Draw(Size.X, Size.Y);
        base.Draw();
    }
}
