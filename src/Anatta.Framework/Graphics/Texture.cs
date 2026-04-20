using Anatta.Framework.Configuration;
using Anatta.Framework.Graphics.D3D;
using Anatta.Framework.Graphics.Interfaces;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using Vortice.Direct3D11;

namespace Anatta.Framework.Graphics;

public class Texture : ITexture {
    private byte[] _rawData;

    private TextureGL? _gl;
#if WINDOWS
    private TextureD3D? _d3d;
#endif

    public int Width { get; private set; }
    public int Height { get; private set; }
    
    public static Texture WhitePixel { get; } = CreateWhitePixel();

    public Texture(byte[] bytes) {
        using var image = Image.Load<Rgba32>(bytes);

        Width = image.Width;
        Height = image.Height;

        _rawData = new byte[Width * Height * 4];
        image.CopyPixelDataTo(_rawData);
    }
    public Texture(int width, int height, byte[] rawData) {
        Width = width;
        Height = height;
        _rawData = rawData;
    }
    
    private static Texture CreateWhitePixel()
    {
        byte[] data = { 255, 255, 255, 255 };

        var tex = new Texture(1, 1, data);

        return tex;
    }
    
    public void Dispose() {
        _gl?.Dispose();
        #if win
        _d3d?.Dispose();
        #endif
    }

    internal TextureGL GetGL() {
        if (_gl == null)
            _gl = new TextureGL(Width, Height, _rawData);

        return _gl;
    }
#if WINDOWS
    internal TextureD3D GetD3D(ID3D11Device device) {
        if (_d3d == null)
            _d3d = TextureD3D.SetData(device, Width, Height, _rawData);
        return _d3d;
    }
    #endif
}