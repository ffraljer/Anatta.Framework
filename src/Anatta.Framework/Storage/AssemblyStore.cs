using System.Reflection;

namespace Anatta.Framework.Storage;

public class AssemblyStore : IResourceStore {
    private readonly Assembly _asm;
    private readonly string _rootNamespace;

    public AssemblyStore(Assembly asm, string rootNamespace) {
        _asm = asm;
        _rootNamespace = rootNamespace;
    }

    public Stream? Open(string path) {
        string real = $"{_rootNamespace}.{path.Replace('/', '.')}";
        return _asm.GetManifestResourceStream(real);
    }
}