using System;
using System.IO;
using System.Linq;
using osuTK.Input;
using Realms;
using Tetris.Game.Config;

namespace Tetris.Game.Realm;

/// <summary>
/// Static class to manage Realm database.
/// </summary>
public static class RealmManager
{
    /// <summary>
    /// Static Realm configuration.
    /// </summary>
    // TODO move this path to config file
    public static RealmConfiguration Config = new RealmConfiguration(Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), @"Tetris\Data\Database.realm"));

    /// <summary>
    /// Static constructor to create data directory if it doesn't exist.
    /// </summary>
    static RealmManager()
    {
        if (!Directory.Exists(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                @"Tetris\Data")))
        {
            Directory.CreateDirectory(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                @"Tetris\Data"));
        }
    }

    /// <summary>
    /// Gets the Realm Singleton instance.
    /// </summary>
    /// <returns><see cref="Realm"/></returns>
    public static Realms.Realm GetRealmInstance()
    {
        return Realms.Realm.GetInstance(Config);
    }

    /// <summary>
    /// Adds a score to the Realm database.
    /// </summary>
    /// <param name="playerName">Name of the palyer</param>
    /// <param name="score">Achieved score</param>
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

    /// <summary>
    /// Reads all scores from the Realm database.
    /// </summary>
    /// <returns></returns>
    public static IQueryable<RealmScore> ReadScores()
    {
        Realms.Realm Realm = Realms.Realm.GetInstance(Config);
        return Realm.All<RealmScore>().OrderByDescending(x => x.Score);
    }

    /// <summary>
    /// Saves the game configuration to the Realm database.
    /// </summary>
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

    /// <summary>
    /// Loads the game configuration from the Realm database.
    /// </summary>
    /// <param name="attempt"></param>
    public static void LoadConfig(int attempt = 0)
    {
        Realms.Realm Realm = Realms.Realm.GetInstance(Config);
        RealmGameConfig config = Realm.All<RealmGameConfig>().FirstOrDefault();
        if (config == null)
        {
            GameConfigManager.SetDefaults();
            return;
        }

        try
        {
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
                GameConfigManager.OnlineConfig.Add((OnlineSetting)Enum.Parse(typeof(OnlineSetting), kvp.Key),
                    kvp.Value);
            }
        }
        catch (Exception e)
        {
            if (attempt < 3)
            {
                GameConfigManager.Clear();
                LoadConfig(attempt + 1);
            }
            else
            {
                GameConfigManager.SetDefaults();
            }
        }
    }
}
