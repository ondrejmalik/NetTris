using NUnit.Framework;
using osu.Framework.Graphics;
using osu.Framework.Screens;
using Tetris.Game.Game.Screens;

namespace Tetris.Game.Tests.Visual.Screen
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
