using System;
using System.Net;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Sprites;
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
    private SpriteText errorText;

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
                errorText = new HeaderSpriteText()
                {
                    Colour = Colour4.Red,
                },
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
            try
            {
                var ip = IPAddress.Parse(ipBox.Text);
                GameConfigManager.OnlineConfig[OnlineSetting.Ip] = ip.ToString();
                RealmManager.SaveConfig();
                errorText.Text = "";
            }
            catch (Exception e)
            {
                errorText.Text = "Invalid IP";
            }
        };
        portBox.OnCommit += (sender, newText) =>
        {
            try
            {
                int port = int.Parse(portBox.Text);
                GameConfigManager.OnlineConfig[OnlineSetting.Port] = portBox.Text;
                RealmManager.SaveConfig();
                errorText.Text = "";
            }
            catch (Exception e)
            {
                errorText.Text = "Invalid Port";
            }
        };
        tickRateBox.OnCommit += (sender, newText) =>
        {
            try
            {
                int tickRate = int.Parse(tickRateBox.Text);
                GameConfigManager.OnlineConfig[OnlineSetting.TickRate] = tickRate.ToString();
                RealmManager.SaveConfig();
                errorText.Text = "";
            }
            catch (Exception e)
            {
                errorText.Text = "Invalid Tick Rate";
            }
        };
    }
}
