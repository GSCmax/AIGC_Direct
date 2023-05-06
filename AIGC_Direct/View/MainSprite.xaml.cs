using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;

namespace AIGC_Direct.View
{
    /// <summary>
    /// MainSprite.xaml 的交互逻辑
    /// </summary>
    public partial class MainSprite : Window
    {
        public MainSprite()
        {
            InitializeComponent();
        }

        private void chatgpt_Click(object sender, RoutedEventArgs e)
        {
            if (wv1.Visibility == Visibility.Visible)
            {
                wv1.Visibility = Visibility.Collapsed;
                wv2.Visibility = Visibility.Collapsed;
                wv3.Visibility = Visibility.Collapsed;
            }
            else
            {
                wv1.Visibility = Visibility.Visible;
                wv2.Visibility = Visibility.Collapsed;
                wv3.Visibility = Visibility.Collapsed;
            }
        }

        private void bingai_Click(object sender, RoutedEventArgs e)
        {
            if (wv2.Visibility == Visibility.Visible)
            {
                wv1.Visibility = Visibility.Collapsed;
                wv2.Visibility = Visibility.Collapsed;
                wv3.Visibility = Visibility.Collapsed;
            }
            else
            {
                wv1.Visibility = Visibility.Collapsed;
                wv2.Visibility = Visibility.Visible;
                wv3.Visibility = Visibility.Collapsed;
            }
        }

        private void yiyan_Click(object sender, RoutedEventArgs e)
        {
            if (wv3.Visibility == Visibility.Visible)
            {
                wv1.Visibility = Visibility.Collapsed;
                wv2.Visibility = Visibility.Collapsed;
                wv3.Visibility = Visibility.Collapsed;
            }
            else
            {
                wv1.Visibility = Visibility.Collapsed;
                wv2.Visibility = Visibility.Collapsed;
                wv3.Visibility = Visibility.Visible;
            }
        }

        private void chatgpt_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Process.Start(new ProcessStartInfo() { FileName = @"https://chat.openai.com", UseShellExecute = true });
        }

        private void bingai_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Process.Start(new ProcessStartInfo() { FileName = "msedge.exe", Arguments = @"https://www.bing.com/search?q=Bing+AI&showconv=1&FORM=hpcodx", UseShellExecute = true });
        }

        private void yiyan_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Process.Start(new ProcessStartInfo() { FileName = @"https://yiyan.baidu.com", UseShellExecute = true });
        }

        private void info_Click(object sender, RoutedEventArgs e)
        {
            Topmost = !Topmost;
        }

        private void info_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (WindowAttach.GetIsDragElement(this))
                WindowAttach.SetIsDragElement(this, false);
            else
                WindowAttach.SetIsDragElement(this, true);
        }

        private void info_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            var o = Math.Round(this.Opacity, 2);
            if (e.Delta > 0)
            {
                if (o < 1.0)
                    this.Opacity += 0.05;
                else
                    this.Opacity = 1.0;
            }
            else
            {
                if (o > 0.5)
                    this.Opacity -= 0.05;
                else
                    this.Opacity = 0.5;
            }
        }

        private void exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
