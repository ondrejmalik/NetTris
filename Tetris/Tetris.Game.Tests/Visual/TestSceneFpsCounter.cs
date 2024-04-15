using osu.Framework.Graphics;
using NUnit.Framework;
using osu.Framework;
using osu.Framework.Platform;
using osu.Framework.Timing;
using osuTK;
using Tetris.Game.Menu.Ui;
using Tetris.Game.Menu.Ui.Leaderboard;
using Tetris.Game.Realm;

namespace Tetris.Game.Tests.Visual
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
