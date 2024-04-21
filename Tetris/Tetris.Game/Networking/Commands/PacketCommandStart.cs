using System;

namespace Tetris.Game.Networking.Commands;

/// <summary>
/// Packet command to send server client is ready.
/// </summary>
[Serializable]
public class PacketCommandStart() : PacketCommandBase(PacketCommandType.Start, "Start")
{
}
