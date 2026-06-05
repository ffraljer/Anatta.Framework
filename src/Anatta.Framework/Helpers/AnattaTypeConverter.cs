using sVec2  = System.Numerics.Vector2;
using sVec3  = System.Numerics.Vector3;
using sVec4  = System.Numerics.Vector4;
using sMat4  = System.Numerics.Matrix4x4;

using oVec2  = OpenTK.Mathematics.Vector2;
using oVec3  = OpenTK.Mathematics.Vector3;
using oVec4  = OpenTK.Mathematics.Vector4;
using oMat4  = OpenTK.Mathematics.Matrix4;

namespace Anatta.Framework.Helpers;

internal static class AnattaTypeConverter {

    internal static oVec2 ToOtk(sVec2 v) => new(v.X, v.Y);
    internal static oVec3 ToOtk(sVec3 v) => new(v.X, v.Y, v.Z);
    internal static oVec4 ToOtk(sVec4 v) => new(v.X, v.Y, v.Z, v.W);
    internal static oMat4 ToOtk(sMat4 m) => new(
        m.M11, m.M12, m.M13, m.M14,
        m.M21, m.M22, m.M23, m.M24,
        m.M31, m.M32, m.M33, m.M34,
        m.M41, m.M42, m.M43, m.M44
    );

    internal static sVec2 ToSys(oVec2 v) => new(v.X, v.Y);
    internal static sVec3 ToSys(oVec3 v) => new(v.X, v.Y, v.Z);
    internal static sVec4 ToSys(oVec4 v) => new(v.X, v.Y, v.Z, v.W);
    internal static sMat4 ToSys(oMat4 m) => new(
        m.M11, m.M12, m.M13, m.M14,
        m.M21, m.M22, m.M23, m.M24,
        m.M31, m.M32, m.M33, m.M34,
        m.M41, m.M42, m.M43, m.M44
    );
}