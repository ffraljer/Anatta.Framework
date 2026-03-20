using Anatta.Framework.Graphics;
using Anatta.Framework.Graphics.Helpers;
using Anatta.Framework.Input;
using Anatta.Framework.Interfaces.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace Anatta.Framework.Graphics.Managers;

public class Manager : IDisposable {
    private readonly List<IManageable> _managables = new();
    public static Vector2i ScreenSize { get; set; }
    private int _vao;
    private int _vbo;
    private Shader _shd;
    
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
        
        _vao = GL.GenVertexArray();
        _vbo = GL.GenBuffer();

        GL.BindVertexArray(_vao);
        GL.BindBuffer(BufferTarget.ArrayBuffer, _vbo);
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

        _shd = Shader.Load("vertexSprite.glsl", "fragmentSprite.glsl");

        GL.BindVertexArray(0);
        GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
        
        initialized = true;
    }

    public IEnumerable<IManageable> GetAll() => _managables;

    public void Add(IManageable managed) {
        if (managed == null)
            throw new ArgumentNullException(nameof(managed));

        _managables.Add(managed);
    }

    public void AddRange(params IManageable[] managedItems) {
        if (managedItems == null)
            return;

        foreach (var m in managedItems) {
            if (m != null)
                _managables.Add(m);
        }
    }

    public void Remove(IManageable managed) {
        if (managed == null) return;
        _managables.Remove(managed);
    }

    public void Update() {
        float delta = Time.Delta;

        var mousePos = new Vector2(Mouse.X, Mouse.Y);
        
        foreach (var item in _managables) {
            if (item is IUpdatable u)
                u.Update();
            
            if (item is BaseSprite sprite)
            {
                Vector2 size = sprite.GetSize() * sprite.Scale;

                Vector2 originNorm = AnchorHelper.ToNormalised(sprite.Origin);
                Vector2 originOffset = originNorm * size;

                Vector2 anchorNorm = AnchorHelper.ToNormalised(sprite.Anchor);
                Vector2 anchorOffset = new Vector2(
                    anchorNorm.X * ScreenSize.X,
                    anchorNorm.Y * ScreenSize.Y
                );

                Vector2 pos = sprite.Position - originOffset + anchorOffset;

                bool hovering = mousePos.X >= pos.X &&
                                mousePos.X <= pos.X + size.X &&
                                mousePos.Y >= pos.Y &&
                                mousePos.Y <= pos.Y + size.Y;

                if (hovering)
                {
                    if (!sprite._isHovering)
                    {
                        sprite._isHovering = true;
                        sprite.TriggerHover();
                    }

                    if (Mouse.IsButtonPressed(Mouse.Button.Left))
                        sprite.TriggerClick();
                }
                else
                {
                    if (sprite._isHovering)
                    {
                        sprite._isHovering = false;
                        sprite.TriggerHoverLost();
                    }
                }
            }
        }
    }


    public void Draw(int screenW, int screenH)
    {
        Init();
        
        Begin(screenW, screenH);

        foreach (var item in _managables)
            DrawItem(item, screenW, screenH);

        End();
    }

    private void Begin(int screenW, int screenH) {
        _shd.Use();

        var projection = Matrix4.CreateOrthographicOffCenter(
            0, screenW,
            screenH, 0,
            -1, 1
        );

        _shd.SetMatrix4("uProjection", projection);

        GL.ActiveTexture(TextureUnit.Texture0);
        GL.BindVertexArray(_vao);

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
            GL.GetUniformLocation(_shd.Handle, "tex"),
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
        _shd.SetMatrix4("transform", transform);
        _shd.SetVector4("uTint", sprite.Colour.ToVector4());

        GL.DrawArrays(PrimitiveType.Triangles, 0, 6);
    }

    private void End() {
        GL.BindVertexArray(0);
    }

    public void Dispose() {
        foreach (var item in _managables) {
            if (item is IDisposable d)
                d.Dispose();
        }

        _managables.Clear();

        GL.DeleteBuffer(_vbo);
        GL.DeleteVertexArray(_vao);

        _shd.Dispose();
    }
}
