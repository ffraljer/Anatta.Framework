using OpenTK.Mathematics;

namespace Anatta.Framework; 
public class WindowManager {
    internal Action? OnResize;

    public int Width;

    public int Height;

    public Vector2i Size {
        get => new Vector2i(Width, Height);
        set {
            Width = value.X;
            Height = value.Y;
        }
    }

    public int SpriteRes;

    public int WidthScaled => (int)Math.Ceiling((float)Width / Ratio);

    public int HeightScaled => (int)Math.Ceiling((float)Height / Ratio);

    public float Ratio => (float)Height / 480f;
		
    public int NonWidescreenOffsetX => Math.Max(0, (int)(((float)Width - (float)Height * 4f / 3f) / 2f));

    public float OffsetXScaled => (float)NonWidescreenOffsetX / Ratio;

    public float WidthWidescreenRatio => (float)WidthScaled / 640f;
		
    public bool IsWidescreen => NonWidescreenOffsetX > 0;

    public float RatioScaleDown => 1f / Ratio;

    public float RatioInverse => (float)Height / (float)SpriteRes;
    public WindowManager() {
        Width = 640;
        Height = 480;
        SpriteRes = 768;
    }

    public Vector2 ApplyRatio(Vector2 vec) {
        return vec * Ratio + new Vector2(NonWidescreenOffsetX, 0f);
    }


    public void Resize(int width, int height) {
        if ((float)height > (float)width * 0.8f) {
            height = (int)((float)width * 0.8f);
        }
        Width = width;
        Height = height;
        
        OnResize?.Invoke();
    }
}
