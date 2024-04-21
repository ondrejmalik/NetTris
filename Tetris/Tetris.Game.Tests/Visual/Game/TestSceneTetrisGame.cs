using osu.Framework.Allocation;
using osu.Framework.Platform;
using NUnit.Framework;

namespace Tetris.Game.Tests.Visual.Game
{
    [TestFixture]
    public partial class TestSceneTetrisGame : TetrisTestScene
    {
        private TetrisGame game;

        [BackgroundDependencyLoader]
        private void load(GameHost host)
        {
            game = new TetrisGame();
            game.SetHost(host);

            AddGame(game);
        }
    }
}
