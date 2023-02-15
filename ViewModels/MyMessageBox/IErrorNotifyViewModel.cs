namespace ViewModels.MyMessageBox;

public interface IErrorNotifyViewModel
{
    string? ErrorMessage { get; set; }
    string? ErrorTitle { get; set; }

    public abstract void Show(string? message, string? title);
}