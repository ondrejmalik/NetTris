using System;
using System.Collections.Generic;
using osu.Framework.Input.Events;
using osuTK;

namespace Tetris.Game.Game.Tetrimino;

/// <summary>
/// Tetrimino class that represents a tetrimino in the game.
/// </summary>
public partial class Tetrimino : TetriminoBase
{
    protected override void Update()
    {
        DeltaTime += Clock.ElapsedFrameTime;
        UpdateDeltaTime();
        base.Update();
    }

    protected override bool OnKeyDown(KeyDownEvent e)
    {
        OnkeyDown(e);

        return base.OnKeyDown(e);
    }

    /// <param name="level">Current game level</param>
    /// <returns>time between auto move down</returns>
    private double levelScaling(int level)
    {
        return 750 / Math.Log(level + 1, 2);
    }

    /// <summary>
    /// Rotates the tetrimino.
    /// </summary>
    /// <param name="reverse">If tetrimino should be rotated counterclockwise</param>
// TODO Check if occupied in new pos before rotating else don't rotate
    public void Rotate(bool reverse)
    {
        bool revert = false;
        bool revertUp = false;
        bool revertDirection = false;
        for (int i = 0; i < 4; i++)
        {
            if (GridPos[i].Item1 <= 0)
            {
                revert = true;
                moveRight(false);
                moveRight(false);
            }
            else if (GridPos[i].Item1 >= 9)
            {
                revert = true;
                revertDirection = true;
                moveLeft(false);
                moveLeft(false);
            }

            if (GridPos[i].Item2 < 1)
            {
                revertUp = true;
                moveDown();
                moveDown();
            }
        }

        double theta = reverse ? Math.PI / 2 : -Math.PI / 2;

        double[,] rotationMatrix =
        {
            { Math.Cos(theta), -Math.Sin(theta) },
            { Math.Sin(theta), Math.Cos(theta) }
        };
        List<(int, int)> tempGridPos = GridPos;
        for (int i = 0; i < 4; i++)
        {
            double deltaX = GridPos[i].Item1 - Pivot.X;
            double deltaY = GridPos[i].Item2 - Pivot.Y;

            double rotatedX = rotationMatrix[0, 0] * deltaX + rotationMatrix[0, 1] * deltaY;
            double rotatedY = rotationMatrix[1, 0] * deltaX + rotationMatrix[1, 1] * deltaY;
            if (rotatedX + Pivot.X < 0 || rotatedX + Pivot.X > 9 || rotatedY + Pivot.Y > 19 || rotatedY + Pivot.Y < 0)
            {
                GridPos = tempGridPos;
                return;
            }

            GridPos[i] = ((int, int))(rotatedX + Pivot.X, rotatedY + Pivot.Y);
        }

        if (revert)
        {
            //puts the piece to rotatable spot
            if (revertDirection)
            {
                moveRight(false);
                moveRight(false);
            }
            else
            {
                moveLeft(false);
                moveLeft(false);
            }
        }

        if (revertUp)
        {
            MoveUp();
            MoveUp();
        }

        SetDrawPos();
    }

    /// <summary>
    /// Also known as Hard drop.
    /// Drops the tetrimino to the bottom of the playfield or the nearest piece.
    /// </summary>
    private void quickDrop()
    {
        while (true)
        {
            for (int i = 0; i < 4; i++)
            {
                GridPos[i] = (GridPos[i].Item1, GridPos[i].Item2 + 1);
            }

            Pivot = new Vector2(Pivot.X, Pivot.Y + 1);
            if (PlayField.CollisionDetection(0) || PlayField.BottomCollisionDetection())
            {
                break;
            }
        }
    }

    #region Movement

    /// <summary>
    /// Moves the tetrimino up.
    /// </summary>
    /// <param name="checkCollision">If collisions should be checked</param>
    public void MoveUp(bool checkCollision = true)
    {
        for (int i = 0; i < 4; i++)
        {
            GridPos[i] = (GridPos[i].Item1, GridPos[i].Item2 - 1);
        }


        if (checkCollision)
        {
            PlayField.BottomCollisionDetection();
            PlayField.CollisionDetection(0);
        }

        Pivot = new Vector2(Pivot.X, Pivot.Y - 1);
        SetDrawPos();
    }

    /// <summary>
    ///  Moves the tetrimino down.
    /// </summary>
    /// <param name="checkCollision">If collisions should be checked</param>
    private void moveDown(bool checkCollision = true)
    {
        for (int i = 0; i < 4; i++)
        {
            GridPos[i] = (GridPos[i].Item1, GridPos[i].Item2 + 1);
        }

        if (checkCollision)
        {
            PlayField.BottomCollisionDetection();
            PlayField.CollisionDetection(0);
        }

        Pivot = new Vector2(Pivot.X, Pivot.Y + 1);
        SetDrawPos();
    }

    /// <summary>
    ///  Moves the tetrimino left.
    /// </summary>
    /// <param name="checkCollision">If collisions should be checked</param>
    private void moveLeft(bool checkCollision = true)
    {
        for (int i = 0; i < 4; i++)
        {
            if (GridPos[i].Item1 <= 0)
                return;
            if (checkCollision)
            {
                if (PlayField.CollisionDetection(-1))
                {
                    return;
                }
            }
        }

        for (int i = 0; i < 4; i++)
        {
            GridPos[i] = (GridPos[i].Item1 - 1, GridPos[i].Item2);
        }

        Pivot = new Vector2(Pivot.X - 1, Pivot.Y);
        SetDrawPos();
    }

    /// <summary>
    ///  Moves the tetrimino right.
    /// </summary>
    /// <param name="checkCollision">If collisions should be checked</param>
    private void moveRight(bool checkCollision = true)
    {
        for (int i = 0; i < 4; i++)
        {
            if (GridPos[i].Item1 >= 9)
                return;
            if (checkCollision)
            {
                if (PlayField.CollisionDetection(1))
                {
                    return;
                }
            }
        }

        for (int i = 0; i < 4; i++)
        {
            GridPos[i] = (GridPos[i].Item1 + 1, GridPos[i].Item2);
        }

        Pivot = new Vector2(Pivot.X + 1, Pivot.Y);
        SetDrawPos();
    }

    #endregion
}
