using System.Drawing;
using osu.Framework.Allocation;
using osu.Framework.Configuration;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Input.Events;
using osu.Framework.Platform;
using osuTK.Input;
using Tetris.Game.Config;
using Tetris.Game.Menu.Ui.Settings.User;
using Tetris.Game.Networking;

namespace Tetris.Game.Menu.Ui.Settings;

/// <summary>
/// Displays the current FPS, UPS, and network pool rate.
/// </summary>
public partial class FpsCounter : CompositeDrawable
{
    [Resolved] private GameHost host { get; set; }
    [Resolved] private FrameworkConfigManager config { get; set; }
    Container box;
    private SpriteText DrawFpsText;
    private SpriteText UpdateFpsText;
    private SpriteText LastMsText;
    private double deltaTime;

    [BackgroundDependencyLoader]
    private void load()
    {
        UserSettingsSection.ShowFpsChanged += (sender, e) => ToggleVisibility(e.ShowFps);

        AutoSizeAxes = Axes.Both;
        Anchor = Anchor.BottomRight;
        Origin = Anchor.BottomRight;
        InternalChild = box = new Container
        {
            CornerRadius = 20,
            BorderColour = Colour4.DarkGray,
            BorderThickness = 7,
            CornerExponent = 2,
            Masking = true,
            AutoSizeAxes = Axes.Both,
            Alpha = 0.5f,
            Children = new Drawable[]
            {
                new Box
                {
                    Colour = Colour4.Gray,
                    RelativeSizeAxes = Axes.Both,
                    Alpha = 0.5f,
                },
                new FillFlowContainer()
                {
                    Margin = new MarginPadding(5),
                    Anchor = Anchor.TopCentre,
                    Origin = Anchor.TopCentre,
                    Direction = FillDirection.Vertical,
                    AutoSizeAxes = Axes.Both,
                    Children = new[]
                    {
                        DrawFpsText = new SpriteText
                        {
                            Margin = new MarginPadding() { Top = 3, Left = 5, Right = 5 },
                            Anchor = Anchor.TopCentre,
                            Origin = Anchor.TopCentre,
                            Font = new FontUsage(size: 20),
                        },
                        UpdateFpsText = new SpriteText
                        {
                            Margin = new MarginPadding() { Left = 5, Right = 5 },
                            Anchor = Anchor.TopCentre,
                            Origin = Anchor.TopCentre,
                            Font = new FontUsage(size: 20),
                        },
                        LastMsText = new SpriteText
                        {
                            Margin = new MarginPadding() { Bottom = 3, Left = 5, Right = 5 },
                            Anchor = Anchor.TopCentre,
                            Origin = Anchor.TopCentre,
                            Font = new FontUsage(size: 20),
                        },
                    }
                }
            }
        };
        if (GameConfigManager.UserConfig[UserSetting.ShowFps].Equals("False"))
        {
            Hide();
        }
    }

    protected override void Update()
    {
        deltaTime += host.UpdateThread.Clock.ElapsedFrameTime;
        if (deltaTime < 5000) // update counter every 1000ms
        {
            DrawFpsText.Text = $"fps: {host.DrawThread.Clock.FramesPerSecond}";
            UpdateFpsText.Text = $"ups: {host.UpdateThread.Clock.FramesPerSecond}";
            LastMsText.Text = $"net: {NetworkHandler.LastMs} ms";
            deltaTime = 0;
        }

        base.Update();
    }

    protected override bool OnKeyDown(KeyDownEvent e)
    {
        if (e.Key == Key.F1)
        {
            host.MaximumDrawHz = 0; // unlimited fps
            host.MaximumUpdateHz = 0;
        }
        else if (e.Key == Key.F2)
        {
            config.SetValue(FrameworkSetting.WindowedSize, new Size(1280, 720));
        }
        else if (e.Key == Key.F3)
        {
            config.SetValue(FrameworkSetting.WindowedSize, new Size(640, 480));
        }


        return base.OnKeyDown(e);
    }

    public void ToggleVisibility(bool visible)
    {
        if (visible)
        {
            box.FadeIn(300, Easing.OutQuint);
            box.Delay(300).Then().OnComplete(_ => { Show(); });
        }
        else
        {
            box.FadeOut(300, Easing.OutQuint);
            box.Delay(300).Then().OnComplete(_ => { Show(); });
        }
    }
}
