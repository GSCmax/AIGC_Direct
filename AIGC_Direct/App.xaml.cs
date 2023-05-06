using AIGC_Direct.View;
using System.Security.Policy;
using System.Windows;

namespace AIGC_Direct
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        MainSprite ms;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            ms = new MainSprite();

            ms.Opacity = 0;

            ms.Show();

            ms.Left = Settings.Default.X;
            ms.Top = Settings.Default.Y;
            ms.Opacity = Settings.Default.O;
            WindowAttach.SetIsDragElement(ms, Settings.Default.L);
        }

        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);

            Settings.Default.X = ms.Left;
            Settings.Default.Y = ms.Top;
            Settings.Default.O = ms.Opacity;
            Settings.Default.L = WindowAttach.GetIsDragElement(ms);
            Settings.Default.Save();
        }
    }
}
