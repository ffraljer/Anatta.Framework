namespace Anatta.Framework.Graphics.Sprites;

public static class ContainerExtensions {
    public static Container Wrap(this Container container, Drawable drawable) {
        container.Add(drawable);
        return container;
    }
}