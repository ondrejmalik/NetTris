using System;
using System.Collections.Generic;
using osu.Framework.Input.Events;
using osuTK;

namespace Tetris.Game.Game.Playfield.Tetrimino;

public partial class Tetrimino : TetriminoBase
{
    protected override void Update()
    {
        updateDeltaTime();
        base.Update();
    }

    protected override bool OnKeyDown(KeyDownEvent e)
    {
        onkeyDown(e);

        return base.OnKeyDown(e);
    }

    private double levelScaling(int level)
    {
        return 750 / Math.Log(level + 1, 2);
    }

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
            double deltaX = GridPos[i].Item1 - pivot.X;
            double deltaY = GridPos[i].Item2 - pivot.Y;

            double rotatedX = rotationMatrix[0, 0] * deltaX + rotationMatrix[0, 1] * deltaY;
            double rotatedY = rotationMatrix[1, 0] * deltaX + rotationMatrix[1, 1] * deltaY;
            if (rotatedX + pivot.X < 0 || rotatedX + pivot.X > 9 || rotatedY + pivot.Y > 19 || rotatedY + pivot.Y < 0)
            {
                GridPos = tempGridPos;
                return;
            }

            GridPos[i] = ((int, int))(rotatedX + pivot.X, rotatedY + pivot.Y);
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

    private void quickDrop()
    {
        while (true)
        {
            for (int i = 0; i < 4; i++)
            {
                GridPos[i] = (GridPos[i].Item1, GridPos[i].Item2 + 1);
            }

            pivot = new Vector2(pivot.X, pivot.Y + 1);
            if (playField.CollisionDetection(0) || playField.BottomCollisionDetection())
            {
                break;
            }
        }
    }

    #region Movement

    public void MoveUp(bool checkCollision = true)
    {
        for (int i = 0; i < 4; i++)
        {
            GridPos[i] = (GridPos[i].Item1, GridPos[i].Item2 - 1);
        }


        if (checkCollision)
        {
            playField.BottomCollisionDetection();
            playField.CollisionDetection(0);
        }

        pivot = new Vector2(pivot.X, pivot.Y - 1);
        SetDrawPos();
    }

    private void moveDown(bool checkCollision = true)
    {
        for (int i = 0; i < 4; i++)
        {
            GridPos[i] = (GridPos[i].Item1, GridPos[i].Item2 + 1);
        }

        if (checkCollision)
        {
            playField.BottomCollisionDetection();
            playField.CollisionDetection(0);
        }

        pivot = new Vector2(pivot.X, pivot.Y + 1);
        SetDrawPos();
    }


    private void moveLeft(bool checkCollision = true)
    {
        for (int i = 0; i < 4; i++)
        {
            if (GridPos[i].Item1 <= 0)
                return;
            if (checkCollision)
            {
                if (playField.CollisionDetection(-1))
                {
                    return;
                }
            }
        }

        for (int i = 0; i < 4; i++)
        {
            GridPos[i] = (GridPos[i].Item1 - 1, GridPos[i].Item2);
        }

        pivot = new Vector2(pivot.X - 1, pivot.Y);
        SetDrawPos();
    }

    private void moveRight(bool checkCollision = true)
    {
        for (int i = 0; i < 4; i++)
        {
            if (GridPos[i].Item1 >= 9)
                return;
            if (checkCollision)
            {
                if (playField.CollisionDetection(1))
                {
                    return;
                }
            }
        }

        for (int i = 0; i < 4; i++)
        {
            GridPos[i] = (GridPos[i].Item1 + 1, GridPos[i].Item2);
        }

        pivot = new Vector2(pivot.X + 1, pivot.Y);
        SetDrawPos();
    }

    #endregion
}
