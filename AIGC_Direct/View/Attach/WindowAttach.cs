﻿using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interop;

namespace AIGC_Direct.View
{
    public static class WindowAttach
    {
        #region IsDragElement
        public static readonly DependencyProperty IsDragElementProperty = DependencyProperty.RegisterAttached("IsDragElement", typeof(bool), typeof(WindowAttach), new PropertyMetadata(false, OnIsDragElementChanged));

        public static void SetIsDragElement(DependencyObject element, bool value) => element.SetValue(IsDragElementProperty, value);

        public static bool GetIsDragElement(DependencyObject element) => (bool)element.GetValue(IsDragElementProperty);

        private static void OnIsDragElementChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is UIElement ctl)
            {
                if ((bool)e.NewValue)
                {
                    ctl.MouseLeftButtonDown += DragElement_MouseLeftButtonDown;
                }
                else
                {
                    ctl.MouseLeftButtonDown -= DragElement_MouseLeftButtonDown;
                }
            }
        }

        private static void DragElement_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (sender is DependencyObject obj && e.ButtonState == MouseButtonState.Pressed)
            {
                System.Windows.Window.GetWindow(obj)?.DragMove();
            }
        }
        #endregion

        #region IgnoreAltF4
        public static readonly DependencyProperty IgnoreAltF4Property = DependencyProperty.RegisterAttached("IgnoreAltF4", typeof(bool), typeof(WindowAttach), new PropertyMetadata(false, OnIgnoreAltF4Changed));

        public static void SetIgnoreAltF4(DependencyObject element, bool value) => element.SetValue(IgnoreAltF4Property, value);

        public static bool GetIgnoreAltF4(DependencyObject element) => (bool)element.GetValue(IgnoreAltF4Property);

        private static void OnIgnoreAltF4Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is System.Windows.Window window)
            {
                if ((bool)e.NewValue)
                {
                    window.PreviewKeyDown += Window_PreviewKeyDown;
                }
                else
                {
                    window.PreviewKeyDown -= Window_PreviewKeyDown;
                }
            }
        }

        private static void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.System && e.SystemKey == Key.F4)
            {
                e.Handled = true;
            }
        }
        #endregion

        #region ShowInTaskManager
        public static readonly DependencyProperty ShowInTaskManagerProperty = DependencyProperty.RegisterAttached("ShowInTaskManager", typeof(bool), typeof(WindowAttach), new PropertyMetadata(true, OnShowInTaskManagerChanged));

        public static void SetShowInTaskManager(DependencyObject element, bool value) => element.SetValue(ShowInTaskManagerProperty, value);

        public static bool GetShowInTaskManager(DependencyObject element) => (bool)element.GetValue(ShowInTaskManagerProperty);

        private static void OnShowInTaskManagerChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is System.Windows.Window window)
            {
                var v = (bool)e.NewValue;
                window.SetCurrentValue(System.Windows.Window.ShowInTaskbarProperty, v);

                if (v)
                {
                    window.SourceInitialized -= Window_SourceInitialized;
                }
                else
                {
                    window.SourceInitialized += Window_SourceInitialized;
                }
            }
        }

        private static void Window_SourceInitialized(object sender, EventArgs e)
        {
            if (sender is System.Windows.Window window)
            {
                var _ = new WindowInteropHelper(window)
                {
                    Owner = GetDesktopWindow()
                };
            }
        }

        [DllImport("user32.dll")]
        internal static extern IntPtr GetDesktopWindow();
        #endregion

        #region HideWhenClosing
        public static readonly DependencyProperty HideWhenClosingProperty = DependencyProperty.RegisterAttached("HideWhenClosing", typeof(bool), typeof(WindowAttach), new PropertyMetadata(false, OnHideWhenClosingChanged));

        public static void SetHideWhenClosing(DependencyObject element, bool value) => element.SetValue(HideWhenClosingProperty, value);

        public static bool GetHideWhenClosing(DependencyObject element) => (bool)element.GetValue(HideWhenClosingProperty);

        private static void OnHideWhenClosingChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is System.Windows.Window window)
            {
                var v = (bool)e.NewValue;
                if (v)
                {
                    window.Closing += Window_Closing;
                }
                else
                {
                    window.Closing -= Window_Closing;
                }
            }
        }

        private static void Window_Closing(object sender, CancelEventArgs e)
        {
            if (sender is System.Windows.Window window)
            {
                window.Hide();
                e.Cancel = true;
            }
        }
        #endregion

        #region ExtendContentToNonClientArea
        public static readonly DependencyProperty ExtendContentToNonClientAreaProperty = DependencyProperty.RegisterAttached("ExtendContentToNonClientArea", typeof(bool), typeof(WindowAttach), new PropertyMetadata(false));

        public static void SetExtendContentToNonClientArea(DependencyObject element, bool value) => element.SetValue(ExtendContentToNonClientAreaProperty, value);

        public static bool GetExtendContentToNonClientArea(DependencyObject element) => (bool)element.GetValue(ExtendContentToNonClientAreaProperty);
        #endregion
    }
}
