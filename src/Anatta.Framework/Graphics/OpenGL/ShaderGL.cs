using System.Reflection;
using Anatta.Framework.Graphics.Interfaces;
using Anatta.Framework.Logging;
using OpenTK.Graphics.OpenGL;
using G = OpenTK.Graphics.OpenGL.GL;
using OpenTK.Mathematics;
using Vortice.SpirvCross;

namespace Anatta.Framework.Graphics;

public class GLShader : IDisposable, IShader
{
    public int Handle { get; set; }
    private int Gandle => Handle;
    private Logger _logger = new("Shader");

    internal static GLShader Load(string vertex, string fragment)
    {
        var a = Assembly.GetExecutingAssembly();

        using Stream vr = a.GetManifestResourceStream($"Anatta.Framework.Resources.{vertex}")
                          ?? throw new FileNotFoundException($"{vertex}  cannot be found");
        using Stream fr = a.GetManifestResourceStream($"Anatta.Framework.Resources.{fragment}")
                          ?? throw new FileNotFoundException($"{fragment}  cannot be found");

        using var streamReaderVertex = new StreamReader(vr);
        using var streamReaderFragment = new StreamReader(fr);

        return new GLShader(streamReaderVertex.ReadToEnd(), streamReaderFragment.ReadToEnd());
    }

    public GLShader(string vtx, string frg)
    {
        int vtxS = GL.CreateShader(ShaderType.VertexShader);
        GL.ShaderSource(vtxS, vtx);
        GL.CompileShader(vtxS);
        CheckCompile(vtxS, "vertex");
        
        int frgS = GL.CreateShader(ShaderType.FragmentShader);
        GL.ShaderSource(frgS, frg);
        GL.CompileShader(frgS);
        CheckCompile(frgS, "fragment");

        Handle = GL.CreateProgram();
        GL.AttachShader(Handle, vtxS);
        GL.AttachShader(Handle, frgS);
        GL.LinkProgram(Handle);
        
        G.GetProgrami(Handle, ProgramProperty.LinkStatus, out int success);
        if (success == 0)
        {
            GL.GetProgramInfoLog(Handle, out string info);
            throw new Exception($"ur fucking shader broke bro {info}");
            _logger.Error("Shader error!");
        }
        
        G.DetachShader(Handle, vtxS);
        G.DetachShader(Handle, frgS);
        G.DeleteShader(vtxS);
        G.DeleteShader(frgS);
    }
    
    
    private void CheckCompile(int shader, string type)
    {
        G.GetShaderi(shader, ShaderParameterName.CompileStatus, out int success);
        if (success == 0)
        {
            GL.GetShaderInfoLog(shader, out string info);
            throw new Exception($"ur {type} shader fucking broke: {info}");
            _logger.Error("Shader error!");
        }
    }

    public void Use() => G.UseProgram(Handle);
    
    public void SetMatrix4(string name, Matrix4 mat)
    {
        int loc = G.GetUniformLocation(Gandle, name);
        GL.UniformMatrix4f(loc, 1, false, ref mat); 
    }
    public void SetFloat(string name, float value)
    {
        int loc = G.GetUniformLocation(Handle, name);
        G.Uniform1f(loc, value);
    }
    public void SetInt(string name, int value)
    {
        int loc = G.GetUniformLocation(Handle, name);
        G.Uniform1i(loc, value);
    }
    public void UseWith(Action<GLShader> setup) {
        Use();
        setup(this);
    }
    public void SetBool(string name, bool value) {
        int loc = GL.GetUniformLocation(Handle, name);
        GL.Uniform1i(loc, value ? 1 : 0);
    }
    public void SetVector2(string name, Vector2 vec)
    {
        int loc = G.GetUniformLocation(Gandle, name);
        GL.Uniform2f(loc, vec.X, vec.Y);
    }
    public void SetVector3(string name, Vector3 vec)
    {
        int loc = G.GetUniformLocation(Handle, name);
        G.Uniform3f(loc, vec.X, vec.Y, vec.Z);
    }
    public void SetVector4(string name, Vector4 vec)
    {
        int loc = G.GetUniformLocation(Handle, name);
        G.Uniform4f(loc, vec.X, vec.Y, vec.Z, vec.W);
    }

    public void Dispose() => G.DeleteProgram(Handle);
}
