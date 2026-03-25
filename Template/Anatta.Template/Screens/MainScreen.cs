using Anatta.Framework;
using Anatta.Framework.Graphics;
using Anatta.Framework.Graphics.Drawables;
using Anatta.Framework.Graphics.Drawables.Shapes;
using Anatta.Framework.Graphics.Managers;
using OpenTK.Mathematics;

namespace Anatta.Template.Screens;

public class MainScreen : Screen {
    public MainScreen(Manager manager) : base(manager) { }
    
    public override void Load() {
        Add(new Box() {
            Anchor = Anchors.Centre,
            Origin = Anchors.Centre,
            Scale = GameBase.Instance.Size,
            Colour = Colour.Violet
        });
        Add(new Sprite("sample_texture") {
            Origin = Anchors.Centre,
            Anchor = Anchors.Centre,
        }.RotateTo(360, 12, Easing.None, true));
        Add(new Text("Main Screen", 40, Colour.White)
        {
            Anchor = Anchors.Top,
            Origin = Anchors.Top,
            Position = new Vector2(0, 20)
        });
        base.Load();
    }
    public override void Update() {
        base.Update();
    }
    public override void Draw() {
        base.Draw();
    }
}