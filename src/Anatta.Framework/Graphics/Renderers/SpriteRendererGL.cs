using Anatta.Framework.Input;
using Anatta.Framework.Graphics.Helpers;
using Anatta.Framework.Graphics.Interfaces;
using Anatta.Framework.Graphics.OpenGL;
using Anatta.Framework.Graphics.Rendering;
using Anatta.Framework.Graphics.Shapes;
using Anatta.Framework.Graphics.Sprites;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;

namespace Anatta.Framework.Graphics.Renderers;

public class SpriteRendererGL : IRenderer {
    private int _vao;
    private int _vbo;
    private int _qoobo;
    private Matrix4 _projection;
    private ShaderGL _shd;
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
    
    public RenderCommand BuildCommand(ITexturedDrawable sprite, int screenW, int screenH) {
        Vector2 size;
        var cmd = new RenderCommand();

        if (sprite is Box box) {
            size = box.Size * box.Scale;

            cmd.Texture = Texture.WhitePixel;
            cmd.TintTop = box.Colour.Top.ToVector4();
            cmd.TintBottom = box.Colour.Bottom.ToVector4();
            cmd.Size = size;
            cmd.Radius = box.CornerRadius;
            cmd.BorderTop = box.BorderColour.Top.ToVector4();
            cmd.BorderBottom = box.BorderColour.Bottom.ToVector4();
            cmd.BoxBorderThickness = box.BorderThickness;
        }
        else if (sprite is Circle circle) {
            size = new Vector2(
                circle.Radius * 2f * circle.Scale.X,
                circle.Radius * 2f * circle.Scale.Y
            );

            cmd.Texture = Texture.WhitePixel;
            cmd.TintTop = circle.Colour.Top.ToVector4();
            cmd.TintBottom = circle.Colour.Bottom.ToVector4();
            cmd.BorderTop = circle.BorderColour.Top.ToVector4();
            cmd.BorderBottom = circle.BorderColour.Bottom.ToVector4();
            cmd.CircleRadius = circle.Radius * MathF.Max(circle.Scale.X, circle.Scale.Y);
            cmd.CircleThickness = circle.Thickness * MathF.Max(circle.Scale.X, circle.Scale.Y);
            cmd.IsCircle = true;
            cmd.Size = size;
        }
        else if (sprite.Texture != null) {
            var glTex = ((Texture)sprite.Texture);
            if (glTex == null) return default;
            
            size = new Vector2(
                sprite.Texture.Width * sprite.Scale.X,
                sprite.Texture.Height * sprite.Scale.Y
            );

            var tint = sprite.Colour.Top.ToVector4();

            cmd.Texture = glTex;
            cmd.TintTop = tint;
            cmd.TintBottom = tint;
            cmd.Size = size;
        }
        else return default;

        var originNorm = AnchorHelper.ToNormalised(sprite.Origin);
        var originOffset = originNorm * size;

        var anchorNorm = AnchorHelper.ToNormalised(sprite.Anchor);
        var anchorOffset = new Vector2(
            anchorNorm.X * screenW,
            anchorNorm.Y * screenH
        );

        cmd.Transform =
            Matrix4.CreateScale(size.X, size.Y, 1f) *
            Matrix4.CreateTranslation(-originOffset.X, -originOffset.Y, 0f) *
            Matrix4.CreateRotationZ(sprite.Rotation) *
            Matrix4.CreateTranslation(sprite.DrawPosition.X, sprite.DrawPosition.Y, 0f) *
            Matrix4.CreateTranslation(anchorOffset.X, anchorOffset.Y, 0f);

        return cmd;
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
        GL.DeleteBuffer(_vbo);
        GL.DeleteVertexArray(_vao);

        _shd.Dispose();
    }
    
    private static void WriteMatrix(byte[] buf, int offset, Matrix4 m) {
        var floats = new float[] {
            m.M11, m.M12, m.M13, m.M14,
            m.M21, m.M22, m.M23, m.M24,
            m.M31, m.M32, m.M33, m.M34,
            m.M41, m.M42, m.M43, m.M44
        };
        System.Buffer.BlockCopy(floats, 0, buf, offset, 64);
    }
    private static void WriteVec4(byte[] buf, int offset, Vector4 v) {
        var floats = new float[] { v.X, v.Y, v.Z, v.W };
        System.Buffer.BlockCopy(floats, 0, buf, offset, 16);
    }
    private static void WriteVec2(byte[] buf, int offset, Vector2 v) {
        var floats = new float[] { v.X, v.Y };
        System.Buffer.BlockCopy(floats, 0, buf, offset, 8);
    }
    private static void WriteFloat(byte[] buf, int offset, float v) {
        System.Buffer.BlockCopy(new[] { v }, 0, buf, offset, 4);
    }
}

