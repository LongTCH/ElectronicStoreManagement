using ViewModels.Stores.Navigations;

namespace ViewModels.MyMessageBox;

public class ErrorNotifyViewModel : NotifyViewModel
{
    private static ErrorNotifyViewModel instance = new();
    public static ErrorNotifyViewModel Instance => instance;
}
