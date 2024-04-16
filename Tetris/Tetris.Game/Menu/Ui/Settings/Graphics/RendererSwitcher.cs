using System;
using osu.Framework.Allocation;
using osu.Framework.Configuration;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Platform;
using osuTK;

namespace Tetris.Game.Menu.Ui;

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
        AutoSizeAxes = Axes.Both;
        Margin = new MarginPadding(25);
        InternalChild = box = new Container
        {
            AutoSizeAxes = Axes.Both,
            Children = new Drawable[]
            {
                new SpriteText()
                {
                    Text = "Renderer",
                    Font = new FontUsage(size: 30),
                    Origin = Anchor.TopCentre,
                    Anchor = Anchor.TopCentre,
                },
                new FillFlowContainer()
                {
                    Position = new Vector2(0, 50),
                    CornerRadius = 13,
                    Masking = true,
                    AutoSizeAxes = Axes.Both,
                    Direction = FillDirection.Horizontal,
                    Spacing = new Vector2(0, 50),

                    Children = new Drawable[]
                    {
                        buttons[0] = new MenuButton()
                        {
                            Text = "OpenGL",
                            Origin = Anchor.Centre,
                            Anchor = Anchor.Centre,
                            Font = new FontUsage(size: 25),
                            BaseSize = ButtonsBaseSize,
                            HoveredSize = ButtonsHoveredSize,
                            Action = () => SwitchRenderer(RendererType.OpenGL, configManager)
                        },
                        buttons[1] = new MenuButton()
                        {
                            Text = "DirectX",
                            Origin = Anchor.Centre,
                            Anchor = Anchor.Centre,
                            Font = new FontUsage(size: 25),
                            BaseSize = ButtonsBaseSize,
                            HoveredSize = ButtonsHoveredSize,
                            Action = () => SwitchRenderer(RendererType.Direct3D11, configManager)
                        },
                        buttons[2] = new MenuButton()
                        {
                            Text = "Vulkan",
                            Origin = Anchor.Centre,
                            Anchor = Anchor.Centre,
                            Font = new FontUsage(size: 25),
                            BaseSize = ButtonsBaseSize,
                            HoveredSize = ButtonsHoveredSize,
                            Action = () => SwitchRenderer(RendererType.Vulkan, configManager)
                        },
                        buttons[3] = new MenuButton()
                        {
                            Text = "Metal",
                            Origin = Anchor.Centre,
                            Anchor = Anchor.Centre,
                            Font = new FontUsage(size: 25),
                            BaseSize = ButtonsBaseSize,
                            HoveredSize = ButtonsHoveredSize,
                            Action = () => SwitchRenderer(RendererType.Metal, configManager)
                        },
                        buttons[4] = new MenuButton()
                        {
                            Text = "Auto",
                            Origin = Anchor.Centre,
                            Anchor = Anchor.Centre,
                            Font = new FontUsage(size: 25),
                            BaseSize = ButtonsBaseSize,
                            HoveredSize = ButtonsHoveredSize,
                            Action = () => SwitchRenderer(RendererType.Automatic, configManager)
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
