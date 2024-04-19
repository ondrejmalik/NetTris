using osu.Framework.Graphics;
using osu.Framework.Graphics.Sprites;

namespace Tetris.Game.Game.UI;

public partial class GameUiSpriteText : SpriteText
{
    public GameUiSpriteText()
    {
        Font = new FontUsage(size: 30);
        Origin = Anchor.TopCentre;
        Anchor = Anchor.TopCentre;
    }
}
