using System.Collections.Generic;
using osu.Framework.Input.Events;
using osu.Framework.Input.States;
using osuTK.Input;

namespace Tetris.Game.Config;

public static class GameConfigManager
{
    private static Dictionary<GameSetting, Key> _config = GameControlsConfigSetDefaults();
    private static Dictionary<GameSetting, Key> _opponentconfig = OpponentControlsConfigSetDefaults();

    public static Dictionary<GameSetting, Key> GameControlsConfig
    {
        get => _config;
        set => _config = value;
    }

    public static Dictionary<GameSetting, Key> OpponentControlsConfig
    {
        get => _opponentconfig;
        set => _opponentconfig = value;
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
}
