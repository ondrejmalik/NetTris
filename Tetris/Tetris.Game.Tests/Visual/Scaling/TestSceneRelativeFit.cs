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
    public partial class TestSceneRelativeFit : TetrisTestScene
    {
        public TestSceneRelativeFit()
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
                        RelativeSizeAxes = Axes.Both, // Set the size relative to the parent container
                        Children = new[]
                        {
                            new Box()
                            {
                                RelativeSizeAxes = Axes.Both,
                                RelativePositionAxes = Axes.Both,
                                FillMode = FillMode.Fit,
                                Size = new Vector2(0.5f, 1), // Adjust the size to fit within the container
                                Colour = Colour4.Red,
                            },
                            new Box()
                            {
                                RelativeSizeAxes = Axes.Both,
                                RelativePositionAxes = Axes.Both,
                                Position = new Vector2(0.5f, 0),
                                FillMode = FillMode.Fit,
                                Size = new Vector2(0.5f, 1), // Adjust the size to fit within the container
                                Colour = Colour4.Blue,
                            },
                        }
                    }
                }
            });
        }
    }
}
