using Anatta.Framework.Configuration;
using Anatta.Framework.Graphics.D3D;
using Anatta.Framework.Graphics.OpenGL;
using Anatta.Framework.Graphics.Shaders;

namespace Anatta.Framework.Storage.Loaders;

public class ShaderLoader {
    public string Folder => "Shaders";
    
    public bool CanLoad(Type type) {
        return type == typeof(Shader);
    }
    
    public object Load(string name, byte[] data)
    {
        throw new InvalidOperationException(
            "use the other one"
        );
    }

    public Shader Load(string vert, string frag)
    {
        var shader = new Shader();

        if (FrameworkConfig.sRenderer == Renderer.GL)
            shader._backend = new ShaderGL(vert, frag);

#if win
        else if (FrameworkConfig.sRenderer == Renderer.D3D)
            shader._backend =
                ShaderD3D.Load(
                    D3DController.Device,
                    D3DController.Context,
                    vert,
                    frag
                );
#endif

        return shader;
    }
}