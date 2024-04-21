using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using Tetris.Game.Config;
using Tetris.Game.Realm;

namespace Tetris.Game.Menu.Ui.Settings.Online;

/// <summary>
///  Settings Section for setting online settings.
/// </summary>
public partial class OnlineSettngsSection : CompositeDrawable
{
    private SettingsTextBox ipBox;
    private SettingsTextBox portBox;
    private SettingsTextBox tickRateBox;

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
                tickRateBox = new SettingsTextBox()
                {
                    PlaceholderText = "Tick Rate (hz)",
                    Text = GameConfigManager.OnlineConfig[OnlineSetting.TickRate],
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
        tickRateBox.OnCommit += (sender, newText) =>
        {
            GameConfigManager.OnlineConfig[OnlineSetting.TickRate] = tickRateBox.Text;
            RealmManager.SaveConfig();
        };
    }
}
