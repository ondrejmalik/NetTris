using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using Tetris.Game.Game.Playfield.Tetrimino;


namespace Tetris.Game.Game.UI;

public partial class LoadingBox : CompositeDrawable
{
    Container box;
    GameUiSpriteText text;
    Tetrimino loadingSprite;

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
                },
                loadingSprite = new Tetrimino(PieceType.T, 0, 0)
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    Size = new osuTK.Vector2(50),
                    Position =  new osuTK.Vector2(0, 50),
                },
            }
        };
    }

    protected override void LoadComplete()
    {
        loadingSprite.Loop(b => b.RotateTo(0).RotateTo(360, 3000));
        base.LoadComplete();
    }
}
