using OpenTK.Mathematics;
using OpenTK.Windowing.Desktop;
using OpenTK.Graphics.OpenGL;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace Anatta.Framework;

internal class ToolkitWindowBackend : IWindowBackend {
    private GameWindow window;

    public Vector2i Size => window.ClientSize;
    public KeyboardState KeyboardState => window.KeyboardState;

    public event Action? Load;
    public event Action<float>? RenderFrame;
    public event Action? Unload;

    public ToolkitWindowBackend(Vector2i size, string title)
    {
        var native = new NativeWindowSettings
        {
            Title = title,
            ClientSize = size
        };

        window = new GameWindow(GameWindowSettings.Default, native);

        window.Load += () => Load?.Invoke();

        window.RenderFrame += args =>
        {
            RenderFrame?.Invoke((float)args.Time);
            window.SwapBuffers();
        };
        window.Resize += args =>
        {
            GL.Viewport(0, 0, args.Width, args.Height);
        };

        window.Unload += () => Unload?.Invoke();
    }

    public void Run() => window.Run();

    public void SwapBuffers() => window.SwapBuffers();

    public void Dispose() => window.Dispose();
}