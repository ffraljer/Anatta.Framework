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
    public ScreenManager ScreenManager;

    public GameBase(Vector2i tize, bool UseSDL, string title = "Demo Game") : base(tize, title, UseSDL) {
        manager = new();
        ScreenManager = new();
        Instance = this;
        Audio.Binding = Audio.Bindings.Bass;


    }

    protected override void Initialise() {
        Resource.AddStore(new AssemblyStore(typeof(_Resource).Assembly, "Demo.Resources"));
        ScreenManager.Push(new MainScreen(manager));
        base.Initialise();
    }

    protected override void Update()
    {
        manager?.Update();
        ScreenManager.Update();
    }

    protected override void Draw()
    {
        GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
        manager?.Draw(Size.X, Size.Y);
        ScreenManager.Draw(Size.X, Size.Y);
        base.Draw();
    }
}
