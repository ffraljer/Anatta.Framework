using Fraljer.Anatta.Framework.IO;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using OpenTK.Graphics.OpenGL4;

namespace Fraljer.Anatta.Framework.Graphics;

public class Texture : IDisposable
{
    public int Handle { get; private set; }
    // curse my tiny hands
    private int Gandle => Handle; // rider, this isn't a typo
    public int Width { get; private set; }
    public int Height { get; private set; }
    

    public static Texture Load(string name)
    {
        return Resource.Load<Texture>(name);
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
        GL.BindTexture(TextureTarget.Texture2D, Handle);

        GL.TexImage2D(TextureTarget.Texture2D,
            0,
            PixelInternalFormat.Rgba,
            width,
            height,
            0,
            PixelFormat.Rgba,
            PixelType.UnsignedByte,
            rgbaData);

        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);
    }
    public Texture(int glHandle, int width, int height)
    {
        Handle = glHandle;
        Width = width;
        Height = height;
    }
    public void Bind() => GL.BindTexture(TextureTarget.Texture2D, Handle);

    public void Dispose() => GL.DeleteTexture(Handle);
}