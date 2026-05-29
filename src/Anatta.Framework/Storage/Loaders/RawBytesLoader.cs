namespace Anatta.Framework.Storage.Loaders;

public class RawBytesLoader(string folder) : IAssetLoader {
    // do it yourself
    public string Folder { get; } = folder;

    public bool CanLoad(Type type) {
        return type == typeof(byte[]);
    }

    public object Load(string name, byte[] data) {
        return data;
    }
}