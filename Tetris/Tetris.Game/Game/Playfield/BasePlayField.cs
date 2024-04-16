using System;
using System.Collections.Generic;
using osu.Framework.Graphics.Containers;
using Tetris.Game.Game.UI;
using Tetris.Game.Networking;

namespace Tetris.Game.Game.Playfield
{
    public abstract partial class BasePlayField : CompositeDrawable
    {
        public static List<int> x = new List<int>();
        public static List<int> y = new List<int>();
        public event EventHandler<SendLinesEventArgs> ClearedLinesChanged;

        public event EventHandler<EventArgs> GameOverChanged;
        public HoldPreview HoldPreview;
        public Tetrimino.Tetrimino Piece;
        public PlayField OpponentPlayField { get; set; }
        public List<OccupiedSet> Occupied = new List<OccupiedSet>();
        protected int clearedLines;
        protected Container box;
        protected Container grid;
        protected Container droppedContainer;
        protected bool isOpponent;
        protected bool isOnline;
        protected List<(int, int)> lastPieceGridPos;

        protected void OnClearedLinesChanged()
        {
            ClearedLinesChanged?.Invoke(this, new SendLinesEventArgs(clearedLines, lastPieceGridPos));
        }

        protected void OnGameOverChanged()
        {
            GameOverChanged?.Invoke(this, new EventArgs());
        }
    }
}
