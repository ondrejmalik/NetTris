using NUnit.Framework;
using osu.Framework;
using osu.Framework.Platform;
using Tetris.Game.Game.Tetrimino;
using Tetris.Game.Game.UI;

namespace Tetris.Game.Tests.Visual.Game
{
    [TestFixture]
    public partial class TestSceneTetrimino : TetrisTestScene
    {
        Tetrimino tetrimino;

        public TestSceneTetrimino()
        {
            GameHost host = Host.GetSuitableDesktopHost("visual-tests");
            Add(tetrimino = new Tetrimino(PieceType.I, 0, 0, isDummy: true));
            AddStep("Change to J", () =>
                {
                    Clear();
                    Add(tetrimino = new Tetrimino(PieceType.J, 0, 0, isDummy: true));
                }
            );
            AddStep("Change to L", () =>
                {
                    Clear();
                    Add(tetrimino = new Tetrimino(PieceType.L, 0, 0));
                }
            );
            AddStep("Change to O", () =>
                {
                    Clear();
                    Add(tetrimino = new Tetrimino(PieceType.O, 0, 0));
                }
            );
            AddStep("Change to S", () =>
                {
                    Clear();
                    Add(tetrimino = new Tetrimino(PieceType.S, 0, 0));
                }
            );
            AddStep("Change to T", () =>
                {
                    Clear();
                    Add(tetrimino = new Tetrimino(PieceType.T, 0, 0));
                }
            );
            AddStep("Change to Z", () =>
                {
                    Clear();
                    Add(tetrimino = new Tetrimino(PieceType.Z, 0, 0));
                }
            );
        }
    }
}
