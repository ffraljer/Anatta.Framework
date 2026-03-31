using System.Runtime.InteropServices;
using Anatta.Framework.Configuration;
using Anatta.Framework.Graphics.Managers;
using Anatta.Framework.Logging;
using Anatta.Framework.Threading;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;

namespace Anatta.Framework;

public class Application : IDisposable {
    public Time Time { get; } = new Time();
    public Scheduler Scheduler { get; } = new Scheduler();
    
    private readonly IWindowBackend _backend;

    protected Logger logger = new("Application");
    private Logger _frameworkLogger = new("Framework");
    
    public static FrameworkConfig Config { get; set; }
    
    public static IWindowBackend Backend;

    public Vector2i Size;

    public bool HideCursor;

    public static Application Instance;

    protected Application(Vector2i size, string title = "Untitled", bool useSdl = true)
    {
        Config = new FrameworkConfig();
        
        _backend = useSdl
            ? new DefaultWindowBackend(size, title)
            : new GameWindowWindowBackend(size, title);
        Backend = _backend;

        Size = _backend.Size;
        Manager.ScreenSize = Size;
        _backend.Load += OnLoad;
        _backend.RenderFrame += OnRenderFrame;
        _backend.Unload += OnUnload;
        _backend.HideCursor = HideCursor;
        Instance = this;
        if (!useSdl)
            logger.Warn("Use SDL.");
        else {
        }
    }
    
    protected virtual void Initialise() {}
    protected virtual void Update() { }

    protected virtual void Draw() { }

    protected virtual void OnExit() { }

    public void Run() => _backend.Run();

    private void OnLoad()
    {
        _frameworkLogger.Info($"Window Size: {_backend.Size.X}x{_backend.Size.Y}");
        _frameworkLogger.Info($"Renderer: {GL.GetString(StringName.Renderer)}");
        _frameworkLogger.Info($".NET Version: {Environment.Version}");
        _frameworkLogger.Info($"OS: {RuntimeInformation.OSDescription}");
        _frameworkLogger.Info($"Window Backend: {_backend.ToString().TrimStart("Anatta.Framework.")}");
        GL.ClearColor(0f, 0f, 0f, 1f);
        Initialise();
    }

    private void OnRenderFrame(float dt)
    {
        Time.Update(dt);

        Scheduler.Update();
        
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
        _backend.Dispose();
        logger.Info("Application disposed.");
    }
}
