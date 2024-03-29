using System;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Screens;
using osuTK;

namespace Tetris.Game
{
    public partial class DoubleGameScreen : Screen
    {
        private GameContainer gameContainer1;
        private GameContainer gameContainer2;

        public DoubleGameScreen()
        {
            gameContainer1 = new GameContainer() { Position = new Vector2(0, 0) };
            gameContainer2 = new GameContainer(true) { Position = new Vector2(1000, 0) };
            gameContainer1.PlayField.OpponentPlayField = gameContainer2.PlayField;
            gameContainer2.PlayField.OpponentPlayField = gameContainer1.PlayField;
        }

        [BackgroundDependencyLoader]
        private void load()
        {
            InternalChildren = new Drawable[]
            {
                gameContainer1,
                gameContainer2,
            };
        }
    }
}
