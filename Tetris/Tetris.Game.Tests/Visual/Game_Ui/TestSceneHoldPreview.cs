using NUnit.Framework;
using Tetris.Game.Game.Bag;
using Tetris.Game.Game.Tetrimino;
using Tetris.Game.Game.UI;

namespace Tetris.Game.Tests.Visual.Game_Ui;

[TestFixture]
public partial class TestSceneHoldPreview : TetrisTestScene
{
    private Hold hold;
    private Bag bag;
    private HoldPreview holdPreview;

    public TestSceneHoldPreview()
    {
        bag = new();
        holdPreview = new HoldPreview(hold);
        hold = new(bag, holdPreview);
        holdPreview.Hold = hold;
        Add(holdPreview);
        AddStep("Fill Bag", () => bag.FillBag());
        AddStep("Update preview Tetriminos", () => holdPreview.UpdatePreviewTetriminos());
        AddAssert("Bag has 7 Tetriminos", () => bag.BagQueue.Count == 7);
        AddStep("Dequeue", () => bag.Dequeue());
        AddStep("Update preview Tetriminos", () => holdPreview.UpdatePreviewTetriminos());
        AddAssert("Bag has 6 Tetriminos", () => bag.BagQueue.Count == 7);
        AddStep("Set hold tetrimino", () =>
            {
                hold.HeldPiece = PieceType.T;
                holdPreview.SetHoldTetrimino();
            }
        );
        AddAssert("Hold has T Tetrimino", () =>
        {
            return hold.HeldPiece == PieceType.T;
        });
    }
}
