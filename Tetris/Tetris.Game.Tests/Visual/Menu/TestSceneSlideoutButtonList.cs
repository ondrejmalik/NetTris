using System.Collections.Generic;
using NUnit.Framework;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osuTK;
using Tetris.Game.Menu.Ui;

namespace Tetris.Game.Tests.Visual.Menu
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
                    new HorizontalSlideoutButtonList(new List<MenuButton>()
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
                    new HorizontalSlideoutButtonList(new List<MenuButton>()
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
