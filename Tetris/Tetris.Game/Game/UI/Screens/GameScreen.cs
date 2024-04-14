using System;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Screens;
using osuTK;
using Tetris.Game.Menu;

namespace Tetris.Game.Game.UI.Screens
{
    public partial class GameScreen : GameScreenBase
    {
        private GameContainer gameContainer1;

        public GameScreen()
        {
            gameContainer1 = new GameContainer() { Position = new Vector2(0, 0) };
            gameContainer1.PlayField.GameOverChanged += handleGameOver;
        }


        [BackgroundDependencyLoader]
        private void load()
        {
            InternalChildren = new Drawable[]
            {
                gameContainer1,
            };
        }

        private void handleGameOver(object sender, EventArgs e)
        {
            this.Push(new MainMenu());
        }
    }
}
