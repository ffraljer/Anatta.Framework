namespace Anatta.Framework.IO;

public interface IResourceStore {
    Stream? Open(string path);
}