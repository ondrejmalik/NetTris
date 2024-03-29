using NUnit.Framework;
using Tetris.Game.Networking;

namespace Tetris.Game.Tests.Unit;

[TestFixture]
public class TestPacket
{
    private Packet packet;

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
}
