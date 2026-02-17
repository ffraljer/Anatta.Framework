using System;
using System.IO;
using System.Reflection;
using OpenTK.Graphics.OpenGL4;
using G = OpenTK.Graphics.OpenGL4.GL;
using OpenTK.Mathematics;

namespace Anatta.Framework.Graphics;

public class Shader : IDisposable
{
    public int Handle { get; private set; }
    private int Gandle => Handle;

    internal static Shader Load(string vertex, string fragment)
    {
        var a = Assembly.GetExecutingAssembly();

        using Stream vr = a.GetManifestResourceStream($"Anatta.Framework.Resources.{vertex}")
                          ?? throw new FileNotFoundException($"{vertex}  cannot be found");
        using Stream fr = a.GetManifestResourceStream($"Anatta.Framework.Resources.{fragment}")
                          ?? throw new FileNotFoundException($"{fragment}  cannot be found");

        using var vreader = new StreamReader(vr);
        using var freader = new StreamReader(fr);

        return new Shader(vreader.ReadToEnd(), freader.ReadToEnd());
    }

    public Shader(string vtx, string frg)
    {
        int vtxS = GL.CreateShader(ShaderType.VertexShader);
        GL.ShaderSource(vtxS, vtx);
        GL.CompileShader(vtxS);
        CheckCompile(vtxS, "vertex");
        
        int frgS = GL.CreateShader(ShaderType.FragmentShader);
        GL.ShaderSource(frgS, frg);
        GL.CompileShader(frgS);
        CheckCompile(vtxS, "fragment");

        Handle = GL.CreateProgram();
        GL.AttachShader(Handle, vtxS);
        GL.AttachShader(Handle, frgS);
        GL.LinkProgram(Handle);
        
        G.GetProgram(Handle, GetProgramParameterName.LinkStatus, out int success);
        if (success == 0)
        {
            string info = G.GetProgramInfoLog(Handle);
            throw new Exception($"ur fucking shader broke broo {info}");
        }
        
        G.DetachShader(Handle, vtxS);
        G.DetachShader(Handle, frgS);
        G.DeleteShader(vtxS);
        G.DeleteShader(frgS);
    }
    
    
    private static void CheckCompile(int shader, string type)
    {
        G.GetShader(shader, ShaderParameter.CompileStatus, out int success);
        if (success == 0)
        {
            string info = GL.GetShaderInfoLog(shader);
            throw new Exception($"ur {type} shader fuckin' broke: {info}");
        }
    }

    public void Use() => G.UseProgram(Handle);
    
    public void SetMatrix4(string name, Matrix4 mat)
    {
        int loc = G.GetUniformLocation(Gandle, name);
        G.UniformMatrix4(loc, false, ref mat);
    }
    public void SetFloat(string name, float value)
    {
        int loc = G.GetUniformLocation(Handle, name);
        G.Uniform1(loc, value);
    }
    public void SetInt(string name, int value)
    {
        int loc = G.GetUniformLocation(Handle, name);
        G.Uniform1(loc, value);
    }
    public void SetVector2(string name, Vector2 vec)
    {
        int loc = G.GetUniformLocation(Gandle, name);
        G.Uniform2(loc, vec);
    }
    public void SetVector3(string name, Vector3 vec)
    {
        int loc = G.GetUniformLocation(Handle, name);
        G.Uniform3(loc, vec);
    }
    public void SetVector4(string name, Vector4 vec)
    {
        int loc = G.GetUniformLocation(Handle, name);
        G.Uniform4(loc, vec);
    }

    public void Dispose() => G.DeleteProgram(Handle);
}
