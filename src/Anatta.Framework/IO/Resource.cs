using System.Reflection;
using Anatta.Framework.Graphics;
using Anatta.Framework.Sound;

namespace Anatta.Framework.IO {
    public class Resource
    {
        private static readonly List<IResourceStore> stores = new();
        private readonly string rootNamespace;
        public static void AddStore(IResourceStore store) {
            if (!stores.Contains(store))
                stores.Add(store);
        }
        public static T Load<T>(string name)
        {
            string folder = GetTypeFolder(typeof(T));
            string fullName = $"{folder}/{name}";
            Stream? stream = null;

            foreach (var store in stores) {
                stream = store.Open(fullName);
                if (stream != null)
                    break;
            }

            if (stream == null)
                throw new Exception($"Embedded resource not found: {fullName}");

            using var ms = new MemoryStream();
            stream.CopyTo(ms);
            var bytes = ms.ToArray();

            if (typeof(T) == typeof(Texture))
            {
                if (name.EndsWith(".xnb", StringComparison.OrdinalIgnoreCase))
                {
                    Texture text = LoadXnbTexture(bytes);
                    return (T)(object)text;
                }
                
                Texture tex = Texture.FromImageBytes(bytes);
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
                return (T)(object)new Track(bytes);
            }
            if (typeof(T) == typeof(FontFace)) {
                return (T)(object)new FontFace(bytes);
            }
            if (typeof(T) == typeof(Shader)) {
                foreach (var store in stores) {
                    using var vr = store.Open($"Shaders/{name}.glsl");
                    using var fr = store.Open($"Shaders/{name}Fragment.glsl");

                    if (vr != null && fr != null) {
                        using var vreader = new StreamReader(vr);
                        using var freader = new StreamReader(fr);

                        return (T)(object)new Shader(
                            vreader.ReadToEnd(),
                            freader.ReadToEnd());
                    }
                }

                throw new FileNotFoundException($"Shader not found: {name}");
            }
            throw new NotSupportedException(typeof(T).Name);
        }
        public static T LoadInternal<T>(string name) {
            string folder = GetTypeFolder(typeof(T));
            string fullName = $"Anatta.Framework.Resources.{name}";
            Stream? stream = null;
            var asm = Assembly.GetExecutingAssembly();
            stream = asm.GetManifestResourceStream(fullName);

            if (stream == null)
                throw new Exception($"Embedded resource not found: {fullName}");

            using var ms = new MemoryStream();
            stream.CopyTo(ms);
            var bytes = ms.ToArray();

            if (typeof(T) == typeof(Texture)) {
                Texture tex = Texture.FromImageBytes(bytes);
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
        public static Shader LoadShader(string name) {
            foreach (var store in stores) {
                using var vr = store.Open($"Shaders/{name}.glsl");
                using var fr = store.Open($"Shaders/{name}Fragment.glsl");

                if (vr != null && fr != null)
                {
                    using var vreader = new StreamReader(vr);
                    using var freader = new StreamReader(fr);

                    return new Shader(
                        vreader.ReadToEnd(),
                        freader.ReadToEnd());
                }
            }

            throw new FileNotFoundException($"Shader not found: {name}");
        }
        private static Texture LoadXnbTexture(byte[] data)
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
        private static string GetTypeFolder(Type t)
        {
            if (t == typeof(Texture)) return "Textures";
            if (t == typeof(byte[])) return "Native";
            if (t == typeof(Sample)) return "Audio.Samples";
            if (t == typeof(Track)) return "Audio.Tracks";
            if (t == typeof(FontFace)) return "Fonts";
            if (t == typeof(Shader)) return "Shaders";
            return "Unknown";
        }
    }
}