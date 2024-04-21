using NUnit.Framework;
using osu.Framework;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Platform;
using osuTK;

namespace Tetris.Game.Tests.Visual.Scaling
{
    [TestFixture]
    public partial class TestSceneRelativeScaling : TetrisTestScene
    {
        public TestSceneRelativeScaling()
        {
            GameHost host = Host.GetSuitableDesktopHost("visual-tests");
            Add(new Container()
            {
                FillMode = FillMode.Stretch,
                FillAspectRatio = 1,
                Width = 150,
                Height = 100,
                Children = new Drawable[]
                {
                    new Container()
                    {
                        FillMode = FillMode.Stretch,
                        BorderColour = Colour4.Yellow,
                        RelativeSizeAxes = Axes.Both,
                        Children = new[]
                        {
                            new Box()
                            {
                                RelativePositionAxes = Axes.Both,
                                RelativeSizeAxes = Axes. Both,
                                Position = new Vector2(0.5f, 0),
                                FillMode = FillMode.Stretch,
                                Size = new Vector2(0.5f),
                                Colour = Colour4.Red,
                            },
                            new Box()
                            {
                                RelativePositionAxes = Axes.Both,
                                RelativeSizeAxes = Axes. Both,
                                Position = new Vector2(0, 0),
                                FillMode = FillMode.Stretch,
                                Size = new Vector2(0.5f),
                                Colour = Colour4.Blue,
                            },
                        }
                    }
                }
            });
        }
    }
}
