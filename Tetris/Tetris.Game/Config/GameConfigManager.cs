using System.Collections.Generic;
using osuTK.Input;
using Tetris.Game.Realm;

namespace Tetris.Game.Config;

/// <summary>
/// Static class to manage game configuration.
/// </summary>
public static class GameConfigManager
{
    #region Private

    private static Dictionary<GameSetting, Key> _gameConfig = new();
    private static Dictionary<GameSetting, Key> _opponentConfig = new();
    private static Dictionary<OnlineSetting, string> _onlineConfig = new();
    private static Dictionary<UserSetting, string> _userConfig = new();

    #endregion

    /// <summary>
    /// Static constructor to load the game configuration.
    /// </summary>
    static GameConfigManager()
    {
        RealmManager.LoadConfig();
    }

    /// <summary>
    /// Clears the game configuration.
    /// </summary>
    public static void Clear()
    {
        GameControlsConfig.Clear();
        OpponentControlsConfig.Clear();
        OnlineConfig.Clear();
        UserConfig.Clear();
    }

    #region Properties

    /// <summary>
    /// Game configuration.
    /// </summary>
    public static Dictionary<GameSetting, Key> GameControlsConfig
    {
        get => _gameConfig;
        set
        {
            _gameConfig = value;
        }
    }

    /// <summary>
    /// Opponent configuration.
    /// </summary>
    public static Dictionary<GameSetting, Key> OpponentControlsConfig
    {
        get => _opponentConfig;
        set
        {
            _opponentConfig = value;
        }
    }

    /// <summary>
    /// Online configuration.
    /// </summary>
    public static Dictionary<OnlineSetting, string> OnlineConfig
    {
        get => _onlineConfig;
        set
        {
            _onlineConfig = value;
        }
    }

    /// <summary>
    /// User configuration.
    /// </summary>
    public static Dictionary<UserSetting, string> UserConfig
    {
        get => _userConfig;
        set
        {
            _userConfig = value;
        }
    }

    #endregion

    #region SetDefaults

    /// <summary>
    /// Sets the default values for the game configuration.
    /// </summary>
    public static void SetDefaults()
    {
        GameControlsConfig = GameControlsConfigSetDefaults();
        OpponentControlsConfig = OpponentControlsConfigSetDefaults();
        OnlineConfig = OnlineConfigSetDefaults();
        UserConfig = UserConfigSetDefaults();
    }

    /// <summary>
    /// Sets the default values for the game configuration.
    /// </summary>
    /// <returns></returns>
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

    /// <summary>
    /// Sets the default values for the opponent configuration.
    /// </summary>
    /// <returns></returns>
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

    /// <summary>
    /// Sets the default values for the online configuration.
    /// </summary>
    /// <returns></returns>
    public static Dictionary<OnlineSetting, string> OnlineConfigSetDefaults()
    {
        return new Dictionary<OnlineSetting, string>
        {
            { OnlineSetting.Ip, "127.0.0.1" },
            { OnlineSetting.Port, "8543" },
            { OnlineSetting.TickRate, "5" }
        };
    }

    /// <summary>
    /// Sets the default values for the user configuration.
    /// </summary>
    /// <returns></returns>
    public static Dictionary<UserSetting, string> UserConfigSetDefaults()
    {
        return new Dictionary<UserSetting, string>
        {
            { UserSetting.Username, "Guest Player" },
            { UserSetting.ShowFps, "false" }
        };
    }

    #endregion
}
