using Anatta.Framework.Graphics.Interfaces;
using Anatta.Framework.Configuration;
using Anatta.Framework.Graphics.D3D;
using OpenTK.Mathematics;

namespace Anatta.Framework.Graphics;

public class Shader : IDisposable {
    private ShaderGL? _gl;
    #if win
    private ShaderD3D? _d3d;
    #endif

    public static Shader Load(string vertex, string fragment,
        (string Name, int Offset, int Size)[]? uniforms = null,
        int cbSize = 0,
        Vortice.Direct3D11.InputElementDescription[]? layout = null)
    {
        var s = new Shader();
        if (FrameworkConfig.sRenderer == Renderer.GL)
            s._gl = ShaderGL.Load(vertex, fragment);
        #if win
        else if (FrameworkConfig.sRenderer == Renderer.D3D)
            s._d3d = ShaderD3D.Load(D3DController.Device, D3DController.Context, vertex, fragment, layout!, uniforms!, cbSize);
#endif
        return s;
    }

    public void Use() {
        _gl?.Use();
        #if win
        _d3d?.Use();
        #endif
    }
    #if win
    public void SetMatrix4(int offset, Matrix4 mat) => _d3d?.SetMatrix4(offset, mat);
    public void SetVector4(int offset, Vector4 vec) => _d3d?.SetVector4(offset, vec);
    public void SetVector2(int offset, Vector2 vec) => _d3d?.SetVector2(offset, vec);
    public void SetFloat(int offset, float value)   => _d3d?.SetFloat(offset, value);
    public void Upload()                             => _d3d?.Upload();
    #endif
    
    public void Dispose() {
        _gl?.Dispose();
        #if win
        _d3d?.Dispose();
        #endif
    }
}