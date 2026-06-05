using Anatta.Framework.Graphics;
using Anatta.Framework.Graphics.Shapes;
using Anatta.Framework.Graphics.Sprites;
using Anatta.Framework.Input;
using OpenTK.Mathematics;

namespace Demo;

public class OsuArgonCursor : Container
{
    private Drawable outerBlack;
    private Circle outerPink;
    private Circle cursormiddl;
    protected override void Load()
    {
        outerBlack = new Circle()
        {
            Radius = 19*2,
            Thickness = 12,
            Colour = Colour4.Transparent,
            BorderColour = ColourInfo.GradientVertical(new Colour4("FC618F"), new Colour4("BB1A41")),
            Origin = Anchors.Centre
        };
        outerPink = new Circle
        {
            Radius = 18 * 2,
            BorderColour = ColourInfo.GradientVertical(new Colour4("FC618F"), new Colour4("BB1A41")),
            Thickness = 12,
            Colour = new Colour4("FC618F").Darken(0.6f).Alpha(128f),
            Origin = Anchors.Centre
        };
        cursormiddl = new Circle
        {
            Radius = 9,
            Colour = Colour4.White,
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

    public override Vector2 GetSize() {
        if (Children.Count == 0)
            return Vector2.Zero;

        float maxX = 0;
        float maxY = 0;

        foreach (var child in Children) {
            var s = child.GetSize();
            maxX = MathF.Max(maxX, s.X);
            maxY = MathF.Max(maxY, s.Y);
        }

        return new Vector2(maxX, maxY);
    }
}