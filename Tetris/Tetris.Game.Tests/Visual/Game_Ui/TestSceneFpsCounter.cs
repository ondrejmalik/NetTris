using NUnit.Framework;
using osu.Framework;
using osu.Framework.Platform;
using osuTK;
using Tetris.Game.Menu.Ui.Settings;

namespace Tetris.Game.Tests.Visual.Game_Ui
{
    [TestFixture]
    public partial class TestSceneFpsCounter : TetrisTestScene
    {
        public TestSceneFpsCounter()
        {
            GameHost host = Host.GetSuitableDesktopHost("visual-tests");
            Add(new FpsCounter()
            {
                Scale = new Vector2(2),
            });
        }
    }
}
