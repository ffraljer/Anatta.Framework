namespace Anatta.Framework {
    public enum Renderer {
        GL,
        D3D
    }
}

namespace Anatta.Framework.Configuration {
    public class FrameworkConfig : ConfigurationManager {
        [ConfigKey("Renderer", Renderer.GL)]
        public static Bindable<Renderer> sRenderer;
        
        public FrameworkConfig() : base("framework.json") {
            Initialize();
        }
    }
}

