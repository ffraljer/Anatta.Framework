using OpenTK;
using SDL3;

internal class SDLBindingsContext : IBindingsContext
{
    public IntPtr GetProcAddress(string procName)
    {
        return SDL.GLGetProcAddress(procName);
    }
}