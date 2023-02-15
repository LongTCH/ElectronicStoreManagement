using System.Windows.Input;

namespace ViewModels.MyMessageBox;

public interface IErrorNotifyViewModel
{
    string? Message { get; set; }
    string? Title { get; set; }

    public ICommand CloseCommand { get; }
    public abstract void Show(string? message, string? title = null);
}