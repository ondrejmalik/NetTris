using NUnit.Framework;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Shapes;
using Tetris.Game.Menu.Ui.Leaderboard;
using Tetris.Game.Realm;

namespace Tetris.Game.Tests.Visual.Leaderboard
{
    [TestFixture]
    public partial class TestSceneLeaderboardScore : TetrisTestScene
    {
        public TestSceneLeaderboardScore()
        {
            AddRange( new Drawable[]
            {
                new Box()
                {
                    RelativeSizeAxes = Axes.Both,
                    Colour = Colour4.Black
                },
                new LeaderboardScore(new RealmScore()
                {
                    PlayerName = "Player",
                    Score = 1000,
                    Date = new System.DateTime(2021, 1, 1, 12, 0, 0)
                })
                {
                    Position = new osuTK.Vector2(100, 100)
                },
                new LeaderboardScore(new RealmScore()
                {
                    PlayerName = "Player2",
                    Score = 2000,
                    Date = new System.DateTime(2021, 1, 2, 12, 0, 0)
                })
                {
                    Position = new osuTK.Vector2(100, 200)
                },
                new LeaderboardScore(new RealmScore()
                {
                    PlayerName = "Player3",
                    Score = 3000,
                    Date = new System.DateTime(2021, 1, 3, 12, 0, 0)
                })
                {
                    Position = new osuTK.Vector2(100, 300)
                },
                new LeaderboardScore(new RealmScore()
                {
                    PlayerName = "LongPlayerNameThatIsTooLongForTheBox",
                    Score = 3000,
                    Date = new System.DateTime(2021, 1, 3, 12, 0, 0)
                })
                {
                    Position = new osuTK.Vector2(100, 400)
                }
            });
        }
    }
}
