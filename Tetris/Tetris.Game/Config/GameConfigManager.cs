using System.Collections.Generic;
using osuTK.Input;
using Tetris.Game.Realm;

namespace Tetris.Game.Config;

public static class GameConfigManager
{
    private static Dictionary<GameSetting, Key> _gameConfig = new();
    private static Dictionary<GameSetting, Key> _opponentConfig = new();
    private static Dictionary<OnlineSetting, string> _onlineConfig = new();
    private static Dictionary<UserSetting, string> _userConfig = new();

    static GameConfigManager()
    {
        // TODO: add other configs to realm db, load them here and set them to defaults if they don't exist
        RealmManager.LoadConfig();
    }

    public static Dictionary<GameSetting, Key> GameControlsConfig
    {
        get => _gameConfig;
        set
        {
            _gameConfig = value;
        }
    }


    public static Dictionary<GameSetting, Key> OpponentControlsConfig
    {
        get => _opponentConfig;
        set
        {
            _opponentConfig = value;
        }
    }

    public static Dictionary<OnlineSetting, string> OnlineConfig
    {
        get => _onlineConfig;
        set
        {
            _onlineConfig = value;
        }
    }

    public static Dictionary<UserSetting, string> UserConfig
    {
        get => _userConfig;
        set
        {
            _userConfig = value;
        }
    }

    public static void SetDefaults()
    {
        GameControlsConfig = GameControlsConfigSetDefaults();
        OpponentControlsConfig = OpponentControlsConfigSetDefaults();
        OnlineConfig = OnlineConfigSetDefaults();
        UserConfig = UserConfigSetDefaults();
    }

    public static Dictionary<GameSetting, Key> GameControlsConfigSetDefaults()
    {
        return new Dictionary<GameSetting, Key>
        {
            { GameSetting.MoveLeft, Key.A },
            { GameSetting.MoveRight, Key.D },
            { GameSetting.RotateLeft, Key.J },
            { GameSetting.RotateRight, Key.K },
            { GameSetting.HardDrop, Key.S },
            { GameSetting.Hold, Key.ShiftLeft },
            { GameSetting.SoftDrop, Key.W },
        };
    }

    public static Dictionary<GameSetting, Key> OpponentControlsConfigSetDefaults()
    {
        return new Dictionary<GameSetting, Key>
        {
            { GameSetting.MoveLeft, Key.Left },
            { GameSetting.MoveRight, Key.Right },
            { GameSetting.RotateLeft, Key.PageUp },
            { GameSetting.RotateRight, Key.PageDown },
            { GameSetting.HardDrop, Key.Down },
            { GameSetting.Hold, Key.Slash },
            { GameSetting.SoftDrop, Key.Up },
        };
    }

    public static Dictionary<OnlineSetting, string> OnlineConfigSetDefaults()
    {
        return new Dictionary<OnlineSetting, string>
        {
            { OnlineSetting.Ip, "127.0.0.1" },
            { OnlineSetting.Port, "8543" },
        };
    }

    public static Dictionary<UserSetting, string> UserConfigSetDefaults()
    {
        return new Dictionary<UserSetting, string>
        {
            { UserSetting.Username, "Guest Player" },
        };
    }
}
