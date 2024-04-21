using osu.Framework.Graphics.Containers;
using Tetris.Game.Game.Bag;
using Tetris.Game.Game.Playfield;
using Tetris.Game.Game.UI;

namespace Tetris.Game.Game;

public abstract partial class GameContainerBase : CompositeDrawable
{
    protected PlayField PlayField;
    protected  HoldPreview holdPreview;
    protected  PlayfieldStats playfieldStats;
    protected  Hold Hold;
    protected  Bag.Bag bag = new Bag.Bag();
}
