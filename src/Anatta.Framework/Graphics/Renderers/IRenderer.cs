
using Anatta.Framework.Graphics.Rendering;

namespace Anatta.Framework.Graphics.Renderers;

public interface IRenderer : IDisposable {
    void Init();
    void Use(int width, int height);
    void Submit(RenderCommand cmd);
    void Kill();
}