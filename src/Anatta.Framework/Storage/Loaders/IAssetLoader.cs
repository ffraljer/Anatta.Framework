namespace Anatta.Framework.Storage.Loaders;

public interface IAssetLoader {
    string Folder { get; }
    bool CanLoad(Type type);
    object Load(string name, byte[] bytes);
}