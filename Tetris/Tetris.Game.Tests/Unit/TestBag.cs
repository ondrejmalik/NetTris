using System.Collections.Generic;
using NUnit.Framework;
using Tetris.Game.Game.Bag;
using Tetris.Game.Game.Tetrimino;
using Tetris.Game.Networking;

namespace Tetris.Game.Tests.Unit;

[TestFixture]
public class TestBag
{
    private Bag bag;

    [SetUp]
    public void SetUp()
    {
        bag = new();
    }

    [Test]
    public void TestFillBag()
    {
        bag.FillBag();

        Assert.AreEqual(7, bag.BagQueue.Count);
    }

    [Test]
    public void TestDequeue()
    {
        PieceType type = bag.Dequeue();

        Assert.NotNull(type);
    }
}
