using osu.Framework.Graphics;
using osu.Framework.Graphics.UserInterface;

namespace Tetris.Game.Menu.Ui.Settings;

public partial class SettingsTextBox : BasicTextBox
{
    public SettingsTextBox()
    {
        CommitOnFocusLost = true;
        Anchor = Anchor.TopCentre;
        Origin = Anchor.TopCentre;
        CornerRadius = 15;
        Margin = new MarginPadding(5);
        RelativeSizeAxes = Axes.X;
        Height = 50;
    }
}
