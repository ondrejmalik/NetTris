using Tetris.Game.Game.Playfield.Tetrimino;

namespace Tetris.Game.Game.Playfield;

public class OccupiedSet
{
    public int I { get; set; }
    public bool O { get; set; }
    public PieceType P { get; set; }

    public static implicit operator bool(OccupiedSet obj)
    {
        return obj != null && obj.O;
    }
}
