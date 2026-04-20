#if WINDOWS
using Anatta.Framework.Graphics;
using Anatta.Framework.Graphics.D3D;
using Vortice.Direct3D11;
using Vortice.DXGI;
using Vortice.Direct3D;
using OpenTK.Mathematics;
using Anatta.Framework.Input;
using SDL3;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace Anatta.Framework.Windowing;

internal class D3D11WindowBackend : IWindowBackend {
    private readonly IntPtr _window;
    private ID3D11Device _device;
    private ID3D11DeviceContext _context;
    private IDXGISwapChain _swapChain;
    private ID3D11RenderTargetView _rtv;

    public KeyboardState KeyboardState { get; }

    public Vector2i Size { get; private set; }
    public bool HideCursor { get; set; }

    public event Action<Vector2i>? Resized;
    public event Action? Load;
    public event Action<float>? RenderFrame;
    public event Action? Unload;

    public IntPtr WindowHandle => _window;
    public ID3D11Device Device => _device;
    public ID3D11DeviceContext DeviceContext => _context;

    public D3D11WindowBackend(Vector2i size, string title) {
        Size = size;

        SDL.Init(SDL.InitFlags.Video);

        _window = SDL.CreateWindow(
            title,
            size.X,
            size.Y,
            SDL.WindowFlags.Resizable
        );

        var hwnd = GetHwnd();
        InitD3D11(hwnd, size);
    }

    private IntPtr GetHwnd() {
        var props = SDL.GetWindowProperties(_window);
        return SDL.GetPointerProperty(props, "SDL.window.win32.hwnd", IntPtr.Zero);
    }

    private void InitD3D11(IntPtr hwnd, Vector2i size) {
        var swapChainDesc = new SwapChainDescription {
            BufferCount = 2,
            BufferDescription = new ModeDescription((uint)size.X, (uint)size.Y, Format.R8G8B8A8_UNorm),
            Windowed = true,
            OutputWindow = hwnd,
            SampleDescription = new SampleDescription(1, 0),
            SwapEffect = SwapEffect.Discard,
            BufferUsage = Usage.RenderTargetOutput
        };

        D3D11.D3D11CreateDeviceAndSwapChain(
            null,
            DriverType.Hardware,
            DeviceCreationFlags.None,
            new[] { FeatureLevel.Level_11_0 },
            swapChainDesc,
            out _swapChain,
            out _device,
            out _,
            out _context
        );

        D3DController.Set(_device, _context);

        CreateRtv();
    }

    private void CreateRtv() {
        using var backBuffer = _swapChain.GetBuffer<ID3D11Texture2D>(0);
        _rtv = _device.CreateRenderTargetView(backBuffer);
        _context.OMSetRenderTargets(_rtv);
    }

    public void OnLoad() { }

    public void Clear() {
        _context.ClearRenderTargetView(_rtv, new Vortice.Mathematics.Color4(0, 0, 0, 1));
    }

    public void Run() {
        Load?.Invoke();

        bool running = true;
        var last = SDL.GetTicks();

        while (running) {
            Keyboard.BeginFrame();
            Mouse.BeginFrame();

            while (SDL.PollEvent(out var @event)) {
                switch ((SDL.EventType)@event.Type) {
                    case SDL.EventType.Quit:
                        running = false;
                        break;
                    case SDL.EventType.WindowResized:
                        Size = new Vector2i(@event.Window.Data1, @event.Window.Data2);
                        Resize(Size);
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

            _context.ClearRenderTargetView(_rtv, new Vortice.Mathematics.Color4(0, 0, 0, 1));
            RenderFrame?.Invoke(dt);
            _swapChain.Present(1, PresentFlags.None);
        }

        Unload?.Invoke();
        SDL.Quit();
    }

    private void Resize(Vector2i size) {
        _rtv.Dispose();
        _swapChain.ResizeBuffers(0, (uint)size.X, (uint)size.Y, Format.Unknown, SwapChainFlags.None);
        CreateRtv();
        Resized?.Invoke(size);
    }

    public void SwapBuffers() => _swapChain.Present(1, PresentFlags.None);

    public void Dispose() {
        _rtv.Dispose();
        _swapChain.Dispose();
        _context.Dispose();
        _device.Dispose();
        SDL.DestroyWindow(_window);
    }

    #region keys

    private static Keyboard.Key ConvertKey(SDL.Scancode key) {
        return key switch {
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
#endif