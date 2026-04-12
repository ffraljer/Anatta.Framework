using Anatta.Framework;
using Anatta.Framework.Graphics;
using Anatta.Framework.Graphics.Sprites;
using Anatta.Framework.IO;
using OpenTK.Mathematics;

namespace Demo;

internal class pSymbolAwesome : Text
{
    internal pSymbolAwesome(SymbolAwesome symbol, float size, Vector2 position)
        : base(((char)symbol).ToString(), size, Color.White, Fonts.FontAwesome)
    {
        Origin = Anchors.BottomLeft;
    }
}
