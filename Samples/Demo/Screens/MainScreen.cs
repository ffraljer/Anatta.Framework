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
        Sprite sprite = new("121tc");
        Add(sprite); // use Add(item) or AddRange(new IManageable[] { shit, shit2 })
        byte[] dar = Resource.Load<byte[]>("best song in the entire album.mp3");
        Audio.Play(dar);
    }

    public override void Update(float delta) {
        
    }

    public override void Draw(int width, int height) {
        //base.Draw(width, height);
    }
}
