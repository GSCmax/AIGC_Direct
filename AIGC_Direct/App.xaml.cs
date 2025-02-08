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
        MainSprite? ms;
        Helpers.HotKeyHelper? hkh;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            ms = new MainSprite();
            ms.Opacity = 0;
            ms.Show();

            ms.Left = Settings.Default.X;
            ms.Top = Settings.Default.Y;
            ms.Opacity = Settings.Default.Opacity;
            ms.Topmost = Settings.Default.Topmost;
            WindowAttach.SetIsDragElement(ms, Settings.Default.CanDrag);
            //ms.Activate();

            string[] HotKey_ModifierKeys = Settings.Default.HotKey.Split(',')[0].Split('+');
            string HotKey_Key = Settings.Default.HotKey.Split(',')[1];

            ModifierKeys mks = ModifierKeys.None;
            Key k = Key.None;

            try
            {
                for (int i = 0; i < HotKey_ModifierKeys.Length; i++)
                    mks = mks | (ModifierKeys)Enum.Parse(typeof(ModifierKeys), HotKey_ModifierKeys[i]);
                k = (Key)Enum.Parse(typeof(Key), HotKey_Key);
            }
            catch
            {
                System.Windows.Forms.Application.EnableVisualStyles();
                System.Windows.Forms.MessageBox.Show($"Hotkey ({Settings.Default.HotKey}) is unrecognizable.\nPlease check the key names.\n\n无法识别热键({Settings.Default.HotKey})。\n请检查键名。", "AIGC Direct", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return;
            }

            try
            {
                hkh = new Helpers.HotKeyHelper(mks, k, ms, (hotkey) => { ms.Topmost = true; ms.Activate(); ms.ShowWV(); });
            }
            catch
            {
                System.Windows.Forms.Application.EnableVisualStyles();
                System.Windows.Forms.MessageBox.Show($"Hotkey ({Settings.Default.HotKey}) has already been registered.\nYou can not use hotkey to call the sprite currently.\n\n热键({Settings.Default.HotKey})已被注册。\n您当前无法使用热键呼出此工具。", "AIGC Direct", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                return;
            }
        }

        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);

            Settings.Default.X = ms.Left;
            Settings.Default.Y = ms.Top;
            Settings.Default.Opacity = Math.Round(ms.Opacity, 2);
            Settings.Default.Topmost = ms.Topmost;
            Settings.Default.CanDrag = WindowAttach.GetIsDragElement(ms);
            Settings.Default.Save();

            hkh.Dispose();
        }
    }
}
