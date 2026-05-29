using Demo.Screens;
using OpenTK.Mathematics;
using Demo.Resources;
using Anatta.Framework;
using Anatta.Framework.Graphics;
using Anatta.Framework.Graphics.Shapes;
using Anatta.Framework.Graphics.Sprites;
using Anatta.Framework.Storage;
using Anatta.Framework.Sound;
using Anatta.Framework.Input;
namespace Demo;

public class GameBase : Application
{
    private SpriteManager _spriteManager;
    private SpriteManager _spriteManagerOverlay;
    private SpriteManager _cursorSpriteManager;
    Sprite overlay;
    public static ScreenStack ScreenStack;
    private bool _escapePressedLastFrame = false;

    public static Screen ClickToEntered;

    public GameBase(Vector2i tize, string title = "Demo Game") : base(tize, title, false) {
        _spriteManager = new();
        _spriteManagerOverlay = new();
        _cursorSpriteManager = new();
        ScreenStack = new(_spriteManager);
        HideCursor = true;
        Audio.Binding = Audio.Bindings.Al;
    }

    protected override void Initialise() {
        LoadScreens();
        Resource.AddStore(new AssemblyStore(typeof(_Resource).Assembly, "Demo.Resources"));
        Resource.AddStore(new FileSystemStore("Content"));
        ScreenStack.Push(new KittyScreen());
        overlay = new Sprite(Resource.Load<Texture>("tbo.png")) {
            Anchor = Anchors.Bottom,
            Origin = Anchors.BottomRight
        };
        _spriteManagerOverlay.Add(overlay);
        overlay = new Sprite(Resource.Load<Texture>("tbo.png")) {
            Anchor = Anchors.Bottom,
            Origin = Anchors.BottomLeft
        };
        _spriteManagerOverlay.Add(overlay);
        base.Initialise();
        _cursorSpriteManager.Add(new OsuArgonCursor());
    }

    private void LoadScreens() {
        ClickToEntered = new ClickToEntered();
    }
    protected override void Update()
    {
        _spriteManager.Update();
        _spriteManagerOverlay.Update();
        if (ScreenStack.Current == ClickToEntered) {
            bool escapeNow = Keyboard.IsKeyDown(Keyboard.Key.Escape);

            if (escapeNow && !_escapePressedLastFrame) {
                ScreenStack.Push(new KittyScreen());
            }

            _escapePressedLastFrame = escapeNow;
        }
        ScreenStack.Update();
        _cursorSpriteManager.Update();
    }

    protected override void Draw()
    {
        base.Draw();
        _spriteManager.Draw();
        ScreenStack.Draw();
        _spriteManagerOverlay.Draw();
        _cursorSpriteManager.Draw();
    }

    protected override void OnExit() {
        base.OnExit();
    }
}
