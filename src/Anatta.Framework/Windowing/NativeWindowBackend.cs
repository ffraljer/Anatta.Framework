using OpenTK.Mathematics;
using OpenTK.Windowing.Desktop;
using Anatta.Framework.Input;
using OpenTK.Graphics.OpenGL;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace Anatta.Framework;

internal class NativeWindowBackend : IWindowBackend {
    private GameWindow _window;

    public Vector2i Size => _window.ClientSize;
    public KeyboardState KeyboardState => _window.KeyboardState;
    private bool _hideCursor;
    public bool HideCursor
    {
	    get => _hideCursor;
	    set
	    {
		    _hideCursor = value;

		    if (_window != null)
		    {
			    _window.CursorState = value
				    ? CursorState.Hidden
				    : CursorState.Normal;
		    }
	    }
    }
    public event Action? Load;
    public event Action<float>? RenderFrame;
    public event Action<Vector2i>? Resized;
    public event Action? Unload;

    public NativeWindowBackend(Vector2i size, string title) {
        var native = new NativeWindowSettings
        {
            Title = title,
            ClientSize = size,
        };

        _window = new GameWindow(GameWindowSettings.Default, native);

        _window.FocusedChanged += (focused) => {
	        if (focused.IsFocused)
		        _window.CursorState = HideCursor ? CursorState.Hidden : CursorState.Normal;
        };
        _window.Load += () => {
	        _window.CursorState = HideCursor ? CursorState.Hidden : CursorState.Normal;
	        Load?.Invoke();
        };
        
        _window.KeyDown += e =>
        {
	        var key = ConvertKey(e.Key);
	        if (key != Keyboard.Key.None)
		        Keyboard.KeyDown(key);
        };

        _window.KeyUp += e =>
        {
	        var key = ConvertKey(e.Key);
	        if (key != Keyboard.Key.None)
		        Keyboard.KeyUp(key);
        };
        
        _window.UpdateFrame += args =>
        {
	        Keyboard.BeginFrame();
	        Mouse.BeginFrame();

	        var mouse = _window.MouseState;

	        _MAP(mouse, MouseButton.Left, Mouse.Button.Left);
	        _MAP(mouse, MouseButton.Right, Mouse.Button.Right);
	        _MAP(mouse, MouseButton.Middle, Mouse.Button.Middle);
	        _MAP(mouse, MouseButton.Button1, Mouse.Button.X1);
	        _MAP(mouse, MouseButton.Button2, Mouse.Button.X2);

	        Mouse.Move(mouse.X, mouse.Y);

	        if (mouse.ScrollDelta.Y != 0)
	        {
	        }
        };
        _window.RenderFrame += args =>
        {
            RenderFrame?.Invoke((float)args.Time);
            _window.SwapBuffers();
        };
        _window.Resize += args =>
        {
            GL.Viewport(0, 0, args.Width, args.Height);
			Resized?.Invoke(new Vector2i(args.Width, args.Height));
        };

        _window.Unload += () => Unload?.Invoke();
    }
    private static void _MAP(MouseState mouse, MouseButton otk, Mouse.Button myButton)
    {
	    if (mouse.IsButtonDown(otk))
		    Mouse.ButtonDown(myButton);
	    else
		    Mouse.ButtonUp(myButton);
    }

    public void Run() => _window.Run();

    public void SwapBuffers() => _window.SwapBuffers();
	
	public void OnLoad() {
                GL.ClearColor(0f, 0f, 0f, 1f);
            }
    
            public void Clear() {
                GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            }

    public void Dispose() => _window.Dispose();
	
	private static Keyboard.Key ConvertKey(Keys key)
	{
		return key switch
		{
			Keys.Space => Keyboard.Key.Space,
			Keys.Enter => Keyboard.Key.Enter,
			Keys.Escape => Keyboard.Key.Escape,
			Keys.Tab => Keyboard.Key.Tab,
			Keys.Backspace => Keyboard.Key.Back,

			Keys.Left => Keyboard.Key.Left,
			Keys.Right => Keyboard.Key.Right,
			Keys.Up => Keyboard.Key.Up,
			Keys.Down => Keyboard.Key.Down,

			Keys.LeftShift => Keyboard.Key.LeftShift,
			Keys.RightShift => Keyboard.Key.RightShift,
			Keys.LeftControl => Keyboard.Key.LeftControl,
			Keys.RightControl => Keyboard.Key.RightControl,
			Keys.LeftAlt => Keyboard.Key.LeftAlt,
			Keys.RightAlt => Keyboard.Key.RightAlt,

			Keys.A => Keyboard.Key.A,
			Keys.B => Keyboard.Key.B,
			Keys.C => Keyboard.Key.C,
			Keys.D => Keyboard.Key.D,
			Keys.E => Keyboard.Key.E,
			Keys.F => Keyboard.Key.F,
			Keys.G => Keyboard.Key.G,
			Keys.H => Keyboard.Key.H,
			Keys.I => Keyboard.Key.I,
			Keys.J => Keyboard.Key.J,
			Keys.K => Keyboard.Key.K,
			Keys.L => Keyboard.Key.L,
			Keys.M => Keyboard.Key.M,
			Keys.N => Keyboard.Key.N,
			Keys.O => Keyboard.Key.O,
			Keys.P => Keyboard.Key.P,
			Keys.Q => Keyboard.Key.Q,
			Keys.R => Keyboard.Key.R,
			Keys.S => Keyboard.Key.S,
			Keys.T => Keyboard.Key.T,
			Keys.U => Keyboard.Key.U,
			Keys.V => Keyboard.Key.V,
			Keys.W => Keyboard.Key.W,
			Keys.X => Keyboard.Key.X,
			Keys.Y => Keyboard.Key.Y,
			Keys.Z => Keyboard.Key.Z,

			Keys.D0 => Keyboard.Key.D0,
			Keys.D1 => Keyboard.Key.D1,
			Keys.D2 => Keyboard.Key.D2,
			Keys.D3 => Keyboard.Key.D3,
			Keys.D4 => Keyboard.Key.D4,
			Keys.D5 => Keyboard.Key.D5,
			Keys.D6 => Keyboard.Key.D6,
			Keys.D7 => Keyboard.Key.D7,
			Keys.D8 => Keyboard.Key.D8,
			Keys.D9 => Keyboard.Key.D9,

			Keys.F1 => Keyboard.Key.F1,
			Keys.F2 => Keyboard.Key.F2,
			Keys.F3 => Keyboard.Key.F3,
			Keys.F4 => Keyboard.Key.F4,
			Keys.F5 => Keyboard.Key.F5,
			Keys.F6 => Keyboard.Key.F6,
			Keys.F7 => Keyboard.Key.F7,
			Keys.F8 => Keyboard.Key.F8,
			Keys.F9 => Keyboard.Key.F9,
			Keys.F10 => Keyboard.Key.F10,
			Keys.F11 => Keyboard.Key.F11,
			Keys.F12 => Keyboard.Key.F12,

			Keys.Insert => Keyboard.Key.Insert,
			Keys.Delete => Keyboard.Key.Delete,
			Keys.Home => Keyboard.Key.Home,
			Keys.End => Keyboard.Key.End,

			_ => Keyboard.Key.None
		};
	}
}