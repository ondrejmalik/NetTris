using System.Net;
using System.Threading;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Screens;
using osuTK;
using Tetris.Game.Networking;

namespace Tetris.Game.Game.UI.Screens
{
    public partial class DoubleGameScreen : GameScreenBase
    {
        private GameContainer gameContainer1;
        private GameContainer gameContainer2;
        private Thread networkingThread;

        private NetworkHandler networkHandler = new(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8543));

        public DoubleGameScreen(bool online = false)
        {
            gameContainer1 = new GameContainer(true) { Position = new Vector2(0, 0) };
            gameContainer2 = new GameContainer(isOnline: online, isOpponent: true) { Position = new Vector2(1000, 0) };
            gameContainer1.PlayField.OpponentPlayField = gameContainer2.PlayField;
            gameContainer2.PlayField.OpponentPlayField = gameContainer1.PlayField;
            if (online)
            {
                networkingThread = new Thread(() =>
                {
                    networkHandler.Loop(gameContainer1.PlayField, gameContainer2.PlayField);
                });
                networkingThread.Start();
            }
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

        protected override void Dispose(bool isDisposing)
        {
            if (networkingThread != null) //in case of playing local
            {
                networkHandler.Running = false;
                networkHandler.Client.Close();
                networkingThread.Join();
            }

            base.Dispose(isDisposing);
        }
    }
}
