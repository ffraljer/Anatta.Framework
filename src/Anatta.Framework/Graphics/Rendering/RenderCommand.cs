using OpenTK.Mathematics;

namespace Anatta.Framework.Graphics.Rendering;

public struct RenderCommand {
    public Texture? Texture;
    public Matrix4 Transform;
    public Vector4 TintTop;
    public Vector4 TintBottom;
    public Vector4 BorderTop;
    public Vector4 BorderBottom;
    public Vector2 Size;
    public float Radius;
    public float CircleRadius;
    public float CircleThickness;
    public float BoxBorderThickness;
}