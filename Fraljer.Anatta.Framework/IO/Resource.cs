using System.Reflection;
using Demo.Resources;
using Fraljer.Anatta.Framework.Graphics;
using Fraljer.Anatta.Framework.Sound;

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
            string folder = GetTypeFolder(typeof(T));
            string fullName = $"{Base}.Resources.{folder}.{name}";
            var asm = typeof(Ass).Assembly; // add a class to your resource assembly and replace.

            
            using Stream? stream = asm.GetManifestResourceStream(fullName);
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
            throw new NotSupportedException(typeof(T).Name);
        }


        private static string GetTypeFolder(Type t)
        {
            if (t == typeof(Texture)) return "Textures";
            if (t == typeof(byte[])) return "Audio";
            return "Unknown";
        }
    }
}