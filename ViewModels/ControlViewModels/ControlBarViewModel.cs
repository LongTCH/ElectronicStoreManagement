using ViewModels.Stores.Accounts;

namespace ViewModels.ControlViewModels;

public class ControlBarViewModel : ViewModelBase
{
    private readonly AccountStore _accountStore;
    public ControlBarViewModel(AccountStore accountStore)
    {
        _accountStore = accountStore;
        _accountStore.CurrentStoreChanged += () => OnPropertyChanged(nameof(Message));
    }

    public string Message => "Xin chào " + _accountStore.CurrentAccount?.FirstName;

}
