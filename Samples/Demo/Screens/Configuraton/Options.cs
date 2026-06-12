using Anatta.Framework;
using Anatta.Framework.Configuration;
using Anatta.Framework.Graphics.Sprites;

namespace Demo.Screens;

public class Options : Screen {
    private Checkbox checkboxRenderer;
   
    protected override void Load() {
        checkboxRenderer = new("OpenGL") {
        };
        checkboxRenderer.OnCheckChange += delegate(bool status) {
            FrameworkConfig.sRenderer.Value = Renderer.GL;
        };
        Add(checkboxRenderer);
    }
    public override void Update() {
        base.Update();
    }
}