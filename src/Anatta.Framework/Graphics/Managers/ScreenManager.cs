namespace Anatta.Framework.Graphics.Managers;

public class ScreenManager {
    
    private readonly Stack<Screen> _screens = new();
    public Screen? Current => _screens.Count > 0 ? _screens.Peek() : null;

    public void Push(Screen screen) {

        var s = screen;
        var s2 = _screens;
        if (s == null)
            throw new ArgumentNullException(nameof(s));

        if (s2.Count > 0)
            _screens.Peek().OnExit();

        s2.Push(s);
        s.OnEnter();
        s.Load();
    }
    public void Pop() { if (_screens.Count == 0) return; var s = _screens.Pop(); s.OnExit(); s.Dispose(); if (_screens.Count > 0) _screens.Peek().OnEnter(); } // LOL
    // long ass line
    public void Update() { _screens.Peek().Update(); }

    public void Draw(int w, int h) {
        _screens.Peek().Draw(w, h);
    }

    public void Dispose() {
        while (_screens.Count > 0) {
            _screens.Pop().Dispose();
        }
    }
}