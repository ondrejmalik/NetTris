using System;
using NUnit.Framework;
using Tetris.Game.Config;
using Tetris.Game.Networking;
using Tetris.Game.Realm;

namespace Tetris.Game.Tests.Unit;

[TestFixture]
public class TestRealmManager
{
    [Test]
    public void TestGetInstance()
    {
        try
        {
            Realms.Realm result = RealmManager.GetRealmInstance();
            Assert.NotNull(result);
        }
        catch (Exception e)
        {
            Assert.Fail(e.Message);
        }
    }

    [Test]
    public void TestAddScore()
    {
        try
        {
            RealmManager.AddScore("Test", 1);
        }
        catch (Exception e)
        {
            Assert.Fail(e.Message);
        }
    }

    [Test]
    public void TestReadScores()
    {
        try
        {
            var scores = RealmManager.ReadScores();
            Assert.NotNull(scores);
        }
        catch (Exception e)
        {
            Assert.Fail(e.Message);
        }
    }

    [Test]
    public void TestSaveConfig()
    {
        try
        {
            RealmManager.SaveConfig();
        }
        catch (Exception e)
        {
            Assert.Fail(e.Message);
        }
    }

    [Test]
    public void TestLoadConfig()
    {
        try
        {
            GameConfigManager.Clear();
            RealmManager.LoadConfig();
            if (GameConfigManager.OnlineConfig == null || GameConfigManager.UserConfig == null ||
                GameConfigManager.OpponentControlsConfig == null || GameConfigManager.GameControlsConfig == null)
            {
                Assert.Fail("Config is null");
            }
        }
        catch (Exception e)
        {
            Assert.Fail(e.Message);
        }
    }
}
