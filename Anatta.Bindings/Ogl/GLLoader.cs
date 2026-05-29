// auto-generated, do not edit.

using System;
using System.Runtime.InteropServices;

namespace Anatta.Framework.Graphics.OpenGL
{
    public static class GLLoader
    {
        private static bool _loaded;

        public static void LoadAll(Func<string, nint> getProcAddress)
        {
            if (_loaded) return;
            ArgumentNullException.ThrowIfNull(getProcAddress);
            GL.LoadFunctions(getProcAddress);
            _loaded = true;
        }

        public static unsafe void LoadAll(delegate* unmanaged[Cdecl]<byte*, nint> getProcAddress)
        {
            LoadAll(name =>
            {
                byte[] utf8 = System.Text.Encoding.UTF8.GetBytes(name + '\0');
                fixed (byte* p = utf8)
                    return getProcAddress(p);
            });
        }

        public static bool IsLoaded => _loaded;

        public static void Reset() => _loaded = false;
    }
}
