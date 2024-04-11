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
    public List<Button> Buttons;

    public HorizontalSlideoutButtonList(List<Button> buttons)
    {
        Buttons = buttons;
    }

    [BackgroundDependencyLoader]
    private void load()
    {
        MainButton.RelativeAnchorPosition = new osuTK.Vector2(0, 0);
        MainButton.Anchor = Anchor.TopLeft;
        MainButton.Origin = Anchor.TopLeft;
        MainButton.Action = () =>
        {
            if (ffContainer.Width == 0)
            {
                float width = 0;
                foreach (var child in ffContainer.Children)
                {
                    width += child.DrawWidth;
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
            Anchor = Anchor.TopLeft,
            Origin = Anchor.TopLeft,
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
