using Anatta.Framework.Graphics.OpenGL;
using Anatta.Framework.Graphics.Rendering;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;

namespace Anatta.Framework.Graphics.Renderers;

public class SpriteRendererGL : IRenderer {
    private int _vao;
    private int _vbo;
    private int _qoobo;
    private Matrix4 _projection;
    private ShaderGL? _shd;
    private bool _initialized;
    private readonly List<RenderCommand> _commands = new(256);
    private readonly byte[] _uniformData = new byte[224];

    public void Submit(RenderCommand cmd) => _commands.Add(cmd);

    private static readonly float[] Quad =
    {
        // X,  Y,  U,  V
        0f, 1f, 0f, 1f,
        1f, 0f, 1f, 0f,
        0f, 0f, 0f, 0f,

        0f, 1f, 0f, 1f,
        1f, 1f, 1f, 1f,
        1f, 0f, 1f, 0f
    };
    
    public void Init()
    {
        if (_initialized) return;
        
        _vao = GL.GenVertexArray();
        _vbo = GL.GenBuffer();
        
        _qoobo = GL.GenBuffer();
        GL.BindBuffer(BufferTarget.UniformBuffer, _qoobo);
        GL.BufferData(BufferTarget.UniformBuffer, 256, IntPtr.Zero, BufferUsage.DynamicDraw);
        GL.BindBufferBase(BufferTarget.UniformBuffer, 0, _qoobo);

        GL.BindVertexArray(_vao);
        GL.BindBuffer(BufferTarget.ArrayBuffer, _vbo);
        GL.BufferData(
            BufferTarget.ArrayBuffer,
            Quad.Length * sizeof(float),
            Quad,
            BufferUsage.StaticDraw
        );

        GL.EnableVertexAttribArray(0);
        GL.VertexAttribPointer(0, 2, VertexAttribPointerType.Float, false, 4 * sizeof(float), 0);

        GL.EnableVertexAttribArray(1);
        GL.VertexAttribPointer(
            1, 2, VertexAttribPointerType.Float, false, 4 * sizeof(float), 2 * sizeof(float)
        );

        _shd = ShaderGL.Load("sprite.avs", "sprite.afs");

        GL.BindVertexArray(0);
        GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
        
        _initialized = true;
    }
    public void Use(int screenW, int screenH) {
        if (_shd is null) return;
        _shd.Use();
        GL.Uniform1i(GL.GetUniformLocation(_shd.Handle, "tex"), 0);

        
        _projection = Matrix4.CreateOrthographicOffCenter(0, screenW, screenH, 0, -1, 1);
        
        
        GL.ActiveTexture(TextureUnit.Texture0);
        GL.BindVertexArray(_vao);
        GL.BindBufferBase(BufferTarget.UniformBuffer, 0, _qoobo);

        GL.Enable(EnableCap.Blend);
        GL.BlendFunc(
            BlendingFactor.SrcAlpha,
            BlendingFactor.OneMinusSrcAlpha
        );
    }

    private void DrawCommand(RenderCommand cmd) {
        cmd.Texture!.GetGL().Bind();
        WriteMatrix(_uniformData, 0, cmd.Transform);
        WriteMatrix(_uniformData, 64, _projection);
        WriteVec4(_uniformData, 128, cmd.TintTop);
        WriteVec4(_uniformData, 144, cmd.TintBottom);
        WriteVec4(_uniformData, 160, cmd.BorderTop);
        WriteVec4(_uniformData, 176, cmd.BorderBottom);
        WriteVec2(_uniformData, 192, cmd.Size);
        WriteFloat(_uniformData, 200, cmd.Radius);
        WriteFloat(_uniformData, 204, cmd.CircleRadius);
        WriteFloat(_uniformData, 208, cmd.CircleThickness);
        WriteFloat(_uniformData, 212, cmd.BoxBorderThickness);
        GL.BindBuffer(BufferTarget.UniformBuffer, _qoobo);
        GL.BufferSubData(BufferTarget.UniformBuffer, 0, _uniformData.Length, _uniformData);
        GL.DrawArrays(PrimitiveType.Triangles, 0, 6);
    }

    public void Kill() {
        foreach (var cmd in _commands)
            DrawCommand(cmd);
        _commands.Clear();
        GL.BindVertexArray(0);
    }
    
    public void Dispose() {
        if (!_initialized) return;
        
        GL.DeleteBuffer(_vbo);
        GL.DeleteBuffer(_qoobo);
        GL.DeleteVertexArray(_vao);

        _shd!.Dispose(); // if it's already disposing, shader CAN'T be null, right?
        _initialized = false;
    }
    
    private static void WriteMatrix(byte[] buf, int offset, Matrix4 m) {
        float[] floats = [
            m.M11, m.M12, m.M13, m.M14,
            m.M21, m.M22, m.M23, m.M24,
            m.M31, m.M32, m.M33, m.M34,
            m.M41, m.M42, m.M43, m.M44
        ];
        System.Buffer.BlockCopy(floats, 0, buf, offset, 64);
    }
    private static void WriteVec4(byte[] buf, int offset, Vector4 v) {
        float[] floats = [v.X, v.Y, v.Z, v.W];
        System.Buffer.BlockCopy(floats, 0, buf, offset, 16);
    }
    private static void WriteVec2(byte[] buf, int offset, Vector2 v) {
        float[] floats = [v.X, v.Y];
        System.Buffer.BlockCopy(floats, 0, buf, offset, 8);
    }
    private static void WriteFloat(byte[] buf, int offset, float v) {
        System.Buffer.BlockCopy(new[] { v }, 0, buf, offset, 4);
    }
}

