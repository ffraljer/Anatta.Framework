using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using OpenTK.Windowing.GraphicsLibraryFramework;
using SDL3;
using System.Runtime.InteropServices;

namespace Anatta.Framework;

internal class SDLWindowBackend : IWindowBackend
{
    private IntPtr window;
    private IntPtr context;

    public Vector2i Size { get; private set; }

    public KeyboardState KeyboardState => default;

    public IntPtr WindowHandle;

    public event Action? Load;
    public event Action<float>? RenderFrame;
    public event Action? Unload;

    public bool MaximizeButton => false;

    public SDLWindowBackend(Vector2i size, string title)
    {
        Size = size;

        SDL.Init(SDL.InitFlags.Video);
        
        SDL.GLSetAttribute(SDL.GLAttr.ContextMajorVersion, 4);
        SDL.GLSetAttribute(SDL.GLAttr.ContextMinorVersion, 6 );
        SDL.GLSetAttribute(SDL.GLAttr.ContextProfileMask, (int)OpenGlProfile.Core);
        SDL.GLSetAttribute(SDL.GLAttr.DoubleBuffer, 1);
        SDL.GLSetAttribute(SDL.GLAttr.DepthSize, 24);

        window = SDL.CreateWindow(
            title,
            size.X,
            size.Y,
            SDL.WindowFlags.OpenGL | SDL.WindowFlags.Resizable
        );
        context = SDL.GLCreateContext(window);
        SDL.GLMakeCurrent(window, context);
        GLLoader.LoadBindings(new SDLBindingsContext());
    }
    public void Run()
    {
        Load?.Invoke();

        bool running = true;
        var last = SDL.GetTicks();

        while (running)
        {
            
            while (SDL.PollEvent(out var @event) == true)
            {
                switch ((SDL.EventType)@event.Type)
                {
                    case SDL.EventType.Quit:
                        running = false;
                        break;
                    case SDL.EventType.WindowResized:
                        Size = new Vector2i(@event.Window.Data1, @event.Window.Data2);
                        GL.Viewport(0, 0, Size.X, Size.Y);
                        break;
                }
            }
            ulong now = SDL.GetTicks();
            float dt = (now - last) / 1000f;
            last = now;

            RenderFrame?.Invoke(dt);

            SDL.GLSwapWindow(window);
        }

        Unload?.Invoke();
        SDL.Quit();
    }

    public void SwapBuffers()
    {
        SDL.GLSwapWindow(window);
    }

    public void Dispose()
    {
        SDL.GLDestroyContext(context);
        SDL.DestroyWindow(window);
    }
}