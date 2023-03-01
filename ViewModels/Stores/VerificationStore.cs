using ViewModels.Stores.Accounts;

namespace ViewModels.Stores;

public class VerificationStore
{
    private readonly AccountStore _store;

    public VerificationStore(AccountStore store)
    {
        _store = store;
    }

    private string? _id;
    public string? Id
    {
        get
        {
            if (_id == null) return _store.CurrentAccount?.Id;
            return _id;
        }
        set => _id = value;
    }
    public string? VerificationCode { get; set; }
    public string? EmailMark { get; set; }
}
