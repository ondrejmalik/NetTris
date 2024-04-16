using osu.Framework.Graphics;
using osu.Framework.Graphics.Sprites;

namespace Tetris.Game.Menu.Ui;

public partial class HeaderSpriteText : SpriteText
{
    public HeaderSpriteText()
    {
        Anchor = Anchor.TopCentre;
        Origin = Anchor.TopCentre;
        Font = new FontUsage(size: 40);
    }
}
