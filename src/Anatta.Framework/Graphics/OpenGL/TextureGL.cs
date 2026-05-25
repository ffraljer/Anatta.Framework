using Anatta.Framework.Graphics.Interfaces;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using OpenTK.Graphics.OpenGL;
using Anatta.Framework.Storage;

namespace Anatta.Framework.Graphics.OpenGL;

public class TextureGL : ITexture {
    /// <summary>
    /// the Handle of the Texture.
    /// </summary>
    public int Handle { get; private set; }
    /// <summary>
    /// Width of the current Texture.
    /// </summary>
    public int Width { get; private set; }
    
    public int Height { get; private set; }

    public static TextureGL SetData(byte[] bytes) {
        using Image<Rgba32> image = Image.Load<Rgba32>(bytes);

        int width = image.Width;
        int height = image.Height;

        byte[] rgba = new byte[width * height * 4];
        image.CopyPixelDataTo(rgba);

        return new TextureGL(width, height, rgba);
    }


    public TextureGL(int width, int height, byte[] rgbaData) {
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
    public TextureGL(int glHandle, int width, int height) {
        Handle = glHandle;
        Width = width;
        Height = height;
    }
    public void Bind() => GL.BindTexture(TextureTarget.Texture2d, Handle);

    public void Dispose() => GL.DeleteTexture(Handle);
}