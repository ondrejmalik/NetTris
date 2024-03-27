using osu.Framework.Graphics;
using NUnit.Framework;

namespace Tetris.Game.Tests.Visual
{
    [TestFixture]
    public partial class TestSceneSpinningBox : TetrisTestScene
    {

        public TestSceneSpinningBox()
        {
            Hold hold = new Hold(null, null);
            HoldPreview holdPreview = new HoldPreview(hold);
            hold.HoldPreview = holdPreview;
            holdPreview.Hold = hold;
             holdPreview = new HoldPreview(hold);
            Add(new PlayField(holdPreview)
            {
                Position = new osuTK.Vector2(250, 0),
            });
        }
    }
}
