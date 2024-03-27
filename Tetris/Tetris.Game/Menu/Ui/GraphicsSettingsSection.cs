using osu.Framework.Allocation;
using osu.Framework.Configuration;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Platform;
using osuTK;


namespace Tetris.Game;

public partial class GraphicsSettingsSection : CompositeDrawable
{
    Container box;
    public SpriteText SettingsTitleText;

    [BackgroundDependencyLoader]
    private void load()
    {
        AutoSizeAxes = Axes.Both;
        InternalChild = box = new Container
        {
            AutoSizeAxes = Axes.Both,
            Anchor = Anchor.TopLeft,
            Origin = Anchor.TopLeft,
            Children = new Drawable[]
            {
                new FillFlowContainer()
                {
                    AutoSizeAxes = Axes.Both,
                    Direction = FillDirection.Vertical,
                    Spacing = new Vector2(0, 50),
                    Children = new Drawable[]
                    {
                        SettingsTitleText = new SpriteText()
                        {
                            Text = "Graphics Settings",
                            Font = new FontUsage(size: 50),
                            Origin = Anchor.TopCentre,
                            Anchor = Anchor.TopCentre,
                        },
                        new RendererSwitcher()
                    }
                }
            }
        };
    }
}
