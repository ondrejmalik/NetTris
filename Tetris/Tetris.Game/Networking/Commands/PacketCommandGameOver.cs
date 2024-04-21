using System;

namespace Tetris.Game.Networking.Commands;

/// <summary>
///Packet command for game over.
/// </summary>
[Serializable]
public class PacketCommandGameOver() : PacketCommandBase(PacketCommandType.GameOver, "GameOver")
{
}
