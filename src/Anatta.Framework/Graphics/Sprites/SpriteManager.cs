using Anatta.Framework.Configuration;
using Anatta.Framework.Input;
using Anatta.Framework.Graphics.D3D;
using Anatta.Framework.Graphics.Helpers;
using Anatta.Framework.Graphics.Interfaces;
using Anatta.Framework.Graphics.Renderers;
using Anatta.Framework.Graphics.Rendering;
using OpenTK.Mathematics;

namespace Anatta.Framework.Graphics.Sprites;

public class SpriteManager : IDisposable {
    private readonly List<IManageable> _managables = new();
    public static Vector2i ScreenSize { get; set;  }

    private readonly Batcher _batcher = new();
    
    internal IRenderer _renderer;
    
    private bool _wasMouseDown;
    public IEnumerable<IManageable> GetAll() => _managables;

    public SpriteManager() {
        if (FrameworkConfig.sRenderer == Renderer.GL)
                _renderer = new SpriteRendererGL();
    }

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
    private void HandleInput(Drawable sprite) {
        if (!sprite.HandleInput) return;
        
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
        foreach (var item in _managables.AsEnumerable().Reverse().OrderByDescending(i => i is Drawable d ? d.Depth : 0f))
            UpdateItem(item);
    }
    private void EnsureRenderer() {
        if (_renderer != null) return;
        if (FrameworkConfig.sRenderer == Renderer.GL)
            _renderer = new SpriteRendererGL();
    }
#if win
    private void EnsureD3D() {
        if (_renderer != null) return;

        if (FrameworkConfig.sRenderer == Renderer.GL)
            _renderer = new SpriteRendererGL();

        else if (FrameworkConfig.sRenderer == Renderer.D3D)
        {
            if (D3DController.Device == null)
                throw new Exception("D3D not initialized yet.");

            _renderer = new SpriteRendererD3D(
                D3DController.Device,
                D3DController.Context
            );
        }
    }
    #endif
    public void Draw() {
#if win
        EnsureD3D();
#else
    EnsureRenderer();
#endif
        var screenH = ScreenSize.Y;
        var screenW = ScreenSize.X;
        
        
        _renderer.Init();
        
        _renderer.Use(screenW, screenH);


        foreach (var item in _managables.OrderBy(i => i is Drawable d ? d.Depth : 0f))
            _Draw(item);

        _renderer.Kill();
    }
    private void _Draw(IManageable item)
    {
        if (item is Container container) {
            foreach (var child in container.Children)
                _Draw(child);
        }
        else if (item is Drawable drawable) {
            var cmd = drawable.BuildRenderCommand(ScreenSize);
            _batcher.Add(cmd);
            _renderer.Submit(cmd);
        }
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

    public void Dispose() {
        foreach (var item in _managables) {
            if (item is IDisposable d)
                d.Dispose();
        }

        _managables.Clear();
        
        _renderer.Dispose();
    }
}
