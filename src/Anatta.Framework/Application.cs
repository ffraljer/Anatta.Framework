using System.Reflection;
using System.Runtime.InteropServices;
using Anatta.Framework.Configuration;
using Anatta.Framework.Graphics.Sprites;
using Anatta.Framework.Logging;
using Anatta.Framework.Threading;
#if win
using Anatta.Framework.fWindowing;
#endif
using OpenTK.Mathematics;

namespace Anatta.Framework;

public static class BackendFactory {
    public static IWindowBackend Create(Vector2i size, string title)
    {
        #if WINDOWS
        if (FrameworkConfig.sRenderer == Renderer.D3D)
            return new D3D11WindowBackend(size, title);
        #endif
        return new DefaultWindowBackend(size, title);
        // can the GameWindow be used in my Vortiche? maybe not.
    }
}

public class Application : IDisposable {  
    private Time _time { get; } = new Time();
    public Scheduler Scheduler { get; } = new Scheduler();
    
    private readonly IWindowBackend _backend;

    protected Logger logger = new("Application");

    private Logger _frameworkLogger = new("Framework");
    
    public static FrameworkConfig Config { get; set; }

    public static WindowManager WindowManager { get; private set; } = new WindowManager();

    public static IWindowBackend Backend;

    public bool HideCursor;

    public static Application Instance;

    protected Application(Vector2i size, string title = "Untitled", bool dontlaunch = false) {
        if (dontlaunch == true) return;
        /*
          ⣠⣤⣤⣤⡤⢤⣤⣤⣤⣤⣤⣄⣀⣀⡀⠀⠀⠀⠀⠀⠀⠀⠀
⠀⠀⠀⠀⠀⣠⣿⡿⣟⠯⡒⢯⣽⣓⣒⢾⣯⣭⣿⣿⠿⠭⠭⣯⣷⣦⡀⠀⠀⠀
⠀⠀⠀⠀⣰⣿⣯⣞⣕⣽⠾⠿⠿⠿⢿⣏⣿⣿⣿⡗⣽⣿⣿⣷⡝⣿⣿⡆⠀⠀
⠀⠀⠀⣀⣛⠛⢿⣛⢝⢁⣀⣀⣀⠓⠶⠈⣿⣿⡿⠗⠉⠁⢀⣀⣹⣛⣛⣳⢄⠀
⠀⡔⡾⢁⣴⡆⢦⣬⣙⣛⣋⣤⣿⣿⣷⣾⣿⣿⣿⡆⢿⣿⡟⠻⠛⡉⣍⣲⢱⠁
⠀⣇⣇⢸⣉⡀⢦⣌⡙⠻⠿⣯⣭⣥⠡⡤⠿⢿⣿⣿⡆⠉⡻⢿⣿⠇⢻⣟⠼⠀
⠀⠈⠪⣴⣿⣧⡀⢉⠛⠘⢶⣦⣬⠉⣀⠓⠿⠿⠯⢉⣴⠿⠿⠓⡁⡄⠀⣿⠃⠀
⠀⠀⠀⠙⣿⣿⣷⣌⠻⢠⣤⣀⠉⠐⠛⠿⠿⠰⠶⠦⠰⠶⠇⠘⠃⠁⠀⣿⠀⠀
⠀⠀⠀⠀⠘⢿⣿⣿⣷⣌⠻⢿⠇⣼⣶⣦⡄⣄⣀⡀⢀⡀⢀⡀⡀⠀⢠⣿⠀⠀
⠀⠀⠀⠀⠀⠀⠙⠯⣛⠭⣻⠶⣬⣉⣛⠛⠃⠿⠿⠃⠿⠃⠚⣀⣁⣤⣾⣿⡀⠀
⠀⠀⠀⠀⠀⠀⠀⠀⠀⠙⠒⠯⣶⣋⡽⢛⣿⣯⣿⣭⣭⡿⢿⣿⣻⣾⢟⣿⡇⠀
⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠉⠛⠿⠿⣶⣾⣿⣿⣿⣭⣭⣭⣶⣿⡿⠁⠀
⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠉⠙⠛⠛⠛⠛⠋⠁⠀⠀⠀
         */


        FrameworkController.GlobalGame = this;
        
        Config = new FrameworkConfig();
        
        _backend = BackendFactory.Create(size, title);
        Backend = _backend;

        _backend.Load += OnLoad;
        _backend.RenderFrame += OnRenderFrame;
        _backend.Unload += OnUnload;
        _backend.HideCursor = HideCursor;
        _backend.Resized += OnResize;

        Instance = this;
    }
    
    protected virtual void Initialise() { }
    protected virtual void Update() { }

    protected virtual void Draw() { }

    protected virtual void OnExit() { }

    public void Run() => _backend.Run();

    private void OnLoad()
    {
        _frameworkLogger.Info($"Window Size: {_backend.Size.X}x{_backend.Size.Y}");
        _frameworkLogger.Info($"Renderer: {FrameworkConfig.sRenderer.ToString()}");
        _frameworkLogger.Info($".NET Version: {Environment.Version}");
        _frameworkLogger.Info($"OS: {RuntimeInformation.OSDescription}");
        _frameworkLogger.Info($"Window Backend: {_backend.ToString().TrimStart("Anatta.Framework.")}");
        WindowManager.Width = _backend.Size.X;
        WindowManager.Height = _backend.Size.Y;


        SpriteManager.ScreenSize = new Vector2i(
            WindowManager.Width,
            WindowManager.Height
        );

        _backend.OnLoad();
        Initialise();
    }
    
    public void Exit() {
        OnExit();
        _backend.Dispose();
    }

    private void OnRenderFrame(float dt)
    {
        Time.Update(dt);

        Scheduler.Update();
        
        Update();

        _backend.Clear();

        Draw();
    }
    

    private void OnResize(Vector2i size) {
        WindowManager.Width = size.X;
        WindowManager.Height = size.Y;

        SpriteManager.ScreenSize = new Vector2i(
            WindowManager.Width,
            WindowManager.Height
        );
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
