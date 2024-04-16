using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using Tetris.Game.Config;
using Tetris.Game.Menu.Ui.Controls;

namespace Tetris.Game.Menu.Ui;

public partial class SettingsMenu : CompositeDrawable
{
    FillFlowContainer box;

    [BackgroundDependencyLoader]
    private void load()
    {
        Padding = new MarginPadding(25);
        Anchor = Anchor.TopLeft;
        Origin = Anchor.TopLeft;
        AutoSizeAxes = Axes.Both;
        InternalChild = box = new FillFlowContainer()
        {
            Direction = FillDirection.Vertical,
            Spacing = new osuTK.Vector2(0, 10),
            AutoSizeAxes = Axes.Both,
            Children = new Drawable[]
            {
                new GraphicsSettingsSection(),
                new ControlsRemapSection(),
            }
        };
        Position = new osuTK.Vector2(-1500, 0);
        Show();
    }

    public void Show()
    {
        base.Show();
        this.MoveTo(new osuTK.Vector2(0, 0), 250, Easing.OutQuint);
    }

    public override void Hide()
    {
        this.MoveTo(new osuTK.Vector2(-1500, -200), 250, Easing.InQuint).Then().Delay(250).OnComplete(_ => base.Hide());
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
