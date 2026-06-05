using System.Collections.Concurrent;

namespace Anatta.Framework.Storage;

public class AssetCache {
    private readonly ConcurrentDictionary<string, Lazy<object>> _cache = new();

    public bool TryGet<T>(string key, out T asset) {
        if (_cache.TryGetValue(key, out var lazy)) {
            asset = (T)lazy.Value;
            return true;
        }

        asset = default!;
        return false;
    }

    public T GetOrLoad<T>(string key, Func<T> load) {
        var lazy = _cache.GetOrAdd(key, _ => new Lazy<object>(() => load()!));
        return (T)lazy.Value;
    }

    public void Clear() {
        _cache.Clear();
    }
}