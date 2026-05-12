using OpenTK.Mathematics;

namespace Anatta.Framework.Graphics.Animations;

public class Transformation {
    public enum Type {
        Move, Scale, Rotate, Colour, Fade
    }
    
    public Type TransformType;

    public Vector2 VecStart;
    public Vector2 VecEnd;

    public Colour4 ColStart;
    public Colour4 ColEnd;
    
    public float FloatStart;
    public float FloatEnd;

    public float StartTime; // I question myself every day.
    public float EndTime;

    public Easing Easing;
    
    public Transformation(Type type, Vector2 start, Vector2 end, float startTime, float endTime, Easing easing = Easing.None) {
        TransformType = type;
        VecStart = start;
        VecEnd = end;
        StartTime = startTime;
        EndTime = endTime;
        Easing = easing;
    }
    
    public Transformation(Type type, Colour4 start, Colour4 end, float startTime, float endTime, Easing easing = Easing.None) {
        TransformType = type;
        ColStart = start;
        ColEnd = end;
        StartTime = startTime;
        EndTime = endTime;
        Easing = easing;
    }


    public Transformation(Type type, float start, float end, float startTime, float endTime, Easing easing = Easing.None) {
        TransformType = type;
        FloatStart = start;
        FloatEnd = end;
        StartTime = startTime;
        EndTime = endTime;
        Easing = easing;
    }
}