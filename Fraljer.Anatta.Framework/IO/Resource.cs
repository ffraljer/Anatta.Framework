using System.Reflection;
using Demo.Resources;
using Fraljer.Anatta.Framework.Graphics;

namespace Fraljer.Anatta.Framework.IO
{
    public class Resource
    {
        public static string Base { get; private set; }

        public static void Init(string basename)
        {
            Base = basename;
        }

        public static T Load<T>(string name)
        {
            if (string.IsNullOrEmpty(Base))
                throw new InvalidOperationException("Resource.Base not set.");

            string fullName = $"{Base}.Resources.Textures.{name}";
            var asm = typeof(Ass).Assembly; // add an empty class to your resource assembly and replace.
            
            using Stream? stream = asm.GetManifestResourceStream(fullName);
            if (stream == null)
                throw new Exception($"Embedded resource not found: {fullName}");

            if (typeof(T) == typeof(Texture))
            {
                using var ms = new MemoryStream();
                stream.CopyTo(ms);

                Texture tex = Texture.FromImageBytes(ms.ToArray());
                return (T)(object)tex;
            }

            if (typeof(T) == typeof(byte[]))
            {
                using var ms = new MemoryStream();
                stream.CopyTo(ms);
                return (T)(object)ms.ToArray();
            }

            throw new NotSupportedException(typeof(T).Name);
        }


        private static string GetTypeFolder(Type t)
        {
            if (t == typeof(Texture)) return "Texture";
            if (t == typeof(byte[])) return "Binary";
            return "Unknown";
        }
    }
}