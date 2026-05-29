// auto-generated, do not edit.

using System;
using System.Runtime.InteropServices;

namespace Anatta.Framework.Graphics.OpenGL
{
    /// <summary>
    /// Managed delegate types for OpenGL callbacks.
    /// </summary>
    public static class GLDelegates
    {
        /// <summary>
        /// Callback signature for <c>glDebugMessageCallback</c>.
        /// </summary>
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void DebugProc(
            DebugSource source,
            DebugType   type,
            uint        id,
            DebugSeverity severity,
            int         length,
            nint        message,
            nint        userParam);
    }
}
