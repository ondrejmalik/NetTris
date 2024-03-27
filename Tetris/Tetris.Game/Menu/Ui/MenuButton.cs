using System;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.UserInterface;
using osu.Framework.Input.Events;
using osuTK;

namespace Tetris.Game;

public partial class MenuButton : BasicButton
{
    Vector2 _baseSize;
    public Action Action { get; set; }

    public Vector2 BaseSize
    {
        get
        {
            return _baseSize;
        }
        set
        {
            _baseSize = value;
            Size = value;
        }
    }

    public FontUsage Font
    {
        get
        {
            return SpriteText.Font;
        }
        set
        {
            SpriteText.Font = value;
        }
    }

    public Vector2 HoveredSize { get; set; }

    public MenuButton()
    {
        Font = new FontUsage(size: 30, weight: "bold");
        BaseSize = new Vector2(250, 75);
        HoveredSize = new Vector2(325, 75);
        Anchor = Anchor.Centre;
        Origin = Anchor.Centre;
    }

    protected override bool OnHover(HoverEvent e)
    {
        this.ResizeTo(new Vector2(HoveredSize.X, HoveredSize.Y), 100, Easing.OutQuad);
        return base.OnHover(e);
    }

    protected override void OnHoverLost(HoverLostEvent e)
    {
        this.ResizeTo(new Vector2(BaseSize.X, HoveredSize.Y), 100, Easing.OutQuad);
        base.OnHoverLost(e);
    }

    protected override bool OnClick(ClickEvent e)
    {
        Action?.Invoke();
        return base.OnClick(e);
    }
}
