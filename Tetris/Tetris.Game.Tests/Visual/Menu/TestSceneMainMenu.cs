using NUnit.Framework;
using osu.Framework.Graphics;
using osu.Framework.Screens;
using Tetris.Game.Menu;

namespace Tetris.Game.Tests.Visual.Menu
{
    [TestFixture]
    public partial class TestSceneMainMenu : TetrisTestScene
    {
        //TODO: Add folders and steps to all tests
        ScreenStack stack;
        MainMenu mainMenu;

        public TestSceneMainMenu()
        {
            Add(stack = new ScreenStack(mainMenu = new MainMenu()) { RelativeSizeAxes = Axes.Both });
            AddStep("Show", () => stack.Push(mainMenu = new MainMenu() { RelativeSizeAxes = Axes.Both }));

            AddStep("Show Play", () =>
                {
                    mainMenu.PlayButton.TriggerClick();
                }
            );
            AddStep("Show Multiplayer", () =>
                {
                    mainMenu.MultiplayerButton.TriggerClick();
                }
            );

            AddStep("Show Settings", () =>
                {
                    mainMenu.SettingsButton.TriggerClick();
                }
            );
            AddStep("Show Settings", () =>
                {
                    mainMenu.LeaderboardsButton.TriggerClick();
                }
            );
        }
    }
}
