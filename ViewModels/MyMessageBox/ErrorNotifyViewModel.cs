using Microsoft.Extensions.Configuration;
using System.IO;
using System.Windows.Input;
using ViewModels.Commands;
using ViewModels.Services;
using ViewModels.Stores.Navigations;

namespace ViewModels.MyMessageBox;

public class ErrorNotifyViewModel : ViewModelBase, IErrorNotifyViewModel
{
    private static ErrorNotifyViewModel? _instance = new ErrorNotifyViewModel();
    internal static ErrorNotifyViewModel? Instance => _instance;
    public string? ErrorMessage { get; set; }
    public string? ErrorTitle { get; set; }
    private ErrorNotifyViewModel()
    {
        CloseCommand = new RelayCommand<object>(_ => TopLevelStore.Instance!.Close());
    }
    public void Show(string? message, string? title = null)
    {
        ErrorMessage = message;
        ErrorTitle = title;
        TopLevelStore.Instance!.CurrentViewModel = this;
    }
    public ICommand CloseCommand { get; }
}
