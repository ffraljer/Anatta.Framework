using Anatta.Framework.Graphics;

namespace Anatta.Framework.Graphics.Interfaces;

public interface IHasGradients {
    ColourInfo? Gradient { get; set; }

    ColourInfo? BorderGradient { get; set; }
}