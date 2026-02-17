using Anatta.Framework.Graphics;
using Anatta.Framework.Graphics.Managers;
using Anatta.Framework.IO;
using Anatta.Framework.Sound;
using OpenTK.Graphics.ES20;
using OpenTK.Mathematics;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace Demo.Screens;

public class MainScreen : Screen {
    Text player;
    Vector2 mouseUV;

    public MainScreen(Manager manager) : base(manager) {
    }

    public override void Load() {
        base.Load();
        Sprite sprite = new("121tc");
        FontFace f = new(File.ReadAllBytes("Content/font_allerbold.ttf"));
        player = new Text("TEST FONT", f, 32f, Colour.Red);
        var playe2r = new Text("TEST FONT", 32f, Colour.Red);
        Add(sprite); // use Add(item) or AddRange(new IManageable[] { shit, shit2 })
        Add(player);
        Add(playe2r);
        byte[] dar = Resource.Load<byte[]>("best song in the entire album.mp3");
        Audio.Play(dar);
    }

    public override void Update(float delta, KeyboardState ks) {
        Vector2 direction = Vector2.Zero;
        if (ks.IsKeyDown(Keys.W) || ks.IsKeyDown(Keys.Up))
            direction.Y -= 0.1f;
        if (ks.IsKeyDown(Keys.S) || ks.IsKeyDown(Keys.Down))
            direction.Y += 0.1f;
        if (ks.IsKeyDown(Keys.A) || ks.IsKeyDown(Keys.Left))
            direction.X -= 0.1f;
        if (ks.IsKeyDown(Keys.D) || ks.IsKeyDown(Keys.Right))
            direction.X += 0.1f;

        if (direction.LengthSquared > 0)
            direction = direction.Normalized();

        player.Position += direction * delta;

        var mouse = GameBase.Window.MousePosition;
        var size = GameBase.Size;

        mouseUV = new Vector2(
        mouse.X / size.X,
        1f - (mouse.Y / size.Y)
    );
    }

    public override void Draw(int width, int height) {
    }
}
