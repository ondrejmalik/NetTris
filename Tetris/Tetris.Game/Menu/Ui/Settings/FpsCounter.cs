using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Input.Events;
using osu.Framework.Platform;
using osuTK.Input;
using Tetris.Game.Networking;

namespace Tetris.Game.Menu.Ui.Settings;

/// <summary>
/// Displays the current FPS, UPS, and network pool rate.
/// </summary>
public partial class FpsCounter : CompositeDrawable
{
    [Resolved] private GameHost host { get; set; }
    Container box;
    private SpriteText DrawFpsText;
    private SpriteText UpdateFpsText;
    private SpriteText LastMsText;
    private double deltaTime;

    [BackgroundDependencyLoader]
    private void load()
    {
        AutoSizeAxes = Axes.Both;
        Anchor = Anchor.BottomRight;
        Origin = Anchor.BottomRight;
        InternalChild = box = new Container
        {
            CornerRadius = 20,
            BorderColour = Colour4.DarkGray,
            BorderThickness = 10,
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
                            Margin = new MarginPadding() { Top = 2, Left = 5, Right = 5 },
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
                            Margin = new MarginPadding() { Bottom = 2, Left = 5, Right = 5 },
                            Anchor = Anchor.TopCentre,
                            Origin = Anchor.TopCentre,
                            Font = new FontUsage(size: 20),
                        },
                    }
                }
            }
        };
    }

    protected override void Update()
    {
        deltaTime += host.DrawThread.Clock.ElapsedFrameTime;
        if (deltaTime < 5000) // update counter every 1000ms
        {
            DrawFpsText.Text = $"FPS: {host.DrawThread.Clock.FramesPerSecond}";
            UpdateFpsText.Text = $"UPS: {host.UpdateThread.Clock.FramesPerSecond}";
            LastMsText.Text = $"Net: {NetworkHandler.LastMs} ms";
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

        return base.OnKeyDown(e);
    }

    public void ToggleVisibility(bool visible)
    {
        if (visible)
        {
            Show();
        }
        else
        {
            Hide();
        }
    }
}
