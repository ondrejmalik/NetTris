using osu.Framework.Graphics;
using NUnit.Framework;
using osu.Framework.Graphics.Containers;

namespace Tetris.Game.Tests.Visual
{
    [TestFixture]
    public partial class TestSceneTetrimino : TetrisTestScene
    {
        // Add visual tests to ensure correct behaviour of your game: https://github.com/ppy/osu-framework/wiki/Development-and-Testing
        // You can make changes to classes associated with the tests and they will recompile and update immediately.
        private FillFlowContainer FF;

        public TestSceneTetrimino()
        {
            Add(FF = new FillFlowContainer()
            {
            });
            FF.Add(
                new Tetrimino(PieceType.I, 0, 0) { Margin = new MarginPadding(10) }
            );
            FF.Add(
                new Tetrimino(PieceType.J, 0, 0) { Margin = new MarginPadding(10) }
            );
            FF.Add(
                new Tetrimino(PieceType.L, 0, 0) { Margin = new MarginPadding(10) }
            );
            FF.Add(
                new Tetrimino(PieceType.O, 0, 0) { Margin = new MarginPadding(10) }
            );
        }
    }
}
