using Anatta.Framework.Graphics.Animations;
using Anatta.Framework.Graphics.Helpers;
using Anatta.Framework.Graphics.Renderers;
using Anatta.Framework.Interfaces.Graphics;
using Anatta.Framework.IO;
using OpenTK.Mathematics;

namespace Anatta.Framework.Graphics {
    public class Text : ISprite, IUpdatable {
        public event Action<ISprite>? OnClick;
        
        public Texture Texture { get; private set; }
        public Vector2 Position { get; set; } = Vector2.Zero;
        public Vector2 Scale { get; set; } = Vector2.One;
        public Anchors Origin { get; set; } = Anchors.TopLeft;
        public Anchors Anchor { get; set; } = Anchors.TopLeft;
        public float Rotation { get; set; } = 0f;
        private Vector2 _mousePosition;
        private bool _mousePressed;
        public string Content { get; private set; }
        public float FontSize { get; private set; }
        private List<ITween> tweens = new();
        public Colour Colour { get; private set; }
        public FontFace? Font
        {
            get => _font;
            set
            {
                _font = value ?? Resource.LoadInternal<FontFace>("odat.ttf");
                refresh();
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
            for (int i = tweens.Count - 1; i >= 0; i--)
            {
                if (tweens[i].Update())
                    tweens.RemoveAt(i);
                    tweens.RemoveAt(i);
            }
        }
        public void TriggerClick()
        {
            OnClick?.Invoke(this);
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
            Texture = NativeTextRenderer.CreateString(Font!, Content, FontSize, Colour);
        }

        /// <param name="position">Position of the Sprite.</param>
        /// <param name="duration">Seconds.</param>
        /// <returns>The current <see cref="ISprite"/> instance, allowing method chaining.</returns>
        public ISprite MoveTo(Vector2 position, float duration, Easing easing = Easing.None, bool loop = false)
        {
            tweens.Add(new Tween<Vector2>
            {
                Loop = loop,
                Getter = () => Position,
                Setter = v => Position = v,
                Start = Position,
                End = position,
                Ease = easing,
                Duration = duration,
                Lerp = AnimationHelper.Lerp
            });

            return this;
        }
        /// <param name="scale">Scale of the Sprite.</param>
        /// <param name="duration">Seconds.</param>
        /// <returns>The current <see cref="ISprite"/> instance, allowing method chaining.</returns>
        public ISprite ScaleTo(Vector2 scale, float duration, Easing easing = Easing.None, bool loop = false)
        {
            tweens.Add(new Tween<Vector2>
            {
                Loop = loop,
                Getter = () => Scale,
                Setter = v => Scale = v,
                End = scale,
                Ease = easing,
                Duration = duration,
                Lerp = AnimationHelper.Lerp
            });

            return this;
        }
        /// <param name="rotation">Degrees.</param>
        /// <param name="duration">Seconds.</param>
        /// <returns>The current <see cref="ISprite"/> instance, allowing method chaining.</returns>
        public ISprite RotateTo(float rotation, float duration, Easing easing = Easing.None, bool loop = false)
        {
            var r = MathHelper.DegreesToRadians(rotation);
            tweens.Add(new Tween<float>
            {
                Loop = loop,
                Getter = () => Rotation,
                Setter = v => Rotation = v,
                Ease = easing,
                Start = Rotation,
                End = r,
                Duration = duration,
                Lerp = AnimationHelper.Lerp
            });

            return this;
        }
    }
}
