using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.UserInterface;
using Tetris.Game.Config;


namespace Tetris.Game.Menu.Ui.Online;

public partial class OnlineSettngsSection : CompositeDrawable
{
    SettingsTextBox ipBox;
    SettingsTextBox portBox;

    [BackgroundDependencyLoader]
    private void load()
    {
        AutoSizeAxes = Axes.Y;
        RelativeSizeAxes = Axes.X;
        InternalChild = new FillFlowContainer()
        {
            Direction = FillDirection.Vertical,
            AutoSizeAxes = Axes.Y,
            RelativeSizeAxes = Axes.X,
            Children = new Drawable[]
            {
                new HeaderSpriteText() { Text = "Online Settings", },
                ipBox = new SettingsTextBox()
                {
                    PlaceholderText = "Server IP",
                    Text = GameConfigManager.OnlineConfig[OnlineSetting.Ip],
                },
                portBox = new SettingsTextBox()
                {
                    PlaceholderText = "Server Port",
                    Text = GameConfigManager.OnlineConfig[OnlineSetting.Port],
                },
            }
        };

        ipBox.OnCommit += (sender, newText) =>
        {
            GameConfigManager.OnlineConfig[OnlineSetting.Ip] = ipBox.Text;
        };
        portBox.OnCommit += (sender, newText) =>
        {
            GameConfigManager.OnlineConfig[OnlineSetting.Port] = portBox.Text;
        };
    }
}
