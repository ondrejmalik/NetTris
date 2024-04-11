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

        protected virtual void OnClearedLinesChanged(int lines)
        {
            ClearedLinesChanged?.Invoke(this, new SendLinesEventArgs(lines));
        }
    }
}
