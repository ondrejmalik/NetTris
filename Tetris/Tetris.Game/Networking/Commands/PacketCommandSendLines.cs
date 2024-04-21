using System;
using System.Collections.Generic;
using System.Linq;

namespace Tetris.Game.Networking.Commands;

/// <summary>
/// Packet command for sending garbage lines to the opponent.
/// </summary>
/// <param name="newLines">the new number of lines</param>
/// <param name="lastPieceGridPos">where piece that cleared lines was placed</param>
[Serializable]
// converts lastPieceGridPos to cords X,Y separeted by ; also check if null insert empty string
public class PacketCommandSendLines(int newLines, List<(int X, int Y)> lastPieceGridPos)
    : PacketCommandBase(PacketCommandType.SendLines,
        $"{newLines}-" + (lastPieceGridPos != null
            ? string.Join(";", lastPieceGridPos.Select(pos => $"{pos.X},{pos.Y}"))
            : ""))
{
}
