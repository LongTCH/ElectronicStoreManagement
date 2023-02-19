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
        CloseCommand = new RelayCommand<object>(_ => closeCommand());
        MaximizeCommand = new RelayCommand<object>(_ => maximizeCommand());
        MinimizeCommand = new RelayCommand<object>(_ => minimizeCommand());
        DragMoveCommand = new RelayCommand<object>(_ => dragMoveCommand());
    }

    [DllImport("user32.dll")]
    public static extern IntPtr SendMessage(IntPtr hWnd, int wMsg, int wParam, int lParam);
    private void dragMoveCommand()
    {
        Window window = Application.Current.MainWindow;
        WindowInteropHelper helper = new(window);
        SendMessage(helper.Handle, 161, 2, 0);
    }
    private void closeCommand()
    {
        Application.Current.Shutdown();
    }
    private void maximizeCommand()
    {
        Window w = Application.Current.MainWindow;
        if (w.WindowState == WindowState.Normal)
            w.WindowState = WindowState.Maximized;
        else w.WindowState = WindowState.Normal;
    }
    private void minimizeCommand()
    {
        Window w = Application.Current.MainWindow;
        w.WindowState = WindowState.Minimized;
    }

}
