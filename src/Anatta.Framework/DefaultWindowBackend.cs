    using OpenTK.Graphics;
    using OpenTK.Graphics.OpenGL;
    using OpenTK.Mathematics;
    using OpenTK.Windowing.GraphicsLibraryFramework;
    using SDL3;
    using Anatta.Framework.Input;

    namespace Anatta.Framework;

    internal class DefaultWindowBackend : IWindowBackend
    {
        private readonly IntPtr _window;
        private readonly IntPtr _context;

        public Vector2i Size { get; private set; }

        public KeyboardState KeyboardState => default;

        public bool HideCursor { get; set; }

        public IntPtr WindowHandle => _window;

        public event Action? Load;
        public event Action<float>? RenderFrame;
        public event Action? Unload;


        public DefaultWindowBackend(Vector2i size, string title)
        {
            Size = size;

            SDL.Init(SDL.InitFlags.Video);
            
            SDL.GLSetAttribute(SDL.GLAttr.ContextMajorVersion, 4);
            SDL.GLSetAttribute(SDL.GLAttr.ContextMinorVersion, 6 );
            SDL.GLSetAttribute(SDL.GLAttr.ContextProfileMask, (int)OpenGlProfile.Core);
            SDL.GLSetAttribute(SDL.GLAttr.DoubleBuffer, 1);
            SDL.GLSetAttribute(SDL.GLAttr.DepthSize, 24);

            _window = SDL.CreateWindow(
                title,
                size.X,
                size.Y,
                SDL.WindowFlags.OpenGL | SDL.WindowFlags.Resizable
            );
            if (HideCursor) {
                SDL.HideCursor();
            }
            _context = SDL.GLCreateContext(_window);
            SDL.GLMakeCurrent(_window, _context);
            GLLoader.LoadBindings(new _SDLBindingsContext());
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
                while (SDL.PollEvent(out var @event))
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
                            Keyboard.KeyDown(ConvertKey(@event.Key.Scancode));
                            break;

                        case SDL.EventType.KeyUp:
                            Keyboard.KeyUp(ConvertKey(@event.Key.Scancode));
                            break;
                        case SDL.EventType.MouseButtonDown:
                            Mouse.ButtonDown((Mouse.Button)(@event.Button.Button - 1));
                            break;

                        case SDL.EventType.MouseButtonUp:
                            Mouse.ButtonUp((Mouse.Button)(@event.Button.Button - 1));
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

                SDL.GLSwapWindow(_window);
            }

            Unload?.Invoke();
            SDL.Quit();
        }

        public void SwapBuffers()
        {
            SDL.GLSwapWindow(_window);
        }

        public void Dispose()
        {
            SDL.GLDestroyContext(_context);
            SDL.DestroyWindow(_window);
        }

        #region  keys
        private static Keyboard.Key ConvertKey(SDL.Scancode key)
        {
            return key switch
            {
                SDL.Scancode.Space => Keyboard.Key.Space,
                SDL.Scancode.Return => Keyboard.Key.Enter,
                SDL.Scancode.Escape => Keyboard.Key.Escape,
                SDL.Scancode.Tab => Keyboard.Key.Tab,
                SDL.Scancode.Backspace => Keyboard.Key.Back,

                SDL.Scancode.Left => Keyboard.Key.Left,
                SDL.Scancode.Right => Keyboard.Key.Right,
                SDL.Scancode.Up => Keyboard.Key.Up,
                SDL.Scancode.Down => Keyboard.Key.Down,

                SDL.Scancode.LShift => Keyboard.Key.LeftShift,
                SDL.Scancode.RShift => Keyboard.Key.RightShift,
                SDL.Scancode.LCtrl => Keyboard.Key.LeftControl,
                SDL.Scancode.RCtrl => Keyboard.Key.RightControl,
                SDL.Scancode.LAlt => Keyboard.Key.LeftAlt,
                SDL.Scancode.RAlt => Keyboard.Key.RightAlt,

                SDL.Scancode.A => Keyboard.Key.A,
                SDL.Scancode.B => Keyboard.Key.B,
                SDL.Scancode.C => Keyboard.Key.C,
                SDL.Scancode.D => Keyboard.Key.D,
                SDL.Scancode.E => Keyboard.Key.E,
                SDL.Scancode.F => Keyboard.Key.F,
                SDL.Scancode.G => Keyboard.Key.G,
                SDL.Scancode.H => Keyboard.Key.H,
                SDL.Scancode.I => Keyboard.Key.I,
                SDL.Scancode.J => Keyboard.Key.J,
                SDL.Scancode.K => Keyboard.Key.K,
                SDL.Scancode.L => Keyboard.Key.L,
                SDL.Scancode.M => Keyboard.Key.M,
                SDL.Scancode.N => Keyboard.Key.N,
                SDL.Scancode.O => Keyboard.Key.O,
                SDL.Scancode.P => Keyboard.Key.P,
                SDL.Scancode.Q => Keyboard.Key.Q,
                SDL.Scancode.R => Keyboard.Key.R,
                SDL.Scancode.S => Keyboard.Key.S,
                SDL.Scancode.T => Keyboard.Key.T,
                SDL.Scancode.U => Keyboard.Key.U,
                SDL.Scancode.V => Keyboard.Key.V,
                SDL.Scancode.W => Keyboard.Key.W,
                SDL.Scancode.X => Keyboard.Key.X,
                SDL.Scancode.Y => Keyboard.Key.Y,
                SDL.Scancode.Z => Keyboard.Key.Z,

                SDL.Scancode.Alpha0 => Keyboard.Key.D0,
                SDL.Scancode.Alpha1 => Keyboard.Key.D1,
                SDL.Scancode.Alpha2 => Keyboard.Key.D2,
                SDL.Scancode.Alpha3 => Keyboard.Key.D3,
                SDL.Scancode.Alpha4 => Keyboard.Key.D4,
                SDL.Scancode.Alpha5 => Keyboard.Key.D5,
                SDL.Scancode.Alpha6 => Keyboard.Key.D6,
                SDL.Scancode.Alpha7 => Keyboard.Key.D7,
                SDL.Scancode.Alpha8 => Keyboard.Key.D8,
                SDL.Scancode.Alpha9 => Keyboard.Key.D9,

                SDL.Scancode.F1 => Keyboard.Key.F1,
                SDL.Scancode.F2 => Keyboard.Key.F2,
                SDL.Scancode.F3 => Keyboard.Key.F3,
                SDL.Scancode.F4 => Keyboard.Key.F4,
                SDL.Scancode.F5 => Keyboard.Key.F5,
                SDL.Scancode.F6 => Keyboard.Key.F6,
                SDL.Scancode.F7 => Keyboard.Key.F7,
                SDL.Scancode.F8 => Keyboard.Key.F8,
                SDL.Scancode.F9 => Keyboard.Key.F9,
                SDL.Scancode.F10 => Keyboard.Key.F10,
                SDL.Scancode.F11 => Keyboard.Key.F11,
                SDL.Scancode.F12 => Keyboard.Key.F12,

                SDL.Scancode.Insert => Keyboard.Key.Insert,
                SDL.Scancode.Delete => Keyboard.Key.Delete,
                SDL.Scancode.Home => Keyboard.Key.Home,
                SDL.Scancode.End => Keyboard.Key.End,

                _ => Keyboard.Key.None
            };
        }
        #endregion
    }