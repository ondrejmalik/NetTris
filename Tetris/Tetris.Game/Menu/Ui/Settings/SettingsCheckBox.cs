using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.UserInterface;

namespace Tetris.Game.Menu.Ui.Settings;

/// <summary>
/// Checkbox and label for settings.
/// </summary>
public partial class SettingsCheckBox : CompositeDrawable
{
    private FillFlowContainer box;
    public required BasicCheckbox ShowFpsCheckbox;
    public required SpriteText LabelText;

    [BackgroundDependencyLoader]
    private void load()
    {
        AutoSizeAxes = Axes.Both;
        ShowFpsCheckbox.CornerRadius = 10;
        ShowFpsCheckbox.Masking = true;
        ShowFpsCheckbox.Anchor = Anchor.CentreLeft;
        ShowFpsCheckbox.Origin = Anchor.CentreLeft;
        LabelText.Font = new(size: 25);
        LabelText.Anchor = Anchor.CentreLeft;
        LabelText.Origin = Anchor.CentreLeft;
        LabelText.Margin = new MarginPadding() { Right = 10 };
        InternalChild = box = new FillFlowContainer()
        {
            AutoSizeAxes = Axes.Both,
            Children = new Drawable[]
            {
                LabelText,
                ShowFpsCheckbox,
            }
        };
    }
}
