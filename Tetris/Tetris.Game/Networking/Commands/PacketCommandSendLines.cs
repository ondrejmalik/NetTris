using System;
using System.Text.Json;
using Newtonsoft.Json;

namespace Tetris.Game.Networking;

public class PacketCommandSendLines : PacketCommandBase
{
    [JsonConstructor]
    public PacketCommandSendLines(int newLines) : base(PacketCommandType.SendLines, newLines.ToString())
    {
    }

}
