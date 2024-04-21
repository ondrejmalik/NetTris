using Tetris.Game.Game.Tetrimino;

namespace Tetris.Game.Game.Playfield;

public class OccupiedSet
{
    /// <summary>
    /// Index of the piece in the grid.
    /// Example: index 12 is 2nd row from top and 3rd column from left.
    /// </summary>
    public int I { get; set; }

    /// <summary>
    /// Is the piece occupied.
    /// </summary>
    public bool O { get; set; }

    /// <summary>
    /// Type of the piece.
    /// Can be used to determine the color of the piece.
    /// </summary>
    public PieceType P { get; set; }

    public static implicit operator bool(OccupiedSet obj)
    {
        return obj != null && obj.O;
    }
}
