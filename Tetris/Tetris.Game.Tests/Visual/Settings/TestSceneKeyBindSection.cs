using NUnit.Framework;
using osu.Framework.Testing;
using Tetris.Game.Config;
using Tetris.Game.Menu.Ui.Settings.Controls;

namespace Tetris.Game.Tests.Visual.Settings
{
    [TestFixture]
    public partial class TestSceneKeyBindSection : TetrisTestScene
    {
        KeyBindsSection keyBindsSection;

        public TestSceneKeyBindSection()
        {
            Add(keyBindsSection = new KeyBindsSection(GameConfigManager.GameControlsConfig));
            keyBindsSection.OnLoadComplete += _ =>
            {
                foreach (var keyBind in keyBindsSection.Box.ChildrenOfType<KeyBind>())
                {
                    AddStep($"Trigger click {keyBind.Key}", () =>
                    {
                        keyBind.Clicked = true;
                    });
                }
            };
        }
    }
}
