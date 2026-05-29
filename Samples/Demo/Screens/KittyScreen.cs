using Anatta.Framework;
using Anatta.Framework.Configuration;
using Anatta.Framework.Graphics;
using Anatta.Framework.Graphics.Animations;
using Anatta.Framework.Graphics.Sprites;
using Anatta.Framework.Storage;
using Anatta.Framework.Sound;
using OpenTK.Mathematics;

namespace Demo.Screens;

public class KittyScreen : Screen {
    Text player;
    private Track _music;

    protected override void Load() {
        Scale = new(0.8f);
        base.Load();
        var transformas = new Transformation[] {
            new (Transformation.Type.Colour, Colour4.White, Colour4.Green, 0f, 2f, Easing.OutCubic),
            new (Transformation.Type.Colour, Colour4.Green, Colour4.White, 2f, 4f, Easing.OutCubic),
        };
        _music = Resource.Load<Track>("longcat.mp3");
        _music.Loop = true;
        Sprite sprite = new(Resource.Load<Texture>("finished.png")) {
            Anchor = Anchors.CentreLeft,
            Position = new Vector2(0),
            Origin = Anchors.CentreLeft,
            Colour = new Colour4(255, 255, 255, 0),
            CornerRadius = 4f
        };
        Vector2 size = GameBase.WindowManager.Size;
        float baseRatio = 16f / 9f;
        float ratio = (size.X / size.Y) / baseRatio;
        sprite.Scale = new Vector2(ratio, 1f);
        sprite.HandleInput = true;
        player = new Text("I NEED TO LAY OFF THE CATNIP...", 32f, Colour4.White, Fonts.AllerBold);
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
        var playe2r = new Text("I NEED TO LAY OFF THE CATNIP...", 32f, Colour4.White);
        var playes2r = new Text("I NEED TO LAY OFF THE CATNIP...", 24f, Colour4.White) {
            Font = Resource.Load<FontFace>("TIMES.ttf"),
            Position = new(0,2),
            Origin = Anchors.Bottom,
            Colour = ColourInfo.GradientVertical(Colour4.White, Colour4.Black), // doesn't work?
            Anchor = Anchors.Bottom
        };
        Add(sprite);
        Add(player);
        Add(playes2r);
        Add(playe2r);
        var CLICKTO = new Text("CLICK TO ENTER A SCREEN", 24f, Colour4.White) {
            Anchor = Anchors.BottomRight,
            Origin = Anchors.BottomRight,
            HandleInput = true
        };
        CLICKTO.ApplyTransformationSequence(new TransformationSequence(transformas, true));
        CLICKTO.OnClick += delegate {
            _music.Stop();
            MoveToX(-SpriteManager.ScreenSize.X + 14f, 0.5f);
            GameBase.Instance.Scheduler.AddDelayed(delegate {
                GameBase.ScreenStack.Push(GameBase.ClickToEntered); 
            }, 0.5f);
        };
        Add(CLICKTO);
    }

    public override void Update() {
        base.Update();
        //Console.Write($"\rMouse: {Mouse.X}, {Mouse.Y}      "); // having something like this writing recursively might mess up logging.
    }
}
