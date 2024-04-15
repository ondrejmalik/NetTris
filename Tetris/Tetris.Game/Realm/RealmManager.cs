using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Realms;

namespace Tetris.Game.Realm;

public static class RealmManager
{
    // TODO move this path to config file
    public static RealmConfiguration config = new RealmConfiguration(Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), @"NetTris\Data\Database.realm"));

    public static void AddScore(string playerName, int score)
    {
        Realms.Realm Realm = Realms.Realm.GetInstance(config);
        Realm.Write(() =>
        {
            Realm.Add(new RealmScore
            {
                PlayerName = playerName,
                Score = score
            });
        });
    }

    public static List<RealmScore> ReadScores()
    {
        Realms.Realm Realm = Realms.Realm.GetInstance(config);
        List<RealmScore> list = Realm.All<RealmScore>().OrderByDescending(x => x.Score).ToList();
        return list.Take(5).ToList();
    }
}
