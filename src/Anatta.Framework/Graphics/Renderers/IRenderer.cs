using Anatta.Framework.Graphics.Interfaces;
using Anatta.Framework.Graphics.Rendering;

namespace Anatta.Framework.Graphics.Sprites;

public interface IRenderer {
    void Init();
    void Use(int width, int height);
    void DrawBatch(IReadOnlyList<RenderCommand> commands, int screenW, int screenH);
    void Kill();
    RenderCommand BuildCommand(ISprite sprite, int screenW, int screenH);
    void Dispose();
}