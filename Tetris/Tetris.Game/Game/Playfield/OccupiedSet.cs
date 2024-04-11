using osu.Framework.Graphics;

namespace Tetris.Game.Game.Playfield;

public class OccupiedSet
{
    public int X { get; set; }
    public int Y { get; set; }
    public bool Occupied { get; set; }
    public Colour4 Colour { get; set; }

    public static implicit operator bool(OccupiedSet obj)
    {
        return obj != null && obj.Occupied;
    }
}
