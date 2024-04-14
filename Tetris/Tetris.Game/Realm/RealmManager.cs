using System;
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
        using (var trans = Realm.BeginWrite())
        {
            Realm.Add(
                new RealmScore()
                {
                    PlayerName = playerName,
                    Score = score,
                });
            trans.Commit();
        }
    }

    public static void ReadScores()
    {
         AddScore("Test", 100);
        Realms.Realm Realm = Realms.Realm.GetInstance(config);
        IOrderedQueryable<RealmScore> credentials = Realm.All<RealmScore>().OrderByDescending(x => x.Score);
        foreach (var score in credentials)
        {
            Console.WriteLine(score.PlayerName + " " + score.Score);
        }
    }
}
