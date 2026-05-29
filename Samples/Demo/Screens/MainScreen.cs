using Anatta.Framework;
using Anatta.Framework.Configuration;
using Anatta.Framework.Graphics;
using Anatta.Framework.Graphics.Animations;
using Anatta.Framework.Graphics.Interfaces;
using Anatta.Framework.Graphics.Shapes;
using Anatta.Framework.Graphics.Sprites;
using Anatta.Framework.Storage;
using Anatta.Framework.Sound;
using Anatta.Framework.Threading;
using OpenTK.Mathematics;

namespace Demo.Screens;

public class MainScreen : Screen {
    Text player;
    private Sprite mrladybug;
    private Track _music;

    protected override void Load() {
        mrladybug = new(Resource.Load<Texture>("pillowpetladybug2")) {
            Anchor = Anchors.Centre,
            Origin = Anchors.Centre,
            Scale = new(3)
        };
        mrladybug.OnHover += delegate { mrladybug.ScaleTo(new(3.2f), 3); };
        mrladybug.OnHoverLost += delegate { mrladybug.ScaleTo(new(3f), 3, Easing.OutCubic); };
        Add(mrladybug);
        AddMenuButton("Play",    new(0, -50),  Colour4.Green, _ => { GameBase.ScreenStack.Push(new KittyScreen()); });
        AddMenuButton("Options", new(0, -170), Colour4.Gray, _ => { GameBase.ScreenStack.Push(new Options()); });
        AddMenuButton("Quit",    new(0, -290), Colour4.Red, _ => { GameBase.Instance.Exit(); });
    }

    private void AddMenuButton(string name, Vector2 pos, Colour4 colour, Action<IDrawable> onClick) {
        var pSprite = new Box() {
            Size = new(300, 100),
            Colour = colour,
            BorderColour = Colour4.Black,
            Position = pos,
            BorderThickness = 2,
            Anchor = Anchors.Bottom,
            Origin = Anchors.Centre
        };
        pSprite.OnClick += onClick;
        pSprite.OnHover += delegate { pSprite.ScaleTo(new(1.2f), 3); };
        pSprite.OnHoverLost += delegate { pSprite.ScaleTo(new(1), 3, Easing.OutCubic); };
        var pText = new Text(name, 14f, Colour4.White) {
            Anchor = Anchors.Bottom,
            Origin = Anchors.Centre,
            Position = pSprite.Position
        };

        Add(pSprite);
        Add(pText);
    }

    public override void Update() {
        base.Update();
    }
}