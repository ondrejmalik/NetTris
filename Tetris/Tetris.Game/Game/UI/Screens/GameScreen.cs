using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Screens;
using osuTK;

namespace Tetris.Game.Game.UI.Screens
{
    public partial class GameScreen : Screen
    {
        private GameContainer gameContainer1;

        public GameScreen()
        {
            gameContainer1 = new GameContainer() { Position = new Vector2(0, 0) };
        }

        [BackgroundDependencyLoader]
        private void load()
        {
            InternalChildren = new Drawable[]
            {
                gameContainer1,
            };
        }
    }
}
