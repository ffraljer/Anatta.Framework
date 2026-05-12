#if win
using Vortice.Direct3D11;
using Vortice.Direct3D;
using Vortice.DXGI;
using OpenTK.Mathematics;
using System.Runtime.InteropServices;
using Anatta.Framework.Graphics.D3D;
using Anatta.Framework.Graphics.Helpers;
using Anatta.Framework.Graphics.Interfaces;
using Anatta.Framework.Graphics.Rendering;
using Anatta.Framework.Graphics.Shapes;
using Anatta.Framework.Graphics.Sprites;

namespace Anatta.Framework.Graphics.Renderers;

public class SpriteRendererD3D : IRenderer {
    private readonly ID3D11Device _device;
    private readonly ID3D11DeviceContext _context;
    private ID3D11BlendState _blendState;
    private ID3D11SamplerState _samplerState;
    private ID3D11RasterizerState _rasterizerState;

    private ID3D11Buffer _vertexBuffer;
    private ShaderD3D _shd;
    private bool _initialized;

    private readonly List<RenderCommand> _commands = new(256);
    private Matrix4 _projection;

    private static readonly float[] Quad = {
        // X,  Y,  U,  V
        0f, 1f, 0f, 1f,
        1f, 0f, 1f, 0f,
        0f, 0f, 0f, 0f,

        0f, 1f, 0f, 1f,
        1f, 1f, 1f, 1f,
        1f, 0f, 1f, 0f
    };

    private static readonly (string Name, int Offset, int Size)[] Uniforms = {
        ("transform", 0, 64),
        ("uProjection", 64, 64),
        ("uTintTop", 128, 16),
        ("uTintBottom", 144, 16),
        ("uBorderTop", 160, 16),
        ("uBorderBottom", 176, 16),
        ("uSize", 192, 8),
        ("uRadius", 200, 4),
        ("uCircleRadius", 204, 4),
        ("uCircleThickness", 208, 4),
    };

    private static readonly InputElementDescription[] InputLayout = {
        new("TEXCOORD", 0, Format.R32G32_Float, 0, 0),
        new("TEXCOORD", 1, Format.R32G32_Float, 8, 0),
    };

    public SpriteRendererD3D(ID3D11Device device, ID3D11DeviceContext context) {
        _device = device;
        _context = context;
    }

    public void Submit(RenderCommand cmd) => _commands.Add(cmd);

    public void Init() {
        if (_initialized) return;

        unsafe {
            fixed (float* ptr = Quad) {
                var vbDesc = new BufferDescription {
                    ByteWidth = (uint)(Quad.Length * sizeof(float)),
                    Usage = ResourceUsage.Immutable,
                    BindFlags = BindFlags.VertexBuffer
                };
                //var initData = new SubresourceData((IntPtr)ptr, (uint)(4 * sizeof(float)));
                var initData = new SubresourceData((IntPtr)ptr);
                _vertexBuffer = _device.CreateBuffer(vbDesc, initData);
            }
        }

        _shd = ShaderD3D.Load(
            _device, _context,
            "sprite.avs", "sprite.afs"
        );

        var samplerDesc = new SamplerDescription {
            Filter = Filter.MinMagMipLinear,
            AddressU = TextureAddressMode.Clamp,
            AddressV = TextureAddressMode.Clamp,
            AddressW = TextureAddressMode.Clamp,
        };
        _samplerState = _device.CreateSamplerState(samplerDesc);

        var rasterDesc = new RasterizerDescription {
            CullMode = CullMode.None,
            FillMode = FillMode.Solid,
            DepthClipEnable = false
        };
        _rasterizerState = _device.CreateRasterizerState(rasterDesc);

        var blendDesc = new BlendDescription();
        blendDesc.RenderTarget[0] = new RenderTargetBlendDescription {
            BlendEnable = true,
            SourceBlend = Blend.SourceAlpha,
            DestinationBlend = Blend.InverseSourceAlpha,
            BlendOperation = BlendOperation.Add,
            SourceBlendAlpha = Blend.One,
            DestinationBlendAlpha = Blend.Zero,
            BlendOperationAlpha = BlendOperation.Add,
            RenderTargetWriteMask = ColorWriteEnable.All
        };
        _blendState = _device.CreateBlendState(blendDesc);

        _initialized = true;
    }

    public void Use(int screenW, int screenH) {
        _shd.Use();

        _context.RSSetViewport(0, 0, screenW, screenH, 0f, 1f);
        _context.PSSetSamplers(1, new[] { _samplerState });
        _context.RSSetState(_rasterizerState);
        _context.OMSetBlendState(_blendState);

        var projection = Matrix4.CreateOrthographicOffCenter(0, screenW, screenH, 0, -1, 1);
        _shd.SetMatrix4(64, projection);

        uint stride = (uint)(4 * sizeof(float));
        uint offset = 0;
        _context.IASetVertexBuffers(0, new[] { _vertexBuffer }, new[] { stride }, new[] { offset });
        _context.IASetPrimitiveTopology(PrimitiveTopology.TriangleList);
    }

    public void DrawBatch(IReadOnlyList<RenderCommand> commands, int screenW, int screenH) {
        foreach (var cmd in commands) {
            cmd.Texture!.GetD3D(_device).Bind(_context, 1);

            _shd.SetMatrix4(0, cmd.Transform);
            _shd.SetVector4(128, cmd.TintTop);
            _shd.SetVector4(144, cmd.TintBottom);
            _shd.SetVector4(160, cmd.BorderTop);
            _shd.SetVector4(176, cmd.BorderBottom);
            _shd.SetVector2(192, cmd.Size);
            _shd.SetFloat(200, cmd.Radius);
            _shd.SetFloat(204, cmd.CircleRadius);
            _shd.SetFloat(208, cmd.CircleThickness);
            _shd.SetFloat(212, cmd.BoxBorderThickness);
            _shd.Upload();

            _context.Draw(6, 0);
        }
    }

    public void Kill() {
        foreach (var cmd in _commands)
            DrawCommand(cmd);
        _commands.Clear();
        _context.IASetVertexBuffers(0, new ID3D11Buffer[] { null }, new uint[] { 0 }, new uint[] { 0 });
    }


    private void DrawCommand(RenderCommand cmd) {
        cmd.Texture!.GetD3D(_device).Bind(_context, 1);
        _shd.SetMatrix4(0, cmd.Transform);
        _shd.SetVector4(128, cmd.TintTop);
        _shd.SetVector4(144, cmd.TintBottom);
        _shd.SetVector4(160, cmd.BorderTop);
        _shd.SetVector4(176, cmd.BorderBottom);
        _shd.SetVector2(192, cmd.Size);
        _shd.SetFloat(200, cmd.Radius);
        _shd.SetFloat(204, cmd.CircleRadius);
        _shd.SetFloat(208, cmd.CircleThickness);
        _shd.SetFloat(212, cmd.BoxBorderThickness);
        _shd.Upload();
        _context.Draw(6, 0);
    }

    public void Dispose() {
        _vertexBuffer.Dispose();
        _blendState.Dispose();
        _samplerState.Dispose();
        _rasterizerState.Dispose();
        _shd.Dispose();
    }
}
#endif