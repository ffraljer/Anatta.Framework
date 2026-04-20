#if win
using System.Reflection;
using System.Runtime.InteropServices;
using Anatta.Framework.Graphics.Helpers;
using Anatta.Framework.Logging;
using Vortice.Direct3D11;
using Vortice.DXGI;
using Vortice.SpirvCross;
using OpenTK.Mathematics;
using Vortice.ShaderCompiler;
using Compiler = Vortice.D3DCompiler.Compiler;

namespace Anatta.Framework.Graphics.D3D;

public unsafe class ShaderD3D : IDisposable {
    private readonly ID3D11Device _device;
    private readonly ID3D11DeviceContext _context;

    private ID3D11VertexShader _vertexShader;
    private ID3D11PixelShader _pixelShader;
    private ID3D11InputLayout _inputLayout;
    private ID3D11Buffer _constantBuffer;

    private readonly byte[] _cbData;
    private readonly Dictionary<string, (int Offset, int Size)> _uniforms = new();
    private bool _dirty;

    private readonly Logger _logger = new("D3DShader");

    internal static ShaderD3D Load(
        ID3D11Device device,
        ID3D11DeviceContext context,
        string vertex,
        string fragment,
        InputElementDescription[] layout,
        (string Name, int Offset, int Size)[] uniforms,
        int cbSize)
    {
        var a = Assembly.GetExecutingAssembly();

        using Stream vs = a.GetManifestResourceStream($"Anatta.Framework.Resources.{vertex}")
                          ?? throw new FileNotFoundException($"{vertex} cannot be found");
        using Stream fs = a.GetManifestResourceStream($"Anatta.Framework.Resources.{fragment}")
                          ?? throw new FileNotFoundException($"{fragment} cannot be found");

        string vsGlsl = new StreamReader(vs).ReadToEnd();
        string fsGlsl = new StreamReader(fs).ReadToEnd();

        byte[] vsSpirvBytes = AnattaShaderCompiler.CompileGlslToSpirv(vsGlsl, ShaderKind.VertexShader);
        byte[] fsSpirvBytes = AnattaShaderCompiler.CompileGlslToSpirv(fsGlsl, ShaderKind.FragmentShader);

        string vsHlsl = AnattaShaderCompiler.SpirvToHlsl(vsSpirvBytes);
        string pissHlsl = AnattaShaderCompiler.SpirvToHlsl(fsSpirvBytes);

        return new ShaderD3D(device, context, vsHlsl, pissHlsl, layout, uniforms, cbSize);
    }

    public ShaderD3D(
        ID3D11Device device,
        ID3D11DeviceContext context,
        string vsHlsl,
        string psHlsl,
        InputElementDescription[] inputLayout,
        (string Name, int Offset, int Size)[] uniforms,
        int cbSize)
    {
        _device = device;
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
    public void Use() {
        _context.VSSetShader(_vertexShader);
        _context.PSSetShader(_pixelShader);
        _context.IASetInputLayout(_inputLayout);
        _context.VSSetConstantBuffers(0, new[] { _constantBuffer });
        _context.PSSetConstantBuffers(0, new[] { _constantBuffer });
    }

    public void Upload() {
        if (!_dirty) return;

        var mapped = _context.Map(_constantBuffer, 0, MapMode.WriteDiscard, Vortice.Direct3D11.MapFlags.None);
        Marshal.Copy(_cbData, 0, mapped.DataPointer, _cbData.Length);
        _context.Unmap(_constantBuffer, 0);

        _dirty = false;
    }

    public void SetMatrix4(int offset, Matrix4 mat) {
        Span<float> f = stackalloc float[16] {
            mat.M11, mat.M12, mat.M13, mat.M14,
            mat.M21, mat.M22, mat.M23, mat.M24,
            mat.M31, mat.M32, mat.M33, mat.M34,
            mat.M41, mat.M42, mat.M43, mat.M44
        };
        MemoryMarshal.AsBytes(f).CopyTo(_cbData.AsSpan(offset));
        _dirty = true;
    }
    public void SetVector4(int offset, Vector4 vec) {
        Span<float> f = stackalloc float[] { vec.X, vec.Y, vec.Z, vec.W };
        MemoryMarshal.AsBytes(f).CopyTo(_cbData.AsSpan(offset));
        _dirty = true;
    }
    public void SetVector2(int offset, Vector2 vec) {
        Span<float> f = stackalloc float[] { vec.X, vec.Y };
        MemoryMarshal.AsBytes(f).CopyTo(_cbData.AsSpan(offset));
        _dirty = true;
    }
    public void SetFloat(int offset, float value) {
        Span<float> f = stackalloc float[] { value };
        MemoryMarshal.AsBytes(f).CopyTo(_cbData.AsSpan(offset));
        _dirty = true;
    }

    private void Write(string name, ReadOnlySpan<byte> data)
    {
        if (!_uniforms.TryGetValue(name, out var slot))
        {
            _logger.Warn($"Unknown uniform '{name}'");
            return;
        }
        data[..slot.Size].CopyTo(_cbData.AsSpan(slot.Offset));
        _dirty = true;
    }
    

    private static byte[] ReadBytes(Stream s)
    {
        using var ms = new MemoryStream();
        s.CopyTo(ms);
        return ms.ToArray();
    }

    public void Dispose()
    {
        _inputLayout.Dispose();
        _vertexShader.Dispose();
        _pixelShader.Dispose();
        _constantBuffer.Dispose();
    }
}
#endif
