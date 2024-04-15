using System;
using System.Linq;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Screens;
using osuTK;
using Tetris.Game.Menu;
using Tetris.Game.Menu.Ui;
using Tetris.Game.Realm;

namespace Tetris.Game.Game.UI.Screens
{
    public partial class FpsCounterScreen : Screen
    {
        private FpsCounter fpsCounter;

        [BackgroundDependencyLoader]
        private void load()
        {
            InternalChildren = new Drawable[]
            {
                fpsCounter = new FpsCounter(),
            };
        }
    }
}
