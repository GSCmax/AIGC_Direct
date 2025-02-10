using AIGC_Direct.View;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;

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

            #region 窗口初始化
            ms = new MainSprite
            {
                Left = Settings.Default.X,
                Top = Settings.Default.Y,
                Opacity = Settings.Default.Opacity,
                Topmost = Settings.Default.Topmost
            };
            WindowAttach.SetIsDragElement(ms, Settings.Default.CanDrag);
            ms.Show();
            #endregion

            #region 热键注册
            string[] HotKey_ModifierKeys = Settings.Default.HotKey.Split(',')[0].Split('|');
            string HotKey_Key = Settings.Default.HotKey.Split(',')[1];

            ModifierKeys mks = ModifierKeys.None;
            Key k = Key.None;

            try
            {
                for (int i = 0; i < HotKey_ModifierKeys.Length; i++)
                    mks |= (ModifierKeys)Enum.Parse(typeof(ModifierKeys), HotKey_ModifierKeys[i]);
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
                hkh = new Helpers.HotKeyHelper(mks, k, ms, (hotkey) => { ms.Topmost = true; ms.Activate(); });
            }
            catch
            {
                System.Windows.Forms.Application.EnableVisualStyles();
                System.Windows.Forms.MessageBox.Show($"Hotkey ({Settings.Default.HotKey}) has already been registered.\nYou can not use hotkey to call the sprite currently.\n\n热键({Settings.Default.HotKey})已被注册。\n您当前无法使用热键呼出此工具。", "AIGC Direct", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                return;
            }
            #endregion
        }

        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);

            Settings.Default.X = ms!.Left;
            Settings.Default.Y = ms.Top;
            Settings.Default.Opacity = Math.Round(ms.Opacity, 2);
            Settings.Default.Topmost = ms.Topmost;
            Settings.Default.CanDrag = WindowAttach.GetIsDragElement(ms);
            Settings.Default.Save();

            hkh!.Dispose();
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

    public class WebViewControlShowConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if ((Visibility)values[0] == Visibility.Visible
                || (Visibility)values[1] == Visibility.Visible
                || (Visibility)values[2] == Visibility.Visible
                || (Visibility)values[3] == Visibility.Visible
                || (Visibility)values[4] == Visibility.Visible)
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

    public class Value2HalfConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (double)value / 2;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    #endregion
}
