using osu.Framework.Graphics.Containers;

namespace Tetris.Game;

public abstract partial class GameContainerBase : CompositeDrawable
{
    protected PlayField playField;
    protected  HoldPreview holdPreview;
    protected  PlayfieldStats playfieldStats;
    protected  Hold hold;
    protected  Bag bag = new Bag();


}
