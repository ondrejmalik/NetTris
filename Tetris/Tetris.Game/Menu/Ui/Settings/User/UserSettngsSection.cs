using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.UserInterface;
using Tetris.Game.Config;
using Tetris.Game.Realm;

namespace Tetris.Game.Menu.Ui.Settings.User;

/// <summary>
///  Settings Section for setting user settings.
/// </summary>
public partial class UserSettingsSection : CompositeDrawable
{
    private SettingsTextBox usernameBox;
    private SettingsCheckBox showFpsCheckbox;

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
            Width = 90 * 4,
            Children = new Drawable[]
            {
                new HeaderSpriteText() { Text = "User Settings", },
                usernameBox = new SettingsTextBox()
                {
                    PlaceholderText = "Username",
                    Text = GameConfigManager.UserConfig[UserSetting.Username],
                },
                showFpsCheckbox = new SettingsCheckBox()
                {
                    LabelText = new SpriteText()
                    {
                        Text = "Show FPS Counter",
                    },
                    ShowFpsCheckbox = new BasicCheckbox()
                    {
                        Current = new Bindable<bool>(bool.Parse(GameConfigManager.UserConfig[UserSetting.ShowFps])),
                    },
                }
            }
        };
        usernameBox.OnCommit += (sender, newText) =>
        {
            GameConfigManager.UserConfig[UserSetting.Username] = usernameBox.Text;
            RealmManager.SaveConfig();
        };
        showFpsCheckbox.ShowFpsCheckbox.Current.ValueChanged += (sender) =>
        {
            GameConfigManager.UserConfig[UserSetting.ShowFps] = sender.NewValue.ToString();
            RealmManager.SaveConfig();
        };
    }
}
