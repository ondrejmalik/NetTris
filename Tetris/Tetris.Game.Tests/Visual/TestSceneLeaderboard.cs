using osu.Framework.Graphics;
using NUnit.Framework;
using Tetris.Game.Menu.Ui;
using Tetris.Game.Menu.Ui.Leaderboard;

namespace Tetris.Game.Tests.Visual
{
    [TestFixture]
    public partial class TestSceneLeaderboard : TetrisTestScene
    {
        public TestSceneLeaderboard()
        {
            Add(new Leaderboard());
        }
    }
}
