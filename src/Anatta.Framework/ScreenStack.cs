using Anatta.Framework.Graphics.Sprites;

namespace Anatta.Framework;

public class ScreenStack {
    
    private readonly Stack<Screen> _screens = new();
    public Screen? Current => _screens.Count > 0 ? _screens.Peek() : null;
    
    private readonly SpriteManager _spriteManager;

    public ScreenStack(SpriteManager spriteManager) {
        _spriteManager = spriteManager ?? throw new ArgumentNullException(nameof(spriteManager));
    }
    
    public void Push(Screen screen) {
        if (screen == null)
            throw new ArgumentNullException(nameof(screen));
        
            if (_screens.Count > 0) {
                var old = _screens.Pop();
                old.OnExit();
                old.Dispose();
            }

            _screens.Push(screen);
            _spriteManager.Add(screen);
            screen.OnEnter();
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