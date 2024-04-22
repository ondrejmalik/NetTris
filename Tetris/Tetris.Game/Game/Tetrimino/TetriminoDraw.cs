using System;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Logging;
using osuTK;
using Tetris.Game.Config;
using Tetris.Game.Game.Playfield;

namespace Tetris.Game.Game.Tetrimino;

public partial class Tetrimino : TetriminoBase
{
    public Tetrimino(PieceType type, int x, int y, PlayField playField = null, bool isOpponent = false,
        bool isDummy = false)
    {
        this.IsDummy = isDummy;
        this.IsOpponent = isOpponent;
        this.PlayField = playField;
        PosX = x;
        PosY = y;
        setPieceType(type);
        if (playField != null && !isDummy)
        {
            ControlsConfig =
                isOpponent ? GameConfigManager.OpponentControlsConfig : GameConfigManager.GameControlsConfig;
            UpdateDeltaTime = () =>
            {
                //takes the Level number that is bigger between the current player and the opponent
                if (DeltaTime > levelScaling(
                        Math.Max(
                            playField.Level,
                            playField.OpponentPlayField != null ? playField.OpponentPlayField.Level : 0)))
                {
                    DeltaTime = 0;
                    moveDown();
                }
                /*
                debugText.Text =
                    $"{GridPos[0].Item1},  {GridPos[0].Item2}, {GridPos[1].Item1},  {GridPos[1].Item2}, {GridPos[2].Item1},  {GridPos[2].Item2}, {GridPos[3].Item1},  {GridPos[3].Item2}";
                DeltaTime += Time.Elapsed;
                */
            };
            OnkeyDown = e =>
            {
                switch (e.Key)
                {
                    case var value when value == ControlsConfig[GameSetting.MoveLeft]:
                        moveLeft();
                        break;
                    case var value when value == ControlsConfig[GameSetting.MoveRight]:
                        moveRight();
                        break;
                    case var value when value == ControlsConfig[GameSetting.HardDrop]:
                        quickDrop();
                        break;
                    case var value when value == ControlsConfig[GameSetting.SoftDrop]:
                        moveDown();
                        break;
                    case var value when value == ControlsConfig[GameSetting.RotateLeft]:
                        Rotate(false);
                        break;
                    case var value when value == ControlsConfig[GameSetting.RotateRight]:
                        Rotate(true);
                        break;
                }
            };
        }
        else
        {
            UpdateDeltaTime = () => { };
            OnkeyDown = e => { };
        }
    }


    [BackgroundDependencyLoader]
    private void load()
    {
        AutoSizeAxes = Axes.Both;
        InternalChild = Container = new Container
        {
            AutoSizeAxes = Axes.Both,

            Children = new Drawable[]
            {
                /*
                debugText = new SpriteText()
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    Text = "",
                    Font = new FontUsage(size: 25),
                    Colour = Colour4.Black,
                    Depth = -10,
                }
                */
            }
        };
        Container.Add(Boxes[0] = new TetriminoBlock(PieceColour));
        Container.Add(Boxes[1] = new TetriminoBlock(PieceColour));
        Container.Add(Boxes[2] = new TetriminoBlock(PieceColour));
        Container.Add(Boxes[3] = new TetriminoBlock(PieceColour));
        Container.Add(new Circle()
        {
            Position = Pivot * 50,
            Size = new Vector2(5, 5),

            Colour = Colour4.Purple,
            Depth = -20,
        });
        SetDrawPos();
    }

    #region SetDrawPos SetPieceType

    /// <summary>
    /// Sets the absolute draw position of the tetrimino blocks.
    /// </summary>
    public void SetDrawPos()
    {
        if (IsDummy) { }

        foreach (var pos in GridPos) //check if the piece is out of bounds
        {
            if (pos.Item1 < 0 || pos.Item1 >= 10 || pos.Item2 < 0 || pos.Item2 >= 20)
            {
                return;
            }
        }

        try
        {
            Boxes[0].Position = new Vector2(PlayFieldBase.x[GridPos[0].Item1] + 5,
                PlayFieldBase.y[GridPos[0].Item2] + 5);
            Boxes[1].Position = new Vector2(PlayFieldBase.x[GridPos[1].Item1] + 5,
                PlayFieldBase.y[GridPos[1].Item2] + 5);
            Boxes[2].Position = new Vector2(PlayFieldBase.x[GridPos[2].Item1] + 5,
                PlayFieldBase.y[GridPos[2].Item2] + 5);
            Boxes[3].Position = new Vector2(PlayFieldBase.x[GridPos[3].Item1] + 5,
                PlayFieldBase.y[GridPos[3].Item2] + 5);
        }
        catch (Exception e)
        {
            Logger.Log(e.Message);
        }
    }

    /// <summary>
    /// Sets the pivot, colour and relative positions of the blocks in the tetrimino.
    ///
    /// </summary>
    /// <param name="type">Type of the piece</param>
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
            Pivot = new Vector2(1.5f + PosX, 0.5f + PosY);
        else if (type == PieceType.L)
        {
            Pivot = new Vector2(PosX, 1 + PosY);
        }
        else if (type == PieceType.Z)
        {
            Pivot = new Vector2(1 + PosX, PosY + 1f);
        }
        else if (type == PieceType.O)
        {
            Pivot = new Vector2(0.5f + PosX, 0.5f + PosY);
        }
        else
            Pivot = new Vector2(1 + PosX, 1 + PosY);
    }

    #endregion
}
