using System;
using System.Collections.Generic;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Input.Events;
using osuTK;
using osuTK.Input;
using Tetris.Game.Config;

namespace Tetris.Game.Game.Playfield.Tetrimino;

public abstract partial class TetriminoBase : CompositeDrawable
{
    #region Public

    public int PosX { get; set; }
    public int PosY { get; set; }
    public List<(int X, int Y)> GridPos = new List<(int, int)>();
    public Colour4 PieceColour;
    public PieceType PieceType;

    #endregion

    #region Protected

    protected PlayField playField;
    protected SpriteText debugText;
    protected Box[] boxes = new Box[4];
    protected Vector2 pivot;
    protected Container container;
    protected double deltaTime = 0;
    protected Action updateDeltaTime;
    protected Action<KeyDownEvent> onkeyDown;
    protected bool isOpponent;
    protected Dictionary<GameSetting, Key> controlsConfig;
    protected bool isDummy;

    #endregion
}
