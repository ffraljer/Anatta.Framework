using Anatta.Framework;
using Anatta.Framework.Graphics;
using Anatta.Framework.Graphics.Animations;
using Anatta.Framework.Graphics.Interfaces;
using Anatta.Framework.Graphics.Shapes;
using Anatta.Framework.Graphics.Sprites;
using Anatta.Framework.Storage;
using Anatta.Framework.Sound;
using OpenTK.Mathematics;

namespace Demo.Screens;

public class ClickToEntered : Screen {
    private Track track;
    private ITexturedDrawable title;
    private Text back;
    protected override void Load() {
        
        track = Resource.Load<Track>("hlfswebbq.mp3");
        track.Play();
        var Boc = new Box() {
            Anchor = Anchors.Centre,
            Origin = Anchors.Centre,
            BorderThickness = 12f,
            Size = GameBase.WindowManager.Size - new Vector2i(12),
            CornerRadius = 12,
            Colour = new ColourInfo(DemoColours.Purple0, DemoColours.Purple1).Darken(0.6f) 
        };
        Add(Boc);
        title = new Text("YOU HAVE CLICKED AND ENTERED", 24f, Colour4.White) {
            Anchor = Anchors.BottomRight,
            Origin = Anchors.BottomRight
        };
        back = new pSymbolAwesome(SymbolAwesome.chevron_circle_left, 24f,
            new Vector2(0, 0)) {
            Origin = Anchors.TopLeft
        };
        Add((Drawable)title);
        Add(back);
    }

    public override void OnExit() {
        base.OnExit();
        track.Stop();
    }

    public override void Update()
    {
        base.Update();
    }
}