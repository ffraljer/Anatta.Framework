using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace Anatta.Framework;

public class Application : IDisposable {
    public Time Time { get; } = new Time();
    private readonly IWindowBackend backend;

    public static KeyboardState KeyboardState;

    public Vector2i Size;

    protected Application(Vector2i size, bool UseSDL, string title = "Untitled")
    {
        backend = UseSDL
            ? new SDLWindowBackend(size, title)
            : new ToolkitWindowBackend(size, title);

        KeyboardState = backend.KeyboardState;

        Size = backend.Size;
        backend.Load += OnLoad;
        backend.RenderFrame += OnRenderFrame;
        backend.Unload += OnUnload;
    }
    
    protected virtual void Initialise() {}
    protected virtual void Update(float dt) { }

    protected virtual void Draw() { }

    protected virtual void OnExit() { }

    public void Run() => backend.Run();

    private void OnLoad()
    {
        GL.ClearColor(0f, 0f, 0f, 1f);
        Initialise();
    }

    private void OnRenderFrame(float dt)
    {
        Time.Update(dt);

        Update(Time.Delta);

        GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

        Draw();
    }

    private void OnUnload()
    {
        OnExit();
    }

    public void Dispose()
    {
        backend.Dispose();
    }
}
