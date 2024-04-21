using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using Tetris.Game.Config;

namespace Tetris.Game.Menu.Ui.Settings.Controls;

public partial class ControlsSettingsSection : CompositeDrawable
{
    [BackgroundDependencyLoader]
    private void load()
    {
        AutoSizeAxes = Axes.Both;
        InternalChild = new FillFlowContainer()
        {
            AutoSizeAxes = Axes.Both,
            Direction = FillDirection.Vertical,
            Spacing = new osuTK.Vector2(0, 10),
            Children = new Drawable[]
            {
                new HeaderSpriteText() { Text = "Controls" },
                new FillFlowContainer()
                {
                    AutoSizeAxes = Axes.Both,
                    Direction = FillDirection.Horizontal,
                    Anchor = Anchor.TopCentre,
                    Origin = Anchor.TopCentre,
                    Children = new Drawable[]
                    {
                        new KeyBindsSection(GameConfigManager.GameControlsConfig, "Game Controls"),
                        new KeyBindsSection(GameConfigManager.OpponentControlsConfig, "Opponent Controls")
                    }
                }
            }
        };
    }
}
