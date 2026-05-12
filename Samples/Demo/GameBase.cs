using System.Reflection;
using Demo.Screens;
using OpenTK.Mathematics;
using OpenTK.Graphics.OpenGL;
using Demo.Resources;
using Anatta.Framework;
using Anatta.Framework.Graphics;
using Anatta.Framework.Graphics.Sprites;
using Anatta.Framework.Storage;
using Anatta.Framework.Sound;
using Anatta.Framework.Input;
namespace Demo;

public class GameBase : Application
{
    private SpriteManager _spriteManager;
    private SpriteManager _cursorSpriteManager;
    public static ScreenStack ScreenStack;
    private bool _escapePressedLastFrame = false;

    public static Screen ClickToEntered;

    public GameBase(Vector2i tize, string title = "Demo Game") : base(tize, title, false) {
        _spriteManager = new();
        _cursorSpriteManager = new();
        ScreenStack = new();
        HideCursor = true;
    }

    protected override void Initialise() {
        loadScreens();
        Resource.AddStore(new AssemblyStore(typeof(_Resource).Assembly, "Demo.Resources"));
        Resource.AddStore(new FileSystemStore("Content"));
        ScreenStack.Push(new KittyScreen(_spriteManager));
        base.Initialise();
        _cursorSpriteManager.Add(new OsuArgonCursor());
    }

    private void loadScreens() {
        ClickToEntered = new ClickToEntered(_spriteManager);
    }
    protected override void Update()
    {
        _spriteManager?.Update();
        if (ScreenStack.Current == ClickToEntered) {
            bool escapeNow = Keyboard.IsKeyDown(Keyboard.Key.Escape);

            if (escapeNow && !_escapePressedLastFrame) {
                ScreenStack.Push(new KittyScreen(_spriteManager), true, 0.5f);
            }

            _escapePressedLastFrame = escapeNow;
        }
        ScreenStack.Update();
        _cursorSpriteManager.Update();
    }

    protected override void Draw()
    {
        base.Draw();
        _spriteManager?.Draw();
        ScreenStack.Draw();
        _cursorSpriteManager.Draw();
    }

    protected override void OnExit() {
        base.OnExit();
    }
}
