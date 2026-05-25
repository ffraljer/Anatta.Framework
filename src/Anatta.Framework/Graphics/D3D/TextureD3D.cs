#if WINDOWS
using Anatta.Framework.Graphics.Interfaces;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using Vortice.Direct3D11;
using Vortice.DXGI;

namespace Anatta.Framework.Graphics.D3D;

public class TextureD3D : ITexture {
    public int Width { get; private set; }
    public int Height { get; private set; }

    public ID3D11Texture2D Texture2D { get; set; }
    public ID3D11ShaderResourceView ShaderResourceView { get; private set; }

    private TextureD3D(ID3D11Device device, int width, int height, byte[] rgbaData) {
        Width = width;
        Height = height;

        var desc = new Texture2DDescription {
            Width = (uint)width,
            Height = (uint)height,
            MipLevels = 1,
            ArraySize = 1,
            Format = Format.R8G8B8A8_UNorm,
            SampleDescription = new SampleDescription(1, 0),
            Usage = ResourceUsage.Default,
            BindFlags = BindFlags.RenderTarget | BindFlags.ShaderResource
        };

        unsafe {
            fixed (byte* ptr = rgbaData) {
                var initData = new SubresourceData((IntPtr)ptr, (uint)(width * 4));
                Texture2D = device.CreateTexture2D(desc, new[] { initData });
            }
        }

        ShaderResourceView = device.CreateShaderResourceView(Texture2D);
    }

    public void Bind(ID3D11DeviceContext context, int slot = 0) {
        context.PSSetShaderResources((uint)slot, [ShaderResourceView]);
    }
    
    public static TextureD3D SetData(ID3D11Device device, byte[] bytes) {
        using Image<Rgba32> image = Image.Load<Rgba32>(bytes);

        int width = image.Width;
        int height = image.Height;

        byte[] rgba = new byte[width * height * 4];
        image.CopyPixelDataTo(rgba);

        return new TextureD3D(device, width, height, rgba);
    }
    
    public static TextureD3D SetData(ID3D11Device device, int width, int height, byte[] rgbaData) {
        return new TextureD3D(device, width, height, rgbaData);
    }

    public void Dispose() {
        ShaderResourceView.Dispose();
        Texture2D.Dispose();
    }
}
#endif