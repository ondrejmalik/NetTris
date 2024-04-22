using System;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Input.Events;
using osu.Framework.Platform;
using osu.Framework.Screens;
using osuTK;
using Tetris.Game.Menu.Ui.Settings;

namespace Tetris.Game.Game.Screens
{
    public partial class GameOverScreen : Screen
    {
        [Resolved] private GameHost host { get; set; }
        private HeaderSpriteText resultText;
        private double loadTime;


        public GameOverScreen(bool lost)
        {
            resultText = new();
            resultText.Text = lost ? "You Lost :C" : "You Win!";
        }

        public GameOverScreen(string statsString)
        {
            resultText = new();
            resultText.Text = statsString;
        }


        [BackgroundDependencyLoader]
        private void load()
        {
            resultText.Anchor = Anchor.Centre;
            resultText.Origin = Anchor.Centre;
            host.Window.Title = "Game Over";
            InternalChildren = new Drawable[]
            {
                new FillFlowContainer()
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    Direction = FillDirection.Vertical,
                    Spacing = new Vector2(0, 25),
                    AutoSizeAxes = Axes.Both,
                    Children = new Drawable[]
                    {
                        new HeaderSpriteText()
                        {
                            Anchor = Anchor.Centre,
                            Origin = Anchor.Centre,
                            Text = "Game Over",
                        },
                        resultText,
                        new SpriteText()
                        {
                            Anchor = Anchor.Centre,
                            Origin = Anchor.Centre,
                            Font = new FontUsage(size: 30),
                            Text = "Press any key to continue",
                        },
                    }
                },
                new FpsCounter()
            };
            loadTime = Clock.CurrentTime;
        }

        protected override void LoadComplete()
        {
            try
            {
                this.Delay(5000).Then().FadeOut(2000).OnComplete(_ =>
                {
                    if (this.IsCurrentScreen())
                    {
                        this.Push(new MainMenuScreen());
                    }
                });
                base.LoadComplete();
            }
            catch (Exception e)
            {
                ;
            }
        }

        protected override bool OnKeyDown(KeyDownEvent e)
        {
            if (Clock.CurrentTime - loadTime > 500)
            {
                Hide();
                this.Push(new MainMenuScreen());
            }

            return base.OnKeyDown(e);
        }
    }
}
