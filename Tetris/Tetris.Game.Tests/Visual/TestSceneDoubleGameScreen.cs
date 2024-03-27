using osu.Framework.Graphics;
using osu.Framework.Screens;
using NUnit.Framework;

namespace Tetris.Game.Tests.Visual
{
    [TestFixture]
    public partial class TestSceneDoubleGameScreen : TetrisTestScene
    {

        public TestSceneDoubleGameScreen()
        {
            Add(new ScreenStack(new DoubleGameScreen()) { RelativeSizeAxes = Axes.Both });
        }
    }
}
