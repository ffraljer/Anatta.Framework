using Anatta.Framework;
using Anatta.Framework.Graphics;
using Anatta.Framework.Graphics.Managers;
using Anatta.Framework.Graphics.Drawables.Shapes;
using Anatta.Framework.Graphics.Drawables;
using Anatta.Framework.Input;
using Anatta.Framework.IO;
using Anatta.Framework.Sound;
using OpenTK.Mathematics;

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
        var Boc = new Box() {
            Anchor = Anchors.Centre,
            Origin = Anchors.Centre,
            Size = GameBase.Instance.Size - new Vector2i(100),
            CornerRadius = 12,
            Colour = Colour.HotPink
        };
        Add(Boc);
    }
    public override void Draw() {
        base.Draw();
    }
    public override void Update() {
        base.Update();                            
    }
}