using Anatta.Framework.Graphics.Sprites;
using OpenTK.Mathematics;

namespace Anatta.Framework.Graphics.Animations;

public class SpriteAnimation : Sprite {
    private class Frame {
        public Texture Texture { get; }
        public float Duration { get; }


        public Frame(Texture texture, float duration) {
            Texture = texture;
            Duration = duration;
        }
    }

    private readonly List<Frame> _frames = new();
    private int _frameIndex = 0;
    private float _time = 0;
    public float DefaultFrameDuration { get; set; } = 0.1f;

    public SpriteAnimation() : base((Texture)null!) { }

    public void AddFrame(Texture texture, float? duration = null) {
        _frames.Add(new Frame(texture, duration ?? DefaultFrameDuration));

        if (_frames.Count == 1)
            Texture = texture;
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
        foreach (var framew in _frames) {
            framew.Texture.Dispose();
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