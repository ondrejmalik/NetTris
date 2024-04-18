using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using osuTK.Input;
using Realms;
using Tetris.Game.Config;

namespace Tetris.Game.Realm;

public static class RealmManager
{
    // TODO move this path to config file
    public static RealmConfiguration Config = new RealmConfiguration(Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), @"NetTris\Data\Database.realm"));

    public static Realms.Realm GetRealmInstance()
    {
        return Realms.Realm.GetInstance(Config);
    }

    public static void AddScore(string playerName, int score)
    {
        Realms.Realm Realm = Realms.Realm.GetInstance(Config);
        Realm.Write(() =>
        {
            Realm.Add(new RealmScore
            {
                PlayerName = playerName,
                Score = score
            });
        });
    }

    public static IQueryable<RealmScore> ReadScores()
    {
        Realms.Realm Realm = Realms.Realm.GetInstance(Config);
        return Realm.All<RealmScore>().OrderByDescending(x => x.Score);
    }

    public static void SaveConfig()
    {
        Realms.Realm Realm = Realms.Realm.GetInstance(Config);
        Realm.Write(() =>
        {
            Realm.RemoveAll<RealmGameConfig>();
            var config = Realm.Add(new RealmGameConfig
                {
                }
            );
            var gameConfig = config.GameConfig;
            var opponentConfig = config.OpponentConfig;
            var userConfig = config.UserConfig;
            var onlineConfig = config.OnlineConfig;
            foreach (var setting in GameConfigManager.GameControlsConfig)
            {
                gameConfig.Add(setting.Key.ToString(), setting.Value.ToString());
            }

            foreach (var setting in GameConfigManager.OpponentControlsConfig)
            {
                opponentConfig.Add(setting.Key.ToString(), setting.Value.ToString());
            }

            foreach (var setting in GameConfigManager.UserConfig)
            {
                userConfig.Add(setting.Key.ToString(), setting.Value);
            }

            foreach (var setting in GameConfigManager.OnlineConfig)
            {
                onlineConfig.Add(setting.Key.ToString(), setting.Value);
            }
        });
    }

    public static void LoadConfig()
    {
        Realms.Realm Realm = Realms.Realm.GetInstance(Config);
        RealmGameConfig config = Realm.All<RealmGameConfig>().FirstOrDefault();
        if (config == null)
        {
            GameConfigManager.SetDefaults();
            return;
        }

        foreach (var kvp in config.GameConfig)
        {
            GameConfigManager.GameControlsConfig.Add(
                (GameSetting)Enum.Parse(typeof(GameSetting), kvp.Key), (Key)Enum.Parse(typeof(Key), kvp.Value));
        }

        foreach (var kvp in config.OpponentConfig)
        {
            GameConfigManager.OpponentControlsConfig.Add(
                (GameSetting)Enum.Parse(typeof(GameSetting), kvp.Key), (Key)Enum.Parse(typeof(Key), kvp.Value));
        }

        foreach (var kvp in config.UserConfig)
        {
            GameConfigManager.UserConfig.Add((UserSetting)Enum.Parse(typeof(UserSetting), kvp.Key), kvp.Value);
        }

        foreach (var kvp in config.OnlineConfig)
        {
            GameConfigManager.OnlineConfig.Add((OnlineSetting)Enum.Parse(typeof(OnlineSetting), kvp.Key), kvp.Value);
        }
    }
}
