using OpenTK.Mathematics;

namespace Anatta.Framework.Graphics.Interfaces;

public interface IShader : IDisposable{
    void Use();
    void SetMatrix4(string name, Matrix4 mat);
    void SetFloat(string name, float value);
    void SetInt(string name, int value);
    void SetBool(string name, bool value);
    void SetVector2(string name, Vector2 vec);
    void SetVector3(string name, Vector3 vec);
    void SetVector4(string name, Vector4 vec);
}