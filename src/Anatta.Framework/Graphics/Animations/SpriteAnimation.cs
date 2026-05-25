using Anatta.Framework.Graphics.Sprites;
using Anatta.Framework.Threading;
using OpenTK.Mathematics;

namespace Anatta.Framework.Graphics.Animations;

public class SpriteAnimation() : Sprite(null!) {
    private class Frame(Texture texture, float duration) {
        public Texture Texture { get; } = texture;
        public float Duration { get; } = duration;
    }

    private readonly List<Frame> _frames = new();
    private int _frameIndex;
    private float _time;
    public float DefaultFrameDuration { get; set; } = 0.1f;

    public void AddFrame(Texture glTexture, float? duration = null) {
        _frames.Add(new Frame(glTexture, duration ?? DefaultFrameDuration));

        if (_frames.Count == 1)
            Texture = glTexture;
    }

    public override void Update() {
        base.Update();

        if (_frames.Count <= 0) return;

        var current = _frames[_frameIndex];

        _time = Time.Delta;

        if (_time >= current.Duration) {
            _time -= current.Duration;
            _frameIndex = (_frameIndex + 1) % _frames.Count;
            Texture = _frames[_frameIndex].Texture;
        }
    }

    public override void Dispose() {
        foreach (var frame in _frames) {
            frame.Texture.Dispose();
        }
    }

    public override Vector2 GetSize() {
        if (_frames.Count == 0) return Vector2.Zero;
        return new Vector2(
        _frames[_frameIndex].Texture.Width,
        _frames[_frameIndex].Texture.Height
            );
    }

}