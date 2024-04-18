using System;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Sprites;
using osuTK;
using Tetris.Game.Game.Playfield;

namespace Tetris.Game.Game.UI;

public partial class PlayfieldStats(PlayField playField) : CompositeDrawable
{
    Container box;
    PlayField playField = playField;
    private SpriteText linesClearedText;
    private SpriteText levelText;

    [BackgroundDependencyLoader]
    private void load()
    {
        playField.ClearedLinesChanged += handleValueChanged;
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
                    Direction = FillDirection.Vertical,
                    Spacing = new Vector2(0, 20),
                    AutoSizeAxes = Axes.Both,
                    Children = new Drawable[]
                    {
                        new SpriteText()
                        {
                            Text = "Stats",
                            Font = new FontUsage(size: 50),
                            Origin = Anchor.TopCentre,
                            Anchor = Anchor.TopCentre,
                        },
                        linesClearedText = new SpriteText()
                        {
                            Text = $"Lines Cleared: {playField.ClearedLines}",
                            Font = new FontUsage(size: 30),
                            Origin = Anchor.TopCentre,
                            Anchor = Anchor.TopCentre,
                        },
                        levelText = new SpriteText()
                        {
                            Text = $"Level: {playField.Level}",
                            Font = new FontUsage(size: 30),
                            Origin = Anchor.TopCentre,
                            Anchor = Anchor.TopCentre,
                        },
                    },
                }
            },
        };
    }

    private void handleValueChanged(object sender, EventArgs e)
    {
        linesClearedText.Text = $"Lines Cleared: {playField.ClearedLines}";
        levelText.Text = $"Level: {playField.Level}";
    }
}
