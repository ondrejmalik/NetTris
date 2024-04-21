using System;
using System.Collections.Generic;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Input.Events;
using osuTK;
using osuTK.Input;
using Tetris.Game.Config;
using Tetris.Game.Game.Playfield;

namespace Tetris.Game.Game.Tetrimino;

public abstract partial class TetriminoBase : CompositeDrawable
{
    #region Public

    /// <summary>
    /// Position X of the tetrimino pivot in the grid.
    /// </summary>
    public int PosX { get; set; }

    /// <summary>
    /// Position Y of the tetrimino pivot in the grid.
    /// </summary>
    public int PosY { get; set; }

    /// <summary>
    /// Relative positions of all blocks in tetrimino to the pivot (PosX,PosY).
    /// </summary>
    public List<(int X, int Y)> GridPos = new List<(int, int)>();

    /// <summary>
    /// Colour of the tetrimino.
    /// </summary>
    public Colour4 PieceColour;

    /// <summary>
    ///  PieceType of the tetrimino.
    /// </summary>
    public PieceType PieceType;

    #endregion

    #region Protected

    /// <summary>
    ///  Playfield the tetrimino is in.
    /// </summary>
    protected PlayField PlayField;

    //protected SpriteText debugText;
    /// <summary>
    /// All the blocks in the tetrimino.
    /// </summary>
    protected Box[] Boxes = new Box[4];

    /// <summary>
    /// Pivot of the tetrimino.
    /// </summary>
    protected Vector2 Pivot;

    protected Container Container;

    /// <summary>
    /// Time from last auto move.
    /// </summary>
    protected double DeltaTime = 0;

    protected Action UpdateDeltaTime;
    protected Action<KeyDownEvent> OnkeyDown;
    protected bool IsOpponent;
    protected Dictionary<GameSetting, Key> ControlsConfig;
    protected bool IsDummy;

    #endregion
}
