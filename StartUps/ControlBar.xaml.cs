using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace StartUps
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
        [DllImport("user32.dll")]
        public static extern IntPtr SendMessage(IntPtr hWnd, int wMsg, int wParam, int lParam);
        private void controlBar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            FrameworkElement window = this;
            while (window is not Window)
            {
                window = (FrameworkElement)window.Parent;
            }
            WindowInteropHelper helper = new WindowInteropHelper((Window)window);
            SendMessage(helper.Handle, 161, 2, 0);

        }

        private void controlBar_MouseEnter(object sender, MouseEventArgs e)
        {
            this.MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btnMaximize_Click(object sender, RoutedEventArgs e)
        {
            FrameworkElement window = this;
            while (window is not Window)
            {
                window = (FrameworkElement)window.Parent;
            }
            Window w = (Window)window;
            if (w.WindowState == WindowState.Normal) 
                w.WindowState = WindowState.Maximized;
            else w.WindowState = WindowState.Normal;
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            FrameworkElement window = this;
            while (window is not Window)
            {
                window = (FrameworkElement)window.Parent;
            }
            Window w = (Window)window;
            w.WindowState = WindowState.Minimized;
        }
    }
}
