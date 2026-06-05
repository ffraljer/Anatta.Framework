namespace Anatta.Framework.Storage;

public class FileSystemStore : IResourceStore {
    private readonly string _root;

    public FileSystemStore(string root) {
        _root = root;
    }

    public Stream? Open(string path) {
        string full = Path.Combine(_root, path);

        if (!File.Exists(full))
            return null;

        return File.OpenRead(full);
    }
}