using System.Collections.Generic;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Localisation;
using osu.Framework.Screens;
using osuTK;
using Tetris.Game.Game.UI.Screens;
using Tetris.Game.Menu.Ui;
using Tetris.Game.Menu.Ui.Leaderboard;


namespace Tetris.Game.Menu;

public partial class MainMenu : Screen
{
    public Container Box;
    public MenuButton PlayButton;
    public MenuButton MultiplayerButton;
    public MenuButton SettingsButton;
    public MenuButton LeaderboardsButton;
    public SettingsMenu SettingsMenu;
    public Leaderboard Leaderboard;

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
                                Text = "Endless", Action = () => { this.Push(new GameScreen()); },
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
                                Action = () => { this.Push(new DoubleGameScreen()); },
                                BaseSize = new Vector2(100, 75),
                                HoveredSize = new Vector2(120, 75)
                            },
                            new RoundedMenuButton()
                            {
                                Text = "Online",
                                Action = () => { this.Push(new DoubleGameScreen(true)); },
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
                                    Box.Add(SettingsMenu = new SettingsMenu());
                                }
                                else
                                {
                                    SettingsMenu.ToggleShow();
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
                                    Box.Add(Leaderboard = new Leaderboard());
                                }
                                else
                                {
                                    Leaderboard.ToggleShow();
                                }
                            }
                        },
                    }
                },
            }
        };
    }
}
