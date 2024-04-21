using System;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Screens;
using Tetris.Game.Config;
using Tetris.Game.Menu;
using Tetris.Game.Menu.Ui.Settings;
using Tetris.Game.Realm;

namespace Tetris.Game.Game.Screens
{
    public partial class GameScreen : GameScreenBase
    {
        private GameContainer gameContainer;

        public GameScreen()
        {
            gameContainer = new GameContainer()
            {
                Anchor = Anchor.TopCentre,
                Origin = Anchor.TopCentre,
            };
            gameContainer.PlayField.GameOverChanged += handleGameOver;
        }


        [BackgroundDependencyLoader]
        private void load()
        {
            InternalChildren = new Drawable[]
            {
                gameContainer,
                new FpsCounter()
            };
        }

        private void handleGameOver(object sender, EventArgs e)
        {
            RealmManager.AddScore(GameConfigManager.UserConfig[UserSetting.Username],
                gameContainer.PlayField.ClearedLines);
            this.Push(new MainMenuScreen());
        }

        protected override void RemoveNetwork()
        {
        }
    }
}
