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

            try
            {
                hkh = new Helpers.HotKeyHelper((ModifierKeys.Control | ModifierKeys.Alt), Key.A, ms, (hotkey) => { ms.Topmost = true; ms.Activate(); });
            }
            catch
            {
                System.Windows.Forms.Application.EnableVisualStyles();
                System.Windows.Forms.MessageBox.Show("Hotkey (Ctrl+Alt+A) has already been Registered.\n热键(Ctrl+Alt+A)已被注册。\nYou can not use hotkey to call the sprite currently.\n您当前无法使用热键呼出此工具。", "AIGC Direct", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
            }
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
