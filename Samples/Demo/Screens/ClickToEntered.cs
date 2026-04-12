using Anatta.Framework;
using Anatta.Framework.Graphics;
using Anatta.Framework.Graphics.Animations;
using Anatta.Framework.Graphics.Interfaces;
using Anatta.Framework.Graphics.Shapes;
using Anatta.Framework.Graphics.Sprites;
using Anatta.Framework.Input;
using Anatta.Framework.IO;
using Anatta.Framework.Sound;
using OpenTK.Mathematics;

namespace Demo.Screens;

public class ClickToEntered : Screen {
    private Track track;
    private ISprite title;
    private Text back;
    public ClickToEntered(SpriteManager spriteManager) : base(spriteManager) { }
    public override void Load() {
        
        track = Resource.Load<Track>("hlfswebbq.mp3");
        track.Play();
        var Boc = new Box() {
            Anchor = Anchors.Centre,
            Origin = Anchors.Centre,
            Size = GameBase.WindowManager.Size - new Vector2i(12),
            CornerRadius = 12,
            Colour = new ColorInfo(DemoColours.Purple0, DemoColours.Purple1).Darken(0.6f) 
        };
        Add(Boc);
        title = new Text("YOU HAVE CLICKED AND ENTERED", 24f, Color.White) {
            Anchor = Anchors.BottomRight,
            Origin = Anchors.BottomRight
        };
        back = new pSymbolAwesome(SymbolAwesome.chevron_circle_left, 24f,
            new Vector2(0, 0)) {
            Origin = Anchors.Centre
        };
        Add(title);
        Add(back);
    }
    public override void Draw() {
        base.Draw();
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