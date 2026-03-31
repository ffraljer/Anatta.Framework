namespace Anatta.Framework {
    public enum Renderer {
        GL, // It only allows for input in the Demo right now, but I WILL try to implement Vulkan.
        DX
    }
}

namespace Anatta.Framework.Configuration {
    public class FrameworkConfig : ConfigurationManager {
        [ConfigKey("Renderer", Renderer.GL)]
        public Bindable<Renderer> GraphicsRenderer;
        
        public FrameworkConfig() : base("framework.json") {
            Initialize();
        }
    }
}

