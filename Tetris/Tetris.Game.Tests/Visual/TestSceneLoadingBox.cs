using NUnit.Framework;
using osu.Framework;
using osu.Framework.Platform;
using Tetris.Game.Game.UI;

namespace Tetris.Game.Tests.Visual
{
    [TestFixture]
    public partial class TestSceneLoadingBox : TetrisTestScene
    {
        public TestSceneLoadingBox()
        {
            GameHost host = Host.GetSuitableDesktopHost("visual-tests");
            Add(new LoadingBox() { });
        }
    }
}
