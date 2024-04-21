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

        /// <summary>
        /// Absolute x coordinates of the grid.
        /// </summary>
        public static List<int> x = new List<int>();

        /// <summary>
        /// Absolute y coordinates of the grid.
        /// </summary>
        public static List<int> y = new List<int>();

        /// <summary>
        /// Invoked when the number of cleared lines changes.
        /// </summary>
        public event EventHandler<SendLinesEventArgs> ClearedLinesChanged;

        /// <summary>
        ///  Invoked when the game is over.
        /// </summary>
        public event EventHandler<GameOverEventArgs> GameOverChanged;

        public HoldPreview HoldPreview;

        public Tetrimino.Tetrimino Piece;

        #endregion

        #region Protected

        /// <summary>
        /// Other player's playfield can be null if playing singleplayer.
        /// </summary>
        public PlayField OpponentPlayField { get; set; }

        /// <summary>
        /// Grid of the playfield.
        /// </summary>
        public List<OccupiedSet> Occupied = new List<OccupiedSet>();

        /// <summary>
        /// Number of cleared lines.
        /// </summary>
        protected int clearedLines;

        protected Container box;
        protected Container grid;
        protected Container droppedContainer;
        protected bool isOpponent;
        protected bool isOnline;
        protected List<(int, int)> lastPieceGridPos;

        /// <summary>
        /// The time when the playfield was loaded.
        /// </summary>
        protected double loadTime;

        #endregion

        static PlayFieldBase()
        {
            SetX();
            SetY();
        }

        #region setDefaultXY

        /// <summary>
        /// Sets the absolute x coordinates of the grid.
        /// </summary>
        protected static void SetX()
        {
            for (int i = 0; i < 10; i++)
            {
                x.Add(50 * i);
            }
        }

        /// <summary>
        /// Sets the absolute y coordinates of the grid.
        /// </summary>
        protected static void SetY()
        {
            for (int i = 0; i < 20; i++)
            {
                y.Add(50 * i);
            }
        }

        #endregion

        #region InvokeEvents

        /// <summary>
        /// Invokes the ClearedLinesChanged event.
        /// </summary>
        protected void OnClearedLinesChanged()
        {
            ClearedLinesChanged?.Invoke(this, new SendLinesEventArgs(clearedLines, lastPieceGridPos));
        }

        /// <summary>
        ///  Invokes the GameOverChanged event.
        /// </summary>
        /// <param name="lost">Whether the player lost or not.</param>
        protected void OnGameOverChanged(bool lost)
        {
            GameOverChanged?.Invoke(this, new GameOverEventArgs(lost));
        }

        #endregion
    }
}
