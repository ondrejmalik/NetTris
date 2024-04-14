using System;
using osu.Framework.Input.Events;
using osu.Framework.Screens;
using Tetris.Game.Menu;

namespace Tetris.Game.Game.UI.Screens
{
    public abstract partial class GameScreenBase : Screen
    {

        protected override bool OnKeyDown(KeyDownEvent e)
        {
            if (e.Key == osuTK.Input.Key.Escape)
            {
                this.Push(new MainMenu());
                return true;
            }
            return base.OnKeyDown(e);
        }
    }

}
