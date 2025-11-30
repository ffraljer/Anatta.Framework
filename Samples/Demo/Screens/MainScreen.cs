using Fraljer.Anatta.Framework.Graphics;
using Fraljer.Anatta.Framework.Graphics.Managers;
using Fraljer.Anatta.Framework.IO;
using Fraljer.Anatta.Framework.Sound; 

namespace Demo.Screens;

public class MainScreen : Screen {
    public MainScreen(Manager manager) : base(manager) {
    }

    public override void Load() {
        base.Load();
        Sprite sprite = new("sample.png");
        Add(sprite); // use Add(item) or AddRange(new IManageable[] { shit, shit2 })
        byte[] dar = Resource.Load<byte[]>("btbbrbbq.mp3");
        int stream = Audio.Play((byte[])dar);
    }

    public override void Update(float delta) {
        
    }

    public override void Draw(int width, int height) {
        //base.Draw(width, height);
    }
}