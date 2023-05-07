using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Threading;

namespace AIGC_Direct.Helpers
{
    public sealed class HotKeyHelper : IDisposable
    {
        private readonly IntPtr _handle;

        private readonly int _id;

        private bool _isKeyRegistered;

        private Dispatcher _currentDispatcher;

        [DllImport("user32.dll")]
        static extern IntPtr GetForegroundWindow();

        public HotKeyHelper(ModifierKeys modifierKeys, Key key, Window window)
            : this(modifierKeys, key, new WindowInteropHelper(window), null)
        {
        }

        public HotKeyHelper(ModifierKeys modifierKeys, Key key, WindowInteropHelper window)
            : this(modifierKeys, key, window.Handle, null)
        {
        }

        public HotKeyHelper(ModifierKeys modifierKeys, Key key, Window window, Action<HotKeyHelper> onKeyAction)
            : this(modifierKeys, key, new WindowInteropHelper(window), onKeyAction)
        {
        }

        public HotKeyHelper(ModifierKeys modifierKeys, Key key, WindowInteropHelper window, Action<HotKeyHelper> onKeyAction)
            : this(modifierKeys, key, window.Handle, onKeyAction)
        {
        }

        public HotKeyHelper(ModifierKeys modifierKeys, Key key, IntPtr windowHandle, Action<HotKeyHelper> onKeyAction = null)
        {
            Key = key;
            KeyModifier = modifierKeys;
            _id = GetHashCode();
            _handle = windowHandle == IntPtr.Zero ? GetForegroundWindow() : windowHandle;
            _currentDispatcher = Dispatcher.CurrentDispatcher;
            RegisterHotKey();
            ComponentDispatcher.ThreadPreprocessMessage += ThreadPreprocessMessageMethod;

            if (onKeyAction != null)
                HotKeyPressed += onKeyAction;
        }

        ~HotKeyHelper()
        {
            Dispose();
        }

        public event Action<HotKeyHelper> HotKeyPressed;

        public Key Key { get; private set; }

        public ModifierKeys KeyModifier { get; private set; }

        private int InteropKey => KeyInterop.VirtualKeyFromKey(Key);

        public void Dispose()
        {
            try
            {
                ComponentDispatcher.ThreadPreprocessMessage -= ThreadPreprocessMessageMethod;
            }
            catch (Exception)
            {
                // ignored
            }
            finally
            {
                UnregisterHotKey();
            }
        }

        private void OnHotKeyPressed()
        {
            _currentDispatcher.Invoke(
                delegate
                {
                    HotKeyPressed?.Invoke(this);
                });
        }

        private void RegisterHotKey()
        {
            if (Key == Key.None)
            {
                return;
            }

            if (_isKeyRegistered)
            {
                UnregisterHotKey();
            }

            _isKeyRegistered = HotKeyWinApi.RegisterHotKey(_handle, _id, KeyModifier, InteropKey);

            if (!_isKeyRegistered)
            {
                throw new ApplicationException("An unexpected Error occured! (Hotkey may already be in use)");
            }
        }

        private void ThreadPreprocessMessageMethod(ref MSG msg, ref bool handled)
        {
            if (handled)
            {
                return;
            }

            if (msg.message != HotKeyWinApi.WmHotKey || (int)(msg.wParam) != _id)
            {
                return;
            }

            OnHotKeyPressed();
            handled = true;
        }

        private void UnregisterHotKey()
        {
            _isKeyRegistered = !HotKeyWinApi.UnregisterHotKey(_handle, _id);
        }
    }

    public class HotKeyWinApi
    {
        public const int WmHotKey = 0x0312;

        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool RegisterHotKey(IntPtr hWnd, int id, ModifierKeys fsModifiers, int vk);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool UnregisterHotKey(IntPtr hWnd, int id);
    }
}
