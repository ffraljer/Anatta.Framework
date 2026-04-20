using Anatta.Framework;
using Anatta.Framework.Configuration;
using Anatta.Framework.Graphics;
using Anatta.Framework.Graphics.Animations;
using Anatta.Framework.Graphics;
using Anatta.Framework.Graphics.Sprites;
using Anatta.Framework.IO;
using Anatta.Framework.Sound;
using OpenTK.Mathematics;

namespace Demo.Screens;

public class MainScreen : Screen {
    Text player;
    private Track _music;

    public MainScreen(SpriteManager spriteManager) : base(spriteManager) {
    }

    public override void Load() {
        base.Load();
        var transformas = new Transformation[] {
            new (Transformation.Type.Colour, Color.White, Color.Green, 0f, 2f, Easing.OutCubic),
            new (Transformation.Type.Colour, Color.Green, Color.White, 2f, 4f, Easing.OutCubic),
        };
        _music = Resource.Load<Track>("longcat.mp3");
        _music.Loop = true;
        Sprite sprite = new("finished.png") {
            Anchor = Anchors.CentreLeft,
            Position = new Vector2(0),
            Origin = Anchors.CentreLeft,
            Colour = new Color(255, 255, 255, 0),
            CornerRadius = 4f
        };
        Vector2 size = GameBase.WindowManager.Size;
        float baseRatio = 16f / 9f;
        float ratio = (size.X / size.Y) / baseRatio;
        sprite.Scale = new Vector2(ratio, 1f);
        player = new Text("I NEED TO LAY OFF THE CATNIP...", 32f, Color.White, Fonts.AllerBold);
        player.Position = new Vector2(0);
        player.Anchor = Anchors.TopRight;
        player.Origin = Anchors.TopRight;
        //sprite.ScaleTo(new Vector2(4), 10);
        if (FrameworkConfig.sRenderer == Renderer.GL) {
            
            sprite.OnClick += delegate {
                _music.Play();
            };
            sprite.OnHover += s => {
                Console.WriteLine("Mouse over sprite!");
            };
            sprite.OnHoverLost += s => {
                Console.WriteLine("Mouse left sprite!");
            };
        }
        else {
            _music.Play();
        }
        sprite
            .FadeTo(255, 10)
            .Then()
            .MoveTo(new Vector2(GameBase.WindowManager.Width - sprite.Texture.Width, 0), 10, Easing.InOutCubic)
            .Then()
            .RotateTo(360, 10, Easing.OutCubic);
        var playe2r = new Text("I NEED TO LAY OFF THE CATNIP...", 32f, Color.White);
        var playes2r = new Text("I NEED TO LAY OFF THE CATNIP...", 24f, Color.White) {
            Font = Resource.Load<FontFace>("TIMES.ttf"),
            Position = new(0,2),
            Origin = Anchors.Bottom,
            Anchor = Anchors.Bottom
        };
        Add(sprite);
        Add(player);
        Add(playes2r);
        Add(playe2r);
        var CLICKTO = new Text("CLICK TO ENTER A SCREEN", 24f, Color.White) {
            Anchor = Anchors.BottomRight,
            Origin = Anchors.BottomRight
        };
        CLICKTO.ApplyTransformationSequence(new TransformationSequence(transformas, true));
        CLICKTO.OnClick += delegate {
            _music.Stop();
            GameBase.ScreenStack.Push(GameBase.ClickToEntered, true, 0.5f); 
        };
        Add(CLICKTO);
    }

    public override void Update() {
        base.Update();
        //Console.Write($"\rMouse: {Mouse.X}, {Mouse.Y}      "); // having something like this writing recursively might mess up logging.
    }

    public override void Draw() {
        base.Draw();
    }
}
