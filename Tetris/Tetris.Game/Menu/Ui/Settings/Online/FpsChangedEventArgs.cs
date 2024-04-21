using System;

namespace Tetris.Game.Menu.Ui.Settings.Online;

public class FpsChangedEventArgs(bool showFps) : EventArgs
{
    public bool ShowFps { get; set; } = showFps;
}
