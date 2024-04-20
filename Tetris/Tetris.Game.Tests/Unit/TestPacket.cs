using System.Collections.Generic;
using NUnit.Framework;
using Tetris.Game.Networking;

namespace Tetris.Game.Tests.Unit;

[TestFixture]
public class TestPacket
{
    private Packet packet;
    private List<(int, int)> piecePos = new() { (1, 1), (2, 2), (3, 3), (4, 4) };

    [SetUp]
    public void SetUp()
    {
        packet = new();
    }

    [Test]
    public void TestSerialize()
    {
        string result = packet.Serialize();

        Assert.NotNull(result);
    }

    [Test]
    public void TestDeserialize()
    {
        Packet result = Packet.Deserialize(packet.Serialize());

        Assert.NotNull(result);
    }

    [Test][Order(1)]
    public void TestSerializePiecePos()
    {
        packet.SerializePiecePos(new() { (1, 1), (2, 2), (3, 3), (4, 4) });

        Assert.AreEqual(4, packet.PiecePos.Count);
    }

    [Test][Order(2)]
    public void TestDeserializePiecePos()
    {
        packet.SerializePiecePos(new() { (1, 1), (2, 2), (3, 3), (4, 4) });
        List<(int, int)> deserializePiecePos = packet.DeserializePiecePos();

        Assert.AreEqual(piecePos, deserializePiecePos);
    }
}
