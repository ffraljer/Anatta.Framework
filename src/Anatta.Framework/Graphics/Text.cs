using Anatta.Framework.Graphics.Renderers;
using Anatta.Framework.Interfaces.Graphics;
using Anatta.Framework.IO;
using OpenTK.Mathematics;

namespace Anatta.Framework.Graphics {
    public class Text : ISprite, IUpdatable {
        public Texture Texture { get; private set; }
        public Vector2 Position { get; set; } = Vector2.Zero;
        public Vector2 Scale { get; set; } = Vector2.One;
        public Anchors Origin { get; set; } = Anchors.TopLeft;
        public Anchors Anchor { get; set; } = Anchors.TopLeft;
        public float Rotation { get; set; } = 0f;

        public string pText { get; private set; }
        public float FontSize { get; private set; }
        public Colour Colour { get; private set; }
        public FontFace? Font
        {
            get => font;
            set
            {
                font = value ?? Resource.LoadInternal<FontFace>("odat.ttf");
                refresh();
            }
        }
        private FontFace font = null!;

        public Text(string text, float fontSize, Colour colour, FontFace? font) {
            pText = text;
            FontSize = fontSize;
            Colour = colour;

            this.font = font ?? Resource.LoadInternal<FontFace>("odat.ttf");
            Texture = NativeTextRenderer.CreateString(this.font, text, fontSize, colour);
        }
        public Text(string text, float fontSize, Colour colour) {
            pText = text;
            FontSize = fontSize;
            Colour = colour;

            Font = Resource.LoadInternal<FontFace>("odat.ttf");
            Texture = NativeTextRenderer.CreateString(this.font, text, fontSize, colour);
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
        private void refresh()
        {
            Texture?.Dispose();
            Texture = NativeTextRenderer.CreateString(Font!, pText, FontSize, Colour);
        }
    }
}
