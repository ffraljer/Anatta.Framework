using Anatta.Framework;
using Anatta.Framework.Graphics.Managers;
using Anatta.Framework.IO;
using Anatta.Template.Resources;
using Anatta.Template.Screens;
using Anatta.Template.UserInterface;
using OpenTK.Mathematics;

namespace Anatta.Template;

public class GameBase : Application {
    internal Manager _manager;
    internal Manager _cursorManager;
    internal ScreenManager _screenManager;
    public static GameBase Instance;

    public GameBase(Vector2i size, string title = "Untitled", bool useSdl = true) : base(size, title, useSdl) {
        _manager = new();
        _cursorManager = new();
        _screenManager = new();
        Instance = this;
    }
    protected override void Initialise() {
        Resource.AddStore(new AssemblyStore(typeof(ResourceAssembly).Assembly, "Anatta.Template.Resources"));
        base.Initialise();
        _screenManager.Push(new MainScreen((_manager)));
        _cursorManager.Add(new CursorContainer());
    }
    protected override void Update() {
        _manager.Update();
        _cursorManager.Update();
        _screenManager.Update();
        base.Update();
    }
    protected override void Draw() {
        base.Draw();
        _manager.Draw();
        _screenManager.Draw();
        _cursorManager.Draw(); // I recommend that you make a separate Manager for Cursor.
    }
}