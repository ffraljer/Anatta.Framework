using Anatta.Framework.Graphics.Helpers;
using Anatta.Framework.Interfaces.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace Anatta.Framework.Graphics.Managers;

public class Manager : IDisposable {
    private readonly List<IManageable> managables = new();
    
    private int vao;
    private int vbo;
    private Shader shd;
    
    private bool initialized = false;

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
        
    }
    private void Init()
    {
        if (initialized) return;
        
        vao = GL.GenVertexArray();
        vbo = GL.GenBuffer();

        GL.BindVertexArray(vao);
        GL.BindBuffer(BufferTarget.ArrayBuffer, vbo);
        GL.BufferData(
            BufferTarget.ArrayBuffer,
            quad.Length * sizeof(float),
            quad,
            BufferUsage.StaticDraw
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
        
        initialized = true;
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

    public void Update() {
        float delta = Time.Delta;

        foreach (var item in managables) {
            if (item is IUpdatable u)
                u.Update();
        }
    }


    public void Draw(int screenW, int screenH)
    {
        Init();
        
        Begin(screenW, screenH);

        foreach (var item in managables)
            DrawItem(item, screenW, screenH);

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

    private void DrawItem(IManageable item, int screenW, int screenH) {
        if (item is not ISprite sprite)
            return;

        sprite.Texture.Bind();
        GL.Uniform1i(
            GL.GetUniformLocation(shd.Handle, "tex"),
            0
        );
        var size = new Vector2(
            sprite.Texture.Width * sprite.Scale.X,
            sprite.Texture.Height * sprite.Scale.Y
        );
        var originNorm = AnchorHelper.ToNormalised(sprite.Origin);
        var originOffset = originNorm * size;
        var anchorNorm = AnchorHelper.ToNormalised(sprite.Anchor);
        var anchorOffset = new Vector2(
            anchorNorm.X * screenW,
            anchorNorm.Y * screenH
        );
        var transform =
            Matrix4.CreateScale(size.X, size.Y, 1f) *
            Matrix4.CreateTranslation(-originOffset.X, -originOffset.Y, 0f) *
            Matrix4.CreateRotationZ(sprite.Rotation) *
            Matrix4.CreateTranslation(sprite.Position.X, sprite.Position.Y, 0f) *
            Matrix4.CreateTranslation(anchorOffset.X, anchorOffset.Y, 0f);
        shd.SetMatrix4("transform", transform);
		var vec4tint = new Vector4(sprite.Colour.R / 255f, 
            sprite.Colour.G / 255f, 
            sprite.Colour.B / 255f, 
            sprite.Alpha / 255f);
        shd.SetVector4("uTint", vec4tint);

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
