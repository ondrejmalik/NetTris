using NUnit.Framework;
using Tetris.Game.Menu.Ui;

namespace Tetris.Game.Tests.Visual.Settings
{
    [TestFixture]
    public partial class TestSceneRenderSwitcher : TetrisTestScene
    {
        private MenuButton[] buttons = new MenuButton[5];

        public TestSceneRenderSwitcher()
        {
            AddStep("Filler", () =>
            {
            });
            AddStep("Show", () =>
            {
            });
        }
    }
}
