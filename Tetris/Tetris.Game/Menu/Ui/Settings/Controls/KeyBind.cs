using System;
using System.Collections.Generic;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.UserInterface;
using osu.Framework.Input.Events;
using osuTK.Input;
using Tetris.Game.Config;
using Tetris.Game.Realm;

namespace Tetris.Game.Menu.Ui.Settings.Controls;

public partial class KeyBind : CompositeDrawable
{
    FillFlowContainer box;
    public GameSetting Setting;
    public Key Key;
    public SpriteText SettingNameText;
    public BasicButton KeyButton;
    public Colour4 BaseColour { get; set; } = Colour4.DeepSkyBlue;
    public Colour4 ClickedColour { get; set; } = Colour4.Red;
    private readonly Dictionary<GameSetting, Key> Config;

    public bool Clicked
    {
        get
        {
            return clicked;
        }
        set
        {
            clicked = value;
            if (clicked)
            {
                KeyButton.Colour = ClickedColour;
            }
            else
            {
                KeyButton.Colour = BaseColour;
            }
        }
    }

    private bool clicked;

    public KeyBind(GameSetting setting, Dictionary<GameSetting, Key> config)
    {
        Setting = setting;
        Key = config[setting];
        Config = config;
    }

    [BackgroundDependencyLoader]
    private void load()
    {
        AutoSizeAxes = Axes.Both;
        InternalChild = box = new FillFlowContainer()
        {
            Margin = new MarginPadding(10),
            Direction = FillDirection.Horizontal,
            AutoSizeAxes = Axes.Both,


            Children = new Drawable[]
            {
                SettingNameText = new SpriteText()
                {
                    Text = Setting.ToString(),
                    Font = new FontUsage(size: 25),


                    Margin = new MarginPadding(10),
                },
                new Container()
                {
                    AutoSizeAxes = Axes.Both,
                    CornerRadius = 13,
                    BorderColour = Colour4.Gray,
                    BorderThickness = 3,
                    Masking = true,
                    Child = KeyButton = new BasicButton()
                    {
                        // Limit the key name to 5 characters to prevent overflow
                        // Math.Min is used to prevent errors when the key name is shorter than 5 characters
                        Text = Key.ToString().Substring(0, Math.Min(5, Key.ToString().Length)),


                        Size = new osuTK.Vector2(50, 50),
                        Colour = BaseColour
                    }
                }
            }
        };
    }


    protected override bool OnKeyDown(KeyDownEvent e)
    {
        if (Clicked)
        {
            Config[Setting] = e.Key;
            RealmManager.SaveConfig();
            Clicked = false;
            Key = e.Key;
            KeyButton.Text = e.Key.ToString().Substring(0, Math.Min(5, Key.ToString().Length));
        }

        return base.OnKeyDown(e);
    }
}
