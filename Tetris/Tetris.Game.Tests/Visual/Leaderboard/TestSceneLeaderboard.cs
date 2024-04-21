using NUnit.Framework;

namespace Tetris.Game.Tests.Visual.Leaderboard
{
    [TestFixture]
    public partial class TestSceneLeaderboard : TetrisTestScene
    {
        public TestSceneLeaderboard()
        {
            Add(new Tetris.Game.Menu.Ui.Leaderboard.Leaderboard());
        }
    }
}
