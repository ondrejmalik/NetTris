using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using JetBrains.Annotations;

namespace Tetris.Game.Networking;

[Serializable]
public class Packet
{
    public Packet()
    {
        // Empty constructor for serialization
    }

    public Packet(List<OccupiedSet> occupied, List<(int, int)> piecePos, PieceType pieceType)
    {
        this.Occupied = occupied;
        this.PieceType = pieceType;
        SerializePiecePos(piecePos);
    }

    public List<OccupiedSet> Occupied { get; set; }
    public List<Dictionary<string, int>> PiecePos { get; set; } = new();
    public PieceType PieceType { get; set; }

    public string Serialize()
    {
        return JsonSerializer.Serialize(this);
    }

    public static Packet Deserialize(string json)
    {
        //TODO Fix Colour Deserialization
        try
        {
            Packet packet = JsonSerializer.Deserialize<Packet>(json);
            return packet;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }

        return null;
    }

    public void SerializePiecePos(List<(int, int)> piecePos)
    {
        // Convert tuples to dictionaries before serialization
        PiecePos.Clear();
        foreach (var tuple in piecePos)
        {
            var dict = new Dictionary<string, int>
            {
                { "X", tuple.Item1 },
                { "Y", tuple.Item2 }
            };
            PiecePos.Add(dict);
        }
    }

    public List<(int, int)> DeserializePiecePos()
    {
        List<(int, int)> list = new();
        foreach (var piecePos in PiecePos)
        {
            list.Add((piecePos["X"], piecePos["Y"]));
        }

        return list;
    }
}
