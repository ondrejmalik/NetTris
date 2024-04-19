namespace Tetris.Game.Networking;

public enum PacketCommandType
{
    SendLines,
    Start,
    GameOver,
    Pause,
    Resume,
}
