using osu.Framework.Graphics;
using osu.Framework.Screens;
using NUnit.Framework;
using Tetris.Game.Game.UI.Screens;

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
