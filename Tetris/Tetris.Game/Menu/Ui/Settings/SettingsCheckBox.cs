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
        InternalChild = box = new FillFlowContainer()
        {
            CornerRadius = 40,
            Masking = true,
            AutoSizeAxes = Axes.Both,
            Children = new Drawable[]
            {
                LabelText,
                ShowFpsCheckbox,
            }
        };
    }
}
