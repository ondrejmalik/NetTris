using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osuTK;
using Tetris.Game.Realm;


namespace Tetris.Game.Menu.Ui.Leaderboard;

public partial class LeaderboardScore : CompositeDrawable
{
    public RealmScore Score;

    private Container box;
    private SpriteText playerText;
    private SpriteText scoreText;
    private SpriteText dateText;

    public LeaderboardScore(RealmScore score)
    {
        Score = score;
    }

    [BackgroundDependencyLoader]
    private void load()
    {
        AutoSizeAxes = Axes.Both;
        InternalChild = box = new Container()
        {
            AutoSizeAxes = Axes.Both,
            Anchor = Anchor.TopLeft,
            Origin = Anchor.TopLeft,
            CornerRadius = 20,
            BorderColour = Colour4.DimGray,
            BorderThickness = 4,
            Masking = true,
            //Alpha = 0.5f,
            Children = new Drawable[]
            {
                new Box()
                {
                    Alpha = 0.8f,
                    Colour = new Colour4((byte)25, (byte)25, (byte)40, byte.MaxValue),
                    RelativeSizeAxes = Axes.Y,
                    Width = 260,
                },
                new FillFlowContainer()
                {
                    RelativeSizeAxes = Axes.X,
                    AutoSizeAxes = Axes.Y,
                    Direction = FillDirection.Vertical,
                    Children = new Drawable[]
                    {
                        new Container()
                        {
                            AutoSizeAxes = Axes.Y,
                            RelativeSizeAxes = Axes.X,
                            Children = new Drawable[]
                            {
                                playerText = new SpriteText
                                {
                                    Text = Score.PlayerName ?? "Unknown",
                                    MaxWidth = 120,
                                    Anchor = Anchor.TopLeft,
                                    Origin = Anchor.TopLeft,
                                    Margin = new MarginPadding(10),
                                    Font = new FontUsage(size: 30),
                                },
                                scoreText = new SpriteText
                                {
                                    Text = Score.Score.ToString() + " lines",
                                    Anchor = Anchor.TopRight,
                                    Origin = Anchor.TopRight,
                                    Margin = new MarginPadding(10),
                                    Font = new FontUsage(size: 30),
                                },
                            }
                        },

                        dateText = new SpriteText
                        {
                            Text = Score.Date.Day + "/" + Score.Date.Month + "/" + Score.Date.Year + " " +
                                   Score.Date.Hour + ":" + Score.Date.Minute + ":" + Score.Date.Second,
                            Margin = new MarginPadding(10),
                            Font = new FontUsage(size: 20),
                        },
                    },
                },
            }
        };
    }
}
