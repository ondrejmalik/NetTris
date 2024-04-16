using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Sprites;
using osuTK;

namespace Tetris.Game.Menu.Ui;

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
