using osu.Framework.Graphics;
using NUnit.Framework;
using osu.Framework.Configuration;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Sprites;
using osuTK;
using Tetris.Game.Menu.Ui;

namespace Tetris.Game.Tests.Visual
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
