using System;
using System.Collections.Generic;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Input.Events;
using osu.Framework.Logging;
using osuTK;
using osuTK.Input;
using Tetris.Game.Config;

namespace Tetris.Game;

public partial class Tetrimino : CompositeDrawable
{
    public int PosX { get; }
    public int PosY { get; }
    public List<(int, int)> GridPos = new List<(int, int)>();
    public Colour4 PieceColour;
    public PieceType PieceType;

    private PlayField playField;
    private SpriteText debugText;
    private Box[] boxes = new Box[4];
    private Vector2 pivot;
    private Container container;
    private double deltaTime = 0;
    private Action updateDeltaTime;
    private Action<KeyDownEvent> onkeyDown;
    private bool isOpponent;
    private Dictionary<GameSetting, Key> controlsConfig;

    public Tetrimino(PieceType type, int x, int y, PlayField playField = null, bool isOpponent = false)
    {
        controlsConfig = isOpponent ? GameConfigManager.OpponentControlsConfig : GameConfigManager.GameControlsConfig;

        this.isOpponent = isOpponent;
        this.playField = playField;
        PosX = x;
        PosY = y;
        setPieceType(type);
        if (playField != null)
        {
            updateDeltaTime = () =>
            {
                if (deltaTime > 1000)
                {
                    deltaTime = 0;
                    moveDown();
                }

                debugText.Text =
                    $"{GridPos[0].Item1},  {GridPos[0].Item2}, {GridPos[1].Item1},  {GridPos[1].Item2}, {GridPos[2].Item1},  {GridPos[2].Item2}, {GridPos[3].Item1},  {GridPos[3].Item2}";
                deltaTime += Time.Elapsed;
            };
            onkeyDown = e =>
            {
                switch (e.Key)
                {
                    case var value when value == controlsConfig[GameSetting.MoveLeft]:
                        moveLeft();
                        break;
                    case var value when value == controlsConfig[GameSetting.MoveRight]:
                        moveRight();
                        break;
                    case var value when value == controlsConfig[GameSetting.HardDrop]:
                        quickDrop();
                        break;
                    case var value when value == controlsConfig[GameSetting.SoftDrop]:
                        moveDown();
                        break;
                    case var value when value == controlsConfig[GameSetting.RotateLeft]:
                        Rotate(false);
                        break;
                    case var value when value == controlsConfig[GameSetting.RotateRight]:
                        Rotate(true);
                        break;
                }
            };
        }
        else
        {
            updateDeltaTime = () => { };
            onkeyDown = e => { };
        }

        load();
    }

    private void setPieceType(PieceType type)
    {
        PieceType = type;
        switch (type)
        {
            case PieceType.T:
                GridPos = new() { (1, 0), (1, 1), (2, 1), (0, 1) };
                PieceColour = Colour4.Purple;
                break;
            case PieceType.O:
                GridPos = new() { (0, 1), (0, 0), (1, 0), (1, 1) };
                PieceColour = Colour4.Yellow;
                break;
            case PieceType.J:
                GridPos = new() { (1, 1), (1, 0), (1, 2), (0, 2) };
                PieceColour = Colour4.Blue;
                break;
            case PieceType.L:
                GridPos = new() { (0, 1), (0, 0), (0, 2), (1, 2) };
                PieceColour = Colour4.Orange;
                break;
            case PieceType.Z:
                GridPos = new() { (1, 0), (0, 1), (1, 1), (2, 0) };
                PieceColour = Colour4.Red;
                break;
            case PieceType.S:
                GridPos = new() { (1, 1), (0, 0), (1, 0), (2, 1) };
                PieceColour = Colour4.Green;
                break;
            case PieceType.I:
                GridPos = new() { (1, 0), (0, 0), (2, 0), (3, 0) };
                PieceColour = Colour4.SandyBrown;
                break;
        }

        for (int i = 0; i < GridPos.Count; i++)
        {
            GridPos[i] = (GridPos[i].Item1 + PosX, GridPos[i].Item2 + PosY);
        }

        if (type == PieceType.I)
            pivot = new Vector2(1.5f + PosX, 0.5f + PosY);
        else if (type == PieceType.L)
        {
            pivot = new Vector2(PosX, 1 + PosY);
        }
        else if (type == PieceType.Z)
        {
            pivot = new Vector2(1 + PosX, PosY + 1f);
        }
        else if (type == PieceType.O)
        {
            pivot = new Vector2(0.5f + PosX, 0.5f + PosY);
        }
        else
            pivot = new Vector2(1 + PosX, 1 + PosY);
    }

    [BackgroundDependencyLoader]
    private void load()
    {
        AutoSizeAxes = Axes.Both;
        Logger.Log("New Piece");
        InternalChild = container = new Container
        {
            AutoSizeAxes = Axes.Both,
            Anchor = Anchor.TopLeft,
            Origin = Anchor.TopLeft,
            Children = new Drawable[]
            {
                debugText = new SpriteText()
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    Text = "",
                    Font = new FontUsage(size: 25),
                    Colour = Colour4.Black,
                    Depth = -10,
                }
            }
        };
        container.Add(boxes[0] = new TetriminoBlock(PieceColour));
        container.Add(boxes[1] = new TetriminoBlock(PieceColour));
        container.Add(boxes[2] = new TetriminoBlock(PieceColour));
        container.Add(boxes[3] = new TetriminoBlock(PieceColour));
        container.Add(new Circle()
        {
            Position = pivot * 50,
            Size = new Vector2(5, 5),
            Anchor = Anchor.TopLeft,
            Colour = Colour4.Purple,
            Depth = -20,
        });
        setDrawPos();
    }

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

// TODO Check if occupied in new pos before rotating else don't rotate
    public void Rotate(bool reverse)
    {
        bool revert = false;
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

        setDrawPos();
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

    private void moveDown()
    {
        for (int i = 0; i < 4; i++)
        {
            GridPos[i] = (GridPos[i].Item1, GridPos[i].Item2 + 1);
        }

        playField.BottomCollisionDetection();
        playField.CollisionDetection(0);
        pivot = new Vector2(pivot.X, pivot.Y + 1);
        setDrawPos();
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
        setDrawPos();
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
        setDrawPos();
    }

    private void setDrawPos()
    {
        try
        {
            boxes[0].Position = new Vector2(PlayField.x[GridPos[0].Item1] + 5,
                PlayField.y[GridPos[0].Item2] + 5);
            boxes[1].Position = new Vector2(PlayField.x[GridPos[1].Item1] + 5,
                PlayField.y[GridPos[1].Item2] + 5);
            boxes[2].Position = new Vector2(PlayField.x[GridPos[2].Item1] + 5,
                PlayField.y[GridPos[2].Item2] + 5);
            boxes[3].Position = new Vector2(PlayField.x[GridPos[3].Item1] + 5,
                PlayField.y[GridPos[3].Item2] + 5);
        }
        catch (Exception e)
        {
            Logger.Log(e.Message);
        }
    }
}
