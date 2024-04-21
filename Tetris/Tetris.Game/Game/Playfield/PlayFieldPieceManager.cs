using System;
using System.Collections.Generic;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Logging;
using osuTK;
using Tetris.Game.Game.Playfield.Tetrimino;

namespace Tetris.Game.Game.Playfield;

public partial class PlayField : PlayFieldBase
{
    public int ClearedLines
    {
        get => clearedLines;
        set
        {
            clearedLines = value;
            OnClearedLinesChanged();
        }
    }

    public double Cpm => ClearedLines / (Clock.CurrentTime / 60 / 1000);

    public int TimeInSeconds
    {
        get
        {
            return (int)((Clock.CurrentTime - loadTime) / 1000);
        }
    }

    public int Level => (int)Math.Round((decimal)(ClearedLines / 10 + 1));

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

                Colour = c,
            });

            OccupiedSet o;
            o = Occupied[pos.Item1 + pos.Item2 * 10 - 10];
            o.O = true;
            o.P = Piece.PieceType;
            Occupied[pos.Item1 + pos.Item2 * 10 - 10] = o;
        }

        lastPieceGridPos = Piece.GridPos; // used in sending garbage
        expireTetrimino();
        //check if new piece overlaps existing piece
        foreach (var pos in Piece.GridPos)
        {
            if (Occupied[pos.Item1 + pos.Item2 * 10] == true)
            {
                Logger.Log("Game Over");
                OnGameOverChanged(true);
                break;
            }
        }


        int diff = clearLine();
        if (!isOnline && OpponentPlayField != null)
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

    #region Stack Up Down

    private void newstackDown(int j)
    {
        Occupied[j].O = false;
        recurseStackDown(j);
    }

    private void recurseStackDown(int j)
    {
        if (j > 10)
        {
            Occupied[j].O = Occupied[j - 10].O;
            Occupied[j].P = Occupied[j - 10].P;
            recurseStackDown(j - 10);
        }
    }


    private void recurseStackUp(int j)
    {
        if (j < 190)
        {
            if (j > 10)
            {
                Occupied[j - 10].O = Occupied[j].O;
                Occupied[j - 10].P = Occupied[j].P;
            }

            recurseStackUp(j + 10);
        }
    }

    #endregion

    #region Add Garbage Line

    private void addGarbage(int lines, List<(int, int)> emptyGridPos = null)
    {
        // TODO: add move up current piece if overlaps with added garbage
        // TODO: maybe add empty hole in place of last enemy piece instead of line
        for (int i = 0; i < lines; i++)
        {
            int index = 2;
            if (emptyGridPos != null)
            {
                index = emptyGridPos[Random.Shared.Next(3)].Item1;
            }

            addLine(index); // for now only add empty hole where the last piece was
        }

        /*
        if (emptyGridPos != null)
        {
            foreach (var cords in emptyGridPos) // add empty hole in place of last enemy piece
            {
                if (Occupied[cords.Item1 + cords.Item2 * 10].Occupied)
                {
                    Occupied[cords.Item1 + cords.Item2 * 10].Occupied = false;
                }
            }
        }
        */
        OpponentPlayField.ClearedLines += lines;
    }

    private void addLine(int emptyIndex)
    {
        for (int i = 0; i < 10; i++) // move up each X Line
        {
            recurseStackUp(i);
        }

        //------------------ this checks for each Y line if there is garbage and if there is it goes to start of line above it
        bool foundGarbage = false;
        int lineStart = Occupied.Count - 10;
        for (int i = Occupied.Count - 1; i >= 0; i = i - 1)
        {
            // if colour of occupied[i] is colour of garbage
            if (PieceTypeToColour(Occupied[i].P) == PieceTypeToColour(PieceType.Garbage))

            {
                foundGarbage = true;
                i = lineStart - 10; // this goes to start of line above
            }

            // if i is at end of line and not found garbage then break
            if (i % 10 == 0)
            {
                lineStart = i;
                break;
            }
        }
        //------------------ this add line of garbage at lineStart line and add empty hole where the enemy piece was (emptyIndex)

        for (int j = lineStart; j < lineStart + 10; j++)
        {
            if (emptyIndex + lineStart == j) // add empty hole
            {
                Occupied[j].O = false;
                continue;
            }

            Occupied[j].O = true;
            Occupied[j].P = PieceType.Garbage;
        }

        foreach (var pos in Piece.GridPos)
        {
            if (Occupied[pos.Item1 + pos.Item2 * 10].O)
            {
                Piece.MoveUp();
                Piece.SetDrawPos();
            }
        }

        redrawOccupied();
    }

    #endregion
}
