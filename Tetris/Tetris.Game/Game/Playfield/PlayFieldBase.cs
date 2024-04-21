using System;
using System.Collections.Generic;
using osu.Framework.Graphics.Containers;
using Tetris.Game.Game.UI;
using Tetris.Game.Networking;
using Tetris.Game.Networking.Commands;

namespace Tetris.Game.Game.Playfield
{
    public abstract partial class PlayFieldBase : CompositeDrawable
    {
        #region Public

        public static List<int> x = new List<int>();
        public static List<int> y = new List<int>();
        public event EventHandler<SendLinesEventArgs> ClearedLinesChanged;
        public event EventHandler<GameOverEventArgs> GameOverChanged;
        public HoldPreview HoldPreview;
        public Tetrimino.Tetrimino Piece;

        #endregion

        #region Protected

        public PlayField OpponentPlayField { get; set; }
        public List<OccupiedSet> Occupied = new List<OccupiedSet>();
        protected int clearedLines;
        protected Container box;
        protected Container grid;
        protected Container droppedContainer;
        protected bool isOpponent;
        protected bool isOnline;
        protected List<(int, int)> lastPieceGridPos;
        protected double loadTime;

        #endregion

        static PlayFieldBase()
        {
            setX();
            setY();
        }

        #region setDefaultXY

        protected static void setX()
        {
            for (int i = 0; i < 10; i++)
            {
                x.Add(50 * i);
            }
        }

        protected static void setY()
        {
            for (int i = 0; i < 20; i++)
            {
                y.Add(50 * i);
            }
        }

        #endregion

        #region InvokeEvents

        protected void OnClearedLinesChanged()
        {
            ClearedLinesChanged?.Invoke(this, new SendLinesEventArgs(clearedLines, lastPieceGridPos));
        }

        protected void OnGameOverChanged(bool lost)
        {
            GameOverChanged?.Invoke(this, new GameOverEventArgs(lost));
        }

        #endregion
    }
}
