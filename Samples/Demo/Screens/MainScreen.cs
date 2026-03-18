using Anatta.Framework;
using Anatta.Framework.Graphics;
using Anatta.Framework.Graphics.Animations;
using Anatta.Framework.Graphics.Managers;
using Anatta.Framework.Input;
using Anatta.Framework.IO;
using Anatta.Framework.Sound;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace Demo.Screens;

public class MainScreen : Screen {
    Text player;
    private Track _music;

    public MainScreen(Manager manager) : base(manager) {
    }

    public override void Load() {
        base.Load();
        _music = Resource.Load<Track>("longcat.mp3");
        _music.Loop = true;
        Sprite sprite = new("finished") {
            Anchor = Anchors.CentreLeft,
            Position = new Vector2(0),
            Origin = Anchors.CentreLeft,
            Colour = new Colour(255, 255, 255, 0),
        };
        Vector2 size = GameBase.Instance.Size;
        float baseRatio = 16f / 9f;
        float ratio = (size.X / size.Y) / baseRatio;
        sprite.Scale = new Vector2(ratio, 1f);
        FontFace f = Resource.Load<FontFace>("font_allerbold.ttf");
        player = new Text("I NEED TO LAY OFF THE CATNIP...", 32f, Colour.White, f);
        player.Position = new Vector2(0);
        player.Anchor = Anchors.TopRight;
        player.Origin = Anchors.TopRight;
        //sprite.ScaleTo(new Vector2(4), 10);
        sprite
            .FadeTo(255, 10)
            .Then()
            .MoveTo(new Vector2(GameBase.Instance.Size.X - sprite.Texture.Width, 0), 10, Easing.InOutCubic)
            .Then()
            .RotateTo(360, 10, Easing.OutCubic);
            
        var playe2r = new Text("I NEED TO LAY OFF THE CATNIP...", 32f, Colour.White);
        var playes2r = new Text("I NEED TO LAY OFF THE CATNIP...", 24f, Colour.White) {
            Font = Resource.Load<FontFace>("TIMES.ttf"),
            Position = new(0,2),
            Origin = Anchors.Bottom,
            Anchor = Anchors.Bottom
        };
        Add(sprite);
        Add(player);
        Add(playes2r);
        Add(playe2r);
    }

    public override void Update() {
        base.Update();
        Console.Write($"\rMouse: {Mouse.X}, {Mouse.Y}      ");
        if (Keyboard.IsKeyPressed(Keyboard.Key.Space)) {
            _music.Play();
        }
        if (Mouse.IsButtonPressed(Mouse.Button.Left))
        {
            Console.WriteLine("Left click!");
        }
    }

    public override void Draw(int width, int height) {
        base.Draw(width, height);
    }
}
