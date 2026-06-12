namespace Anatta.Framework {
    public enum Renderer {
        GL
    }
}

namespace Anatta.Framework.Configuration {
    public class FrameworkConfig : ConfigurationManager {
        [ConfigKey("Renderer", Renderer.GL)] // some A++ bullshit right here
        public static Bindable<Renderer> sRenderer = null!;
        
        public FrameworkConfig() : base("framework.json") {
            Initialize();
        }
    }
}

