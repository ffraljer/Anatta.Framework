using Anatta.Framework.Graphics.OpenGL;
using Anatta.Framework.Graphics.Interfaces;

using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace Anatta.Framework.Graphics;

public class Texture : ITexture {
    private byte[] _rawData;

    private TextureGL? _gl;

    public int Width { get; private set; }
    public int Height { get; private set; }

    public static Texture WhitePixel { get; } = FromRawBytes(new byte[] { 255, 255, 255, 255 }, 1, 1);

    private Texture() { }
    
    public static Texture FromBytes(byte[] bytes) {
        using var image = Image.Load<Rgba32>(bytes);
        var raw = new byte[image.Width * image.Height * 4];
        image.CopyPixelDataTo(raw);
        return new Texture {
            Width = image.Width,
            Height = image.Height,
            _rawData = raw
        };
    }

    public static Texture FromRawBytes(byte[] rawData, int width, int height) {
        return new Texture {
            Width = width,
            Height = height,
            _rawData = rawData
        };
    }

    public void SetData(byte[] rawData) {
        if (rawData.Length != Width * Height * 4)
            throw new ArgumentException("raw data size mismatch");
        _rawData = rawData;
        _gl?.Dispose();
        _gl = null;
    }

    public void Dispose() {
        _gl?.Dispose();
    }

    internal TextureGL GetTexture() {
        if (_gl == null)
            _gl = new TextureGL(Width, Height, _rawData);
        return _gl;
    }
}