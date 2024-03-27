using osu.Framework.Graphics;
using NUnit.Framework;

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
            });
        }
    }
}
