using System;
using System.Collections.Generic;
using Tetris.Game.Game.Playfield;

namespace Tetris.Game.Networking;

public class SendLinesEventArgs(int lines, List<(int, int)> lastPieceGridPos) : EventArgs
{
    public int Lines { get; set; } = lines;
    public List<(int, int)> LastPieceGridPos { get; set; } = lastPieceGridPos;
}
