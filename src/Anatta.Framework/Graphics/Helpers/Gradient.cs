using OpenTK.Mathematics;

namespace Anatta.Framework.Graphics.Helpers;

public struct Gradient {
    public Colour Start;
    public Colour End;

    public Vector2 Direction;

    public Gradient(Colour start, Colour end, Vector2 direction) {
        Start = start;
        End = end;
        Direction = direction.Normalized();
    }
}