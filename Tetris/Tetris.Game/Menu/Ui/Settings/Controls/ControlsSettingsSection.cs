using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using Tetris.Game.Config;


namespace Tetris.Game.Menu.Ui.Controls;

public partial class ControlsSettingsSection : CompositeDrawable
{
    [BackgroundDependencyLoader]
    private void load()
    {
        AutoSizeAxes = Axes.Y;
        RelativeSizeAxes = Axes.X;
        InternalChild = new FillFlowContainer()
        {
            AutoSizeAxes = Axes.Y,
            RelativeSizeAxes = Axes.X,
            Direction = FillDirection.Vertical,
            Spacing = new osuTK.Vector2(0, 10),
            Children = new Drawable[]
            {
                new HeaderSpriteText() { Text = "Controls" },
                new FillFlowContainer()
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
                }
            }
        };
    }
}
