namespace Anatta.Framework.Graphics.Shaders;

public class ShaderProperty {
    public string Name { get; }
    public Type Type { get; }
    public object? Value { get; set; }

    public ShaderProperty(string name, Type type) {
        Name = name;
        Type = type;
    }
}