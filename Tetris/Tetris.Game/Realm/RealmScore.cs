﻿using System;
using Realms;

namespace Tetris.Game.Realm;

public class RealmScore : RealmObject
{
    [Realms.PrimaryKey] public string Id { get; set; } = Guid.NewGuid().ToString();
    public string PlayerName { get; set; }
    public int Score { get; set; }
    public DateTimeOffset Date { get; set; } = DateTime.Now;
}
