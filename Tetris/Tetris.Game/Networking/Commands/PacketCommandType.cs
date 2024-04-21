namespace Tetris.Game.Networking.Commands;

/// <summary>
/// Type of packet command.
/// </summary>
public enum PacketCommandType
{
    SendLines,
    Start,
    GameOver,
}
