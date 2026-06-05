using Anatta.Framework.Sound;

namespace Anatta.Framework.Storage.Loaders;

public class SampleLoader : IAssetLoader {
    public string Folder => "Audio/Samples";

    public bool CanLoad(Type type) {
        return type == typeof(Sample);
    }

    public object Load(string name, byte[] bytes) {
        return new Sample(bytes);
    }
}

public class TrackLoader : IAssetLoader {
    public string Folder => "Audio/Tracks";
    
    public bool CanLoad(Type type) {
        return type == typeof(Track);
    }

    public object Load(string name, byte[] bytes) {
        return new Track(bytes);
    }
}