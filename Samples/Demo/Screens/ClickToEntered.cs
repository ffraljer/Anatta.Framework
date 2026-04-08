using Anatta.Framework;
using Anatta.Framework.Graphics;
using Anatta.Framework.Graphics.Managers;
using Anatta.Framework.Graphics.Shapes;
using Anatta.Framework.Graphics.Sprites;
using Anatta.Framework.Input;
using Anatta.Framework.IO;
using Anatta.Framework.Sound;
using OpenTK.Mathematics;

namespace Demo.Screens;

public class ClickToEntered : Screen {
    private Track track;
    public ClickToEntered(SpriteManager spriteManager) : base(spriteManager) { }
    public override void Load() {
        Add(new Text("YOU HAVE CLICKED AND ENTERED", 24f, Colour.White) {
            Anchor = Anchors.BottomRight,
            Origin = Anchors.BottomRight
        }.ColourTo(Colour.Red, 2, Easing.None, true)
        .Then());
         track = Resource.Load<Track>("btbbrbbq.mp3");
        track.Play();
        var Boc = new Box() {
            Anchor = Anchors.Centre,
            Origin = Anchors.Centre,
            Size = GameBase.Instance.Size - new Vector2i(100),
            CornerRadius = 12,
            Gradient = new ColourInfo(DemoColours.Purple0, DemoColours.Purple1) 
        };
        Add(Boc);
    }
    public override void Draw() {
        base.Draw();
    }

    public override void OnExit() {
        base.OnExit();
        track.Stop();
    }

    public override void Update() {
        base.Update();                            
    }
}