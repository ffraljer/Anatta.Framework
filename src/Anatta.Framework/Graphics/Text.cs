using Anatta.Framework.Graphics.Animations;
using Anatta.Framework.Graphics.Helpers;
using Anatta.Framework.Graphics.Renderers;
using Anatta.Framework.Interfaces.Graphics;
using Anatta.Framework.IO;
using OpenTK.Mathematics;

namespace Anatta.Framework.Graphics {
    public class Text : _Sprite {
        public override Texture Texture { get; protected set; }
        public string Content { get; private set; }
        public float FontSize { get; private set; }
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

        public Text(string text, float fontSize, Colour colour, FontFace? font = null) {
            Content = text;
            FontSize = fontSize;
            Colour = colour;

            this._font = font ?? Resource.LoadInternal<FontFace>("odat.ttf");
            Texture = NativeTextRenderer.CreateString(this._font, text, fontSize, colour);
        }
        public void Update()
        {
            base.Update();
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
