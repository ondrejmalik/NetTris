using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osuTK;
using Realms;
using Tetris.Game.Realm;


namespace Tetris.Game.Menu.Ui.Leaderboard;

public partial class Leaderboard : CompositeDrawable
{
    public List<RealmScore> Scores;

    private Container box;
    private FillFlowContainer ffContainer;


    [BackgroundDependencyLoader]
    private void load()
    {
        Padding = new MarginPadding(25);
        Anchor = Anchor.TopRight;
        Origin = Anchor.TopRight;
        AutoSizeAxes = Axes.Both;
        InternalChild = box = new Container
        {
            AutoSizeAxes = Axes.Both,
            Anchor = Anchor.TopLeft,
            Origin = Anchor.TopLeft,
            CornerRadius = 40,
            Masking = true,
            Children = new Drawable[]
            {
                new Box()
                {
                    Colour = new Colour4((byte)32, (byte)32, (byte)42, byte.MaxValue),
                    RelativeSizeAxes = Axes.Both,
                    Alpha = 0.5f,
                    Depth = 10000,
                },
                new SpriteText
                {
                    Text = "Leaderboard",
                    Font = new FontUsage(size: 40),
                    Origin = Anchor.TopCentre,
                    Anchor = Anchor.TopCentre,
                    Margin = new MarginPadding(25),
                },
                ffContainer = new FillFlowContainer()
                {
                    AutoSizeAxes = Axes.Both,
                    Direction = FillDirection.Vertical,
                    Spacing = new Vector2(0, 10),
                    Position = new Vector2(0, 75),
                    Margin = new MarginPadding(25),
                }
            }
        };
        Scores = RealmManager.ReadScores();
        foreach (var score in Scores)
        {
            ffContainer.Add(new LeaderboardScore(score));
        }

        Position = new osuTK.Vector2(1500, 0);
    }

    public override void Show()
    {
        base.Show();
        this.MoveTo(new osuTK.Vector2(0, 200), 250, Easing.OutQuint);
    }

    public override void Hide()
    {
        this.MoveTo(new osuTK.Vector2(1500, 0), 250, Easing.InQuint).Then().Delay(250).OnComplete(_ => base.Hide());
    }

    public void ToggleShow()
    {
        if (Position.X == 1500)
        {
            Show();
        }
        else
        {
            Hide();
        }
    }
}
