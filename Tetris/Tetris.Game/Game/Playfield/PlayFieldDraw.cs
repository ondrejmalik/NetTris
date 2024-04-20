using System.Collections.Generic;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Input.Events;
using osuTK;
using osuTK.Input;
using Tetris.Game.Config;
using Tetris.Game.Game.Playfield.Tetrimino;
using Tetris.Game.Game.UI;

namespace Tetris.Game.Game.Playfield
{
    public partial class PlayField : BasePlayField
    {
        public static Colour4 PieceTypeToColour(PieceType type)
        {
            Colour4 colour = Colour4.Black;
            switch (type)
            {
                case PieceType.T:
                    colour = Colour4.Purple;
                    break;
                case PieceType.O:
                    colour = Colour4.Yellow;
                    break;
                case PieceType.J:
                    colour = Colour4.Blue;
                    break;
                case PieceType.L:
                    colour = Colour4.Orange;
                    break;
                case PieceType.Z:
                    colour = Colour4.Red;
                    break;
                case PieceType.S:
                    colour = Colour4.Green;
                    break;
                case PieceType.I:
                    colour = Colour4.SandyBrown;
                    break;
                case PieceType.Garbage:
                    colour = Colour4.Gray;
                    break;
            }

            return colour;
        }

        public PlayField(HoldPreview holdPreview, bool isOpponent = false, bool isOnline = false)
        {
            this.isOpponent = isOpponent;
            this.isOnline = isOnline;
            HoldPreview = holdPreview;

        }

        [BackgroundDependencyLoader]
        private void load()
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

            for (int i = 0; i < 200; i++)
            {
                Occupied.Add(new OccupiedSet()
                {
                    I = i,
                    O = false,
                });
            }


            box.Add(Piece = new Tetrimino.Tetrimino(HoldPreview.Hold.Bag.Dequeue(), 3, 2, this, isOpponent,
                isOnline && isOpponent));
        }

        public void ScheduleRedraw()
        {
            Scheduler.Add(() => redrawOccupied());
            Scheduler.Add(() => Piece.SetDrawPos());
        }

        public void ScheduleAddGarbage(int lines = 1, List<(int, int)> emptyGridPos = null)
        {
            Scheduler.Add(() => addGarbage(lines, emptyGridPos));
        }


        private void redrawOccupied()
        {
            if (isOnline) { }

            droppedContainer.Clear();
            foreach (var set in Occupied)
            {
                if (set)
                {
                    droppedContainer.Add(new Box()
                    {
                        Size = new Vector2(45, 45),
                        Position = new Vector2(PlayField.x[set.I % 10] + 5, PlayField.y[set.I / 10] + 5),
                        Anchor = Anchor.TopLeft,
                        Colour = isOnline ? Colour4.Black : PieceTypeToColour(set.P),
                    });
                }
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
                    addGarbage(2);
                    break;
            }

            return base.OnKeyDown(e);
        }
    }
}
