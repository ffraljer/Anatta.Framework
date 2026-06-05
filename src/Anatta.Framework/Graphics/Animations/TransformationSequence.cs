namespace Anatta.Framework.Graphics.Animations;

public class TransformationSequence {
    public bool Loop;
    public List<Transformation> Transformations = new();

    public TransformationSequence(IEnumerable<Transformation> transformations, bool loop = false) {
        Transformations = transformations.ToList();
        Loop = loop;
    }
}