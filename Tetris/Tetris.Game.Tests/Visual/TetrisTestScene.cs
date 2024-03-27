using osu.Framework.Testing;

namespace Tetris.Game.Tests.Visual
{
    public partial class TetrisTestScene : TestScene
    {
        protected override ITestSceneTestRunner CreateRunner() => new TetrisTestSceneTestRunner();

        private partial class TetrisTestSceneTestRunner : TetrisGameBase, ITestSceneTestRunner
        {
            private TestSceneTestRunner.TestRunner runner;

            protected override void LoadAsyncComplete()
            {
                base.LoadAsyncComplete();
                Add(runner = new TestSceneTestRunner.TestRunner());
            }

            public void RunTestBlocking(TestScene test) => runner.RunTestBlocking(test);
        }
    }
}