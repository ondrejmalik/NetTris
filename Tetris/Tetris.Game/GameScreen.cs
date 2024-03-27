using System;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Screens;
using osuTK;

namespace Tetris.Game
{
    public partial class GameScreen : Screen
    {
        PlayField playField;
        HoldPreview holdPreview;
        PlayfieldStats playfieldStats;
        Hold hold;
        Bag bag = new Bag();

        public GameScreen()
        {
            Hold hold = new Hold(bag, null);
            holdPreview = new HoldPreview(hold) { Position = new Vector2(800, 0) };
            hold.HoldPreview = holdPreview;
            holdPreview.Hold = hold;
        }

        [BackgroundDependencyLoader]
        private void load()
        {
            InternalChildren = new Drawable[]
            {
                holdPreview,
                playField = new PlayField(holdPreview) { Position = new Vector2(250, 0) },
                playfieldStats = new PlayfieldStats(playField),
            };
        }
    }
}
