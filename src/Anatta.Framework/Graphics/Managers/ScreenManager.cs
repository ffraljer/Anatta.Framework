using Anatta.Framework.Graphics.Animations;
using Anatta.Framework.Graphics.Sprites;

namespace Anatta.Framework.Graphics.Managers;

public class ScreenManager {
    
    private readonly Stack<Screen> _screens = new();
    public Screen? Current => _screens.Count > 0 ? _screens.Peek() : null;
    
    private ScreenFadeOverlay? _fadeOverlay;
    private readonly SpriteManager _spriteManager = new();
    
    private void _Fade(Screen screen, float duration = 0.5f) {
        if (_fadeOverlay == null) {
            _fadeOverlay = new ScreenFadeOverlay();
            _spriteManager.Add(_fadeOverlay);
        }

        Screen? old = _screens.Count > 0 ? _screens.Peek() : null;
        _fadeOverlay.FadeTo(255f, duration, Easing.InOutCubic)
            .Then(() => {
                if (old != null) {
                    old.OnExit();
                    old.Dispose();
                    _screens.Pop();
                }

                _screens.Push(screen);
                screen.Load();
                screen.OnEnter();

                _fadeOverlay.FadeTo(0f, duration, Easing.InOutCubic);
            });
    }
    
    public void Push(Screen screen, bool fade = true, float duration = 0.5f) {
        if (screen == null)
            throw new ArgumentNullException(nameof(screen));
        
        if (fade && _screens.Count > 0) {
            _Fade(screen, duration);
        } else {
            if (_screens.Count > 0) {
                var old = _screens.Pop();
                old.OnExit();
                old.Dispose();
            }

            _screens.Push(screen);
            screen.OnEnter();
            screen.Load();
        }
    }
    public void Pop() { if (_screens.Count == 0) return; var s = _screens.Pop(); s.OnExit(); s.Dispose(); if (_screens.Count > 0) _screens.Peek().OnEnter(); } // LOL
    // long ass line
    public void Update() {
        _screens.Peek().Update(); 
        _spriteManager.Update();
    }

    public void Draw() {
        _spriteManager.Draw();
    }

    public void Dispose() {
        while (_screens.Count > 0) {
            _screens.Pop().Dispose();
        }
    }
}