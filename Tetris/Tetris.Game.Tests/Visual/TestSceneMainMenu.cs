using osu.Framework.Graphics;
using NUnit.Framework;
using osu.Framework.Screens;
using Tetris.Game.Menu;

namespace Tetris.Game.Tests.Visual
{
    [TestFixture]
    public partial class TestSceneMainMenu : TetrisTestScene
    {
        ScreenStack stack;
        MainMenu mainMenu;

        public TestSceneMainMenu()
        {
            Add(stack = new ScreenStack(mainMenu = new MainMenu()) { RelativeSizeAxes = Axes.Both });
            AddStep("Show", () => stack.Push(mainMenu = new MainMenu() { RelativeSizeAxes = Axes.Both }));
            AddStep("Show Settings", () =>
                {
                    mainMenu.SettingsButton.TriggerClick();
                }
            );
        }
    }
}
