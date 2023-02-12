using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interop;
using ViewModels.Commands;

namespace ViewModels.ControlViewModels;

public class ControlBarViewModel : ViewModelBase
{
    public ICommand CloseCommand { get; } 
    public ICommand MaximizeCommand { get; }
    public ICommand MinimizeCommand { get; } 
    public ICommand DragMoveCommand { get; }
    public ControlBarViewModel()
    {
        CloseCommand = new RelayCommand<UserControl>(closeCommand);
        MaximizeCommand = new RelayCommand<UserControl>(maximizeCommand);
        MinimizeCommand = new RelayCommand<UserControl>(minimizeCommand);
        DragMoveCommand = new RelayCommand<UserControl>(dragMoveCommand);
    }

    [DllImport("user32.dll")]      
    public static extern IntPtr SendMessage(IntPtr hWnd, int wMsg, int wParam, int lParam);
    private void dragMoveCommand(UserControl userControl)
    {
        Window window = Application.Current.MainWindow;
        WindowInteropHelper helper = new WindowInteropHelper((Window)window);
        SendMessage(helper.Handle, 161, 2, 0);
    }
    private void closeCommand(UserControl userControl)
    {
        Application.Current.Shutdown();
    }
    private void maximizeCommand(UserControl userControl)
    {
        Window w = Application.Current.MainWindow;
        if (w.WindowState == WindowState.Normal)
            w.WindowState = WindowState.Maximized;
        else w.WindowState = WindowState.Normal;
    }
    private void minimizeCommand(UserControl userControl)
    {
        Window w = Application.Current.MainWindow;
        w.WindowState = WindowState.Minimized;
    }

}
