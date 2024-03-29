using System;
using System.Collections.Generic;
using System.Text.Json;

namespace Tetris.Game.Networking;

[Serializable]
public class Packet
{
    public List<OcupiedSet> Occupied { get; set; }

    public string Serialize()
    {
        return JsonSerializer.Serialize(this);
    }
}
