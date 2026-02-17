using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using System.Runtime.InteropServices;

namespace Anatta.Framework;

public class Application : IDisposable {
    public Time Time { get; private set; }
    private GameWindow _gameWindow;
    public static GameWindow Window;
    public static Vector2i Size;
    public KeyboardState KeyboardState => _gameWindow.KeyboardState;

    protected Application(Vector2i size, string title = "Untitled")
    {
        Time = new();

        var nativeSettings = new NativeWindowSettings {
            Title = $"Anatta running: {title}",
            ClientSize = size,
            API = ContextAPI.OpenGL,
            Profile = ContextProfile.Core
        };
        Size = size;
        _gameWindow = new GameWindow(GameWindowSettings.Default, nativeSettings);
        Window = _gameWindow;

        _gameWindow.Load += OnLoad;
        _gameWindow.RenderFrame += OnRenderFrame;
        _gameWindow.Unload += OnUnload;
    }
    
    protected virtual void Initialise() {}
    protected virtual void Update(float dt, KeyboardState keyboard) { }

    protected virtual void Draw() { }

    protected virtual void OnExit() { }

    public void Run() => _gameWindow.Run();

    #region OpenTK Methods
    private void OnLoad()
    {

        #region debug info
        Console.WriteLine($"[Framework]\nWindow Size: {_gameWindow.ClientSize.X}x{_gameWindow.ClientSize.Y}\nRenderer: {GL.GetString(StringName.Renderer)}\n.NET Version: {Environment.Version}\nOS: {RuntimeInformation.OSDescription}");
        Console.WriteLine("\n\n[might be errors idk]");
        #endregion

        GL.ClearColor(new Color4(0,0,0,0));
        Initialise();
    }
    private void OnRenderFrame(FrameEventArgs args)
    {
        Time.Update((float)args.Time);

        Update(Time.Delta, _gameWindow.KeyboardState);

        GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

        Draw();


        var err = GL.GetError();
        if (err != OpenTK.Graphics.OpenGL4.ErrorCode.NoError)
            Console.WriteLine($"GL Error: {err}");

        _gameWindow.SwapBuffers();
    }
    private void OnUnload()
    {
        OnExit();
    }
    #endregion
    public void Dispose() {
        _gameWindow.Dispose();
    }
}
