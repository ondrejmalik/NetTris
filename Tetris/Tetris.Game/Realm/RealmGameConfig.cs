using System;
using System.Collections.Generic;
using Realms;


namespace Tetris.Game.Realm;

/// <summary>
/// Realm object for game configuration.
/// </summary>
public class RealmGameConfig : RealmObject
{
    [PrimaryKey] public string Id { get; set; } = Guid.NewGuid().ToString();

    /// <summary>
    /// Game configuration.
    /// </summary>
    [Required]
    public IDictionary<string, string> GameConfig { get; }

    /// <summary>
    /// Opponent configuration.
    /// </summary>
    [Required]
    public IDictionary<string, string> OpponentConfig { get; }

    /// <summary>
    /// User configuration.
    /// </summary>
    [Required]
    public IDictionary<string, string> UserConfig { get; }

    /// <summary>
    /// Online configuration.
    /// </summary>
    [Required]
    public IDictionary<string, string> OnlineConfig { get; }
}
