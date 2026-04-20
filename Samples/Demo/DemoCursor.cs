using Anatta.Framework;
using Anatta.Framework.Graphics;
using Anatta.Framework.Graphics.Shapes;
using Anatta.Framework.Graphics.Sprites;
using Anatta.Framework.Input;
using OpenTK.Mathematics;

namespace Demo;

public class DemoCursor : Container
{
    private Drawable outerBlack;
    private float fadetimer = 0;
    protected override void Load()
    {
        outerBlack = new Circle()
        {
            Radius = 19*2,
            Thickness = 12,
            Colour = Color.White.Alpha(128),
            BorderColour = ColorInfo.GradientVertical(Color.White, Color.White.Darken(0.6f)),
            Origin = Anchors.Centre
        };

        Add(outerBlack);
    }

    public override void Update()
    {
        base.Update();
        fadetimer += Time.Delta;
        Console.WriteLine(fadetimer);
        Position = new Vector2(Mouse.X, Mouse.Y);
        if (Mouse.IsButtonDown(Mouse.Button.Left)) {
            if (outerBlack.Scale != new Vector2(1.2f)) {
                outerBlack.ClearTransforms();
                outerBlack.ScaleTo(new Vector2(1.2f), 0.1f);

                if (fadetimer <= 5) {
                    outerBlack.FadeTo(128, 2f, Easing.OutCubic);
                }
            }
        }
        else {
            fadetimer = 0;
            if (outerBlack.Scale != Vector2.One) {
                outerBlack.ClearTransforms();
                outerBlack.ScaleTo(Vector2.One, 0.1f);
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