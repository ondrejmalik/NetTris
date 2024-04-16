using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using Tetris.Game.Config;


namespace Tetris.Game.Menu.Ui.Controls;

public partial class ControlsRemapSection : CompositeDrawable
{
    [BackgroundDependencyLoader]
    private void load()
    {
        AutoSizeAxes = Axes.Both;
        InternalChild = new FillFlowContainer()
        {
            AutoSizeAxes = Axes.Both,
            Direction = FillDirection.Horizontal,
            Spacing = new osuTK.Vector2(10),
            Anchor = Anchor.TopCentre,
            Origin = Anchor.TopCentre,
            Children = new Drawable[]
            {
                new KeyBindsSection(GameConfigManager.GameControlsConfig, "Game Controls"),
                new KeyBindsSection(GameConfigManager.OpponentControlsConfig, "Opponent Controls")
            }
        };
    }
}
