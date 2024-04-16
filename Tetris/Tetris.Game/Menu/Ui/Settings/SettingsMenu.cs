using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Configuration;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Platform;
using osuTK;
using Tetris.Game.Config;
using Tetris.Game.Menu.Ui.Controls;
using Tetris.Game.Menu.Ui.Online;

namespace Tetris.Game.Menu.Ui;

public partial class SettingsMenu : CompositeDrawable
{
    BasicScrollContainer box;
    private float relativeHeight;

    public SettingsMenu()
    {
        this.relativeHeight = relativeHeight;
    }

    [BackgroundDependencyLoader]
    private void load()
    {
        // heh :DDD host.Window.Hide();
        Padding = new MarginPadding(25);
        Anchor = Anchor.TopLeft;
        Origin = Anchor.TopLeft;

        InternalChild = box = new BasicScrollContainer()
        {
            Height = relativeHeight,
            Width = 520,
            Child = new FillFlowContainer()
            {
                Direction = FillDirection.Vertical,
                Spacing = new osuTK.Vector2(0, 10),
                AutoSizeAxes = Axes.Both,
                Children = new Drawable[]
                {
                    new UserSettingsSection(),
                    new GraphicsSettingsSection(),
                    new ControlsSettingsSection(),
                    new OnlineSettngsSection(),
                }
            }
        };

        Position = new osuTK.Vector2(-1500, 0);
        Show();
    }

    protected override void Update()
    {
        if (Parent != null) box.Height = Parent.DrawSize.Y - 50; // resizing to fit the parent height
        base.Update();
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
