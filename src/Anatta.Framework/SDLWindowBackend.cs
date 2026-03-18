using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using OpenTK.Windowing.GraphicsLibraryFramework;
using SDL3;
using System.Runtime.InteropServices;
using Anatta.Framework.Input;

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
            Keyboard.BeginFrame();
            Mouse.BeginFrame();
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
                    case SDL.EventType.KeyDown:
                        Keyboard.KeyDown(ConvertKey(@event.Key.Key));
                        break;

                    case SDL.EventType.KeyUp:
                        Keyboard.KeyUp(ConvertKey(@event.Key.Key));
                        break;
                    case SDL.EventType.MouseButtonDown:
                        Mouse.ButtonDown((Input.Mouse.Button)(@event.Button.Button - 1));
                        break;

                    case SDL.EventType.MouseButtonUp:
                        Mouse.ButtonUp((Input.Mouse.Button)(@event.Button.Button - 1));
                        break;
                    case SDL.EventType.MouseMotion:
                        Mouse.Move(@event.Motion.X, @event.Motion.Y);
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

    #region  keys
    private static Keyboard.Key ConvertKey(SDL.Keycode key)
    {
        return key switch
        {
            SDL.Keycode.Space => Keyboard.Key.Space,
            SDL.Keycode.Return => Keyboard.Key.Enter,
            SDL.Keycode.Escape => Keyboard.Key.Escape,
            SDL.Keycode.Tab => Keyboard.Key.Tab,
            SDL.Keycode.Backspace => Keyboard.Key.Back,

            SDL.Keycode.Left => Keyboard.Key.Left,
            SDL.Keycode.Right => Keyboard.Key.Right,
            SDL.Keycode.Up => Keyboard.Key.Up,
            SDL.Keycode.Down => Keyboard.Key.Down,

            SDL.Keycode.LShift => Keyboard.Key.LeftShift,
            SDL.Keycode.RShift => Keyboard.Key.RightShift,
            SDL.Keycode.LCtrl => Keyboard.Key.LeftControl,
            SDL.Keycode.RCtrl => Keyboard.Key.RightControl,
            SDL.Keycode.LAlt => Keyboard.Key.LeftAlt,
            SDL.Keycode.RAlt => Keyboard.Key.RightAlt,

            SDL.Keycode.A => Keyboard.Key.A,
            SDL.Keycode.B => Keyboard.Key.B,
            SDL.Keycode.C => Keyboard.Key.C,
            SDL.Keycode.D => Keyboard.Key.D,
            SDL.Keycode.E => Keyboard.Key.E,
            SDL.Keycode.F => Keyboard.Key.F,
            SDL.Keycode.G => Keyboard.Key.G,
            SDL.Keycode.H => Keyboard.Key.H,
            SDL.Keycode.I => Keyboard.Key.I,
            SDL.Keycode.J => Keyboard.Key.J,
            SDL.Keycode.K => Keyboard.Key.K,
            SDL.Keycode.L => Keyboard.Key.L,
            SDL.Keycode.M => Keyboard.Key.M,
            SDL.Keycode.N => Keyboard.Key.N,
            SDL.Keycode.O => Keyboard.Key.O,
            SDL.Keycode.P => Keyboard.Key.P,
            SDL.Keycode.Q => Keyboard.Key.Q,
            SDL.Keycode.R => Keyboard.Key.R,
            SDL.Keycode.S => Keyboard.Key.S,
            SDL.Keycode.T => Keyboard.Key.T,
            SDL.Keycode.U => Keyboard.Key.U,
            SDL.Keycode.V => Keyboard.Key.V,
            SDL.Keycode.W => Keyboard.Key.W,
            SDL.Keycode.X => Keyboard.Key.X,
            SDL.Keycode.Y => Keyboard.Key.Y,
            SDL.Keycode.Z => Keyboard.Key.Z,

            SDL.Keycode.Alpha0 => Keyboard.Key.D0,
            SDL.Keycode.Alpha1 => Keyboard.Key.D1,
            SDL.Keycode.Alpha2 => Keyboard.Key.D2,
            SDL.Keycode.Alpha3 => Keyboard.Key.D3,
            SDL.Keycode.Alpha4 => Keyboard.Key.D4,
            SDL.Keycode.Alpha5 => Keyboard.Key.D5,
            SDL.Keycode.Alpha6 => Keyboard.Key.D6,
            SDL.Keycode.Alpha7 => Keyboard.Key.D7,
            SDL.Keycode.Alpha8 => Keyboard.Key.D8,
            SDL.Keycode.Alpha9 => Keyboard.Key.D9,

            SDL.Keycode.F1 => Keyboard.Key.F1,
            SDL.Keycode.F2 => Keyboard.Key.F2,
            SDL.Keycode.F3 => Keyboard.Key.F3,
            SDL.Keycode.F4 => Keyboard.Key.F4,
            SDL.Keycode.F5 => Keyboard.Key.F5,
            SDL.Keycode.F6 => Keyboard.Key.F6,
            SDL.Keycode.F7 => Keyboard.Key.F7,
            SDL.Keycode.F8 => Keyboard.Key.F8,
            SDL.Keycode.F9 => Keyboard.Key.F9,
            SDL.Keycode.F10 => Keyboard.Key.F10,
            SDL.Keycode.F11 => Keyboard.Key.F11,
            SDL.Keycode.F12 => Keyboard.Key.F12,

            SDL.Keycode.Insert => Keyboard.Key.Insert,
            SDL.Keycode.Delete => Keyboard.Key.Delete,
            SDL.Keycode.Home => Keyboard.Key.Home,
            SDL.Keycode.End => Keyboard.Key.End,

            _ => Keyboard.Key.None
        };
    }
    #endregion
}