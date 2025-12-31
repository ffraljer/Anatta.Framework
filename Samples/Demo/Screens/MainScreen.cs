using Anatta.Framework.Graphics;
using Anatta.Framework.Graphics.Managers;
using Anatta.Framework.IO;
using Anatta.Framework.Sound;

namespace Demo.Screens;

public class MainScreen : Screen {
    public MainScreen(Manager manager) : base(manager) {
    }

    public override void Load() {
        base.Load();
        Sprite sprite = new("121tc");
        Text pText = new Text("TEST FONT", File.ReadAllBytes("Content/font_allerbold.ttf"), 32f, Colour.Red);
        Add(sprite); // use Add(item) or AddRange(new IManageable[] { shit, shit2 })
        Add(pText);
        byte[] dar = Resource.Load<byte[]>("best song in the entire album.mp3");
        Audio.Play(dar);
    }

    public override void Update(float delta) {
        
    }

    public override void Draw(int width, int height) {
        //base.Draw(width, height);
    }
}
