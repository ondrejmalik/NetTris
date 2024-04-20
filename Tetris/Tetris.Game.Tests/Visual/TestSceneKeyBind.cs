using NUnit.Framework;
using Tetris.Game.Config;
using Tetris.Game.Menu.Ui.Controls;

namespace Tetris.Game.Tests.Visual
{
    [TestFixture]
    public partial class TestSceneKeyBind : TetrisTestScene
    {
        public TestSceneKeyBind()
        {
            Add(new KeyBind(GameSetting.MoveLeft, GameConfigManager.GameControlsConfig));
        }
    }
}
