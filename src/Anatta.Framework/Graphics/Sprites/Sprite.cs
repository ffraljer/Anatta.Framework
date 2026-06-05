using Anatta.Framework.Graphics.Interfaces;
using Anatta.Framework.Graphics.Rendering;
using OpenTK.Mathematics;

namespace Anatta.Framework.Graphics.Sprites;

public class Sprite(Texture texture) : Drawable, ITexturedDrawable {
    public Texture Texture { get; protected set; } = texture;

    public override void Dispose() => Texture.Dispose();
    
    public override Vector2 GetSize() => new Vector2(Texture.Width, Texture.Height);

    public override RenderCommand BuildRenderCommand(Vector2i screenSize) {
        var size = new Vector2(Texture.Width * Scale.X, Texture.Height * Scale.Y);
        var tint = Colour.Top.ToVector4();
        return new RenderCommand {
            Texture = Texture,
            TintTop = tint,
            TintBottom = tint,
            Size = size,
            Transform = BuildTransform(size, screenSize)
        };
    }
}