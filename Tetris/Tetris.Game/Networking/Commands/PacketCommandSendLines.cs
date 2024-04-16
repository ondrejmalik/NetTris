using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using Newtonsoft.Json;

namespace Tetris.Game.Networking;

[Serializable]
// convert lastPieceGridPos to cords X,Y separeted by ; also check if null insert empty string
public class PacketCommandSendLines(int newLines, List<(int X, int Y)> lastPieceGridPos)
    : PacketCommandBase(PacketCommandType.SendLines,
        $"{newLines}-" + (lastPieceGridPos != null
            ? string.Join(";", lastPieceGridPos.Select(pos => $"{pos.X},{pos.Y}"))
            : ""))
{
}
