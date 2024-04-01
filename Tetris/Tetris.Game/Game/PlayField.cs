using System;
using System.Collections.Generic;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Textures;
using osu.Framework.Input.Events;
using osu.Framework.Logging;
using osuTK;
using osuTK.Input;
using Tetris.Game.Config;
namespace Tetris.Game
{
    public partial class PlayField : CompositeDrawable
    {
        public int ClearedLines
        {
            get
            {
                return _clearedLines;
            }
            set
            {
                _clearedLines = value;
                OnClearedLinesChanged(EventArgs.Empty);
            }
        }

        public static List<int> x = new List<int>();
        public static List<int> y = new List<int>();
        public event EventHandler ClearedLinesChanged;
        public HoldPreview HoldPreview;
        public Tetrimino Piece;
        public PlayField OpponentPlayField { get; set; }
        private bool isOpponent;
        public List<OccupiedSet> Occupied = new List<OccupiedSet>();
        private int _clearedLines;
        private Container box;
        private Container grid;
        private Container droppedContainer;
        private bool isOnline;

        public PlayField(HoldPreview holdPreview, bool isOpponent = false, bool isOnline = false)
        {
            this.isOpponent = isOpponent;
            this.isOnline = isOnline;
            HoldPreview = holdPreview;
            for (int i = 0; i < 10; i++)
            {
                x.Add(50 * i);
            }

            for (int i = 0; i < 20; i++)
            {
                y.Add(50 * i);
            }
        }

        [BackgroundDependencyLoader]
        private void load(TextureStore textures)
        {
            AutoSizeAxes = Axes.Both;
            Origin = Anchor.TopLeft;
            InternalChild = box = new Container
            {
                AutoSizeAxes = Axes.Both,
                Anchor = Anchor.TopLeft,
                Origin = Anchor.TopLeft,
                Children = new Drawable[]
                {
                    new Box
                    {
                        RelativeSizeAxes = Axes.Both,
                        Colour = Colour4.White,
                        Anchor = Anchor.Centre,
                        Origin = Anchor.Centre,
                    },
                    grid = new Container()
                    {
                        Anchor = Anchor.BottomRight,
                        Origin = Anchor.BottomRight,
                        AutoSizeAxes = Axes.Both,
                    },
                    droppedContainer = new Container()
                    {
                        Anchor = Anchor.TopLeft,
                        Origin = Anchor.TopLeft,
                        AutoSizeAxes = Axes.Both,
                    },
                }
            };
            for (int i = 0; i < x.Count; i++)
            {
                grid.Add(new Box()
                {
                    Size = new Vector2(5, 1000),
                    Position = new Vector2(x[i], 0),
                    Anchor = Anchor.TopLeft,
                    Colour = Colour4.SlateGray,
                });
            }

            for (int i = 0; i < y.Count; i++)
            {
                grid.Add(new Box()
                {
                    Size = new Vector2(500, 5),
                    Position = new Vector2(0, y[i]),
                    Anchor = Anchor.TopLeft,
                    Colour = Colour4.SlateGray,
                });
            }

            for (int i = 0; i < 20; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    Occupied.Add(new OccupiedSet()
                    {
                        X = j,
                        Y = i,
                        Occupied = false,
                    });
                }
            }


            box.Add(Piece = new Tetrimino(HoldPreview.Hold.Bag.Dequeue(), 3, 2, this, isOpponent, isOnline));
        }

        public void ScheduleRedraw()
        {
            Scheduler.Add(() => redrawOccupied());
            Scheduler.Add( () => Piece.SetDrawPos());
        }

        private void redrawOccupied()
        {
            if (isOnline) { }

            droppedContainer.Clear();
            foreach (var box in Occupied)
            {
                if (box)
                {
                    droppedContainer.Add(new Box()
                    {
                        Size = new Vector2(45, 45),
                        Position = new Vector2(PlayField.x[box.X] + 5, PlayField.y[box.Y] + 5),
                        Anchor = Anchor.TopLeft,
                        Colour = isOnline ? Colour4.Black : box.Colour,
                    });
                }
            }
        }

        internal bool BottomCollisionDetection()
        {
            for (int i = 0; i < Piece.GridPos.Count; i++)
            {
                if (Piece.GridPos[i].Item2 == 19)
                {
                    place();
                    return true;
                }
            }

            return false;
        }

        internal bool CollisionDetection(int diff)
        {
            for (int i = 0; i < Piece.GridPos.Count; i++)
            {
                int index = Piece.GridPos[i].Item1 + diff + Piece.GridPos[i].Item2 * 10;
                if (index < 0 || index > 199)
                {
                    return true;
                }

                if (Occupied[index])
                {
                    if (diff == 0) { place(); }

                    return true;
                }
            }

            return false;
        }

