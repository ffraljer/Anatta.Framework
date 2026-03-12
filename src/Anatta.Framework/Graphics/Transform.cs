using System;

namespace Anatta.Framework.Graphics;

public abstract class Transform {
    protected float time;
    protected readonly float duration;

    protected Transform(float duration) {
        this.duration = duration;
    }

    public bool Finished => time >= duration;

    public void Update(float deltaTime) {
        time += deltaTime;
        float t = Math.Clamp(time / duration, 0f, 1f);
        Apply(t);
    }

    protected abstract void Apply(float t);
}