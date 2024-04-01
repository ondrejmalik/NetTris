using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Configuration;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osuTK;


namespace Tetris.Game;

public partial class GameContainer : GameContainerBase
{
    private Container box;
    public PlayField PlayField { get; }
    public HoldPreview holdPreview { get; }
    public PlayfieldStats playfieldStats { get; }
    Hold hold;
    Bag bag = new Bag();

    public GameContainer(bool isOpponent = false, bool isOnline = false)
    {
        hold = new Hold(bag, null);
        holdPreview = new HoldPreview(hold) { Position = new Vector2(750, 0) };
        hold.HoldPreview = holdPreview;
        holdPreview.Hold = hold;
        PlayField = new PlayField(holdPreview, isOpponent, isOnline) { Position = new Vector2(200, 0) };
        playfieldStats = new PlayfieldStats(PlayField);
    }

    [BackgroundDependencyLoader]
    private void load()
    {
        AutoSizeAxes = Axes.Both;
        InternalChild = box = new Container
        {
            AutoSizeAxes = Axes.Both,
            Anchor = Anchor.TopLeft,
            Origin = Anchor.TopLeft,
            Children = new Drawable[]
            {
                holdPreview,
                PlayField,
                playfieldStats,
            }
        };
    }
}
