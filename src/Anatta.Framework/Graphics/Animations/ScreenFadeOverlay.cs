using Anatta.Framework.Graphics;
using Anatta.Framework.Graphics.Sprites;
using OpenTK.Mathematics;

namespace Anatta.Framework.Graphics.Animations;

internal class ScreenFadeOverlay : Sprite {
    public ScreenFadeOverlay() : base(Texture.WhitePixel) {
        Position = Vector2.Zero;
        Scale = new Vector2(SpriteManager.ScreenSize.X, SpriteManager.ScreenSize.Y);
        Colour = new Colour4(0, 0, 0, 0);
    }

}