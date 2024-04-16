using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.UserInterface;
using Tetris.Game.Config;


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
            Direction = FillDirection.Vertical,
            AutoSizeAxes = Axes.Y,
            RelativeSizeAxes = Axes.X,
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
            GameConfigManager.UserConfig[UserSetting.Username] = usernameBox.Text;
        };
    }
}
