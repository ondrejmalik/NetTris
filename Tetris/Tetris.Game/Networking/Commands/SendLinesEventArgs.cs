using System;
using Tetris.Game.Game.Playfield;

namespace Tetris.Game.Networking;

public class SendLinesEventArgs : EventArgs
{
    public int Lines { get; set; }

    public SendLinesEventArgs(int lines)
    {
        Lines = lines;
    }
}
