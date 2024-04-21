using System;
using System.Threading;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Screens;
using osuTK;
using Tetris.Game.Config;
using Tetris.Game.Game.UI;
using Tetris.Game.Menu;
using Tetris.Game.Networking;
using Tetris.Game.Networking.Commands;

namespace Tetris.Game.Game.Screens
{
    public partial class DoubleGameScreen : GameScreenBase
    {
        private FillFlowContainer ffContainer;
        private GameContainer gameContainer1;
        private GameContainer gameContainer2;
        private Thread networkingThread;
        private LoadingBox loadingBox;
        private NetworkHandler networkHandler;

        public DoubleGameScreen(bool online = false)
        {
            networkHandler = new NetworkHandler(GameConfigManager.OnlineConfig[OnlineSetting.Ip],
                int.Parse(GameConfigManager.OnlineConfig[OnlineSetting.Port]));
            gameContainer1 = new GameContainer(true);
            gameContainer2 = new GameContainer(isOnline: online, isOpponent: true);
            gameContainer1.Scale = new Vector2(0.8f);
            gameContainer2.Scale = new Vector2(0.8f);
            gameContainer1.PlayField.OpponentPlayField = gameContainer2.PlayField;
            gameContainer2.PlayField.OpponentPlayField = gameContainer1.PlayField;
            if (online)
            {
                networkingThread = new Thread(() =>
                {
                    networkHandler.Start(gameContainer1.PlayField, gameContainer2.PlayField);
                });
                networkingThread.Start();
                networkHandler.GameIsReady += handleGameIsReady;
                networkHandler.GameOver += handleGameOver;
            }
            else
            {
                gameContainer1.PlayField.GameOverChanged += handleGameOver;
                OnLoadComplete += _ =>
                {
                    handleGameIsReady(null, null);
                };
            }
        }

        [BackgroundDependencyLoader]
        private void load()
        {
            InternalChildren = new Drawable[]
            {
                loadingBox = new LoadingBox(),
                ffContainer = new FillFlowContainer()
                {
                    Anchor = Anchor.TopCentre,
                    Origin = Anchor.TopCentre,
                    Direction = FillDirection.Horizontal,
                    Spacing = new Vector2(50),
                    AutoSizeAxes = Axes.Both,
                    Children = new Drawable[]
                    {
                    }
                }
            };
        }


        #region Event Handlers

        private void handleGameIsReady(object sender, EventArgs eventArgs)
        {
            Scheduler.Add(() =>
            {
                loadingBox.Hide();
                //loadingBox.Dispose();
                ffContainer.AddRange(new Drawable[]
                {
                    gameContainer1,
                    gameContainer2,
                });
            });
        }

        private void handleGameOver(object sender, GameOverEventArgs eventArgs)
        {
            Scheduler.Add(() =>
            {
                RemoveNetwork();
                this.Push(new MainMenu());
            });
        }

        #endregion

        #region Dispose

        protected override void Dispose(bool isDisposing)
        {
            RemoveNetwork();
            base.Dispose(isDisposing);
        }

        protected override void RemoveNetwork()
        {
            if (networkingThread != null && networkHandler != null) //in case of playing local
            {
                networkHandler.Running = false;
                networkHandler.Client.Close();
                networkHandler = null;
                networkingThread.Join();
            }
        }

        #endregion
    }
}