        private void place()
        {
            foreach (var pos in Piece.GridPos)
            {
                Colour4 c = Piece.PieceColour;
                droppedContainer.Add(new Box()
                {
                    Size = new Vector2(45, 45),
                    Position = new Vector2(PlayField.x[pos.Item1] + 5, PlayField.y[pos.Item2] + 5),
                    Anchor = Anchor.TopLeft,
                    Colour = c,
                });

                OccupiedSet o = new OccupiedSet();
                o = Occupied[pos.Item1 + pos.Item2 * 10 - 10];
                o.Occupied = true;
                o.Colour = c;
                Occupied[pos.Item1 + pos.Item2 * 10 - 10] = o;
            }

            expireTetrimino();
            //check if new piece overlaps existing piece
            foreach (var pos in Piece.GridPos)
            {
                if (Occupied[pos.Item1 + pos.Item2 * 10] == true)
                {
                    Logger.Log("Game Over");
                }
            }


            int diff = clearLine();
            for (int i = 0; i < diff; i++)
            {
                OpponentPlayField?.addLine(2);
            }

            ClearedLines += diff;
            Logger.Log(ClearedLines.ToString());

            box.Add(Piece);
            HoldPreview.Hold.CanHold = true;
            HoldPreview.UpdatePreviewTetriminos();
        }

        private void expireTetrimino()
        {
            Piece.Expire();
            Piece = new Tetrimino(HoldPreview.Hold.Bag.Dequeue(), 4, 0, this, isOpponent, isOnline);
        }

        private int clearLine()
        {
            bool clear = false;
            int cleared = 0;
            for (int i = 0; i < Occupied.Count; i += 10)
            {
                clear = true;
                for (int j = i; j < i + 10; j++)
                {
                    if (!Occupied[j])
                    {
                        clear = false;
                        break;
                    }
                }

                if (clear)
                {
                    cleared++;
                    for (int j = i; j < i + 10; j++)
                    {
                        newstackDown(j);
                    }
                }
            }

            redrawOccupied();
            return cleared;
        }

        private void newstackDown(int j)
        {
            Occupied[j].Occupied = false;
            recurseStackDown(j);
        }

        private void recurseStackDown(int j)
        {
            if (j > 10)
            {
                Occupied[j].Occupied = Occupied[j - 10].Occupied;
                Occupied[j].Colour = Occupied[j - 10].Colour;
                recurseStackDown(j - 10);
            }
        }

        private void addLine(int emptyIndex)
        {
            for (int i = 0; i < 10; i++)
            {
                recurseStackUp(i);
            }

            bool foundGarbage = false;
            int lineStart = Occupied.Count - 10;
            for (int i = Occupied.Count - 1; i >= 0; i = i - 1) // check garbage line
            {
                if (Occupied[i].Colour == Colour4.Gray)
                {
                    foundGarbage = true;
                    i = lineStart - 10;
                }

                if (i % 10 == 0)
                {
                    lineStart = i;
                    if (!foundGarbage)
                    {
                        break;
                    }

                    break;
                }
            }

            for (int j = lineStart; j < lineStart + 10; j++)
            {
                if (emptyIndex + lineStart == j) // add empty hole
                {
                    Occupied[j].Occupied = false;
                    continue;
                }

                Occupied[j].Occupied = true;
                Occupied[j].Colour = Colour4.Gray;
            }


            redrawOccupied();
        }

        private void recurseStackUp(int j)
        {
            if (j < 190)
            {
                if (j > 10)
                {
                    Occupied[j - 10].Occupied = Occupied[j].Occupied;
                    Occupied[j - 10].Colour = Occupied[j].Colour;
                }

                recurseStackUp(j + 10);
            }
        }


        protected override bool OnMouseDown(MouseDownEvent e)
        {
            if (e.Button == MouseButton.Left)
            {
                Piece.Rotate(false);
                return base.OnMouseDown(e);
            }

            Piece.Rotate(true);
            return base.OnMouseDown(e);
        }

        protected override bool OnKeyDown(KeyDownEvent e)
        {
            switch (e.Key)
            {
                case var value when value == GameConfigManager.GameControlsConfig[GameSetting.Hold]:
                    if (HoldPreview.Hold.CanHold)
                    {
                        HoldPreview.Hold.HeldPiece = Piece.PieceType;
                        expireTetrimino();
                        box.Add(Piece);
                        HoldPreview.Hold.CanHold = false;
                    }

                    break;
                case Key.T:
                    addLine(2);
                    break;
            }

            return base.OnKeyDown(e);
        }

        protected virtual void OnClearedLinesChanged(EventArgs e)
        {
            ClearedLinesChanged?.Invoke(this, e);
        }
    }
}
