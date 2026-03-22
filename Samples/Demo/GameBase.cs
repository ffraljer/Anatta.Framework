using System.Reflection;
using Demo.Screens;
using OpenTK.Mathematics;
using OpenTK.Graphics.OpenGL;
using Demo.Resources;
using Anatta.Framework;
using Anatta.Framework.Graphics.Managers;
using Anatta.Framework.Graphics.Drawables.Shapes;
using Anatta.Framework.Graphics.Drawables;
using Anatta.Framework.IO;
using Anatta.Framework.Sound;
using Anatta.Framework.Graphics;
using Anatta.Framework.Input;

namespace Demo;

public class GameBase : Application
{
    private Manager manager;
    private Manager _cursorManager;
    public static GameBase Instance;
    internal Drawable Cursor;
    public ScreenManager ScreenManager;

    public GameBase(Vector2i tize, bool UseSDL, string title = "Demo Game") : base(tize, title, UseSDL) {
        manager = new();
        _cursorManager = new();
        ScreenManager = new();
        Instance = this;
        Audio.Binding = Audio.Bindings.Bass;


    }

    protected override void Initialise() {
        Resource.AddStore(new AssemblyStore(typeof(_Resource).Assembly, "Demo.Resources"));
        ScreenManager.Push(new MainScreen(manager));
        base.Initialise();
        Cursor = new Circle() {
            Colour = Colour.DeepPink,
            Anchor = Anchors.TopLeft ,
            Origin = Anchors.Centre,
            Thickness = 5,
        };
        _cursorManager.Add(Cursor);
    }

    protected override void Update()
    {
        manager?.Update();
        ScreenManager.Update();
        Cursor.Position = new Vector2(Mouse.X, Mouse.Y);
        _cursorManager.Update();
    }

    protected override void Draw()
    {
        GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
        manager?.Draw();
        ScreenManager.Draw();
        _cursorManager.Draw();
        base.Draw();
    }
}
