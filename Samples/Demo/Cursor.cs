using Anatta.Framework.Graphics;
using Anatta.Framework.Graphics.Shapes;
using Anatta.Framework.Graphics.Sprites;
using Anatta.Framework.Input;
using OpenTK.Mathematics;

namespace Demo;

public class CursorContainer : Container
{
    private Drawable outerBlack;
    private Circle outerPink;
    private Circle cursormiddl;
    protected override void Load()
    {
        outerBlack = new Circle()
        {
            BorderColour = new Colour(0, 0, 0),
            Radius = 19*2,
            Thickness = 12,
            BorderGradient = ColourInfo.GradientVertical(new Colour("FC618F"), new Colour("BB1A41")),
            Origin = Anchors.Centre
        };
        var C = new Colour("FC618F").Darken(0.6f);
        C.A = 128;
        outerPink = new Circle
        {
            Radius = 18 * 2,
            BorderGradient = ColourInfo.GradientVertical(new Colour("FC618F"), new Colour("BB1A41")),
            Thickness = 12,
            FillColour = C,
            Origin = Anchors.Centre
        };
        cursormiddl = new Circle
        {
            Radius = 9,
            FillColour = Colour.White,
            Origin = Anchors.Centre
        };

        Add(outerBlack);
        Add(outerPink);
        Add(cursormiddl);
    }

    public override void Update()
    {
        base.Update();
        Position = new Vector2(Mouse.X, Mouse.Y);
        if (Mouse.IsButtonDown(Mouse.Button.Left)) {
            if (outerBlack.Scale != new Vector2(1.2f)) {
                outerBlack.ClearTransforms();
                outerBlack.ScaleTo(new Vector2(1.2f), 0.1f);
                outerPink.ClearTransforms();
                outerPink.ScaleTo(new Vector2(1.2f), 0.1f);
            }
        }
        else
        {
            if (outerBlack.Scale != Vector2.One) {
                outerBlack.ClearTransforms();
                outerBlack.ScaleTo(Vector2.One, 0.1f);
                outerPink.ClearTransforms();
                outerPink.ScaleTo(Vector2.One, 0.1f);
            }
        }
    }

    public override Texture Texture
    {
        get => null!;
        protected set { }
    }

    public override Vector2 GetSize() => Vector2.Zero;
}