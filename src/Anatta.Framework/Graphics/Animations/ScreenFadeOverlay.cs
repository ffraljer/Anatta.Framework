using Anatta.Framework.Graphics;
using Anatta.Framework.Graphics.Sprites;
using OpenTK.Mathematics;

namespace Anatta.Framework.Graphics.Animations;

internal class ScreenFadeOverlay : Drawable {
    public ScreenFadeOverlay()
    {
        Position = Vector2.Zero;
        Scale = new Vector2(SpriteManager.ScreenSize.X, SpriteManager.ScreenSize.Y);
        Colour = new Color(0, 0, 0, 0);
        Texture = Texture.WhitePixel;
    }

    public override Texture Texture { get; protected set; }

    public override Vector2 GetSize() => new Vector2(SpriteManager.ScreenSize.X, SpriteManager.ScreenSize.Y);
}