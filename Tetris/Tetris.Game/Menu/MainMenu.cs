using System.Collections.Generic;
using System.Runtime.Serialization;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.UserInterface;
using osu.Framework.Input.Events;
using osu.Framework.Localisation;
using osu.Framework.Screens;
using osuTK;


namespace Tetris.Game.Menu;

public partial class MainMenu : Screen
{
    public Container Box;
    public MenuButton PlayButton;
    public MenuButton MultiplayerButton;
    public MenuButton SettingsButton;
    public MenuButton LeaderboardsButton;
    public SettingsMenu SettingsMenu;

    public MainMenu()
    {
        RelativeSizeAxes = Axes.Both;
    }

    [BackgroundDependencyLoader]
    private void load()
    {
        InternalChild = Box = new Container
        {
            RelativeSizeAxes = Axes.Both,
            Anchor = Anchor.TopLeft,
            Origin = Anchor.TopLeft,
            Children = new Drawable[]
            {
                new FillFlowContainer()
                {
                    RelativeAnchorPosition = new osuTK.Vector2(0, 0),
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    Direction = FillDirection.Vertical,
                    Spacing = new Vector2(0, 50),
                    AutoSizeAxes = Axes.Y,
                    Children = new Drawable[]
                    {
                        new HorizontalSlideoutButtonList(new List<Button>()
                        {
                            new MenuButton()
                            {
                                Text = "Endless", Action = () => { this.Push(new GameScreen()); },
                                BaseSize = new Vector2(100, 75), HoveredSize = new Vector2(120, 75)
                            },
                        })
                        {
                            Anchor = Anchor.TopCentre,
                            Origin = Anchor.TopCentre,
                            MainButton = new MenuButton()
                                { Text = "Singleplayer", },
                            Position = new Vector2(0, 0),
                        },
                        new HorizontalSlideoutButtonList(new List<Button>()
                        {
                            new MenuButton()
                            {
                                Text = "Local", Action = () => { this.Push(new DoubleGameScreen()); },
                                BaseSize = new Vector2(100, 75),
                                HoveredSize = new Vector2(120, 75)
                            },
                            new MenuButton()
                            {
                                Text = "P2P", Action = () => { }, BaseSize = new Vector2(100, 75),
                                HoveredSize = new Vector2(120, 75)
                            },
                            new MenuButton()
                            {
                                Text = "Online", Action = () => { }, BaseSize = new Vector2(100, 75),
                                HoveredSize = new Vector2(120, 75)
                            },
                        })
                        {
                            Anchor = Anchor.TopCentre,
                            Origin = Anchor.TopCentre,
                            MainButton = new MenuButton()
                                { Text = "Multiplayer", },
                            Position = new Vector2(0, 0),
                        },

                        SettingsButton = new MenuButton()
                        {
                            Anchor = Anchor.TopCentre,
                            Origin = Anchor.TopCentre,
                            RelativeAnchorPosition = new osuTK.Vector2(0, 0),
                            Text = new LocalisableString("Settings"),
                            BackgroundColour = Colour4.Aqua,
                            Colour = Colour4.Wheat,
                            Action = () =>
                            {
                                if (SettingsMenu == null)
                                {
                                    Box.Add(SettingsMenu = new SettingsMenu());
                                    SettingsMenu.Show();
                                }
                                else if (SettingsMenu.IsAlive)
                                {
                                    if (SettingsMenu.Position.X == -1500)
                                    {
                                        SettingsMenu.Show();
                                    }
                                    else
                                    {
                                        SettingsMenu.Hide();
                                    }
                                }
                            }
                        },
                        LeaderboardsButton = new MenuButton()
                        {
                            Anchor = Anchor.TopCentre,
                            Origin = Anchor.TopCentre,
                            RelativeAnchorPosition = new osuTK.Vector2(0, 0),
                            Text = new LocalisableString("Leaderboards"),
                            BackgroundColour = Colour4.Aqua,
                            Colour = Colour4.Wheat,
                        }
                    }
                },
            }
        };
    }
}
