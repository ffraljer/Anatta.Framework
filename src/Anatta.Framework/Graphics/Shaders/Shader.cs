#if win
using Anatta.Framework.Graphics.D3D;
#endif
using Anatta.Framework.Graphics.Interfaces;

namespace Anatta.Framework.Graphics.Shaders;

public class Shader : IDisposable {
    internal IShader? _backend;
    public IShader? Backend => _backend;
    public bool HasBegun { get; private set; }
    
    
    public PropertyCollection Properties { get; }
    
    public Shader() {
        Properties = new PropertyCollection(this);
    }

    public void Use() {
       _backend!.Use();
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