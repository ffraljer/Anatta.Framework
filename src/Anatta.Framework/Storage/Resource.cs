using Anatta.Framework.Graphics;
using System.Reflection; 
using Anatta.Framework.Storage.Loaders;

namespace Anatta.Framework.Storage {
    public static class Resource
    {
        private static readonly List<IResourceStore> Stores = new();
        private static AssetCache Cache = new();
        private static readonly List<IAssetLoader> Loaders = new();
        
        static Resource()
        {
            RegisterLoader(new TextureLoader());
            RegisterLoader(new SampleLoader());
            RegisterLoader(new TrackLoader());
            RegisterLoader(new FontLoader());
        }
        
        public static void RegisterLoader(IAssetLoader loader) {
            if (!Loaders.Contains(loader))
                Loaders.Add(loader);
        }
        
        public static void AddStore(IResourceStore store) {
            if (!Stores.Contains(store))
                Stores.Add(store);
        }
        private static IAssetLoader FindLoader(Type type) {
            var loader = Loaders.FirstOrDefault(l => l.CanLoad(type));

            if (loader == null)
                throw new NotSupportedException(type.Name);

            return loader;
        }
        public static T Load<T>(string name, IAssetLoader? assetLoader = null)
        {
            var loader = assetLoader ?? FindLoader(typeof(T));

            string fullName = $"{loader.Folder}/{name}";

            return Cache.GetOrLoad<T>(fullName, () => {
                byte[] bytes = Read(fullName);
                return (T)loader.Load(name, bytes);
            });
        }
        private static byte[] Read(string fullName)
        {
            Stream? stream = null;

            foreach (var store in Stores)
            {
                stream = store.Open(fullName);

                if (stream != null)
                    break;
            }

            if (stream == null)
                throw new FileNotFoundException(fullName);

            using var ms = new MemoryStream();

            stream.CopyTo(ms);

            return ms.ToArray();
        }
        internal static T LoadInternal<T>(string name) {
            string fullName = $"Anatta.Framework.Resources.{name}";
            Stream? stream = null;
            var asm = Assembly.GetExecutingAssembly();
            stream = asm.GetManifestResourceStream(fullName);

            if (stream == null)
                throw new Exception($"Embedded resource not found: {fullName}");

            using var ms = new MemoryStream();
            stream.CopyTo(ms);
            var bytes = ms.ToArray();
            if (typeof(T) == typeof(byte[])) {
                return (T)(object)bytes;
            }
            if (typeof(T) == typeof(FontFace)) {
                return (T)(object)new FontFace(bytes);
            }
            throw new NotSupportedException(typeof(T).Name);
        }

        public static (string Vertex, string Fragment) LoadShaderSource(string name) {
            foreach (var store in Stores) {
                using var vr = store.Open($"Shaders/{name}.avs");
                using var fr = store.Open($"Shaders/{name}.afs");

                if (vr != null && fr != null) {
                    using var vreader = new StreamReader(vr);
                    using var freader = new StreamReader(fr);
                    return (vreader.ReadToEnd(), freader.ReadToEnd());
                }
            }
            throw new FileNotFoundException($"Shader not found: {name}");
        }
    }
}