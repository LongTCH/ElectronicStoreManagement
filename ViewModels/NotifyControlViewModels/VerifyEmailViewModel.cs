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
    private readonly VerificationStore _store;

    public ICommand CloseCommand { get; }
    public string EmailMark => _store.EmailMark!;
    public int MaxLengthCode { get; init; } = 6;
    public VerifyEmailViewModel(VerificationStore Store,
        CloseModalNavigationService closeModalNavigationService,
        INavigationService resetPasswordNavigate)
    {
        _store = Store;
        _resetPasswordNavigate = resetPasswordNavigate;
        CloseCommand = new RelayCommand<object>(_ => closeModalNavigationService.Navigate());
    }
    private string? _verifyCode;
    public string? VerifyCode
    {
        get => _verifyCode;
        set
        {
            if (_verifyCode == null || _verifyCode.Length < MaxLengthCode) _verifyCode = value;
            if (VerifyCode != _store.VerificationCode) throw new ValidationException("Verify Failed");
            CloseCommand.Execute(null);
            InformationViewModel.Instance!.Show("Please reset your password", "Verified");
            _resetPasswordNavigate.Navigate();
        }
    }
}
