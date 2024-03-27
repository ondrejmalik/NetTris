using System;
using osu.Framework.Graphics;

namespace Tetris.Game;

public class OcupiedSet
{
    public int X { get; set; }
    public int Y { get; set; }
    public bool Occupied { get; set; }
    public Colour4 Colour { get; set; }

    public static implicit operator bool(OcupiedSet obj)
    {
        return obj != null && obj.Occupied;
    }
}
