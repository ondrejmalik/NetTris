using System;
using System.Collections.Generic;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Input.Events;
using osuTK.Input;
using Tetris.Game.Config;


namespace Tetris.Game.Menu.Ui.Controls;

public partial class KeyBindsSection : CompositeDrawable
{
    FillFlowContainer box;
    public Dictionary<GameSetting, Key> Config;

    private SpriteText title = new()
        { Font = new("default", 25), Anchor = Anchor.TopCentre, Origin = Anchor.TopCentre };

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
        InternalChild = box = new FillFlowContainer()
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
            box.Add(new KeyBind(kvp.Key, Config));
        }
    }

    protected override bool OnMouseDown(MouseDownEvent e)
    {
        for (int i = 0; i < box.Children.Count; i++)
        {
            KeyBind child;
            try
            {
                child = (KeyBind)box.Children[i];
            }
            catch (Exception exception)
            {
                continue;
            }

            if (child.KeyButton.IsHovered)
            {
                child.Clicked = true;
                for (int j = i + 1; j < box.Children.Count; j++) // Reset all other keybinds
                {
                    try
                    {
                        child = (KeyBind)box.Children[j];
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
