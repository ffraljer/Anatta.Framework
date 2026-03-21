using Anatta.Framework.Graphics;
using Anatta.Framework.Graphics.Managers;
using Anatta.Framework.IO;
using Anatta.Framework.Sound;

namespace Demo.Screens;

public class ClickToEntered : Screen {
    public ClickToEntered(Manager manager) : base(manager) { }
    public override void Load() {
        Add(new Text("YOU HAVE CLICKED AND ENTERED", 24f, Colour.White) {
            Anchor = Anchors.BottomRight,
            Origin = Anchors.BottomRight
        }.ColourTo(Colour.Red, 2, Easing.None, true)
        .Then());
        var track = Resource.Load<Track>("btbbrbbq.mp3");
        track.Play();
    }
    public override void Draw(int width, int height) {
        base.Draw(width, height);
    }
    public override void Update() {
        base.Update();
    }
}