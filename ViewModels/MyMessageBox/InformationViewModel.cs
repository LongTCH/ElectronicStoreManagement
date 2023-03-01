using ViewModels.Stores.Navigations;

namespace ViewModels.MyMessageBox;

public class InformationViewModel : NotifyViewModel
{
    private static InformationViewModel instance = new();
    public static InformationViewModel Instance => instance;
}
