using OpenTK.Mathematics;

namespace Demo;

class Program
{
    static void Main(string[] args)
    {
        using var g = new GameBase(new Vector2i(1280, 720));
        g.Run();
    }
}