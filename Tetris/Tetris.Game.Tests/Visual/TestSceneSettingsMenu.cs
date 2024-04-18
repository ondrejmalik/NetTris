using osu.Framework.Graphics;
using NUnit.Framework;
using Tetris.Game.Menu.Ui;

namespace Tetris.Game.Tests.Visual
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
