using Anatta.Framework.Graphics;

namespace Anatta.Framework.Storage.Loaders;

public class FontLoader : IAssetLoader {
    public string Folder => "Fonts";
    
    public bool CanLoad(Type type) {
        return type == typeof(FontFace);
    }

    public object Load(string name, byte[] bytes) {
        return new FontFace(bytes);
    }
}