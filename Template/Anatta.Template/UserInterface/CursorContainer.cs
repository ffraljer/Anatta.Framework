using Anatta.Framework.Graphics;
using Anatta.Framework.Graphics.Shapes;
using Anatta.Framework.Graphics.Sprites;
using Anatta.Framework.Input;
using OpenTK.Mathematics;

namespace Anatta.Template.UserInterface;

public class CursorContainer : Container
{
    private Circle _outerBlack;
    private Circle _outerPink;
    private Circle _cursormiddle;
    
    protected override void Load()
    {
        _outerBlack = new Circle
        {
            Radius = 35,
            Colour = Colour4.Transparent,
            BorderColour = new Colour4(0, 0, 0),
            Thickness = 12,
            Origin = Anchors.Centre
        };
        _outerPink = new Circle
        {
            Radius = 34,
            BorderColour = Colour4.HotPink,
            Thickness = 12,
            Colour = new Colour4(0, 0, 0, 128),
            Origin = Anchors.Centre
        };
        _cursormiddle = new Circle
        {
            Radius = 6,
            Colour = Colour4.White,
            Origin = Anchors.Centre
        };

        Add(_outerBlack);
        Add(_outerPink);
        Add(_cursormiddle);
    }

    public override void Update()
    {
        base.Update();
        Position = new Vector2(Mouse.X, Mouse.Y);
        if (Mouse.IsButtonDown(Mouse.Button.Left)) {
            if (_outerBlack.Scale != new Vector2(1.25f)) {
                _outerBlack.ClearTransforms();
                _outerBlack.ScaleTo(new Vector2(1.25f), 0.1f);
                _outerPink.ClearTransforms();
                _outerPink.ScaleTo(new Vector2(1.25f), 0.1f);
            }
        }
        else
        {
            if (_outerBlack.Scale != Vector2.One) {
                _outerBlack.ClearTransforms();
                _outerBlack.ScaleTo(Vector2.One, 0.1f);
                _outerPink.ClearTransforms();
                _outerPink.ScaleTo(Vector2.One, 0.1f);
            }
        }
    }

    public override Vector2 GetSize() => Vector2.Zero;
}