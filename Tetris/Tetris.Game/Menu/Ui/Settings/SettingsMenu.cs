using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Platform;
using Tetris.Game.Menu.Ui.Settings.Controls;
using Tetris.Game.Menu.Ui.Settings.Graphics;
using Tetris.Game.Menu.Ui.Settings.Online;
using Tetris.Game.Menu.Ui.Settings.User;

namespace Tetris.Game.Menu.Ui.Settings;

public partial class SettingsMenu : CompositeDrawable
{
    private Container box;
    private BasicScrollContainer scrollContainer;
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
        AutoSizeAxes = Axes.Both;
        InternalChild = box = new Container()
        {
            CornerRadius = 40,
            Masking = true,
            AutoSizeAxes = Axes.Both,
            Children = new Drawable[]
            {
                new Box()
                {
                    Colour = new Colour4((byte)16, (byte)16, (byte)21, (byte)255),
                    RelativeSizeAxes = Axes.Both,
                },
                scrollContainer = new BasicScrollContainer()
                {
                    Height = relativeHeight,
                    Width = 430,
                    Children = new Drawable[]
                    {
                        new FillFlowContainer()
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
                        },
                    }
                }
            }
        };

        Position = new osuTK.Vector2(-1500, 0);
        Show();
    }

    protected override void Update()
    {
        if (Parent != null) scrollContainer.Height = Parent.DrawSize.Y - 50; // resizing to fit the parent height
        base.Update();
    }

    #region Show/Hide

    public void Show()
    {
        base.Show();
        this.MoveTo(new osuTK.Vector2(0, 0), 250, Easing.OutQuint);
    }

    public override void Hide()
    {
        this.MoveTo(new osuTK.Vector2(-1500, -200), 250, Easing.InQuint).Then().Delay(250).OnComplete(_ => base.Hide());
    }

    public void ToggleShow(string baseTitle, GameHost host = null)
    {
        if (Position.X == -1500)
        {
            if (host != null) host.Window.Title = "Settings";
            Show();
        }
        else
        {
            if (host != null) host.Window.Title = baseTitle;
            Hide();
        }
    }

    #endregion
}
