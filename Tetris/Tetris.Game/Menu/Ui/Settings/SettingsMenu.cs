using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;

namespace Tetris.Game.Menu.Ui;

public partial class SettingsMenu : CompositeDrawable
{
    Container box;

    [BackgroundDependencyLoader]
    private void load()
    {
        Padding = new MarginPadding(25);
        Anchor = Anchor.TopLeft;
        Origin = Anchor.TopLeft;
        AutoSizeAxes = Axes.Both;
        InternalChild = box = new Container
        {
            Colour = Colour4.Gold,
            AutoSizeAxes = Axes.Both,
            Anchor = Anchor.TopLeft,
            Origin = Anchor.TopLeft,
            Children = new Drawable[]
            {
                new GraphicsSettingsSection()
            }
        };
        Position = new osuTK.Vector2(-1500, 0);
    }

    public void Show()
    {
        base.Show();
        this.MoveTo(new osuTK.Vector2(0, 200), 250, Easing.OutQuint);
    }

    public override void Hide()
    {
        this.MoveTo(new osuTK.Vector2(-1500, 0), 250, Easing.InQuint).Then().Delay(250).OnComplete(_ => base.Hide());
    }

    public void ToggleShow()
    {
        if (Position.X == -1500)
        {
            Show();
        }
        else
        {
            Hide();
        }
    }
}
