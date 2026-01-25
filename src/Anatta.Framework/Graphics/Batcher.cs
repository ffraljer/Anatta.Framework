using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using Anatta.Framework.Interfaces.Graphics;

namespace Anatta.Framework.Graphics;

// my plan is to "unify" Manager and this. or, possibly behead batcher and replace it with
//manager. (or, you know. kill both of them)
// done
[Obsolete("Batcher is obsolete, use Manager instead.")]
public class Batcher : IDisposable
{
    // I wish OpenTK had a built-in sprite batcher. (or at least some 4k of it)
    public int vao, vbo;
    public Shader shd;
    public static readonly float[] quad =
    {
        // X,  Y,  U,  V
        0f, 1f, 0f, 1f,
        1f, 0f, 1f, 0f,
        0f, 0f, 0f, 0f,

        0f, 1f, 0f, 1f,
        1f, 1f, 1f, 1f,
        1f, 0f, 1f, 0f
    };
    public Batcher()
    {
        vao = GL.GenVertexArray();
        vbo = GL.GenBuffer();

        GL.BindVertexArray(vao);
        GL.BindBuffer(BufferTarget.ArrayBuffer, vbo);
        GL.BufferData(BufferTarget.ArrayBuffer, quad.Length * sizeof(float), quad, BufferUsageHint.StaticDraw);


        GL.EnableVertexAttribArray(0);
        GL.VertexAttribPointer(0, 2, VertexAttribPointerType.Float, false, 4 * sizeof(float), 0);
        GL.EnableVertexAttribArray(1);
        GL.VertexAttribPointer(1, 2, VertexAttribPointerType.Float, false, 4 * sizeof(float), 2 * sizeof(float));

        shd = Shader.Load("vertexSprite.glsl", "fragmentSprite.glsl");

        GL.BindVertexArray(0);
        GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
    }
    public void Begin(int screenW, int screenH)
    {
        shd.Use();

        Matrix4 projection = Matrix4.CreateOrthographicOffCenter(
            0, screenW, screenH, 0, -1, 1);
        shd.SetMatrix4("uProjection", projection);


        GL.ActiveTexture(TextureUnit.Texture0);
        GL.BindVertexArray(vao);

        GL.Enable(EnableCap.Blend);
        GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);
    }

    public void End()
    {
        GL.BindVertexArray(0);
    }
       
    public void Draw(IManageable item)
    {
        shd.Use();
        GL.BindVertexArray(vao);
        if (item is not ISprite sprite) return;

        sprite.Texture.Bind();
        GL.Uniform1(GL.GetUniformLocation(shd.Handle, "tex"), 0);

        Vector2 finalPos = sprite.Position;

        var transform =
            Matrix4.CreateTranslation(finalPos.X, finalPos.Y, 0f) *
            Matrix4.CreateRotationZ(sprite.Rotation) *
            Matrix4.CreateScale(sprite.Texture.Width * sprite.Scale.X,
                sprite.Texture.Height * sprite.Scale.Y, 1f) *
            Matrix4.CreateTranslation(sprite.Position.X, sprite.Position.Y, 0f);

        shd.SetMatrix4("transform", transform);

        GL.DrawArrays(PrimitiveType.Triangles, 0, 6);
    }
    public void Dispose()
    {
        if (vbo != 0)
        {
            GL.DeleteBuffer(vbo);
            vbo = 0;
        }

        if (vao != 0)
        {
            GL.DeleteVertexArray(vao);
            vao = 0;
        }

        shd?.Dispose();
        shd = null!;
    }
}
