using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using Tetris.Game.Config;
using Tetris.Game.Realm;

namespace Tetris.Game.Menu.Ui.Settings.Online;

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
            Anchor = Anchor.TopCentre,
            Origin = Anchor.TopCentre,
            Direction = FillDirection.Vertical,
            AutoSizeAxes = Axes.Y,
            Width = 90 * 4, // this is the base size of render switcher
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
            RealmManager.SaveConfig();
        };
        portBox.OnCommit += (sender, newText) =>
        {
            GameConfigManager.OnlineConfig[OnlineSetting.Port] = portBox.Text;
            RealmManager.SaveConfig();
        };
    }
}
