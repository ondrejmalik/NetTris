﻿using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;


namespace Tetris.Game;

public partial class SettingsMenu : CompositeDrawable
{
    Container box;

    public SettingsMenu()
    {
    }

    [BackgroundDependencyLoader]
    private void load()
    {
        AutoSizeAxes = Axes.Both;
        InternalChild = box = new Container
        {
            Colour = Colour4.Gold,
            AutoSizeAxes = Axes.Both,
            Anchor = Anchor.TopLeft,
            Origin = Anchor.TopLeft,
            Children = new Drawable[]
            {
                new GraphicsSettingsSection()
            }
        };
        Position = new osuTK.Vector2(-1000, 200);

    }

    public void Show()
    {
        this.MoveTo( new osuTK.Vector2(0, 200), 250, Easing.OutQuint);
    }
    public void Hide()
    {
        this.MoveTo( new osuTK.Vector2(-1000, 200), 250, Easing.InQuint);
    }
}