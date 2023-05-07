using AIGC_Direct.View;
using System;
using System.Windows;
using System.Windows.Input;

namespace AIGC_Direct
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        MainSprite ms;
        Helpers.HotKeyHelper hkh;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            ms = new MainSprite();
            ms.Opacity = 0;
            ms.Show();

            ms.Left = Settings.Default.X;
            ms.Top = Settings.Default.Y;
            ms.Opacity = Settings.Default.O;
            ms.Topmost = Settings.Default.T;
            WindowAttach.SetIsDragElement(ms, Settings.Default.L);
            ms.Activate();

            hkh = new Helpers.HotKeyHelper((ModifierKeys.Control | ModifierKeys.Alt), Key.A, ms, (hotkey) => { ms.Topmost = true; ms.Activate(); });
        }

        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);

            Settings.Default.X = ms.Left;
            Settings.Default.Y = ms.Top;
            Settings.Default.O = Math.Round(ms.Opacity, 2);
            Settings.Default.T = ms.Topmost;
            Settings.Default.L = WindowAttach.GetIsDragElement(ms);
            Settings.Default.Save();

            hkh.Dispose();
        }
    }
}
