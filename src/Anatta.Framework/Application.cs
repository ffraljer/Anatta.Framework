using System.Reflection.PortableExecutable;
using System.Runtime.InteropServices;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace Anatta.Framework;

public class Application : IDisposable {
    public Time Time { get; } = new Time();
    private readonly IWindowBackend backend;
    

    /// <summary>
    /// The bindow backend
    /// </summary>
    public static IWindowBackend Backend;

    public static KeyboardState KeyboardState;

    public Vector2i Size;

    protected Application(Vector2i size, string title = "Untitled", bool UseSDL = true)
    {
        backend = UseSDL
            ? new SDLWindowBackend(size, title)
            : new ToolkitWindowBackend(size, title);
        Backend = backend;

        KeyboardState = backend.KeyboardState;

        Size = backend.Size;
        backend.Load += OnLoad;
        backend.RenderFrame += OnRenderFrame;
        backend.Unload += OnUnload;
        if (!UseSDL)
            Console.WriteLine("Use SDL.");
        else {
            return;
        }
    }
    
    protected virtual void Initialise() {}
    protected virtual void Update() { }

    protected virtual void Draw() { }

    protected virtual void OnExit() { }

    public void Run() => backend.Run();

    private void OnLoad()
    {
        Console.WriteLine($"[Framework]\nWindow Size: {backend.Size.X}x{backend.Size.Y}\n" +
                          $"Renderer: {GL.GetString(StringName.Renderer)}" +
                          $"\n.NET Version: {Environment.Version}\n" +
                          $"OS: {RuntimeInformation.OSDescription}" +
                          $"\nWindow Backend: {backend.ToString().TrimStart("Anatta.Framework.")}");
        //Console.WriteLine("\n\n[might be errors idk]");
        GL.ClearColor(0f, 0f, 0f, 1f);
        Initialise();
    }

    private void OnRenderFrame(float dt)
    {
        Time.Update(dt);

        Update();

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
