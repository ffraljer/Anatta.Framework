using Anatta.Framework.Configuration;
using Anatta.Framework.Graphics.Shapes;
using Anatta.Framework.Graphics;
using Anatta.Framework.Graphics.Sprites;
using OpenTK.Mathematics;

namespace Demo.Screens;

public class Checkbox : Container {
    public Bindable<bool> Current { get; set; }

    public event Action<bool>? OnCheckChange;

    private readonly Box _border;
    private readonly Box _fill;
    private readonly Text _label;

    public Checkbox(string text) : this(new Bindable<bool>(false), text) { }

    public Checkbox(Bindable<bool> current, string text) {
        Current = current;

        _border = new Box {
            Size = new Vector2(24, 24),
            CornerRadius = 4,
            BorderThickness = 2,
        };

        _fill = new Box {
            Size = new Vector2(14, 14),
            CornerRadius = 2,
            Origin = Anchors.Centre,
            Anchor = Anchors.Centre,
        };

        _label = new Text(text, 14f, Colour4.White) {
            Position = new Vector2(32, 4),
        };

        _border.OnClick += _ => { Current.Value = !Current.Value; };

        Current.ValueChanged += v => {
            _fill.FadeTo(v ? 1f : 0f, 0.1f);
            OnCheckChange?.Invoke(v);
        };

        Current.Value = false;

        Add(_border);
        Add(_fill);
        Add(_label);
    }

    public override Vector2 GetSize() => _border.GetSize();
}