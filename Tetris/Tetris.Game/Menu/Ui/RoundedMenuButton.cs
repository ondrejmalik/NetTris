using System;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.UserInterface;
using osu.Framework.Input.Events;
using osuTK;

namespace Tetris.Game.Menu.Ui;

public partial class RoundedMenuButton : MenuButton
{
    public RoundedMenuButton()
    {
        CornerRadius = 20;
        Masking = true;
    }
}
