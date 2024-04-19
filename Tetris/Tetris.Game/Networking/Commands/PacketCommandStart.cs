using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using Newtonsoft.Json;

namespace Tetris.Game.Networking;

[Serializable]
// convert lastPieceGridPos to cords X,Y separeted by ; also check if null insert empty string
public class PacketCommandStart() : PacketCommandBase(PacketCommandType.Start, "Start")
{
}
