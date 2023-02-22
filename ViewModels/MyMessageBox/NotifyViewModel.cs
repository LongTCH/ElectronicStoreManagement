using System;
using System.Windows.Input;
using ViewModels.Commands;
using ViewModels.Stores.Navigations;

namespace ViewModels.MyMessageBox;

public class NotifyViewModel : ViewModelBase
{

    public string? Message { get; set; }
    public string? Title { get; set; }

    public ICommand CloseCommand { get; }
    protected NotifyViewModel()
    {
        CloseCommand = new RelayCommand<object>(_ => TopLevelStore.Instance!.Close());
    }
    public virtual void Show(string? message, string? title = null)
    {
        Message = message;
        Title = title;
        TopLevelStore.Instance!.CurrentViewModel = this;
    }
}