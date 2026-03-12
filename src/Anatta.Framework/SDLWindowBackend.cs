using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using OpenTK.Windowing.GraphicsLibraryFramework;
using SDL2;

namespace Anatta.Framework;

internal class SDLWindowBackend : IWindowBackend
{
    private IntPtr window;
    private IntPtr context;

    public Vector2i Size { get; private set; }

    public KeyboardState KeyboardState => default;

    public event Action? Load;
    public event Action<float>? RenderFrame;
    public event Action? Unload;

    public bool MaximizeButton => false;

    public SDLWindowBackend(Vector2i size, string title)
    {
        Size = size;

        SDL.SDL_Init(SDL.SDL_INIT_VIDEO);

        window = SDL.SDL_CreateWindow(
            title,
            SDL.SDL_WINDOWPOS_CENTERED,
            SDL.SDL_WINDOWPOS_CENTERED,
            size.X,
            size.Y,
            SDL.SDL_WindowFlags.SDL_WINDOW_OPENGL
        );

        context = SDL.SDL_GL_CreateContext(window);
        GLLoader.LoadBindings(new SDLBindingsContext());
    }

    public void Run()
    {
        Load?.Invoke();

        bool running = true;
        var last = SDL.SDL_GetTicks();

        while (running)
        {
            while (SDL.SDL_PollEvent(out var e) == 1)
            {
                switch (e.type)
                {
                    case SDL.SDL_EventType.SDL_QUIT:
                        running = false;
                        break;

                    case SDL.SDL_EventType.SDL_WINDOWEVENT:
                        if (e.window.windowEvent == SDL.SDL_WindowEventID.SDL_WINDOWEVENT_RESIZED)
                        {
                            Size = new Vector2i(e.window.data1, e.window.data2);
                            GL.Viewport(0, 0, Size.X, Size.Y);
                        }
                        break;
                }
            }

            var now = SDL.SDL_GetTicks();
            float dt = (now - last) / 1000f;
            last = now;

            RenderFrame?.Invoke(dt);

            SDL.SDL_GL_SwapWindow(window);
        }

        Unload?.Invoke();
        SDL.SDL_Quit();
    }

    public void SwapBuffers()
    {
        SDL.SDL_GL_SwapWindow(window);
    }

    public void Dispose()
    {
        SDL.SDL_GL_DeleteContext(context);
        SDL.SDL_DestroyWindow(window);
    }
}