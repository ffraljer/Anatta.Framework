using System.Reflection;
using Anatta.Framework.Graphics;

namespace Anatta.Framework.IO {
    public class Resource
    {
        public static string Base { get; private set; }
        private static readonly List<Assembly> stores = new();

        public static void Init(string basename)
        {
            Base = basename;
        }
        // not sure if this actually works, but here it is
        public static void AddStore(Assembly assembly) {
            if (!stores.Contains(assembly))
                stores.Add(assembly);
        }
        public static T Load<T>(string name)
        {
            if (string.IsNullOrEmpty(Base))
                throw new InvalidOperationException("Resource.Base not set.");
            string folder = GetTypeFolder(typeof(T));
            string fullName = $"{Base}.Resources.{folder}.{name}";
            Stream? stream = null;

            foreach (var asm in stores) {
                stream = asm.GetManifestResourceStream(fullName);
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
                Texture tex = Texture.FromImageBytes(bytes);
                return (T)(object)tex;
            }
            if (typeof(T) == typeof(byte[]))
            {
                return (T)(object)bytes;
            }
            if (typeof(T) == typeof(Shader)) {
                foreach (var asm in stores) {
                    var vr = asm.GetManifestResourceStream(
                        $"{Base}.Resources.Shaders.{name}.glsl");
                    var fr = asm.GetManifestResourceStream(
                        $"{Base}.Resources.Shaders.{name}Fragment.glsl");

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
            if (typeof(T) == typeof(FontFace)) {
                return (T)(object)new FontFace(bytes);
            }
            throw new NotSupportedException(typeof(T).Name);
        }
        public static Shader LoadShader(string name) {
            foreach (var asm in stores) {
                using var vr = asm.GetManifestResourceStream(
                    $"{Base}.Resources.Shaders.{name}.glsl");
                using var fr = asm.GetManifestResourceStream(
                    $"{Base}.Resources.Shaders.{name}Fragment.glsl");

                if (vr != null && fr != null) {
                    using var vreader = new StreamReader(vr);
                    using var freader = new StreamReader(fr);
                    return new Shader(
                        vreader.ReadToEnd(),
                        freader.ReadToEnd());
                }
            }

            throw new FileNotFoundException($"Shader not found: {name}");
        }

        private static string GetTypeFolder(Type t)
        {
            if (t == typeof(Texture)) return "Textures";
            if (t == typeof(byte[])) return "Audio";
            if (t == typeof(FontFace)) return "Fonts";
            if (t == typeof(Shader)) return "Shaders";
            return "Unknown";
        }
    }
}