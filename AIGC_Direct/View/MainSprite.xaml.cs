using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;

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

        private void link1_Click(object sender, RoutedEventArgs e)
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
            }
        }

        private void link2_Click(object sender, RoutedEventArgs e)
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
            }
        }

        private void link3_Click(object sender, RoutedEventArgs e)
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
            }
        }

        private void link4_Click(object sender, RoutedEventArgs e)
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
            }
        }

        private void link5_Click(object sender, RoutedEventArgs e)
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
            }
        }

        private void link1_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            wv1.CoreWebView2.Navigate(Settings.Default.link1);
        }

        private void link2_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            wv2.CoreWebView2.Navigate(Settings.Default.link2);
        }

        private void link3_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            wv3.CoreWebView2.Navigate(Settings.Default.link3);
        }

        private void link4_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            wv4.CoreWebView2.Navigate(Settings.Default.link4);
        }

        private void link5_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            wv5.CoreWebView2.Navigate(Settings.Default.link5);
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

    #region Converters
    public class BorderClipConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Length == 3 && values[0] is double width && values[1] is double height && values[2] is CornerRadius radius)
            {
                if (width < double.Epsilon || height < double.Epsilon)
                {
                    return Geometry.Empty;
                }

                var clip = new PathGeometry
                {
                    Figures = new PathFigureCollection
                {
                    new(new Point(radius.TopLeft, 0), new PathSegment[]
                    {
                        new LineSegment(new Point(width - radius.TopRight, 0), false),
                        new ArcSegment(new Point(width, radius.TopRight), new Size(radius.TopRight, radius.TopRight), 90, false, SweepDirection.Clockwise, false),
                        new LineSegment(new Point(width, height - radius.BottomRight), false),
                        new ArcSegment(new Point(width - radius.BottomRight, height), new Size(radius.BottomRight, radius.BottomRight), 90, false, SweepDirection.Clockwise, false),
                        new LineSegment(new Point(radius.BottomLeft, height), false),
                        new ArcSegment(new Point(0, height - radius.BottomLeft), new Size(radius.BottomLeft, radius.BottomLeft), 90, false, SweepDirection.Clockwise, false),
                        new LineSegment(new Point(0, radius.TopLeft), false),
                        new ArcSegment(new Point(radius.TopLeft, 0), new Size(radius.TopLeft, radius.TopLeft), 90, false, SweepDirection.Clockwise, false),
                    }, false)
                }
                };
                clip.Freeze();

                return clip;
            }

            return DependencyProperty.UnsetValue;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
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
            if ((bool)values[0]
                || (bool)values[1]
                || (Visibility)values[2] == Visibility.Visible
                || (Visibility)values[3] == Visibility.Visible
                || (Visibility)values[4] == Visibility.Visible
                || (Visibility)values[5] == Visibility.Visible
                || (Visibility)values[6] == Visibility.Visible)
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
                && (Visibility)values[4] != Visibility.Visible
                && (Visibility)values[5] != Visibility.Visible)
                return Visibility.Visible;
            else return Visibility.Collapsed;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class CornerRadiusSelConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if ((Visibility)values[0] != Visibility.Visible
                && (Visibility)values[1] != Visibility.Visible
                && (Visibility)values[2] != Visibility.Visible
                && (Visibility)values[3] != Visibility.Visible
                && (Visibility)values[4] != Visibility.Visible)
                return new CornerRadius(0, 24, 24, 24);
            else return new CornerRadius(0, 0, 0, 24);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class Link2FaviconConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return $"https://www.google.com/s2/favicons?domain={value}&sz=40";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    #endregion
}
