using System.Collections.Generic;
using osu.Framework.Graphics;
using NUnit.Framework;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.UserInterface;
using osu.Framework.Localisation;
using osuTK;
using Tetris.Game.Menu.Ui;

namespace Tetris.Game.Tests.Visual
{
    [TestFixture]
    public partial class TestSceneSlideoutButtonList : TetrisTestScene
    {
        public TestSceneSlideoutButtonList()
        {
            Add(new FillFlowContainer()
            {
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
                Direction = FillDirection.Vertical,
                Spacing = new Vector2(0, 50),
                AutoSizeAxes = Axes.Both,
                Children = new Drawable[]
                {
                    new HorizontalSlideoutButtonList(new List<Button>()
                    {
                        new MenuButton()
                            { Text = "Endless", BaseSize = new Vector2(100, 75), HoveredSize = new Vector2(120, 75) },
                    })
                    {
                        Anchor = Anchor.TopCentre,
                        Origin = Anchor.TopCentre,
                        MainButton = new MenuButton()
                            { Text = "Singleplayer", },
                    },
                    new HorizontalSlideoutButtonList(new List<Button>()
                    {
                        new MenuButton()
                            { Text = "Local", BaseSize = new Vector2(100, 75), HoveredSize = new Vector2(120, 75) },
                        new MenuButton()
                            { Text = "P2P", BaseSize = new Vector2(100, 75), HoveredSize = new Vector2(120, 75) },
                        new MenuButton()
                            { Text = "Online", BaseSize = new Vector2(100, 75), HoveredSize = new Vector2(120, 75) },
                    })
                    {
                        Anchor = Anchor.TopCentre,
                        Origin = Anchor.TopCentre,
                        MainButton = new MenuButton()
                            { Text = "Multiplayer", },
                    },
                },
            });
        }
    }
}
