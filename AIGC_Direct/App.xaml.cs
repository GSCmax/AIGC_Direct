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
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var ms = new MainSprite();

            ms.Opacity = 0;

            ms.Show();

            ms.Left = 25;
            ms.Top = 0;

            ms.Opacity = 1;
        }
    }
}
