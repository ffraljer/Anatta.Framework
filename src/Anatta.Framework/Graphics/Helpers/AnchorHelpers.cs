using OpenTK.Mathematics;

namespace Anatta.Framework.Graphics.Helpers;

public static class AnchorHelper {
    public static Vector2 ToNormalised(Anchors anchor)
    {
        return anchor switch
        {
            Anchors.TopLeft => new Vector2(0f, 0f),
            Anchors.Top => new Vector2(0.5f, 0f),
            Anchors.TopRight => new Vector2(1f, 0f),
            Anchors.CentreLeft => new Vector2(0f, 0.5f),
            Anchors.Centre => new Vector2(0.5f, 0.5f),
            Anchors.CentreRight => new Vector2(1f, 0.5f),
            Anchors.BottomLeft => new Vector2(0f, 1f),
            Anchors.Bottom => new Vector2(0.5f, 1f),
            Anchors.BottomRight => new Vector2(1f, 1f),
            _ => Vector2.Zero
        };
    }
}