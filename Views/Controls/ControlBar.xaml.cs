using System.Runtime.InteropServices;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interop;

namespace Views.Controls
{
    /// <summary>
    /// Interaction logic for ControlBar.xaml
    /// </summary>
    public partial class ControlBar : UserControl
    {
        public ControlBar()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btnMaximize_Click(object sender, RoutedEventArgs e)
        {
            Window w = Application.Current.MainWindow;
            if (w.WindowState == WindowState.Normal)
                w.WindowState = WindowState.Maximized;
            else w.WindowState = WindowState.Normal;
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            Window w = Application.Current.MainWindow;
            w.WindowState = WindowState.Minimized;
        }
        [DllImport("user32.dll")]
        public static extern IntPtr SendMessage(IntPtr hWnd, int wMsg, int wParam, int lParam);
        private void controlBar_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Window window = Application.Current.MainWindow;
            WindowInteropHelper helper = new(window);
            SendMessage(helper.Handle, 161, 2, 0);
        }
    }
}
