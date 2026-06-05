using OpenTK.Mathematics;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace Anatta.Framework;

public interface IWindowBackend : IDisposable {
    Vector2i Size { get; }
    bool HideCursor { get; set; }
    KeyboardState KeyboardState { get; }

    void Run();
    void SwapBuffers();

    event Action? Load;
    event Action<float>? RenderFrame;
    event Action<Vector2i>? Resized;
    event Action? Unload;
    
    void OnLoad();
    void Clear(); 
}