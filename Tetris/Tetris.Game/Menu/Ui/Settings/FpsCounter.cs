using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.UserInterface;
using osu.Framework.Platform;
using osuTK;


namespace Tetris.Game.Menu.Ui;

public partial class FpsCounter : CompositeDrawable
{
    [Resolved] private GameHost host { get; set; }
    Container box;
    private SpriteText DrawFpsText;
    private SpriteText UpdateFpsText;

    [BackgroundDependencyLoader]
    private void load()
    {
        //host.MaximumDrawHz = 0;
        //host.MaximumUpdateHz = 0;
        AutoSizeAxes = Axes.Both;

        InternalChild = box = new Container
        {
            CornerRadius = 20,
            BorderColour = Colour4.DarkGray,
            BorderThickness = 10,
            CornerExponent = 2,
            Masking = true,
            AutoSizeAxes = Axes.Both,
            Anchor = Anchor.TopLeft,
            Origin = Anchor.TopLeft,

            Children = new Drawable[]
            {
                new Box
                {
                    Colour = Colour4.Gray,
                    Size = new Vector2( 130, 75),

                },
                DrawFpsText = new SpriteText
                {
                    Margin = new MarginPadding(10),
                    Anchor = Anchor.TopCentre,
                    Origin = Anchor.TopCentre,
                    Font = new FontUsage(size: 25),
                },
                UpdateFpsText = new SpriteText
                {
                    Margin = new MarginPadding(10),
                    Anchor = Anchor.TopCentre,
                    Origin = Anchor.TopCentre,
                    Font = new FontUsage(size: 25),
                    Y = 30,
                },
            }
        };
    }

    protected override void Update()
    {
        DrawFpsText.Text = $"FPS: {host.DrawThread.Clock.FramesPerSecond}";
        UpdateFpsText.Text = $"UPS: {host.UpdateThread.Clock.FramesPerSecond}";
        base.Update();
    }
}
