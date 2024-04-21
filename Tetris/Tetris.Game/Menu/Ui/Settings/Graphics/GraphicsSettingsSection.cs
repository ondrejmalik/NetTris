using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Sprites;

namespace Tetris.Game.Menu.Ui.Settings.Graphics;

public partial class GraphicsSettingsSection : CompositeDrawable
{
    Container box;
    public SpriteText SettingsTitleText;

    [BackgroundDependencyLoader]
    private void load()
    {
        Anchor = Anchor.TopCentre;
        Origin = Anchor.TopCentre;
        AutoSizeAxes = Axes.Both;
        InternalChild = box = new Container
        {
            AutoSizeAxes = Axes.Both,
            Anchor = Anchor.TopCentre,
            Origin = Anchor.TopCentre,
            Children = new Drawable[]
            {
                new FillFlowContainer()
                {
                    AutoSizeAxes = Axes.Both,
                    Direction = FillDirection.Vertical,
                    Anchor = Anchor.TopCentre,
                    Origin = Anchor.TopCentre,
                    Children = new Drawable[]
                    {
                        SettingsTitleText = new HeaderSpriteText() { Text = "Graphics" },
                        new RendererSwitcher()
                    }
                }
            }
        };
    }
}
