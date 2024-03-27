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
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    Direction = FillDirection.Vertical,
                    Spacing = new Vector2(0, 50),
                    AutoSizeAxes = Axes.Both,
                    Children = new Drawable[]
                    {
                        PlayButton = new MenuButton()
                        {
                            Text = new LocalisableString("Play"),
                            BackgroundColour = Colour4.Aqua,
                            Colour = Colour4.Wheat,
                            Action = () => this.Push(new GameScreen())
                        },
                        MultiplayerButton = new MenuButton()
                        {
                            Text = new LocalisableString("Multiplayer"),
                            BackgroundColour = Colour4.Aqua,
                            Colour = Colour4.Wheat,
                            //Action = () => this.Push(new MultiplayerMenu())
                        },
                        SettingsButton = new MenuButton()
                        {
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
                                    if (SettingsMenu.Position.X == -1000)
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
                            Text = new LocalisableString("Leaderboards"),
                            BackgroundColour = Colour4.Aqua,
                            Colour = Colour4.Wheat,
                        }
                    }
                }
            },
        };
    }
}
