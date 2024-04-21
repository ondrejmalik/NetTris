using NUnit.Framework;
using osu.Framework.Graphics;
using osu.Framework.Screens;
using Tetris.Game.Game.Screens;

namespace Tetris.Game.Tests.Visual.Screen
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
