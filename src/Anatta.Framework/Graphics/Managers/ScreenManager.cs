using OpenTK.Windowing.GraphicsLibraryFramework;

namespace Anatta.Framework.Graphics.Managers;

public class ScreenManager {
    
    private readonly Stack<Screen> screens = new();
    public Screen? Current => screens.Count > 0 ? screens.Peek() : null;

    public void Push(Screen screen) {

        var s = screen;
        var s2 = screens;
        if (s == null)
            throw new ArgumentNullException(nameof(s));

        if (s2.Count > 0)
            screens.Peek().OnExit();

        s2.Push(s);
        s.OnEnter();
        s.Load();
    }
    public void Pop() { if (screens.Count == 0) return; var s = screens.Pop(); s.OnExit(); s.Dispose(); if (screens.Count > 0) screens.Peek().OnEnter(); } // LOL
    // long ass line
    public void Update() { screens.Peek().Update(); }

    public void Draw(int w, int h) {
        screens.Peek().Draw(w, h);
    }

    public void Dispose() {
        while (screens.Count > 0) {
            screens.Pop().Dispose();
        }
    }
}