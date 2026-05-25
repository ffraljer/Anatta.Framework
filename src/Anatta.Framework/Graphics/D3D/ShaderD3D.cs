#if win
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Text.Unicode;
using Anatta.Framework.Graphics.Helpers;
using Anatta.Framework.Graphics.Interfaces;
using Anatta.Framework.Logging;
using Vortice.Direct3D11;
using OpenTK.Mathematics;
using Vortice.ShaderCompiler;

namespace Anatta.Framework.Graphics.D3D;

public class ShaderD3D : IShader {
    #region Private Variables
    private readonly ID3D11DeviceContext _context;

    private readonly ID3D11VertexShader _vertexShader;
    private readonly ID3D11PixelShader _pixelShader;
    private readonly ID3D11InputLayout _inputLayout;
    private readonly ID3D11Buffer _constantBuffer;

    private readonly byte[] _cbData;
    private readonly Dictionary<string, (int Offset, int Size)> _uniforms = new();
    private bool _dirty;
    private static string shaderCacheDir => 
        Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
        "fraljer", "Anatta", "cache", "shader");

    private static readonly Logger _logger = new("D3DShader");
    #endregion
    
    #region Internal Load Methods
    internal static ShaderD3D Load(
        ID3D11Device device,
        ID3D11DeviceContext context,
        string vsGlsl,
        string fsGlsl) {

        byte[] spvVsbytes = AnattaShaderCompiler.CompileGlslToSpirv(vsGlsl, ShaderKind.VertexShader);
        byte[] spvPsbytes = AnattaShaderCompiler.CompileGlslToSpirv(fsGlsl, ShaderKind.FragmentShader);

        var (uniforms, cbSize, layout) = AnattaShaderCompiler.ReflectSpirv(spvVsbytes, spvPsbytes);

        string vsHlsl = AnattaShaderCompiler.SpirvToHlsl(spvVsbytes);
        string psHlsl = AnattaShaderCompiler.SpirvToHlsl(spvPsbytes);

        return new ShaderD3D(device, context, vsHlsl, psHlsl, layout, uniforms, cbSize);
    }

    internal static ShaderD3D LoadShaderInternal(
        ID3D11Device device,
        ID3D11DeviceContext context,
        string vertex,
        string fragment)
    {
        var a = Assembly.GetExecutingAssembly();

        using Stream vs = a.GetManifestResourceStream($"Anatta.Framework.Resources.{vertex}")
                          ?? throw new FileNotFoundException($"{vertex} cannot be found");
        using Stream fs = a.GetManifestResourceStream($"Anatta.Framework.Resources.{fragment}")
                          ?? throw new FileNotFoundException($"{fragment} cannot be found");

        string vsGlsl = new StreamReader(vs).ReadToEnd();
        string fsGlsl = new StreamReader(fs).ReadToEnd();

        byte[] spvVsbytes = GetOrCompileShader(vsGlsl, ShaderKind.VertexShader);
        byte[] spvPsbytes = GetOrCompileShader(fsGlsl, ShaderKind.FragmentShader);

        var (uniforms, cbSize, layout) = 
            AnattaShaderCompiler.ReflectSpirv(spvVsbytes, spvPsbytes);
        
        string vsHlsl = AnattaShaderCompiler.SpirvToHlsl(spvVsbytes);
        string pissHlsl = AnattaShaderCompiler.SpirvToHlsl(spvPsbytes);

        return new ShaderD3D(device, context, vsHlsl, pissHlsl, layout, uniforms, cbSize);
    }
    #endregion
    
    #region Constructor
    public ShaderD3D(
        ID3D11Device device,
        ID3D11DeviceContext context,
        string vsHlsl,
        string psHlsl,
        InputElementDescription[] inputLayout,
        (string Name, int Offset, int Size)[] uniforms,
        int cbSize)
    {
        _context = context;

        byte[] vsBlob = AnattaShaderCompiler.CompileHlsl(vsHlsl, "vs_5_0");
        byte[] psBlob = AnattaShaderCompiler.CompileHlsl(psHlsl, "ps_5_0");

        _vertexShader = device.CreateVertexShader(vsBlob);
        _pixelShader = device.CreatePixelShader(psBlob);
        _inputLayout = device.CreateInputLayout(inputLayout, vsBlob);

        foreach (var (name, offset, size) in uniforms)
            _uniforms[name] = (offset, size);

        int aligned = (cbSize + 15) & ~15;
        _cbData = new byte[aligned];

        _constantBuffer = device.CreateBuffer(new BufferDescription
        {
            ByteWidth = (uint)aligned,
            Usage = ResourceUsage.Dynamic,
            BindFlags = BindFlags.ConstantBuffer,
            CPUAccessFlags = CpuAccessFlags.Write
        });
    }
    #endregion
    
    #region Public Methods
    public void Use() {
        _context.VSSetShader(_vertexShader);
        _context.PSSetShader(_pixelShader);
        _context.IASetInputLayout(_inputLayout);
        _context.VSSetConstantBuffers(0, [_constantBuffer]);
        _context.PSSetConstantBuffers(0, [_constantBuffer]);
    }

    public void Upload() {
        if (!_dirty) return;

        var mapped = _context.Map(_constantBuffer, 0, MapMode.WriteDiscard);
        Marshal.Copy(_cbData, 0, mapped.DataPointer, _cbData.Length);
        _context.Unmap(_constantBuffer, 0);

        _dirty = false;
    }
    #endregion
    
    #region Set* Methods

    public void SetMatrix4(int offset, Matrix4 mat) {
        Span<float> f = [
            mat.M11, mat.M12, mat.M13, mat.M14,
            mat.M21, mat.M22, mat.M23, mat.M24,
            mat.M31, mat.M32, mat.M33, mat.M34,
            mat.M41, mat.M42, mat.M43, mat.M44
        ];
        MemoryMarshal.AsBytes(f).CopyTo(_cbData.AsSpan(offset));
        _dirty = true;
    }
    public void SetVector4(int offset, Vector4 vec) {
        Span<float> f = [vec.X, vec.Y, vec.Z, vec.W];
        MemoryMarshal.AsBytes(f).CopyTo(_cbData.AsSpan(offset));
        _dirty = true;
    }
    public void SetVector2(int offset, Vector2 vec) {
        Span<float> f = [vec.X, vec.Y];
        MemoryMarshal.AsBytes(f).CopyTo(_cbData.AsSpan(offset));
        _dirty = true;
    }
    public void SetFloat(int offset, float value) {
        Span<float> f = [value];
        MemoryMarshal.AsBytes(f).CopyTo(_cbData.AsSpan(offset));
        _dirty = true;
    }
    
    public void SetMatrix4(string name, Matrix4 mat) {
        if (!_uniforms.TryGetValue(name, out var slot)) {
            _logger.Warn($"Unknown uniform '{name}'");
            return;
        }
        SetMatrix4(slot.Offset, mat);
    }

    public void SetVector4(string name, Vector4 vec) {
        if (!_uniforms.TryGetValue(name, out var slot)) {
            _logger.Warn($"Unknown uniform '{name}'");
            return;
        }
        SetVector4(slot.Offset, vec);
    }

    public void SetVector3(string name, Vector3 vec) {
        if (!_uniforms.TryGetValue(name, out var slot)) {
            _logger.Warn($"Unknown uniform '{name}'");
            return;
        }
        Span<float> f = [vec.X, vec.Y, vec.Z];
        MemoryMarshal.AsBytes(f).CopyTo(_cbData.AsSpan(slot.Offset));
        _dirty = true;
    }

    public void SetVector2(string name, Vector2 vec) {
        if (!_uniforms.TryGetValue(name, out var slot)) {
            _logger.Warn($"Unknown uniform '{name}'");
            return;
        }
        SetVector2(slot.Offset, vec);
    }

    public void SetFloat(string name, float value) {
        if (!_uniforms.TryGetValue(name, out var slot)) {
            _logger.Warn($"Unknown uniform '{name}'");
            return;
        }
        SetFloat(slot.Offset, value);
    }

    public void SetInt(string name, int value) {
        if (!_uniforms.TryGetValue(name, out var slot)) {
            _logger.Warn($"Unknown uniform '{name}'");
            return;
        }
        Span<float> f = [value];
        MemoryMarshal.AsBytes(f).CopyTo(_cbData.AsSpan(slot.Offset));
        _dirty = true;
    }

    public void SetBool(string name, bool value) => SetInt(name, value ? 1 : 0);
    #endregion
    
    #region Private Methods

    private static byte[] GetOrCompileShader(string src, ShaderKind kind) { // VERYRYRYRYRYRYRYRYR MUCH REduced loading
        //time
        var hsh = Convert.ToHexString(SHA256.HashData(
            Encoding.UTF8.GetBytes(src + kind)));
        var path = Path.Combine(shaderCacheDir, $"{hsh}.spv");

        if (File.Exists(path)) {
            return File.ReadAllBytes(path);
        }

        var spv = AnattaShaderCompiler.CompileGlslToSpirv(src, kind);
        Directory.CreateDirectory(shaderCacheDir);
        File.WriteAllBytes(path, spv);
        return spv;
    }
    #endregion

    public void Dispose()
    {
        _inputLayout.Dispose();
        _vertexShader.Dispose();
        _pixelShader.Dispose();
        _constantBuffer.Dispose();
    }
}
#endif
