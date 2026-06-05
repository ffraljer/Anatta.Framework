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

public class Application : IDisposable {  
    private Time _time { get; } = new Time();
    public Scheduler Scheduler { get; } = new Scheduler();
    
    private readonly Window _window;

    protected Logger logger = new("Application");

    private Logger _frameworkLogger = new("Framework");
    
    public static FrameworkConfig Config { get; set; }

    public static WindowManager WindowManager { get; private set; } = new WindowManager();

    public static Window Window;

    public bool HideCursor;

    public static Application Instance;

    protected Application(Vector2i size, string title = "Untitled", bool dontlaunch = false) {
        if (dontlaunch is true) return;
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
        
        _window = new(size, title);
        Window = _window;

        _window.Load += OnLoad;
        _window.RenderFrame += OnRenderFrame;
        _window.Unload += OnUnload;
        _window.HideCursor = HideCursor;
        _window.Resized += OnResize;

        Instance = this;
    }
    
    protected virtual void Initialise() { }
    protected virtual void Update() { }

    protected virtual void Draw() { }

    protected virtual void OnExit() { }

    public void Run() => _window.Run();

    private void OnLoad()
    {
        _frameworkLogger.Info($"Window Size: {_window.Size.X}x{_window.Size.Y}");
        _frameworkLogger.Info($"Renderer: {FrameworkConfig.sRenderer.ToString()}");
        _frameworkLogger.Info($"Graphics Renderer: {_window.GetGraphicsRenderer()}");
        _frameworkLogger.Info($".NET Version: {Environment.Version}");
        _frameworkLogger.Info($"OS: {RuntimeInformation.OSDescription}");
        //_frameworkLogger.Info($"Window Backend: {_backend.ToString().TrimStart("Anatta.Framework.")}");
        // add back IF I ever add back other backends
        WindowManager.Width = _window.Size.X;
        WindowManager.Height = _window.Size.Y;


        SpriteManager.ScreenSize = new Vector2i(
            WindowManager.Width,
            WindowManager.Height
        );

        _window.OnLoad();
        Initialise();
    }
    
    public void Exit() {
        OnExit();
        _window.Dispose();
    }

    private void OnRenderFrame(float dt)
    {
        Time.Update(dt);

        Scheduler.Update();
        
        Update();

        _window.Clear();

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
        _window.Dispose();
        logger.Info("Application disposed.");
    }
}
