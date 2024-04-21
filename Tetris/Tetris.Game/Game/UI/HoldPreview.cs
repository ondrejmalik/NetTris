using System.Collections.Generic;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osuTK;
using osuTK.Graphics;
using Tetris.Game.Game.Bag;
using Tetris.Game.Game.Playfield.Tetrimino;

namespace Tetris.Game.Game.UI;

public partial class HoldPreview : CompositeDrawable
{
    private FillFlowContainer ffBox;
    private Tetrimino holdTetrimino;
    private List<Tetrimino> queueTetriminos = new();
    private FillFlowContainer bagQueueContainer;
    public Hold Hold { get; set; }

    public HoldPreview(Hold hold)
    {
        Hold = hold;
    }

    [BackgroundDependencyLoader]
    private void load()
    {
        AutoSizeAxes = Axes.Both;
        InternalChild = new Container
        {
            CornerRadius = 40,
            Masking = true,
            AutoSizeAxes = Axes.Both,
            Children = new Drawable[]
            {
                new Box()
                {
                    RelativeSizeAxes = Axes.Both,
                    Colour = new Color4(16, 16, 21, 255),
                },
                ffBox = new FillFlowContainer()
                {
                    Margin = new MarginPadding(20),
                    Direction = FillDirection.Vertical,
                    Spacing = new Vector2(0, 20),
                    AutoSizeAxes = Axes.Y,
                    Width = 260, // this is static so it doesn't change size when hold tetrimino is changed
                    Children = new Drawable[]
                    {
                        holdTetrimino = new(PieceType.I, 0, 0)
                        {
                            Margin = new MarginPadding(20),
                            Anchor = Anchor.TopCentre,
                            Origin = Anchor.TopCentre,
                        },
                        new SpriteText()
                        {
                            Text = "Next",
                            Font = new FontUsage(size: 50),
                            Origin = Anchor.TopCentre,
                            Anchor = Anchor.TopCentre,
                        },
                        bagQueueContainer = new FillFlowContainer()
                        {
                            Position = new Vector2(0, 125),
                            AutoSizeAxes = Axes.Both,
                            Direction = FillDirection.Vertical,
                            Spacing = new Vector2(0, 10),
                            Anchor = Anchor.TopCentre,
                            Origin = Anchor.TopCentre,
                        }
                    }
                }
            }
        };

        ffBox.Insert(-2, new SpriteText()
        {
            Text = "Hold",
            Font = new FontUsage(size: 50),
            Origin = Anchor.TopCentre,
            Anchor = Anchor.TopCentre,
        });
        holdTetrimino.Hide();
    }

    #region Update Content

    public void SetHoldTetrimino()
    {
        holdTetrimino.Expire();
        if (Hold.HeldPiece != null) holdTetrimino = new Tetrimino((PieceType)Hold.HeldPiece, 0, 0);
        holdTetrimino.Show();
        holdTetrimino.Alpha = 1;
        ffBox.Insert(-1, holdTetrimino);
    }

    public void UpdatePreviewTetriminos()
    {
        queueTetriminos.Clear();
        LinkedListNode<PieceType> type = Hold.Bag.BagQueue.First;
        for (int i = 0; i < 4; i++)
        {
            queueTetriminos.Add(new Tetrimino(type.Value, 0, 0) { Margin = new MarginPadding(10) });
            type = type.Next;
        }

        bagQueueContainer.Clear();
        foreach (var tetrimino in queueTetriminos)
        {
            bagQueueContainer.Add(tetrimino);
        }
    }

    #endregion
}
