using Anatta.Framework;
using Anatta.Framework.Graphics;
using Anatta.Framework.Graphics.Sprites;
using Anatta.Framework.Storage;
using OpenTK.Mathematics;

namespace Demo;

internal class pSymbolAwesome : Text
{
    internal pSymbolAwesome(SymbolAwesome symbol, float size, Vector2 position)
        : base(((char)symbol).ToString(), size, Colour4.White, Fonts.FontAwesome)
    {
        Origin = Anchors.BottomLeft;
    }
}
