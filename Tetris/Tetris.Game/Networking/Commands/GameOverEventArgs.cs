using System;

namespace Tetris.Game.Networking.Commands;

public class GameOverEventArgs(bool lost = true) : EventArgs
{
    public bool lost { get; set; } = lost;
}
