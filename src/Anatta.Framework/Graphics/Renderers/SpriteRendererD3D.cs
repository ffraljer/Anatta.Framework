#if win
using Vortice.Direct3D11;
using Vortice.Direct3D;
using OpenTK.Mathematics;
using Anatta.Framework.Graphics.D3D;
using Anatta.Framework.Graphics.Rendering;
using Anatta.Framework.Graphics.Sprites;
using Anatta.Framework.Logging;

namespace Anatta.Framework.Graphics.Renderers;

// yeah, that's right, I'm organising code like flibitijibibo now
public class SpriteRendererD3D(ID3D11Device device, ID3D11DeviceContext context) : IRenderer {
    #region Private Variables
    private readonly ID3D11Device? _device = device;
    private readonly ID3D11DeviceContext? _context = context;
    private ID3D11BlendState? _blendState;
    private ID3D11SamplerState? _samplerState;
    private ID3D11RasterizerState? _rasterizerState;

    private ID3D11Buffer? _vertexBuffer;
    private ShaderD3D? _shd;
    private bool _initialized;


    private Logger _logger = new("SpriteRenderer (D3D)");

    private readonly List<RenderCommand> _commands = new(256);

    private static readonly float[] Quad = [
        // X,  Y,  U,  V
        0f, 1f, 0f, 1f,
        1f, 0f, 1f, 0f,
        0f, 0f, 0f, 0f,

        0f, 1f, 0f, 1f,
        1f, 1f, 1f, 1f,
        1f, 0f, 1f, 0f
    ]; // it just replaced brackets with square brackets...
    #endregion
    
    #region Public Methods
    public void Submit(RenderCommand cmd) => _commands.Add(cmd);

    public void Init() {
        if (_initialized) return;
        if (_device == null) return;

        unsafe {
            fixed (float* ptr = Quad) {
                var vbDesc = new BufferDescription {
                    ByteWidth = (uint)(Quad.Length * sizeof(float)),
                    Usage = ResourceUsage.Immutable,
                    BindFlags = BindFlags.VertexBuffer
                };
                var initData = new SubresourceData((IntPtr)ptr);
                _vertexBuffer = _device!.CreateBuffer(vbDesc, initData);
            }
        }

        _shd = ShaderD3D.LoadShaderInternal(
            _device, _context!,
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
        if (_context == null) return;
        if (_vertexBuffer == null) return;
        if (_samplerState == null) return;
        if (_shd == null) return;
        
        _shd!.Use();

        _context.RSSetViewport(0, 0, screenW, screenH);
        _context.PSSetSamplers(1, [_samplerState]);
        _context.RSSetState(_rasterizerState);
        _context.OMSetBlendState(_blendState);
        // really just found out how to use !, don't know if I'm doing it wrong.

        var projection = Matrix4.CreateOrthographicOffCenter(0, screenW, screenH, 0, -1, 1);
        _shd!.SetMatrix4(64, projection);

        const uint stride = 4 * sizeof(float);
        const uint offset = 0;
        _context.IASetVertexBuffers(0, [_vertexBuffer]!, [stride], [offset]);
        _context.IASetPrimitiveTopology(PrimitiveTopology.TriangleList);
    }

    public void Kill() {
        foreach (var cmd in _commands)
            DrawCommand(cmd);
        _commands.Clear();
        _context!.IASetVertexBuffers(0, [null!], [0], [0]); // woah!
        // hmm, null isn't null.
    }
    #endregion
 
    #region Private Methods
    private void DrawCommand(RenderCommand cmd) {
        if (_device is null) return;
        if (_context is null) return;
        if (_shd is null) return;
        
        cmd.Texture!.GetD3D(_device!).Bind(_context!, 1);
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
    #endregion
    
    public void Dispose() {
        _vertexBuffer?.Dispose();
        _blendState?.Dispose();
        _samplerState?.Dispose();
        _rasterizerState?.Dispose();
        _shd?.Dispose();
    }
}
#endif