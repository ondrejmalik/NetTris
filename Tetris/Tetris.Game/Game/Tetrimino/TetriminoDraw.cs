using System;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Logging;
using osuTK;
using Tetris.Game.Config;

namespace Tetris.Game.Game.Playfield.Tetrimino;

public partial class Tetrimino : TetriminoBase
{
    public Tetrimino(PieceType type, int x, int y, PlayField playField = null, bool isOpponent = false,
        bool isDummy = false)
    {
        this.isDummy = isDummy;
        this.isOpponent = isOpponent;
        this.playField = playField;
        PosX = x;
        PosY = y;
        setPieceType(type);
        if (playField != null && !isDummy)
        {
            controlsConfig =
                isOpponent ? GameConfigManager.OpponentControlsConfig : GameConfigManager.GameControlsConfig;
            updateDeltaTime = () =>
            {
                if (deltaTime > levelScaling(
                        Math.Max(
                            playField.Level,
                            playField.OpponentPlayField != null ? playField.OpponentPlayField.Level : 0)))
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
    }


    [BackgroundDependencyLoader]
    private void load()
    {
        AutoSizeAxes = Axes.Both;
        InternalChild = container = new Container
        {
            AutoSizeAxes = Axes.Both,


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

            Colour = Colour4.Purple,
            Depth = -20,
        });
        SetDrawPos();
    }

    #region SetDrawPos SetPieceType

    public void SetDrawPos()
    {
        if (isDummy) { }

        try
        {
            boxes[0].Position = new Vector2(PlayFieldBase.x[GridPos[0].Item1] + 5,
                PlayFieldBase.y[GridPos[0].Item2] + 5);
            boxes[1].Position = new Vector2(PlayFieldBase.x[GridPos[1].Item1] + 5,
                PlayFieldBase.y[GridPos[1].Item2] + 5);
            boxes[2].Position = new Vector2(PlayFieldBase.x[GridPos[2].Item1] + 5,
                PlayFieldBase.y[GridPos[2].Item2] + 5);
            boxes[3].Position = new Vector2(PlayFieldBase.x[GridPos[3].Item1] + 5,
                PlayFieldBase.y[GridPos[3].Item2] + 5);
        }
        catch (Exception e)
        {
            Logger.Log(e.Message);
        }
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

    #endregion
}
