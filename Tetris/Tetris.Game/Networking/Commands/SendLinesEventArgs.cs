using System;
using System.Collections.Generic;

namespace Tetris.Game.Networking.Commands;

public class SendLinesEventArgs(int lines, List<(int, int)> lastPieceGridPos) : EventArgs
{
    public int Lines { get; set; } = lines;
    public List<(int, int)> LastPieceGridPos { get; set; } = lastPieceGridPos;
}


