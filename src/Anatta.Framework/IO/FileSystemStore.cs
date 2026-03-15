namespace Anatta.Framework.IO;

public class FileSystemStore {
    private readonly string _rooot;

    public FileSystemStore(string rooot) {
        _rooot = rooot;
    }

    public Stream? Open(string path) {
        string full = Path.Combine(_rooot, path.Replace('.', Path.DirectorySeparatorChar));

        if (!File.Exists(full))
            return null;

        return File.OpenRead(full);
    }
}