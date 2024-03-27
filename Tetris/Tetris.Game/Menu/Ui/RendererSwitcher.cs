using System;
using osu.Framework.Allocation;
using osu.Framework.Configuration;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Platform;
using osuTK;


namespace Tetris.Game;

public partial class RendererSwitcher : CompositeDrawable
{
    public Vector2 ButtonsBaseSize = new Vector2(90, 50);
    public Vector2 ButtonsHoveredSize = new Vector2(100, 50);

    private Container box;
    private MenuButton[] buttons = new MenuButton[5];
    private SpriteText disclaimerText;

    [BackgroundDependencyLoader]
    private void load(FrameworkConfigManager configManager, GameHost host)
    {
        AutoSizeAxes = Axes.Both;
        Anchor = Anchor.TopCentre;
        Origin = Anchor.TopCentre;
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
                    Direction = FillDirection.Horizontal,
                    Spacing = new Vector2(0, 50),
                    Children = new Drawable[]
                    {
                        new SpriteText()
                        {
                            Text = "Renderer:",
                            Font = new FontUsage(size: 25),
                            Origin = Anchor.Centre,
                            Anchor = Anchor.Centre,
                            Margin = new MarginPadding(25),
                        },
                        buttons[0] = new MenuButton()
                        {
                            Text = "OpenGL",
                            Font =  new FontUsage(size: 25),
                            Action = () => SwitchRenderer(RendererType.OpenGL, configManager),
                            BaseSize = ButtonsBaseSize,
                            HoveredSize = ButtonsHoveredSize
                        },
                        buttons[2] = new MenuButton()
                        {
                            Text = "DirectX",
                            Font =  new FontUsage(size: 25),
                            Action = () => SwitchRenderer(RendererType.Direct3D11, configManager),
                            BaseSize = ButtonsBaseSize,
                            HoveredSize = ButtonsHoveredSize
                        },
                        buttons[1] = new MenuButton()
                        {
                            Text = "Vulkan",
                            Font =  new FontUsage(size: 25),
                            Action = () => SwitchRenderer(RendererType.Vulkan, configManager),
                            BaseSize = ButtonsBaseSize,
                            HoveredSize = ButtonsHoveredSize
                        },
                        buttons[3] = new MenuButton()
                        {
                            Text = "Metal",
                            Font =  new FontUsage(size: 25),
                            Action = () => SwitchRenderer(RendererType.Metal, configManager),
                            BaseSize = ButtonsBaseSize,
                            HoveredSize = ButtonsHoveredSize
                        },
                        buttons[3] = new MenuButton()
                        {
                            Text = "Auto",
                            Font =  new FontUsage(size: 25),
                            Action = () => SwitchRenderer(RendererType.Automatic, configManager),
                            BaseSize = ButtonsBaseSize,
                            HoveredSize = ButtonsHoveredSize
                        },
                        disclaimerText = new SpriteText()
                        {
                            Origin = Anchor.Centre,
                            Anchor = Anchor.Centre,
                            Margin = new MarginPadding(25),
                        }
                    }
                }
            }
        };
    }

    private Action SwitchRenderer(RendererType rendererType, FrameworkConfigManager configManager)
    {
        configManager.SetValue(FrameworkSetting.Renderer, rendererType);
        disclaimerText.Text = "*requires restart*";
        return null;
    }
}
