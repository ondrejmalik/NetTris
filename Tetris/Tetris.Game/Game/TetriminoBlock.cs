using osu.Framework.Graphics;
using osu.Framework.Graphics.Shapes;
using osuTK;

namespace Tetris.Game;

public partial class TetriminoBlock: Box
{
    public TetriminoBlock(Colour4 colour)
    {
        Size = new Vector2(45, 45);
        Anchor = Anchor.TopLeft;
        Colour = colour;
    }

}
