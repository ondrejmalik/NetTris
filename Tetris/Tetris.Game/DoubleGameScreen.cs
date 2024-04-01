using System;
using System.Diagnostics;
using System.Net;
using System.Threading;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Screens;
using osuTK;
using Tetris.Game.Networking;

namespace Tetris.Game
{
    public partial class DoubleGameScreen : Screen
    {
        private GameContainer gameContainer1;
        private GameContainer gameContainer2;


        private NetworkHandler networkHandler = new(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8543));

        public DoubleGameScreen(bool online = false)
        {
            gameContainer1 = new GameContainer() { Position = new Vector2(0, 0) };
            gameContainer2 = new GameContainer(true, online) { Position = new Vector2(1000, 0) };
            gameContainer1.PlayField.OpponentPlayField = gameContainer2.PlayField;
            gameContainer2.PlayField.OpponentPlayField = gameContainer1.PlayField;
            if (online)
            {
                Thread networkingThread = new Thread(() =>
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
    }
}
