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
        public static List<int> x = new List<int>();
        public static List<int> y = new List<int>();
        public event EventHandler ClearedLinesChanged;
        public HoldPreview HoldPreview;
        public PlayField OpponentPlayField { get; set; }
        private bool isOpponent;

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

        public List<OcupiedSet> ocupied = new List<OcupiedSet>();

        private int _clearedLines;
        private Container box;
        private Tetrimino p;
        private Container grid;
        private Container droppedContainer;

        public PlayField(HoldPreview holdPreview, bool isOpponent = false)
        {
            this.isOpponent = isOpponent;
            HoldPreview = holdPreview;
            for (int i = 0; i < 10; i++)
            {
                x.Add(50 * i);
            }

            for (int i = 0; i < 20; i++)
            {
                y.Add(50 * i);
            }

            AutoSizeAxes = Axes.Both;
            Origin = Anchor.TopLeft;
        }

        [BackgroundDependencyLoader]
        private void load(TextureStore textures)
        {
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
                    ocupied.Add(new OcupiedSet()
                    {
                        X = j,
                        Y = i,
                        Occupied = false,
                    });
                }
            }

            box.Add(p = new Tetrimino(HoldPreview.Hold.Bag.Dequeue(), 3, 2, this, isOpponent));
        }

        internal bool BottomCollisionDetection()
        {
            for (int i = 0; i < p.GridPos.Count; i++)
            {
                if (p.GridPos[i].Item2 == 19)
                {
                    place();
                    return true;
                }
            }

            return false;
        }

        internal bool CollisionDetection(int diff)
        {
            for (int i = 0; i < p.GridPos.Count; i++)
            {
                int index = p.GridPos[i].Item1 + diff + p.GridPos[i].Item2 * 10;
                if (index < 0 || index > 199)
                {
                    return true;
                }

                if (ocupied[index])
                {
                    if (diff == 0) { place(); }

                    return true;
                }
            }

            return false;
        }

        private void place()
        {
            foreach (var pos in p.GridPos)
            {
                Colour4 c = p.PieceColour;
                droppedContainer.Add(new Box()
                {
                    Size = new Vector2(45, 45),
                    Position = new Vector2(PlayField.x[pos.Item1] + 5, PlayField.y[pos.Item2] + 5),
                    Anchor = Anchor.TopLeft,
                    Colour = c,
                });

                OcupiedSet o = new OcupiedSet();
                o = ocupied[pos.Item1 + pos.Item2 * 10 - 10];
                o.Occupied = true;
                o.Colour = c;
                ocupied[pos.Item1 + pos.Item2 * 10 - 10] = o;
            }

            expireTetrimino();
            //check if new piece overlaps existing piece
            foreach (var pos in p.GridPos)
            {
                if (ocupied[pos.Item1 + pos.Item2 * 10] == true)
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

            box.Add(p);
            HoldPreview.Hold.CanHold = true;
            HoldPreview.UpdatePreviewTetriminos();
        }

        private void expireTetrimino()
        {
            p.Expire();
            p = new Tetrimino(HoldPreview.Hold.Bag.Dequeue(), 4, 0, this, isOpponent);
        }

        private int clearLine()
        {
            bool clear = false;
            int cleared = 0;
            for (int i = 0; i < ocupied.Count; i += 10)
            {
                clear = true;
                for (int j = i; j < i + 10; j++)
                {
                    if (!ocupied[j])
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

            redrawOcupied();
            return cleared;
        }

        private void newstackDown(int j)
        {
            ocupied[j].Occupied = false;
            recurseStackDown(j);
        }

        private void recurseStackDown(int j)
        {
            if (j > 10)
            {
                ocupied[j].Occupied = ocupied[j - 10].Occupied;
                ocupied[j].Colour = ocupied[j - 10].Colour;
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
            int lineStart = ocupied.Count - 10;
            for (int i = ocupied.Count - 1; i >= 0; i = i - 1) // check garbage line
            {
                if (ocupied[i].Colour == Colour4.Gray)
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
                    ocupied[j].Occupied = false;
                    continue;
                }

                ocupied[j].Occupied = true;
                ocupied[j].Colour = Colour4.Gray;
            }


            redrawOcupied();
        }

        private void recurseStackUp(int j)
        {
            if (j < 190)
            {
                if (j > 10)
                {
                    ocupied[j - 10].Occupied = ocupied[j].Occupied;
                    ocupied[j - 10].Colour = ocupied[j].Colour;
                }

                recurseStackUp(j + 10);
            }
        }

        private void redrawOcupied()
        {
            droppedContainer.Clear();
            foreach (var box in ocupied)
            {
                if (box)
                {
                    droppedContainer.Add(new Box()
                    {
                        Size = new Vector2(45, 45),
                        Position = new Vector2(PlayField.x[box.X] + 5, PlayField.y[box.Y] + 5),
                        Anchor = Anchor.TopLeft,
                        Colour = box.Colour,
                    });
                }
            }
        }

        protected override bool OnMouseDown(MouseDownEvent e)
        {
            if (e.Button == MouseButton.Left)
            {
                p.Rotate(false);
                return base.OnMouseDown(e);
            }

            p.Rotate(true);
            return base.OnMouseDown(e);
        }

        protected override bool OnKeyDown(KeyDownEvent e)
        {
            switch (e.Key)
            {
                case var value when value == GameConfigManager.GameControlsConfig[GameSetting.Hold]:
                    if (HoldPreview.Hold.CanHold)
                    {
                        HoldPreview.Hold.HeldPiece = p.PieceType;
                        expireTetrimino();
                        box.Add(p);
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
