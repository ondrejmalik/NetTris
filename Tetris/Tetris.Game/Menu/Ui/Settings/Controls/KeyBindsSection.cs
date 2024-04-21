using System;
using System.Collections.Generic;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Input.Events;
using osuTK.Input;
using Tetris.Game.Config;

namespace Tetris.Game.Menu.Ui.Settings.Controls;

/// <summary>
/// Lists all keybinds in a Config.
/// </summary>
public partial class KeyBindsSection : CompositeDrawable
{
    public FillFlowContainer Box;
    public Dictionary<GameSetting, Key> Config;

    private SpriteText title = new()
    {
        Font = new(size: 30),
        Anchor = Anchor.TopCentre,
        Origin = Anchor.TopCentre
    };

    public string TitleText
    {
        get
        {
            return title.Text.ToString();
        }
        set
        {
            title.Text = value;
        }
    }

    /// <param name="config">Game or opponent config form <see cref="GameConfigManager"/></param>
    /// <param name="titleText">Displayed title of the section</param>
    public KeyBindsSection(Dictionary<GameSetting, Key> config, string titleText = null)
    {
        if (titleText != null)
        {
            TitleText = titleText;
        }

        Config = config;
    }

    [BackgroundDependencyLoader]
    private void load()
    {
        Anchor = Anchor.TopCentre;
        Origin = Anchor.TopCentre;
        AutoSizeAxes = Axes.Both;
        InternalChild = Box = new FillFlowContainer()
        {
            Direction = FillDirection.Vertical,
            AutoSizeAxes = Axes.Both,
            Children = new Drawable[]
            {
                title
            }
        };
        foreach (var kvp in Config)
        {
            Box.Add(new KeyBind(kvp.Key, Config));
        }
    }

    /// <summary>
    /// Checks if a keybind is clicked and resets all other keybinds.
    /// </summary>
    protected override bool OnMouseDown(MouseDownEvent e)
    {
        for (int i = 0; i < Box.Children.Count; i++)
        {
            KeyBind child;
            try
            {
                child = (KeyBind)Box.Children[i];
            }
            catch (Exception exception)
            {
                continue;
            }

            if (child.KeyButton.IsHovered)
            {
                child.Clicked = true;
                for (int j = i + 1; j < Box.Children.Count; j++) // Reset all other keybinds
                {
                    try
                    {
                        child = (KeyBind)Box.Children[j];
                    }
                    catch (Exception exception)
                    {
                        continue;
                    }

                    child.Clicked = false;
                }
            }
            else
            {
                child.Clicked = false;
            }
        }

        return base.OnMouseDown(e);
    }
}
