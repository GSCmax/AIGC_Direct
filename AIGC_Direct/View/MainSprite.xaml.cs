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

        private void Link1_Click(object sender, RoutedEventArgs e)
        {
            if (wv1.Visibility == Visibility.Visible)
            {
                wv1.Visibility = Visibility.Collapsed;
            }
            else
            {
                wv1.Visibility = Visibility.Visible;
            }
        }

        private void Link1_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            wv1.CoreWebView2.Navigate("https://chat.deepseek.com");
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

        private void Exit_Click(object sender, RoutedEventArgs e)
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

    public class ExitIconShowConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if ((bool)values[0] && (Visibility)values[1] != Visibility.Visible)
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
            if ((Visibility)values[0] != Visibility.Visible)
                return new CornerRadius(0, 24, 24, 24);
            else return new CornerRadius(0, 0, 0, 24);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    #endregion
}
