using System;
using System.Collections.Generic;

namespace Tetris.Game.Networking.Commands;

/// <param name="lines">the new number of lines</param>
/// <param name="lastPieceGridPos">where piece that cleared lines was placed</param>
public class SendLinesEventArgs(int lines, List<(int, int)> lastPieceGridPos) : EventArgs
{
    public int Lines { get; set; } = lines;
    public List<(int, int)> LastPieceGridPos { get; set; } = lastPieceGridPos;
}
