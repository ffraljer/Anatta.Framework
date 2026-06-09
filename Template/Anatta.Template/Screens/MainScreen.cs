using Anatta.Framework;
using Anatta.Framework.Graphics;
using Anatta.Framework.Storage;
using Anatta.Framework.Graphics.Sprites;
using Anatta.Framework.Graphics.Shapes;
using OpenTK.Mathematics;
using Anatta.Framework.Graphics.Animations;
using OpenTK.Graphics.OpenGL;

namespace Anatta.Template.Screens;

public class MainScreen : Screen {
    
    protected override void Load() {
        Add(new Box() {
            Anchor = Anchors.Centre,
            Origin = Anchors.Centre,
            Scale = GameBase.WindowManager.Size,
            Colour = ColourInfo.GradientVertical(Colour4.HotPink, Colour4.Honeydew)
        });
        var t = new TransformationSequence([new Transformation(Transformation.Type.Rotate, 0f, MathHelper.DegreesToRadians(360f), 0f, 12f, Easing.None)], true);
        var sprite = new Sprite(Resource.Load<Texture>("sample_texture.png")) {
            Origin = Anchors.Centre,
            Anchor = Anchors.Centre,
            HandleInput = true
        };
        sprite.OnClick += delegate {
            sprite.ColourTo(Colour4.HotPink, 0.2f, Easing.OutBounce);
            GameBase.Instance.Scheduler.AddDelayed(delegate {
                sprite.ColourTo(Colour4.White, 0.2f, Easing.OutBounce);
            }, 0.5f);
        };
        sprite.ApplyTransformationSequence(t);
        Add(sprite);
        Add(new Text("Main Screen", 40, Colour4.White)
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
}