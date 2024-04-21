using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osuTK;
using Tetris.Game.Game.Bag;
using Tetris.Game.Game.Playfield;
using Tetris.Game.Game.UI;

namespace Tetris.Game.Game;

/// <summary>
/// Holds the game elements.
/// </summary>
public partial class GameContainer : GameContainerBase
{
    private Container box;
    public PlayField PlayField { get; }
    public HoldPreview holdPreview { get; }
    public PlayfieldStats playfieldStats { get; }
    Hold hold;
    Bag.Bag bag = new Bag.Bag();

    /// <summary>
    /// Initializes a new instance of the <see cref="GameContainer"/> class.
    /// </summary>
    /// <param name="isOnline"></param>
    /// <param name="isOpponent"></param>
    public GameContainer(bool isOnline = false, bool isOpponent = false)
    {
        hold = new Hold(bag, null);
        holdPreview = new HoldPreview(hold) { Position = new Vector2(800, 0) };
        hold.HoldPreview = holdPreview;
        holdPreview.Hold = hold;
        PlayField = new PlayField(holdPreview, isOpponent, isOnline) { Position = new Vector2(250, 0) };
        playfieldStats = new PlayfieldStats(PlayField);
    }

    [BackgroundDependencyLoader]
    private void load()
    {
        AutoSizeAxes = Axes.Both;
        // TODO: Make this and double game Container FillFlowContainer to remove hard coded positions
        InternalChild = box = new Container
        {
            AutoSizeAxes = Axes.Both,
            Children = new Drawable[]
            {
                holdPreview,
                PlayField,
                playfieldStats,
            }
        };
    }
}
