using System;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Logging;
using osuTK;
using Tetris.Game.Networking;

namespace Tetris.Game.Game.Playfield;

public partial class PlayField : BasePlayField
{
    public int ClearedLines
    {
        get => clearedLines;
        set
        {
            clearedLines = value;
            OnClearedLinesChanged(value);
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

            OccupiedSet o;
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
        if (!isOnline)
        {
            for (int i = 0; i < diff; i++)
            {
                OpponentPlayField.addGarbage(1);
            }
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
        Piece = new Tetrimino.Tetrimino(HoldPreview.Hold.Bag.Dequeue(), 4, 0, this, isOpponent, isOnline && isOpponent);
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
}
