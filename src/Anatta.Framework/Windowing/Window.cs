using Anatta.Framework.Configuration;
using OpenTK.Mathematics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Windowing.GraphicsLibraryFramework;
using Vortice.DXGI;

#if win
using Anatta.Framework.fWindowing;
#endif

namespace Anatta.Framework;

public class Window : IDisposable {
    private readonly IWindowBackend _backend;
    internal IWindowBackend Backend => _backend;
    public Vector2i Size => _backend.Size;
    public bool HideCursor { get => _backend.HideCursor; set => _backend.HideCursor = value; }
    public KeyboardState KeyboardState => _backend.KeyboardState;

    public event Action? Load {
        add => _backend.Load += value; 
        remove => _backend.Load -= value;
    }
    public event Action<float>? RenderFrame {
        add => _backend.RenderFrame += value; 
        remove => _backend.RenderFrame -= value;
    }
    public event Action<Vector2i>? Resized {
        add => _backend.Resized += value; 
        remove => _backend.Resized -= value;
    }
    public event Action? Unload {
        add => _backend.Unload += value; 
        remove => _backend.Unload -= value;
    }

    internal Window(Vector2i size, string title) {
        _backend = FrameworkConfig.sRenderer.Value switch {
#if win
        Renderer.D3D => new D3D11WindowBackend(size, title),
#endif
            Renderer.GL => new DefaultWindowBackend(size, title),
            var r=> throw new NotSupportedException(
                $"Renderer {r} is not supported on this platform.")
        };
    }

    internal string GetGraphicsRenderer() {
        #if win // I'm just basically doing this blind because I'm on Arch right now.
        if (FrameworkConfig.sRenderer.Value == Renderer.D3D ) {
            if (_backend is D3D11WindowBackend b) {
                using var dxgiDevice = b.Device.QueryInterface<IDXGIDevice>();
                using var adapter = dxgiDevice.GetAdapter();
                return adapter.Description.Description;
            }
        }
        #endif
        return GL.GetString(StringName.Renderer);
    }

    internal void OnLoad() => _backend.OnLoad();
    internal void Run() => _backend.Run();
    internal void Clear() => _backend.Clear();
    internal void SwapBuffers() => _backend.SwapBuffers();
    public void Dispose() => _backend.Dispose();
}