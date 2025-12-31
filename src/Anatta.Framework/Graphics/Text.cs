using Anatta.Framework.Graphics.Renderers;
using Anatta.Framework.Interfaces.Graphics;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Text;

namespace Anatta.Framework.Graphics {
    public class Text : ISprite, IUpdatable {
        public Texture Texture { get; private set; }
        public Vector2 Position { get; set; } = Vector2.Zero;
        public Vector2 Scale { get; set; } = Vector2.One;
        public float Rotation { get; set; } = 0f;

        public string pText { get; private set; }
        public float FontSize { get; private set; }
        public Colour Colour { get; private set; }

        public Text(string text, byte[] fontData, float fontSize, Colour colour) {
            pText = text;
            FontSize = fontSize;
            Colour = colour;

            Texture = NativeTextRenderer.CreateString(fontData, text, fontSize, colour);
        }

        public void Draw(Batcher batcher) {
            batcher.Draw(this);
        }

        public void Dispose() {
            if (Texture != null) {
                Texture.Dispose();
                Texture = null!;
            }
        }
    }
}
