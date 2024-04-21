using NUnit.Framework;
using osu.Framework.Graphics;
using Tetris.Game.Menu.Ui.Settings;

namespace Tetris.Game.Tests.Visual.Settings
{
    [TestFixture]
    public partial class TestSceneSettingsMenu : TetrisTestScene
    {
        public TestSceneSettingsMenu()
        {
            Add(new SettingsMenu()
            {
                Anchor = Anchor.Centre,
                Scale =  new osuTK.Vector2(1,1),
            });
        }
    }
}
