using Anatta.Framework;
using Anatta.Framework.Graphics;
using Anatta.Framework.Graphics.Managers;
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
            Anchor = Anchors.Centre,
            Origin = Anchors.Centre
        };
        sprite.Scale = new Vector2(1f);
        FontFace f = Resource.Load<FontFace>("font_allerbold.ttf");
        player = new Text("I NEED TO LAY OFF THE CATNIP...", 32f, Colour.White, f);
        player.Position = new Vector2(0, 0);
        player.Anchor = Anchors.TopRight;
        player.Origin = Anchors.TopRight;
        sprite.RotateTo(360, 10, true);
        sprite.ScaleTo(new Vector2(4), 10);
        var playe2r = new Text("I NEED TO LAY OFF THE CATNIP...", 32f, Colour.White);
        var playes2r = new Text("I NEED TO LAY OFF THE CATNIP...", 24f, Colour.White) {
            Font = Resource.Load<FontFace>("TIMES.ttf"),
            Position = new(0,2),
            Origin = Anchors.Bottom,
            Anchor = Anchors.Bottom
        };
        Add(sprite); // use Add(item) or AddRange(new IManageable[] { shit, shit2 })
        Add(player);
        Add(playes2r);
        Add(playe2r);
        _music.Play();
    }

    public override void Update(float delta) {
        base.Update(delta);
    }

    public override void Draw(int width, int height) {
        base.Draw(width, height);
    }
}
