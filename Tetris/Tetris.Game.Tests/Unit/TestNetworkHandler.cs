using System;
using System.Net;
using NUnit.Framework;
using Tetris.Game.Networking;
using Tetris.Game.Networking.Commands;

namespace Tetris.Game.Tests.Unit;

[TestFixture]
public class TestNetworkHandler
{
    private NetworkHandler networkHandler;

    [SetUp]
    public void SetUp()
    {
        networkHandler = new NetworkHandler();
    }

    [Test]
    public void TestHandShake()
    {
        try
        {
            networkHandler.Handshake();
        }
        catch (Exception e)
        {
            Assert.Fail(e.Message);
        }
    }

    [Test]
    public void TestSend()
    {
        try
        {
            networkHandler.Client.Connect(IPAddress.Parse("127.0.0.1"), 8543);
            networkHandler.Send(new Packet(new PacketCommandStart()));
        }
        catch (Exception e)
        {
            Assert.Fail(e.Message);
        }
    }
}
