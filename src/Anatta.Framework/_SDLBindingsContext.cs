using OpenTK;
using SDL3;

namespace Anatta.Framework;

internal class _SDLBindingsContext : IBindingsContext
{
    public IntPtr GetProcAddress(string procName)
    {
        return SDL.GLGetProcAddress(procName);
    }
}