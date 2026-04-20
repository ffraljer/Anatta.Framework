namespace Anatta.Framework.Graphics.Rendering;

public class Batcher {
    private readonly List<RenderCommand> _commands = new();

    public void Add(RenderCommand cmd) => _commands.Add(cmd);

    public IReadOnlyList<RenderCommand> Commands => _commands;

    public void Clear() => _commands.Clear();
}