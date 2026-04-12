using Anatta.Framework.Graphics.Renderers;
using Anatta.Framework.IO;
using OpenTK.Mathematics;

namespace Anatta.Framework.Graphics.Sprites {
    public class Text : Drawable {
        public override Texture Texture { get; protected set; }
        public string Content { get; private set; }
        public float FontSize { get; private set; }
        
        public override Vector2 GetSize() => new Vector2(Texture.Width, Texture.Height);
        public FontFace? Font
        {
            get => _font;
            set
            {
                _font = value ?? Resource.LoadInternal<FontFace>("odat.ttf");
                Refresh();
            }
        }
        private FontFace _font = null!;

        public Text(string text, float fontSize, Color tint, FontFace? font = null) {
            Content = text;
            FontSize = fontSize;
            Colour = tint;

            this._font = font ?? Resource.LoadInternal<FontFace>("odat.ttf");
            Texture = NativeTextRenderer.CreateString(this._font, text, fontSize, tint);
        }
        public override void Dispose() {
            if (Texture != null) {
                Texture.Dispose();
                Texture = null!;
            }
        }
        private void Refresh()
        {
            Texture?.Dispose();
            Texture = NativeTextRenderer.CreateString(Font!, Content, FontSize, Colour);
        }
    }
}
