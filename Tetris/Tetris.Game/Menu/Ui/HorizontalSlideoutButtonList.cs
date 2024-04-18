using System.Collections.Generic;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.UserInterface;

namespace Tetris.Game.Menu.Ui;

public partial class HorizontalSlideoutButtonList : CompositeDrawable
{
    Container box;
    private FillFlowContainer ffContainer;
    public Button MainButton { get; set; }
    public List<MenuButton> Buttons;

    public HorizontalSlideoutButtonList(List<MenuButton> buttons)
    {
        Buttons = buttons;
    }

    [BackgroundDependencyLoader]
    private void load()
    {
        Anchor = Anchor.TopCentre;
        Origin = Anchor.TopCentre;
        MainButton.RelativeAnchorPosition = new osuTK.Vector2(0, 0);
        MainButton.Action = () =>
        {
            if (ffContainer.Width == 0)
            {
                float width = 0;
                foreach (var button in Buttons)
                {
                    width += button.HoveredSize.X;
                }

                ffContainer.ResizeWidthTo(width, 250, Easing.OutQuint);
            }
            else
            {
                ffContainer.ResizeWidthTo(0, 250, Easing.OutQuint);
            }
        };
        AutoSizeAxes = Axes.Both;
        InternalChild = box = new Container
        {
            AutoSizeAxes = Axes.Both,
            Children = new Drawable[]
            {
                new FillFlowContainer()
                {
                    Direction = FillDirection.Horizontal,
                    Spacing = new osuTK.Vector2(0, 0),
                    AutoSizeAxes = Axes.Both,
                    Children = new Drawable[]
                    {
                        MainButton,
                        ffContainer = new FillFlowContainer()
                        {
                            Position = new osuTK.Vector2(0, 0),
                            Direction = FillDirection.Horizontal,
                            Spacing = new osuTK.Vector2(0, 20),
                            Masking = true,
                            AutoSizeAxes = Axes.Y,
                            Width = 0,
                            Children = new Drawable[]
                            {
                            },
                        }
                    },
                },
            }
        };
        ffContainer.AddRange(Buttons);
    }
}
