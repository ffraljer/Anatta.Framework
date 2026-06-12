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

        shader._backend = new ShaderGL(vert, frag);

        return shader;
    }
}