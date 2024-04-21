using NUnit.Framework;
using osu.Framework.Graphics;
using osu.Framework.Screens;
using Tetris.Game.Game.Screens;
using Tetris.Game.Menu;

namespace Tetris.Game.Tests.Visual.Menu
{
    [TestFixture]
    public partial class TestSceneMainMenu : TetrisTestScene
    {
        //TODO: Add folders and steps to all tests
        ScreenStack stack;
        MainMenuScreen mainMenuScreen;

        public TestSceneMainMenu()
        {
            Add(stack = new ScreenStack(mainMenuScreen = new MainMenuScreen()) { RelativeSizeAxes = Axes.Both });
            AddStep("Show", () => stack.Push(mainMenuScreen = new MainMenuScreen() { RelativeSizeAxes = Axes.Both }));

            AddStep("Show Play", () =>
                {
                    mainMenuScreen.PlayButton.TriggerClick();
                }
            );
            AddStep("Show Multiplayer", () =>
                {
                    mainMenuScreen.MultiplayerButton.TriggerClick();
                }
            );

            AddStep("Show Settings", () =>
                {
                    mainMenuScreen.SettingsButton.TriggerClick();
                }
            );
            AddAssert("Settings Menu is not null", () => mainMenuScreen.SettingsMenu != null);
            AddStep("Show Settings", () =>
                {
                    mainMenuScreen.LeaderboardsButton.TriggerClick();
                }
            );
            AddAssert("Leaderboard is not null", () => mainMenuScreen.SettingsMenu != null);
        }
    }
}
