using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using OpenTK.Graphics.OpenGL;
using Anatta.Framework.IO;

namespace Anatta.Framework.Graphics;

/// <summary>
/// A texture class.
/// </summary>
public class Texture : IDisposable
{
    /// <summary>
    /// the Handle of the Texture.
    /// </summary>
    public int Handle { get; private set; }
    /// <summary>
    /// Width of the current Texture.
    /// </summary>
    public int Width { get; private set; }
    
    private static Texture? _whitePixel;
    internal static Texture WhitePixel
    {
        get
        {
            if (_whitePixel == null)
            {
                byte[] data = { 255, 255, 255, 255 };
                _whitePixel = new Texture(1, 1, data);
            }

            return _whitePixel;
        }
    }
    /// <summary>
    /// Height of the current Texture.
    /// </summary>
    public int Height { get; private set; }
    private static string[] _names = { ".png", ".jpeg", ".jpg", ".xnb" };

    /// <summary>
    /// Loads a Texture from an Embedded Resource.
    /// </summary>
    /// <param name="name">Name of the resource</param>
    /// <remarks>The extension is added in automatically.</remarks>
    /// <returns>The loaded Texture.</returns>
    /// <exception cref="FileNotFoundException">Throws if the texture cannot be found.</exception>
    public static Texture Load(string name) {
        foreach (var ext in _names) {
            string resourceName = name + ext;
            try {
                return Resource.Load<Texture>(resourceName);
            }
            catch {
            }
        }
        throw new FileNotFoundException($"{name} not found");
    }
    
    public static Texture FromImageBytes(byte[] bytes)
    {
        using Image<Rgba32> image = Image.Load<Rgba32>(bytes);

        int width = image.Width;
        int height = image.Height;

        byte[] rgba = new byte[width * height * 4];
        image.CopyPixelDataTo(rgba);

        return new Texture(width, height, rgba);
    }
    
    
    public Texture(int width, int height, byte[] rgbaData)
    {
        Width = width;
        Height = height;

        Handle = GL.GenTexture();
        GL.BindTexture(TextureTarget.Texture2d, Handle);

        GL.TexImage2D(TextureTarget.Texture2d,
            0,
            InternalFormat.Rgba,
            width,
            height,
            0,
            PixelFormat.Rgba,
            PixelType.UnsignedByte,
            rgbaData);

        GL.TexParameteri(TextureTarget.Texture2d, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
        GL.TexParameteri(TextureTarget.Texture2d, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);
    }
    public Texture(int glHandle, int width, int height)
    {
        Handle = glHandle;
        Width = width;
        Height = height;
    }
    public void Bind() => GL.BindTexture(TextureTarget.Texture2d, Handle);

    public void Dispose() => GL.DeleteTexture(Handle);
}