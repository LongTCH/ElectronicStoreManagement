using System.ComponentModel.DataAnnotations;
using System.Windows.Input;
using ViewModels.Commands;
using ViewModels.MyMessageBox;
using ViewModels.Services;
using ViewModels.Stores;

namespace ViewModels.NotifyControlViewModels;

public class VerifyEmailViewModel : ViewModelBase
{
    private readonly INavigationService _resetPasswordNavigate;
    private readonly INavigationService _homeNavigate;
    private readonly VerificationStore _store;
    private int counter = 2;
    public ICommand CloseCommand { get; }
    public string EmailMark => _store.EmailMark!;
    public int MaxLengthCode { get; init; } = 6;
    public VerifyEmailViewModel(VerificationStore Store,
        CloseModalNavigationService closeModalNavigationService,
        INavigationService resetPasswordNavigate,
        INavigationService homeNavigate)
    {
        _store = Store;
        _resetPasswordNavigate = resetPasswordNavigate;
        _homeNavigate = homeNavigate;
        CloseCommand = new RelayCommand<object>(_ => closeModalNavigationService.Navigate());
    }
    private string? _verifyCode;
    public string? VerifyCode
    {
        get => _verifyCode;
        set
        {
            if (value?.Length <= MaxLengthCode) _verifyCode = value;
            if (_verifyCode?.Length == MaxLengthCode && VerifyCode != _store.VerificationCode)
            {
                if (counter == 0)
                {
                    CloseCommand.Execute(null);
                    _homeNavigate.Navigate();
                    ErrorNotifyViewModel.Instance.Show("Enter too much wrong", "Failed");
                }
                else --counter;
                throw new ValidationException("Verify Failed");
            }
            else if (VerifyCode == _store.VerificationCode)
            {
                CloseCommand.Execute(null);
                InformationViewModel.Instance!.Show("Please reset your password", "Verified");
                _resetPasswordNavigate.Navigate();
            }
        }
    }
}
