using Anatta.Framework.Graphics;

namespace Anatta.Framework.Interfaces.Graphics;

public interface IHasGradients {
    ColourInfo? Gradient { get; set; }

    ColourInfo? BorderGradient { get; set; }
}