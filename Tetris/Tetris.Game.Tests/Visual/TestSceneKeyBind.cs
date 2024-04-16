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
    public partial class TestSceneKeyBind : TetrisTestScene
    {
        public TestSceneKeyBind()
        {
            Add(new KeyBind(GameSetting.MoveLeft, GameConfigManager.GameControlsConfig));
            Add(new KeyBind(GameSetting.MoveRight, GameConfigManager.GameControlsConfig));
        }
    }
}
