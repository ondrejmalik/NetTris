using Tetris.Game.Game.Tetrimino;
using Tetris.Game.Game.UI;

namespace Tetris.Game.Game.Bag;

/// <summary>
/// Represents the hold system in Tetris.
/// </summary>
public class Hold
{
    private PieceType? heldPiece;
    public Bag Bag { get; set; }
    public HoldPreview HoldPreview { get; set; }

    /// <param name="bag">Bag of curent playfied</param>
    /// <param name="holdPreview">Where hold should be visualized</param>
    public Hold(Bag bag, HoldPreview holdPreview)
    {
        HoldPreview = holdPreview;
        Bag = bag;
    }

    /// <summary>
    /// Held piece can be set only when CanHold == true.
    /// </summary>
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

    /// <summary>
    /// Piece can be held only when CanHold == true.
    /// </summary>
    public bool CanHold { get; set; } = true;
}
