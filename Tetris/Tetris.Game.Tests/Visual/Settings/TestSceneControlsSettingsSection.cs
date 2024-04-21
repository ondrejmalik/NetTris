using NUnit.Framework;
using Tetris.Game.Config;
using Tetris.Game.Menu.Ui.Settings.Controls;

namespace Tetris.Game.Tests.Visual.Settings
{
    [TestFixture]
    public partial class TestSceneControlsSettingsSection : TetrisTestScene
    {
        public TestSceneControlsSettingsSection()
        {
            Add(new KeyBindsSection(GameConfigManager.GameControlsConfig));
        }
    }
}
