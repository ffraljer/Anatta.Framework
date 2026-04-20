using System.Data;
using System.Reflection;
using Anatta.Framework.Graphics;
using Anatta.Framework.Graphics.Interfaces;
using Anatta.Framework.Sound;

namespace Anatta.Framework.IO {
    public static class Resource
    {
        private static readonly List<IResourceStore> Stores = new();
        private static readonly string RootNamespace;
        private static readonly Dictionary<string, object> Cache = new();
        private static string[] _names = { ".png", ".jpeg", ".jpg", ".xnb" };
        
        
        public static void AddStore(IResourceStore store) {
            if (!Stores.Contains(store))
                Stores.Add(store);
        }
        public static T Load<T>(string name)
        {
            string folder = GetTypeFolder(typeof(T));
            string fullName = $"{folder}/{name}";
            if (Cache.TryGetValue(fullName, out var cached))
                return (T)cached;
            Stream? stream = null;

            foreach (var store in Stores) {
                stream = store.Open(fullName);
                if (stream != null)
                    break;
            }

            if (stream == null)
                throw new Exception($"Embedded resource not found: {fullName}");

            using var ms = new MemoryStream();
            stream.CopyTo(ms);
            var bytes = ms.ToArray();

            if (typeof(T) == typeof(ITexture) || typeof(T) == typeof(Texture))
            {
                if (name.EndsWith(".xnb", StringComparison.OrdinalIgnoreCase))
                {
                    ITexture text = LoadXnbTexture(bytes);
                    return (T)(object)text;
                }
                
                var tex = new Texture(bytes);
                return (T)(object)tex;
            }
            if (typeof(T) == typeof(byte[]))
            {
                return (T)(object)bytes;
            }
            if (typeof(T) == typeof(Sample))
            {
                return (T)(object)new Sample(bytes);
            }
            if (typeof(T) == typeof(Track))
            {
                var track = new Track(bytes);
                Cache[fullName] = track;
                return (T)(object)track;
            }
            if (typeof(T) == typeof(FontFace)) {
                return (T)(object)new FontFace(bytes);
            }
            if (typeof(T) == typeof(ShaderGL)) {
                foreach (var store in Stores) {
                    using var vr = store.Open($"Shaders/{name}.avs");
                    using var fr = store.Open($"Shaders/{name}.afs");

                    if (vr != null && fr != null) {
                        using var vreader = new StreamReader(vr);
                        using var freader = new StreamReader(fr);

                        return (T)(object)new ShaderGL(
                            vreader.ReadToEnd(),
                            freader.ReadToEnd());
                    }
                }

                throw new FileNotFoundException($"Shader not found: {name}");
            }
            throw new NotSupportedException(typeof(T).Name);
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

            if (typeof(T) == typeof(ITexture) || typeof(T) == typeof(Texture)) {
                TextureGL tex = TextureGL.SetData(bytes);
                return (T)(object)tex;
            }
            if (typeof(T) == typeof(byte[])) {
                return (T)(object)bytes;
            }
            if (typeof(T) == typeof(Sample))
            {
                return (T)(object)new Sample(bytes);
            }
            if (typeof(T) == typeof(Track))
            {
                return (T)(object)new Track(bytes);
            }
            if (typeof(T) == typeof(FontFace)) {
                return (T)(object)new FontFace(bytes);
            }
            throw new NotSupportedException(typeof(T).Name);
        }
        public static ShaderGL LoadShader(string name) {
            foreach (var store in Stores) {
                using var vr = store.Open($"Shaders/{name}.glsl");
                using var fr = store.Open($"Shaders/{name}Fragment.glsl");

                if (vr != null && fr != null)
                {
                    using var vreader = new StreamReader(vr);
                    using var freader = new StreamReader(fr);

                    return new ShaderGL(
                        vreader.ReadToEnd(),
                        freader.ReadToEnd());
                }
            }

            throw new FileNotFoundException($"Shader not found: {name}");
        }
        private static ITexture LoadXnbTexture(byte[] data)
        {
            using var ms = new MemoryStream(data);
            using var br = new BinaryReader(ms);
            if (new string(br.ReadChars(3)) != "XNB")
                throw new Exception("invalid xnb");
            char platform = br.ReadChar();
            byte version = br.ReadByte();
            byte flags = br.ReadByte();
            int size = br.ReadInt32();
            bool compressed = (flags & 0x80) != 0;
            if (compressed)
                throw new NotSupportedException("compressed xnb not supported");
            int readerCount = br.Read7BitEncodedInt();
            for (int i = 0; i < readerCount; i++)
            {
                br.ReadString();
                br.ReadInt32();
            }
            br.Read7BitEncodedInt();
            br.Read7BitEncodedInt();
            int format = br.ReadInt32();
            int width = br.ReadInt32();
            int height = br.ReadInt32();
            int mipCount = br.ReadInt32();
            int dataSize = br.ReadInt32();
            byte[] pixelData = br.ReadBytes(dataSize);
            for (int i = 0; i < pixelData.Length; i += 4)
            {
                (pixelData[i], pixelData[i + 2]) = (pixelData[i + 2], pixelData[i]);
            }
                
            return new Texture(width, height, pixelData);
        }
        public static Texture LoadSpriteInternal(string name) {
            foreach (var ext in _names) {
                string resourceName = name + ext;
                try {
                    return Load<Texture>(resourceName);
                }
                catch {
                    throw;
                }
            }
            throw new FileNotFoundException($"{name} not found");
        }
        private static string GetTypeFolder(Type t)
        {
            if (t == typeof(Texture)) return "Textures";
            if (t == typeof(byte[])) return "Native";
            if (t == typeof(Sample)) return "Audio.Samples";
            if (t == typeof(Track)) return "Audio.Tracks";
            if (t == typeof(FontFace)) return "Fonts";
            if (t == typeof(ShaderGL)) return "Shaders";
            return "Unknown";
        }
    }
}