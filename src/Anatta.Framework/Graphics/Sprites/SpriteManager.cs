using Anatta.Framework.Configuration;
using Anatta.Framework.Input;
using Anatta.Framework.Graphics.D3D;
using Anatta.Framework.Graphics.Helpers;
using Anatta.Framework.Graphics.Interfaces;
using Anatta.Framework.Graphics.Renderers;
using OpenTK.Mathematics;
using Anatta.Framework.Threading;

namespace Anatta.Framework.Graphics.Sprites;

public class SpriteManager : IDisposable {
    private const double DoubleClickdel = 0.3;
    private readonly Dictionary<Drawable, double> _lastClickTime = new();
    private double _elapsedTime;

    private readonly List<IDrawable> _managables = new();
    public static Vector2i ScreenSize { get; set;  }
    
    internal IRenderer Renderer;
    
    private bool _wasMouseDown;
    public IEnumerable<IDrawable> GetAll() => _managables;

    public SpriteManager() {
        if (FrameworkConfig.sRenderer == Framework.Renderer.GL)
                Renderer = new SpriteRendererGL();
    }

    public void Add(IDrawable managed) {
        if (managed == null)
            throw new ArgumentNullException(nameof(managed));

        _managables.Add(managed);
    }

    public void AddRange(params IDrawable[] managedItems) {
        if (managedItems == null) throw new ArgumentNullException(nameof(managedItems));
        if (managedItems.Length == 0) return;

        foreach (var t in managedItems) {
            _managables.Add(t);
        }
    }

    public void Remove(IDrawable managed) {
        if (managed == null) throw new ArgumentNullException(nameof(managed));
        _managables.Remove(managed);
    }
    private void UpdateItem(IDrawable item) {
        if (item is CompositeDrawable container)
            container.EnsureLoaded();

        if (item is IUpdatable u)
            u.Update();

        if (item is Drawable sprite)
            HandleInput(sprite);

        if (item is CompositeDrawable c)
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

            if (isDown && !_wasMouseDown) {
                if (_lastClickTime.TryGetValue(sprite, out double lastTime) &&
                    (_elapsedTime - lastTime) <= DoubleClickdel) {
                    sprite.TriggerDoubleClick();
                    _lastClickTime.Remove(sprite);
                }
                else {
                    sprite.TriggerClick();
                    _lastClickTime[sprite] = _elapsedTime;
                }
            }

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
        foreach (var key in _lastClickTime.Keys
            .Where(k => _elapsedTime - _lastClickTime[k] > DoubleClickdel)
            .ToList())
            _lastClickTime.Remove(key);

        _elapsedTime += Time.Delta;
        foreach (var item in _managables.AsEnumerable().Reverse().OrderByDescending(i => i is Drawable d ? d.Depth : 0f))
            UpdateItem(item);
    }
    private void EnsureRenderer() {
        if (Renderer != null) return;

        if (FrameworkConfig.sRenderer == Framework.Renderer.GL)
            Renderer = new SpriteRendererGL();

#if win
        else if (FrameworkConfig.sRenderer == Framework.Renderer.D3D)
        {
            if (D3DController.Device == null)
                throw new Exception("D3D not initialized yet.");

            Renderer = new SpriteRendererD3D(
                D3DController.Device,
                D3DController.Context
            );
        }
    #endif
    }
    public void Draw() {
#if win
        EnsureRenderer();
#endif
        var screenH = ScreenSize.Y;
        var screenW = ScreenSize.X;
        
        
        Renderer.Init();
        
        Renderer.Use(screenW, screenH);


        foreach (var item in _managables.OrderBy(i => i is Drawable d ? d.Depth : 0f))
            _Draw(item);

        Renderer.Kill();
    }
    private void _Draw(IDrawable item)
    {
        if (item is CompositeDrawable container) {
            foreach (var child in container.Children)
                _Draw(child);
        }
        else if (item is Drawable drawable) {
            var cmd = drawable.BuildRenderCommand(ScreenSize);
            Renderer.Submit(cmd);
        }
    }
    
    public void InvalidateInput() {
        _wasMouseDown = false;

        foreach (var item in _managables)
            InvalidateInput(item);
    }
    private void InvalidateInput(IDrawable item) {
        if (item is Drawable drawable) {
            if (drawable.IsHovering) {
                drawable.IsHovering = false;
                drawable.TriggerHoverLost();
            }
        }

        if (item is CompositeDrawable container) {
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
        
        Renderer.Dispose();
    }
}
