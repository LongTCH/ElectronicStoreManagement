using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interop;
using ViewModels.Commands;

namespace ViewModels;

public class ControlBarViewModel : ViewModelBase
{
    public ICommand CloseCommand { get; } = new RelayCommand<UserControl>(closeCommand);
    public ICommand MaximaizeCommand { get; } = new RelayCommand<UserControl>(maximizeCommand);
    public ICommand MinimizeCommand { get; } = new RelayCommand<UserControl>(minimizeCommand);
    public ICommand DragMoveCommand { get; } = new RelayCommand<UserControl>(dragMoveCommand);
    public ControlBarViewModel()
    { }

    [DllImport("user32.dll")]      
    public static extern IntPtr SendMessage(IntPtr hWnd, int wMsg, int wParam, int lParam);
    private static void dragMoveCommand(UserControl userControl)
    {
        Window window = Application.Current.MainWindow;
        WindowInteropHelper helper = new WindowInteropHelper((Window)window);
        SendMessage(helper.Handle, 161, 2, 0);
    }
    private static void closeCommand(UserControl userControl)
    {
        Application.Current.Shutdown();
    }
    private static void maximizeCommand(UserControl userControl)
    {
        Window w = Application.Current.MainWindow;
        if (w.WindowState == WindowState.Normal)
            w.WindowState = WindowState.Maximized;
        else w.WindowState = WindowState.Normal;
    }
    private static void minimizeCommand(UserControl userControl)
    {
        Window w = Application.Current.MainWindow;
        w.WindowState = WindowState.Minimized;
    }

}
