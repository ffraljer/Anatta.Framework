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
        Sprite sprite = new("121tc");
        sprite.Scale = new Vector2(0.5f);
        FontFace f = new(File.ReadAllBytes("Content/font_allerbold.ttf"));
        player = new Text("TEST FONT", 32f, Colour.Red, f);
        player.Position = new Vector2(2);
        player.Origin = Anchors.Bottom;
        var playe2r = new Text("TEST FONT", 32f, Colour.Red);
        var playes2r = new Text("TEST FONT", 32f, Colour.Red) {
            Font = Resource.Load<FontFace>("TIMES.ttf"),
            Position = new(4)
        };
        Add(sprite); // use Add(item) or AddRange(new IManageable[] { shit, shit2 })
        Add(player);
        Add(playes2r);
        Add(playe2r);
        _music = Resource.Load<Track>("best song in the entire album.mp3");
        _music.Loop = true;
        _music.Play();
    }

    public override void Update(float delta) {
    }

    public override void Draw(int width, int height) {
    }
}
