using System.Collections.Generic;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Localisation;
using osu.Framework.Platform;
using osu.Framework.Screens;
using osuTK;
using Tetris.Game.Game.Screens;
using Tetris.Game.Menu.Ui;
using Tetris.Game.Menu.Ui.Leaderboard;
using Tetris.Game.Menu.Ui.Settings;


namespace Tetris.Game.Menu;

public partial class MainMenuScreen : Screen
{
    public Container Box;
    public MenuButton PlayButton;
    public MenuButton MultiplayerButton;
    public MenuButton SettingsButton;
    public MenuButton LeaderboardsButton;
    public SettingsMenu SettingsMenu;
    public Leaderboard Leaderboard;
    [Resolved] private GameHost host { get; set; }
    private string windowTitle = "NetTris";

    public MainMenuScreen()
    {
        RelativeSizeAxes = Axes.Both;
    }

    [BackgroundDependencyLoader]
    private void load()
    {
        host.Window.Title = windowTitle;
        InternalChild = Box = new Container
        {
            RelativeSizeAxes = Axes.Both,


            Children = new Drawable[]
            {
                new FillFlowContainer()
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    Direction = FillDirection.Vertical,
                    Spacing = new Vector2(0, 50),
                    AutoSizeAxes = Axes.Y,
                    Children = new Drawable[]
                    {
                        new HorizontalSlideoutButtonList(new List<MenuButton>()
                        {
                            new RoundedMenuButton()
                            {
                                Text = "Endless", Action = () =>
                                {
                                    host.Window.Title = "Playing Endless";
                                    this.Push(new GameScreen());
                                },
                                BaseSize = new Vector2(100, 75), HoveredSize = new Vector2(120, 75)
                            },
                        })
                        {
                            MainButton = PlayButton = new RoundedMenuButton() { Text = "Singleplayer", },
                            Position = new Vector2(0, 0),
                        },
                        new HorizontalSlideoutButtonList(new List<MenuButton>()
                        {
                            new RoundedMenuButton()
                            {
                                Text = "Local",
                                Action = () =>
                                {
                                    host.Window.Title = "Playing Local Multiplayer";
                                    this.Push(new DoubleGameScreen());
                                },
                                BaseSize = new Vector2(100, 75),
                                HoveredSize = new Vector2(120, 75)
                            },
                            new RoundedMenuButton()
                            {
                                Text = "Online",
                                Action = () =>
                                {
                                    host.Window.Title = "Playing Online Multiplayer";
                                    this.Push(new DoubleGameScreen(true));
                                },
                                BaseSize = new Vector2(100, 75),
                                HoveredSize = new Vector2(120, 75)
                            },
                        })
                        {
                            MainButton = MultiplayerButton = new RoundedMenuButton() { Text = "Multiplayer", },
                            Position = new Vector2(0, 0),
                        },
                        // TODO: fix settings opening animation
                        SettingsButton = new RoundedMenuButton()
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
                                    host.Window.Title = "Settings";
                                    Box.Add(SettingsMenu = new SettingsMenu());
                                }
                                else
                                {
                                    host.Window.Title = windowTitle;
                                    SettingsMenu.ToggleShow(windowTitle, host);
                                }
                            }
                        },
                        LeaderboardsButton = new RoundedMenuButton()
                        {
                            Anchor = Anchor.TopCentre,
                            Origin = Anchor.TopCentre,
                            RelativeAnchorPosition = new osuTK.Vector2(0, 0),
                            Text = new LocalisableString("Leaderboards"),
                            BackgroundColour = Colour4.Aqua,
                            Colour = Colour4.Wheat,
                            Action = () =>
                            {
                                if (Leaderboard == null)
                                {
                                    host.Window.Title = "Leaderboards";
                                    Box.Add(Leaderboard = new Leaderboard());
                                }
                                else
                                {
                                    host.Window.Title = windowTitle;
                                    Leaderboard.ToggleShow(windowTitle, host);
                                }
                            }
                        },
                    }
                },
                new FpsCounter()
            }
        };
    }
}
