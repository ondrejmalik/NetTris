using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using Tetris.Game.Config;
using Tetris.Game.Realm;


namespace Tetris.Game.Menu.Ui.Online;

public partial class UserSettingsSection : CompositeDrawable
{
    SettingsTextBox usernameBox;

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
            }
        };
        usernameBox.OnCommit += (sender, newText) =>
        {
            RealmManager.SaveConfig();
            GameConfigManager.UserConfig[UserSetting.Username] = usernameBox.Text;
        };
    }
}
