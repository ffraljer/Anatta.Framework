namespace Anatta.Framework.Storage;

public interface IResourceStore {
    Stream? Open(string path);
}