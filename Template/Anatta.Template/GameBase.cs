using Anatta.Framework;
using Anatta.Framework.Graphics.Sprites;
using Anatta.Framework.Storage;
using Anatta.Template.Resources;
using Anatta.Template.Screens;
using Anatta.Template.UserInterface;
using OpenTK.Mathematics;

namespace Anatta.Template;

public class GameBase : Application {
    internal SpriteManager _manager;
    internal SpriteManager _cursorManager;
    internal ScreenStack _screenManager;

    public GameBase(Vector2i size, string title = "Untitled") : base(size, title) {
    }
    protected override void Initialise() {
        _manager = new();
        _cursorManager = new();
        _screenManager = new(_manager);
        Resource.AddStore(new AssemblyStore(typeof(ResourceAssembly).Assembly, "Anatta.Template.Resources"));
        base.Initialise();
        _screenManager.Push(new MainScreen());
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