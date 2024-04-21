using osu.Framework.Graphics;
using osu.Framework.Graphics.Sprites;

namespace Tetris.Game.Menu.Ui.Settings;

/// <summary>
///  Header SpriteText for consistent headers in settings sections.
/// </summary>
public partial class HeaderSpriteText : SpriteText
{
    public HeaderSpriteText()
    {
        Anchor = Anchor.TopCentre;
        Origin = Anchor.TopCentre;
        Font = new FontUsage(size: 40);
    }
}
