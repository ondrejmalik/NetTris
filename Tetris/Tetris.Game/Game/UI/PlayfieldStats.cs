using System;
using System.Linq;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osuTK;
using Tetris.Game.Game.Playfield;

namespace Tetris.Game.Game.UI;

public partial class PlayfieldStats(PlayField playField) : CompositeDrawable
{
    Container box;
    PlayField playField = playField;
    private GameUiSpriteText levelText;
    private GameUiSpriteText linesClearedText;
    private GameUiSpriteText cpmText;
    private GameUiSpriteText timeText;
    private double deltaTime = 0;

    [BackgroundDependencyLoader]
    private void load()
    {
        playField.ClearedLinesChanged += handleValueChanged;
        AutoSizeAxes = Axes.Both;
        InternalChild = box = new Container
        {
            CornerRadius = 40,
            Masking = true,
            AutoSizeAxes = Axes.Both,


            Children = new Drawable[]
            {
                new Box()
                {
                    RelativeSizeAxes = Axes.Both,
                    Colour = new Colour4(16, 16, 21, 255),
                },
                new FillFlowContainer()
                {
                    Direction = FillDirection.Vertical,
                    Spacing = new Vector2(0, 20),
                    Margin = new MarginPadding(20),
                    AutoSizeAxes = Axes.Both,
                    Children = new Drawable[]
                    {
                        new GameUiSpriteText() { Text = "Stats", },
                        levelText = new GameUiSpriteText(),
                        linesClearedText = new GameUiSpriteText(),
                        cpmText = new GameUiSpriteText(),
                        timeText = new GameUiSpriteText(),
                    },
                }
            },
        };
        handleValueChanged(null, null);
        periodicTextUdpate();
    }

    #region UpdateContent

    protected override void Update()
    {
        deltaTime += Clock.ElapsedFrameTime;
        if (deltaTime >= 1000)
        {
            periodicTextUdpate();
            deltaTime = 0;
        }

        base.Update();
    }

    private void periodicTextUdpate()
    {
        cpmText.Text = $"CPM: {Math.Round(playField.Cpm, 2)}";
        timeText.Text = $"Time: {playField.TimeInSeconds} sec";
    }

    #endregion

    private void handleValueChanged(object sender, EventArgs e)
    {
        linesClearedText.Text = $"Lines Cleared: {playField.ClearedLines}";
        levelText.Text = $"Level: {playField.Level}";
    }
}
