using Anatta.Framework.Graphics;
using Anatta.Framework.Graphics.Interfaces;

namespace Anatta.Framework.Storage.Loaders;

public class TextureLoader : IAssetLoader {
    public string Folder => "Textures";
    
    public bool CanLoad(Type type) {
        return type == typeof(Texture)
               || type == typeof(ITexture);
    }

    public object Load(string name, byte[] bytes) {
        if (name.EndsWith(".xnb", StringComparison.OrdinalIgnoreCase))
            return LoadXnbTexture(bytes);
                
        var tex = Texture.FromBytes(bytes);
        return tex;
    }
    
    #region  XNB Loading
    private static ITexture LoadXnbTexture(byte[] data)
    {
        using var ms = new MemoryStream(data);
        using var br = new BinaryReader(ms);
        if (new string(br.ReadChars(3)) != "XNB")
            throw new Exception("invalid xnb");
        char platform = br.ReadChar();
        byte version = br.ReadByte();
        byte flags = br.ReadByte();
        int size = br.ReadInt32();
        bool compressed = (flags & 0x80) != 0;
        if (compressed)
            throw new NotSupportedException("compressed xnb not supported");
        int readerCount = br.Read7BitEncodedInt();
        for (int i = 0; i < readerCount; i++)
        {
            br.ReadString();
            br.ReadInt32();
        }
        br.Read7BitEncodedInt();
        br.Read7BitEncodedInt();
        int format = br.ReadInt32();
        int width = br.ReadInt32();
        int height = br.ReadInt32();
        int mipCount = br.ReadInt32();
        int dataSize = br.ReadInt32();
        byte[] pixelData = br.ReadBytes(dataSize);
        for (int i = 0; i < pixelData.Length; i += 4)
        {
            (pixelData[i], pixelData[i + 2]) = (pixelData[i + 2], pixelData[i]);
        }
                
        return Texture.FromRawBytes(pixelData, width, height);
    }
    #endregion
}