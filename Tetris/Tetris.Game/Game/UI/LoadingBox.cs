using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using Tetris.Game.Game.Tetrimino;


namespace Tetris.Game.Game.UI;

/// <summary>
/// Displays a loading box with a rotating tetrimino and text.
/// </summary>
public partial class LoadingBox : CompositeDrawable
{
    Container box;
    GameUiSpriteText text;
    Tetrimino.Tetrimino loadingSprite;

    [BackgroundDependencyLoader]
    private void load()
    {
        Anchor = Anchor.Centre;
        Origin = Anchor.Centre;
        AutoSizeAxes = Axes.Both;
        InternalChild = box = new Container
        {
            AutoSizeAxes = Axes.Both,
            Children = new Drawable[]
            {
                text = new GameUiSpriteText
                {
                    Text = "Waiting for other player",
                    Font = new(size: 35),
                },
                loadingSprite = new Tetrimino.Tetrimino(PieceType.T, 0, 0)
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    Size = new osuTK.Vector2(50),
                    Position = new osuTK.Vector2(0, 50),
                },
            }
        };
    }

    protected override void LoadComplete()
    {
        rotate();
        base.LoadComplete();
    }

    /// <summary>
    /// Rotates the tetrimino.
    /// </summary>
    private void rotate()
    {
        loadingSprite.Loop(b => b.RotateTo(0).RotateTo(360, 3000));
    }
}
