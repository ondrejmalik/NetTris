using System;
using System.Collections.Generic;
using Realms;


namespace Tetris.Game.Realm;

public class RealmGameConfig : RealmObject
{
    [PrimaryKey] public string Id { get; set; } = Guid.NewGuid().ToString();
    [Required] public IDictionary<string, string> GameConfig { get; }
    [Required] public IDictionary<string, string> OpponentConfig { get; }
    [Required] public IDictionary<string, string> UserConfig { get; }
    [Required] public IDictionary<string, string> OnlineConfig { get; }
}
