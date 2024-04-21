using osu.Framework.Input.Events;
using osu.Framework.Screens;
using Tetris.Game.Menu;

namespace Tetris.Game.Game.Screens
{
    public abstract partial class GameScreenBase : Screen
    {
        protected override bool OnKeyDown(KeyDownEvent e)
        {
            if (e.Key == osuTK.Input.Key.Escape)
            {
                this.Push(new MainMenu());
                RemoveNetwork();
                return true;
            }

            return base.OnKeyDown(e);
        }

        protected abstract void RemoveNetwork();
    }
}
