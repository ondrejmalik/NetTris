using NUnit.Framework;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using Tetris.Game.Game.Playfield.Tetrimino;

namespace Tetris.Game.Tests.Visual.Game_Ui
{
    [TestFixture]
    public partial class TestSceneFillFlowTetrimino : TetrisTestScene
    {
        private FillFlowContainer FF;

        public TestSceneFillFlowTetrimino()
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
