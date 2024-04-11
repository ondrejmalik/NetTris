using System;
using Newtonsoft.Json;

namespace Tetris.Game.Networking;

[Serializable]
public class PacketCommandBase
{
    public PacketCommandBase()
    {
        // Empty constructor for serialization
    }

    [JsonConstructor]
    public PacketCommandBase(PacketCommandType packetCommandType, string commandData)
    {
        PacketCommandType = packetCommandType;
        CommandData = commandData;
    }

    public PacketCommandType PacketCommandType { get; set; }
    public string CommandData { get; set; }
}
