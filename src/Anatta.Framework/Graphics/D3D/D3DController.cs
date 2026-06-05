#if WINDOWS
using Vortice.Direct3D11;

namespace Anatta.Framework.Graphics.D3D;

internal static class D3DController {
    public static ID3D11Device Device { get; private set; }
    public static ID3D11DeviceContext Context { get; private set; }

    public static void Set(ID3D11Device device, ID3D11DeviceContext context) {
        Device = device ?? throw new ArgumentNullException(nameof(device));
        Context = context ?? throw new ArgumentNullException(nameof(context));
    }
}
#endif