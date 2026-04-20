#if win
using Anatta.Framework.Graphics.Helpers;
using Anatta.Framework.Graphics.Shapes;
using Anatta.Framework.Graphics.Interfaces;
using Anatta.Framework.Graphics.Rendering;
using Anatta.Framework.Graphics.D3D;
using Vortice.Direct3D11;
using Vortice.Direct3D;
using Vortice.DXGI;
using OpenTK.Mathematics;
using System.Runtime.InteropServices;
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
            "sprite.avs", "sprite.afs",
            InputLayout,
            Uniforms,
            cbSize: 216
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

    public RenderCommand BuildCommand(ISprite sprite, int screenW, int screenH) {
        Vector2 size;
        var cmd = new RenderCommand();

        if (sprite is Box box) {
            size = box.Size * box.Scale;
            cmd.Texture = TextureD3D.WhitePixel(_device);
            cmd.TintTop = box.Colour.Top.ToVector4();
            cmd.TintBottom = box.Colour.Bottom.ToVector4();
            cmd.BorderTop = box.BorderColour.Top.ToVector4();
            cmd.BorderBottom = box.BorderColour.Bottom.ToVector4();
            cmd.Size = size;
            cmd.Radius = box.CornerRadius;
            cmd.BoxBorderThickness = box.BorderThickness;
        }
        else if (sprite is Circle circle) {
            size = new Vector2(
                circle.Radius * 2f * circle.Scale.X,
                circle.Radius * 2f * circle.Scale.Y
            );
            cmd.Texture = TextureD3D.WhitePixel(_device);
            cmd.TintTop = circle.Colour.Top.ToVector4();
            cmd.TintBottom = circle.Colour.Bottom.ToVector4();
            cmd.BorderTop = circle.BorderColour.Top.ToVector4();
            cmd.BorderBottom = circle.BorderColour.Bottom.ToVector4();
            cmd.CircleRadius = circle.Radius * MathF.Max(circle.Scale.X, circle.Scale.Y);
            cmd.CircleThickness = circle.Thickness * MathF.Max(circle.Scale.X, circle.Scale.Y);
            cmd.IsCircle = true;
            cmd.Size = size;
        }
        else if (sprite.Texture != null) {
            size = new Vector2(
                sprite.Texture.Width * sprite.Scale.X,
                sprite.Texture.Height * sprite.Scale.Y
            );
            var tint = sprite.Colour.Top.ToVector4();
            cmd.Texture = sprite.Texture;
            cmd.TintTop = tint;
            cmd.TintBottom = tint;
            cmd.Size = size;
            cmd.Radius = sprite.CornerRadius;
        }
        else return default;

        var originNorm = AnchorHelper.ToNormalised(sprite.Origin);
        var originOffset = originNorm * size;
        var anchorNorm = AnchorHelper.ToNormalised(sprite.Anchor);
        var anchorOffset = new Vector2(
            anchorNorm.X * screenW,
            anchorNorm.Y * screenH
        );

        cmd.Transform =
            Matrix4.CreateScale(size.X, size.Y, 1f) *
            Matrix4.CreateTranslation(-originOffset.X, -originOffset.Y, 0f) *
            Matrix4.CreateRotationZ(sprite.Rotation) *
            Matrix4.CreateTranslation(sprite.DrawPosition.X, sprite.DrawPosition.Y, 0f) *
            Matrix4.CreateTranslation(anchorOffset.X, anchorOffset.Y, 0f);

        return cmd;
    }

    public void DrawBatch(IReadOnlyList<RenderCommand> commands, int screenW, int screenH) {
        foreach (var cmd in commands) {
            if (cmd.Texture is TextureD3D tex)
                tex.Bind(_context, 1);
            else if (cmd.Texture is Texture t)
                t.GetD3D(_device).Bind(_context, 1);

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
        _context.IASetVertexBuffers(0, new ID3D11Buffer[] { null }, new uint[] { 0 }, new uint[] { 0 });
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