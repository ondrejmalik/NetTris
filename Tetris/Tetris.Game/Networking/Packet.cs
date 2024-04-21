using System;
using System.Collections.Generic;
using System.Text.Json;
using Tetris.Game.Game.Playfield;
using Tetris.Game.Game.Tetrimino;
using Tetris.Game.Networking.Commands;

namespace Tetris.Game.Networking;

/// <summary>
/// Packet class for sending data between server and client.
/// </summary>
[Serializable]
public class Packet
{
    /// <summary>
    /// Empty constructor for serialization.
    /// </summary>
    //TODO: Add a diferential update system to reduce the amount of data sent
    public Packet()
    {
        // Empty constructor for serialization
    }

    /// <param name="command">Packet command that should be sent</param>
    public Packet(PacketCommandBase command = null)
    {
        this.Command = command;
    }

    /// <summary>
    /// Constructor for sending piece data.
    /// </summary>
    /// <param name="occupied"></param>
    /// <param name="piecePos"></param>
    /// <param name="pieceType"></param>
    /// <param name="command"></param>
    public Packet(List<OccupiedSet> occupied, List<(int, int)> piecePos, PieceType pieceType,
        PacketCommandSendLines command = null)
    {
        this.Occupied = occupied;
        this.PieceType = pieceType;
        this.Command = command;
        SerializePiecePos(piecePos);
    }

    /// <summary>
    /// List of all Occupied possitions in playfield
    /// </summary>
    public List<OccupiedSet> Occupied { get; set; }

    /// <summary>
    /// Position of current tetrimino's pivot
    /// </summary>
    public List<Dictionary<string, int>> PiecePos { get; set; } = new();

    /// <summary>
    /// Type of current tetrimino
    /// </summary>
    public PieceType PieceType { get; set; }

    /// <summary>
    /// Send lines command
    /// </summary>
    public PacketCommandBase Command { get; set; }

    /// <summary>
    /// Serialize the packet to a json string.
    /// </summary>
    /// <returns>json string</returns>
    public string Serialize()
    {
        return JsonSerializer.Serialize(this);
    }

    /// <summary>
    /// Deserialize a json string to a packet object.
    /// </summary>
    /// <param name="json"></param>
    /// <returns></returns>
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

    /// <summary>
    /// Because System.Tuple is not serializable we need to convert it to a dictionary before serialization.
    /// </summary>
    /// <param name="piecePos">tetrimino positions</param>
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

    /// <summary>
    /// Deserialize the piece positions from dictionaries to tuples.
    /// </summary>
    /// <returns></returns>
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
