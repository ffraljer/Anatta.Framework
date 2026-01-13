using Anatta.Framework.Interfaces.Graphics;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace Anatta.Framework.Graphics.Managers;

public class Manager : IDisposable {
    private readonly List<IManageable> managables = new();

    private readonly int vao;
    private readonly int vbo;
    private readonly Shader shd;

    public KeyboardState KeyboardState { get; set; }

    private static readonly float[] quad =
    {
      // X,  Y,  U,  V
        0f, 1f, 0f, 1f,
        1f, 0f, 1f, 0f,
        0f, 0f, 0f, 0f,

        0f, 1f, 0f, 1f,
        1f, 1f, 1f, 1f,
        1f, 0f, 1f, 0f
    };

    public Manager() {
        vao = GL.GenVertexArray();
        vbo = GL.GenBuffer();

        GL.BindVertexArray(vao);
        GL.BindBuffer(BufferTarget.ArrayBuffer, vbo);
        GL.BufferData(
            BufferTarget.ArrayBuffer,
            quad.Length * sizeof(float),
            quad,
            BufferUsageHint.StaticDraw
        );

        GL.EnableVertexAttribArray(0);
        GL.VertexAttribPointer(0, 2, VertexAttribPointerType.Float, false, 4 * sizeof(float), 0);

        GL.EnableVertexAttribArray(1);
        GL.VertexAttribPointer(
            1, 2, VertexAttribPointerType.Float, false, 4 * sizeof(float), 2 * sizeof(float)
        );

        shd = Shader.Load("vertexSprite.glsl", "fragmentSprite.glsl");

        GL.BindVertexArray(0);
        GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
    }


    public IEnumerable<IManageable> GetAll() => managables;

    public void Add(IManageable managed) {
        if (managed == null)
            throw new ArgumentNullException(nameof(managed));

        managables.Add(managed);
    }

    public void AddRange(params IManageable[] managedItems) {
        if (managedItems == null)
            return;

        foreach (var m in managedItems) {
            if (m != null)
                managables.Add(m);
        }
    }

    public void Remove(IManageable managed) {
        if (managed == null) return;
        managables.Remove(managed);
    }

    public void Update(float delta) {
        foreach (var item in managables) {
            if (item is IUpdatable u)
                u.Update(delta);
        }
    }


    public void Draw(int screenW, int screenH) {
        Begin(screenW, screenH);

        foreach (var item in managables)
            DrawItem(item);

        End();
    }

    private void Begin(int screenW, int screenH) {
        shd.Use();

        var projection = Matrix4.CreateOrthographicOffCenter(
            0, screenW,
            screenH, 0,
            -1, 1
        );

        shd.SetMatrix4("uProjection", projection);

        GL.ActiveTexture(TextureUnit.Texture0);
        GL.BindVertexArray(vao);

        GL.Enable(EnableCap.Blend);
        GL.BlendFunc(
            BlendingFactor.SrcAlpha,
            BlendingFactor.OneMinusSrcAlpha
        );
    }

    private void DrawItem(IManageable item) {
        if (item is not ISprite sprite)
            return;

        sprite.Texture.Bind();
        GL.Uniform1(
            GL.GetUniformLocation(shd.Handle, "tex"),
            0
        );

        var transform =
            Matrix4.CreateTranslation(sprite.Position.X, sprite.Position.Y, 0f) *
            Matrix4.CreateRotationZ(sprite.Rotation) *
            Matrix4.CreateScale(
                sprite.Texture.Width * sprite.Scale.X,
                sprite.Texture.Height * sprite.Scale.Y,
                1f
            );

        shd.SetMatrix4("transform", transform);

        GL.DrawArrays(PrimitiveType.Triangles, 0, 6);
    }

    private void End() {
        GL.BindVertexArray(0);
    }

    public void Dispose() {
        foreach (var item in managables) {
            if (item is IDisposable d)
                d.Dispose();
        }

        managables.Clear();

        GL.DeleteBuffer(vbo);
        GL.DeleteVertexArray(vao);

        shd.Dispose();
    }
}
