using Anatta.Framework.Graphics.Interfaces;
using Anatta.Framework.Configuration;
using Anatta.Framework.Graphics.D3D;
using Anatta.Framework.Graphics.OpenGL;
using OpenTK.Mathematics;

namespace Anatta.Framework.Graphics.Shaders;

public class Shader : IDisposable {
    private IShader? _backend;
    public IShader? Backend => _backend;
    public bool HasBegun { get; private set; }
    
    
    public PropertyCollection Properties { get; }
    
    public Shader() {
        Properties = new PropertyCollection(this);
    }

    public static Shader Load(string vertex, string fragment) { // so low cortisol
        var s = new Shader();
        if (FrameworkConfig.sRenderer == Renderer.GL)
            s._backend = ShaderGL.Load(vertex, fragment);
#if win
        else if (FrameworkConfig.sRenderer == Renderer.D3D)
            s._backend = ShaderD3D.Load(D3DController.Device, D3DController.Context, vertex, fragment);
#endif
        return s;
    }

    public void Use() {
       _backend.Use();
       HasBegun = true;
       Properties.Set();
       #if win
               if (_backend is ShaderD3D dekt) dekt.Upload();
       #endif
    }
    public void Kill() {
        HasBegun = false;
    }

    public void Dispose() {
        _backend?.Dispose();
    }
}