using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using System.Runtime.InteropServices;

namespace Fraljer.Anatta.Framework;

public class App : GameWindow // I want this to not be a GameWindow so you can find stuff easier.
{
    public Time Time { get; private set; }

    protected App(Vector2i size, string title = "Untitled") : base(GameWindowSettings.Default,
        new NativeWindowSettings
        {
            Title = $"Anatta running: {title}",
            ClientSize = size,
            API = ContextAPI.OpenGL,
            Profile = ContextProfile.Core
        })
    {
        Time = new();
    }
    
    protected virtual void Initialise() {} // I wanted to use "Load" but GameWindow already has it.
    protected virtual void Update(float dt) { }
    
    protected virtual void Draw() { }

    protected virtual void OnExit() { }
    
    #region OpenTK Methods
    protected override void OnLoad()
    {
        base.OnLoad();

        #region debug info
        Console.WriteLine($"[Framework]\nWindow Size: {Size.X}x{Size.Y}\nRenderer: {GL.GetString(StringName.Renderer)}\n.NET Version: {Environment.Version}\nOS: {RuntimeInformation.OSDescription}");
        Console.WriteLine("\n\n[might be errors idk]");
        #endregion

        GL.ClearColor(new Color4(0,0,0,0));
        Initialise();
    }
    protected override void OnRenderFrame(FrameEventArgs args)
    {
        Time.Update((float)args.Time);

        Update(Time.Delta);

        GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

        Draw();


        var err = GL.GetError();
        if (err != ErrorCode.NoError)
            Console.WriteLine($"GL Error: {err}");

        SwapBuffers();
    }
    protected override void OnUnload()
    {
        base.OnUnload();
        OnExit();
    }
    #endregion
}
