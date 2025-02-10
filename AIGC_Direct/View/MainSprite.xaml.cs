using Microsoft.Web.WebView2.Wpf;
using System;
using System.Windows;
using System.Windows.Input;

namespace AIGC_Direct.View
{
    /// <summary>
    /// MainSprite.xaml 的交互逻辑
    /// </summary>
    public partial class MainSprite : Window
    {
        WebView2? nowWV;

        public MainSprite()
        {
            InitializeComponent();
        }

        private void Link1_Click(object sender, RoutedEventArgs e)
        {
            if (wv1.Visibility == Visibility.Visible)
            {
                wv1.Visibility = Visibility.Collapsed;
                wv2.Visibility = Visibility.Collapsed;
                wv3.Visibility = Visibility.Collapsed;
                wv4.Visibility = Visibility.Collapsed;
                wv5.Visibility = Visibility.Collapsed;
            }
            else
            {
                wv1.Visibility = Visibility.Visible;
                wv2.Visibility = Visibility.Collapsed;
                wv3.Visibility = Visibility.Collapsed;
                wv4.Visibility = Visibility.Collapsed;
                wv5.Visibility = Visibility.Collapsed;
                nowWV = wv1;
            }
        }

        private void Link2_Click(object sender, RoutedEventArgs e)
        {
            if (wv2.Visibility == Visibility.Visible)
            {
                wv1.Visibility = Visibility.Collapsed;
                wv2.Visibility = Visibility.Collapsed;
                wv3.Visibility = Visibility.Collapsed;
                wv4.Visibility = Visibility.Collapsed;
                wv5.Visibility = Visibility.Collapsed;
            }
            else
            {
                wv1.Visibility = Visibility.Collapsed;
                wv2.Visibility = Visibility.Visible;
                wv3.Visibility = Visibility.Collapsed;
                wv4.Visibility = Visibility.Collapsed;
                wv5.Visibility = Visibility.Collapsed;
                nowWV = wv2;
            }
        }

        private void Link3_Click(object sender, RoutedEventArgs e)
        {
            if (wv3.Visibility == Visibility.Visible)
            {
                wv1.Visibility = Visibility.Collapsed;
                wv2.Visibility = Visibility.Collapsed;
                wv3.Visibility = Visibility.Collapsed;
                wv4.Visibility = Visibility.Collapsed;
                wv5.Visibility = Visibility.Collapsed;
            }
            else
            {
                wv1.Visibility = Visibility.Collapsed;
                wv2.Visibility = Visibility.Collapsed;
                wv3.Visibility = Visibility.Visible;
                wv4.Visibility = Visibility.Collapsed;
                wv5.Visibility = Visibility.Collapsed;
                nowWV = wv3;
            }
        }

        private void Link4_Click(object sender, RoutedEventArgs e)
        {
            if (wv4.Visibility == Visibility.Visible)
            {
                wv1.Visibility = Visibility.Collapsed;
                wv2.Visibility = Visibility.Collapsed;
                wv3.Visibility = Visibility.Collapsed;
                wv4.Visibility = Visibility.Collapsed;
                wv5.Visibility = Visibility.Collapsed;
            }
            else
            {
                wv1.Visibility = Visibility.Collapsed;
                wv2.Visibility = Visibility.Collapsed;
                wv3.Visibility = Visibility.Collapsed;
                wv4.Visibility = Visibility.Visible;
                wv5.Visibility = Visibility.Collapsed;
                nowWV = wv4;
            }
        }

        private void Link5_Click(object sender, RoutedEventArgs e)
        {
            if (wv5.Visibility == Visibility.Visible)
            {
                wv1.Visibility = Visibility.Collapsed;
                wv2.Visibility = Visibility.Collapsed;
                wv3.Visibility = Visibility.Collapsed;
                wv4.Visibility = Visibility.Collapsed;
                wv5.Visibility = Visibility.Collapsed;
            }
            else
            {
                wv1.Visibility = Visibility.Collapsed;
                wv2.Visibility = Visibility.Collapsed;
                wv3.Visibility = Visibility.Collapsed;
                wv4.Visibility = Visibility.Collapsed;
                wv5.Visibility = Visibility.Visible;
                nowWV = wv5;
            }
        }

        private void Info_Click(object sender, RoutedEventArgs e)
        {
            Topmost = !Topmost;
        }

        private void Info_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (WindowAttach.GetIsDragElement(this))
                WindowAttach.SetIsDragElement(this, false);
            else
                WindowAttach.SetIsDragElement(this, true);
        }

        private void Info_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            var o = Math.Round(Opacity, 2);
            if (e.Delta > 0)
            {
                if (o < 1.0)
                    Opacity += 0.05;
                else
                    Opacity = 1.0;
            }
            else
            {
                if (o > 0.5)
                    Opacity -= 0.05;
                else
                    Opacity = 0.5;
            }
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Restore_Click(object sender, RoutedEventArgs e)
        {
            nowWV!.CoreWebView2.Navigate((string)nowWV.Tag);
        }
    }
}
