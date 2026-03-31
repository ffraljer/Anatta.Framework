using System.Reflection;
using Demo.Screens;
using OpenTK.Mathematics;
using OpenTK.Graphics.OpenGL;
using Demo.Resources;
using Anatta.Framework;
using Anatta.Framework.Graphics.Managers;
using Anatta.Framework.Graphics.Shapes;
using Anatta.Framework.IO;
using Anatta.Framework.Sound;
using Anatta.Framework.Graphics;
using Anatta.Framework.Input;

namespace Demo;

public class GameBase : Application
{
    private Manager manager;
    private Manager _cursorManager;
    public static ScreenManager ScreenManager;
    private bool _escapePressedLastFrame = false;

    public static Screen ClickToEntered;

    public GameBase(Vector2i tize, bool UseSDL, string title = "Demo Game") : base(tize, title, UseSDL) {
        manager = new();
        _cursorManager = new();
        ScreenManager = new();
        HideCursor = true;
    }

    protected override void Initialise() {
        loadScreens();
        Resource.AddStore(new AssemblyStore(typeof(_Resource).Assembly, "Demo.Resources"));
        Resource.AddStore(new FileSystemStore("Content"));
        ScreenManager.Push(new MainScreen(manager));
        base.Initialise();
        _cursorManager.Add(new CursorContainer());
    }

    private void loadScreens() {
        ClickToEntered = new ClickToEntered(manager);
    }
    protected override void Update()
    {
        manager?.Update();
        if (ScreenManager.Current == ClickToEntered) {
            bool escapeNow = Keyboard.IsKeyDown(Keyboard.Key.Escape);

            if (escapeNow && !_escapePressedLastFrame) {
                ScreenManager.Push(new MainScreen(manager), true, 0.5f);
            }

            _escapePressedLastFrame = escapeNow;
        }
        ScreenManager.Update();
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
