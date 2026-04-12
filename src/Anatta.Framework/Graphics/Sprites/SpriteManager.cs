using Anatta.Framework.Graphics.Helpers;
using Anatta.Framework.Graphics.Shapes;
using Anatta.Framework.Graphics.Sprites;
using Anatta.Framework.Input;
using Anatta.Framework.Graphics.Interfaces;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;

namespace Anatta.Framework.Graphics.Sprites;

public class SpriteManager : IDisposable {
    private readonly List<IManageable> _managables = new();
    public static Vector2i ScreenSize { get; set; }
    private int _vao;
    private int _vbo;
    private Shader _shd;
    
    private bool _wasMouseDown;
    
    private bool _initialized;

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
    private void Init()
    {
        if (_initialized) return;
        
        _vao = GL.GenVertexArray();
        _vbo = GL.GenBuffer();

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

        _shd = Shader.Load("sprite.avs", "sprite.afs");

        GL.BindVertexArray(0);
        GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
        
        _initialized = true;
    }

    public IEnumerable<IManageable> GetAll() => _managables;

    public void Add(IManageable managed) {
        if (managed == null)
            throw new ArgumentNullException(nameof(managed));

        _managables.Add(managed);
    }

    public void AddRange(params IManageable[] managedItems) {
        if (managedItems == null) throw new ArgumentNullException(nameof(managedItems));
        if (managedItems.Length == 0) return;

        for (int i = 0; i < managedItems.Length; i++) {
            _managables.Add(managedItems[i]);
        }
    }

    public void Remove(IManageable managed) {
        if (managed == null) throw new ArgumentNullException(nameof(managed));
        _managables.Remove(managed);
    }
    private void UpdateItem(IManageable item) {
        if (item is Container container)
            container.EnsureLoaded();

        if (item is IUpdatable u)
            u.Update();

        if (item is Drawable sprite && item is not Container)
            HandleInput(sprite);

        if (item is Container c)
        {
            foreach (var child in c.Children)
                UpdateItem(child);
        }
    }
    #region INPUT
    private void HandleInput(Drawable sprite)
    {
        var mousePos = new Vector2(Mouse.X, Mouse.Y);

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
            if (!sprite.IsHovering)
            {
                sprite.IsHovering = true;
                sprite.TriggerHover();
            }

            bool isDown = Mouse.IsButtonPressed(Mouse.Button.Left);

            if (isDown && !_wasMouseDown)
                sprite.TriggerClick();

            _wasMouseDown = isDown;
        }
        else
        {
            if (sprite.IsHovering)
            {
                sprite.IsHovering = false;
                sprite.TriggerHoverLost();
            }
        }
    }
    #endregion
    public void Update() {
        foreach (var item in _managables.AsEnumerable().Reverse())
            UpdateItem(item);
    }


    public void Draw() {
        var screenH = ScreenSize.Y;
        var screenW = ScreenSize.X;
        Init();
        
        Begin(screenW, screenH);

        foreach (var item in _managables)
            _Draw(item, screenW, screenH);

        End();
    }
    private void _Draw(IManageable item, int screenW, int screenH)
    {
        if (item is Container container) {
            foreach (var child in container.Children)
                _Draw(child, screenW, screenH);
        }
        else if (item is Drawable drawable) {
            DrawItem(drawable, screenW, screenH);
        }
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
        
        Vector2 size;
        
        if (item is Box box) {
            size = box.Size * box.Scale;
            Vector4 top = box.Colour.Top.ToVector4();
            Vector4 bottom = box.Colour.Bottom.ToVector4();
            
            _shd.SetVector4("uTintTop", top);
            _shd.SetVector4("uTintBottom", bottom);

            _shd.SetVector2("uSize", size);
            _shd.SetFloat("uRadius", box.CornerRadius);
            
            _shd.SetFloat("uCircleRadius", 0f);
            _shd.SetFloat("uCircleThickness", 0f); 
            _shd.SetVector4("uBorderColour", new Vector4(0f));
            Texture.WhitePixel.Bind();
            GL.Uniform1i(GL.GetUniformLocation(_shd.Handle, "tex"), 0);
            
        }
        else if (item is Circle circle)
        { 
            size = new Vector2(circle.Radius * 2f * circle.Scale.X, circle.Radius * 2f * circle.Scale.Y);
            Vector4 top = circle.FillColor.Top.ToVector4();
            Vector4 bottom = circle.FillColor.Bottom.ToVector4();
            Vector4 topp = circle.BorderColor.Top.ToVector4();
            Vector4 botmm = circle.BorderColor.Bottom.ToVector4();

            _shd.SetVector4("uTintTop", top);
            _shd.SetVector4("uTintBottom", bottom);
            _shd.SetVector4("uTint", top);
            _shd.SetVector4("uBorderTop", topp);
            _shd.SetVector4("uBorderBottom", botmm);
            _shd.SetVector2("uSize", size);
            _shd.SetFloat("uCircleRadius", circle.Radius * MathF.Max(circle.Scale.X, circle.Scale.Y));
            _shd.SetFloat("uCircleThickness", circle.Thickness * MathF.Max(circle.Scale.X, circle.Scale.Y));

            Texture.WhitePixel.Bind();
            GL.Uniform1i(GL.GetUniformLocation(_shd.Handle, "tex"), 0);
        }
        else if (sprite.Texture != null) {
            size = new Vector2(sprite.Texture.Width * sprite.Scale.X, sprite.Texture.Height * sprite.Scale.Y);

            sprite.Texture.Bind();
            GL.Uniform1i(GL.GetUniformLocation(_shd.Handle, "tex"), 0);
            Vector4 tint = sprite.Colour.Top.ToVector4();
            _shd.SetVector4("uTintTop", tint);
            _shd.SetVector4("uTintBottom", tint);
            _shd.SetVector4("uTint", tint);

            _shd.SetFloat("uCircleRadius", 0f);
            _shd.SetFloat("uCircleThickness", 0f);
            _shd.SetVector4("uBorderColour", new Vector4(0f));
        }
        else {
            return;
        }
        
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
            Matrix4.CreateTranslation(sprite.DrawPosition.X, sprite.DrawPosition.Y, 0f) *
            Matrix4.CreateTranslation(anchorOffset.X, anchorOffset.Y, 0f);
        _shd.SetMatrix4("transform", transform);

        GL.DrawArrays(PrimitiveType.Triangles, 0, 6);
    }
    public void InvalidateInput() {
        _wasMouseDown = false;

        foreach (var item in _managables)
            InvalidateInput(item);
    }
    private void InvalidateInput(IManageable item) {
        if (item is Drawable drawable) {
            if (drawable.IsHovering) {
                drawable.IsHovering = false;
                drawable.TriggerHoverLost();
            }
        }

        if (item is Container container) {
            foreach (var child in container.Children)
                InvalidateInput(child);
        }
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
