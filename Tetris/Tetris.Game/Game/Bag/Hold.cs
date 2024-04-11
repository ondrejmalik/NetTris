using Tetris.Game.Game.Playfield.Tetrimino;
using Tetris.Game.Game.UI;

namespace Tetris.Game.Game.Bag;

public class Hold
{
    private PieceType? heldPiece;
    public Bag Bag { get; set; }
    public HoldPreview HoldPreview { get; set; }

    public Hold(Bag bag, HoldPreview holdPreview)
    {
        HoldPreview = holdPreview;
        Bag = bag;
    }

    public PieceType? HeldPiece
    {
        get
        {
            return heldPiece;
        }
        set
        {
            if (heldPiece == null)
            {
                heldPiece = value;
            }
            else
            {
                PieceType temp = (PieceType)heldPiece;
                Bag.BagQueue.AddFirst(temp);
                heldPiece = value;
            }

            HoldPreview.SetHoldTetrimino();
        }
    }

    public bool CanHold { get; set; } = true;
}
