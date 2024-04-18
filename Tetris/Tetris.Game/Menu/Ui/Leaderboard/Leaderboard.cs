using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Logging;
using osuTK;
using Realms;
using Tetris.Game.Realm;


namespace Tetris.Game.Menu.Ui.Leaderboard;

public partial class Leaderboard : CompositeDrawable
{
    public List<RealmScore> Scores;
    private Container box;
    private BasicScrollContainer ScrollBox;
    private FillFlowContainer ffContainer;
    private SpriteText title;

    [BackgroundDependencyLoader]
    private void load()
    {
        Padding = new MarginPadding(25);
        Anchor = Anchor.CentreRight;
        Origin = Anchor.CentreRight;
        CornerRadius = 40;
        Masking = true;
        AutoSizeAxes = Axes.Both;
        InternalChild = box = new Container()
        {
            CornerRadius = 40,
            Masking = true,
            AutoSizeAxes = Axes.Both,
            Children = new Drawable[]
            {
                new Box()
                {
                    Colour = new Colour4((byte)32, (byte)32, (byte)42, byte.MaxValue),
                    RelativeSizeAxes = Axes.Both,
                    Alpha = 0.5f,
                },
                ScrollBox = new BasicScrollContainer()
                {
                    Height = 500,
                    Child = new Container()
                    {
                        AutoSizeAxes = Axes.Both,
                        Children = new Drawable[]
                        {
                            title = new SpriteText
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
                    }
                }
            }
        };
        ScrollBox.Margin = ScrollBox.Margin with { Vertical = 5 };
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();
        new Task(UpdateScores).Start();
        stopwatch.Stop();
        Logger.Log($"Leaderboard loaded in {stopwatch.ElapsedMilliseconds}ms");
        Show();
    }

    protected override void Update()
    {
        ScrollBox.Width = Math.Max(ffContainer.Width + ffContainer.Margin.Left * 2,
            title.Width + title.Margin.Left * 2);
        base.Update();
    }

    public override void Show()
    {
        base.Show();
        this.MoveTo(new Vector2(0, 0), 250, Easing.OutQuint);
    }

    public override void Hide()
    {
        this.MoveTo(new Vector2(1500, -200), 250, Easing.InQuint).Then().Delay(256).OnComplete(_ => base.Hide());
    }

    public void ToggleShow()
    {
        if (Position.X == 1500)
        {
            Show();
            Show();
        }
        else
        {
            Hide();
        }
    }

    private void UpdateScores()
    {
        var tsScores = ThreadSafeReference.Create(RealmManager.ReadScores());
        Scheduler.Add(() =>
        {
            Realms.Realm realm = RealmManager.GetRealmInstance();
            var scores = realm.ResolveReference(tsScores).ToList();
            ffContainer.Clear();
            foreach (var score in scores)
            {
                ffContainer.Add(new LeaderboardScore(score));
            }
        });
    }
}
