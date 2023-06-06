using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
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
                wv4.Visibility = Visibility.Collapsed;
            }
            else
            {
                wv1.Visibility = Visibility.Visible;
                wv2.Visibility = Visibility.Collapsed;
                wv3.Visibility = Visibility.Collapsed;
                wv4.Visibility = Visibility.Collapsed;
            }
        }

        private void bingai_Click(object sender, RoutedEventArgs e)
        {
            if (wv2.Visibility == Visibility.Visible)
            {
                wv1.Visibility = Visibility.Collapsed;
                wv2.Visibility = Visibility.Collapsed;
                wv3.Visibility = Visibility.Collapsed;
                wv4.Visibility = Visibility.Collapsed;
            }
            else
            {
                wv1.Visibility = Visibility.Collapsed;
                wv2.Visibility = Visibility.Visible;
                wv3.Visibility = Visibility.Collapsed;
                wv4.Visibility = Visibility.Collapsed;
            }
        }

        private void bard_Click(object sender, RoutedEventArgs e)
        {
            if (wv3.Visibility == Visibility.Visible)
            {
                wv1.Visibility = Visibility.Collapsed;
                wv2.Visibility = Visibility.Collapsed;
                wv3.Visibility = Visibility.Collapsed;
                wv4.Visibility = Visibility.Collapsed;
            }
            else
            {
                wv1.Visibility = Visibility.Collapsed;
                wv2.Visibility = Visibility.Collapsed;
                wv3.Visibility = Visibility.Visible;
                wv4.Visibility = Visibility.Collapsed;
            }
        }

        private void yiyan_Click(object sender, RoutedEventArgs e)
        {
            if (wv4.Visibility == Visibility.Visible)
            {
                wv1.Visibility = Visibility.Collapsed;
                wv2.Visibility = Visibility.Collapsed;
                wv3.Visibility = Visibility.Collapsed;
                wv4.Visibility = Visibility.Collapsed;
            }
            else
            {
                wv1.Visibility = Visibility.Collapsed;
                wv2.Visibility = Visibility.Collapsed;
                wv3.Visibility = Visibility.Collapsed;
                wv4.Visibility = Visibility.Visible;
            }
        }

        private void chatgpt_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            //Process.Start(new ProcessStartInfo() { FileName = Settings.Default.chatgpt, UseShellExecute = true });
            wv1.CoreWebView2.Navigate(Settings.Default.chatgpt);
        }

        private void bingai_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            //Process.Start(new ProcessStartInfo() { FileName = "msedge.exe", Arguments = Settings.Default.bingai, UseShellExecute = true });
            wv2.CoreWebView2.Navigate(Settings.Default.bingai);
        }

        private void bard_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            //Process.Start(new ProcessStartInfo() { FileName = Settings.Default.bard, UseShellExecute = true });
            wv3.CoreWebView2.Navigate(Settings.Default.bard);
        }

        private void yiyan_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            //Process.Start(new ProcessStartInfo() { FileName = Settings.Default.yiyan, UseShellExecute = true });
            wv4.CoreWebView2.Navigate(Settings.Default.yiyan);
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

    public class Bool2ResourceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((bool)value)
                return Application.Current.FindResource(((string)parameter).Split(',')[0]);
            else
                return Application.Current.FindResource(((string)parameter).Split(',')[1]);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class IconShowConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if ((bool)values[0] || (bool)values[1]
                || (Visibility)values[2] == Visibility.Visible
                || (Visibility)values[3] == Visibility.Visible
                || (Visibility)values[4] == Visibility.Visible
                || (Visibility)values[5] == Visibility.Visible)
                return Visibility.Visible;
            else return Visibility.Collapsed;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class ExitIconShowConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if ((bool)values[0]
                && (Visibility)values[1] != Visibility.Visible
                && (Visibility)values[2] != Visibility.Visible
                && (Visibility)values[3] != Visibility.Visible
                && (Visibility)values[4] != Visibility.Visible)
                return Visibility.Visible;
            else return Visibility.Collapsed;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
