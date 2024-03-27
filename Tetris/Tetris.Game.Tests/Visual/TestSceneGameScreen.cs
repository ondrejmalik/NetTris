using osu.Framework.Graphics;
using osu.Framework.Screens;
using NUnit.Framework;

namespace Tetris.Game.Tests.Visual
{
    [TestFixture]
    public partial class TestSceneGameScreen : TetrisTestScene
    {

        public TestSceneGameScreen()
        {
            Add(new ScreenStack(new GameScreen()) { RelativeSizeAxes = Axes.Both });
        }
    }
}
