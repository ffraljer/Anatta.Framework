using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using Anatta.Framework.Interfaces.Graphics;

namespace Anatta.Framework.Graphics;

// my plan is to "unify" Manager and this. or, possibly behead batcher and replace it with
//manager. (or, you know. kill both of them)
// done
[Obsolete("Use Manager instead.", true)]
public class Batcher : IDisposable
{
    public int VAO, VBO;
    public Shader Shader;
    
    private static readonly float[] _quad =
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
        VAO = GL.GenVertexArray();
        VBO = GL.GenBuffer();

        GL.BindVertexArray(VAO);
        GL.BindBuffer(BufferTarget.ArrayBuffer, VBO);
        GL.BufferData(BufferTarget.ArrayBuffer, _quad.Length * sizeof(float), _quad, BufferUsage.StaticDraw);


        GL.EnableVertexAttribArray(0);
        GL.VertexAttribPointer(0, 2, VertexAttribPointerType.Float, false, 4 * sizeof(float), 0);
        GL.EnableVertexAttribArray(1);
        GL.VertexAttribPointer(1, 2, VertexAttribPointerType.Float, false, 4 * sizeof(float), 2 * sizeof(float));

        Shader = Shader.Load("vertexSprite.glsl", "fragmentSprite.glsl");

        GL.BindVertexArray(0);
        GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
    }
    public void Begin(int screenW, int screenH)
    {
        Shader.Use();

        Matrix4 projection = Matrix4.CreateOrthographicOffCenter(
            0, screenW, screenH, 0, -1, 1);
        Shader.SetMatrix4("uProjection", projection);


        GL.ActiveTexture(TextureUnit.Texture0);
        GL.BindVertexArray(VAO);

        GL.Enable(EnableCap.Blend);
        GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);
    }

    public void End()
    {
        GL.BindVertexArray(0);
    }
       
    public void Draw(IManageable item)
    {
        Shader.Use();
        GL.BindVertexArray(VAO);
        if (item is not ISprite sprite) return;

        sprite.Texture.Bind();
        GL.Uniform1i(GL.GetUniformLocation(Shader.Handle, "tex"), 0);

        Vector2 finalPos = sprite.Position;

        var transform =
            Matrix4.CreateTranslation(finalPos.X, finalPos.Y, 0f) *
            Matrix4.CreateRotationZ(sprite.Rotation) *
            Matrix4.CreateScale(sprite.Texture.Width * sprite.Scale.X,
                sprite.Texture.Height * sprite.Scale.Y, 1f) *
            Matrix4.CreateTranslation(sprite.Position.X, sprite.Position.Y, 0f);

        Shader.SetMatrix4("transform", transform);

        GL.DrawArrays(PrimitiveType.Triangles, 0, 6);
    }
    public void Dispose()
    {
        if (VBO != 0)
        {
            GL.DeleteBuffer(VBO);
            VBO = 0;
        }

        if (VAO != 0)
        {
            GL.DeleteVertexArray(VAO);
            VAO = 0;
        }

        Shader?.Dispose();
        Shader = null!;
    }
}
