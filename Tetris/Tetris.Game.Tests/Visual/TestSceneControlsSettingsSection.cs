using osu.Framework.Graphics;
using NUnit.Framework;
using osu.Framework.Graphics.Shapes;
using Tetris.Game.Config;
using Tetris.Game.Menu.Ui;
using Tetris.Game.Menu.Ui.Controls;
using Tetris.Game.Menu.Ui.Leaderboard;
using Tetris.Game.Realm;

namespace Tetris.Game.Tests.Visual
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
