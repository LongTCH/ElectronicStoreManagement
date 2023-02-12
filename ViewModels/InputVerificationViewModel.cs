using Microsoft.EntityFrameworkCore.Metadata;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Windows.Input;
using ViewModels.Commands;
using ViewModels.Services;
using ViewModels.Stores;

namespace ViewModels;

public class InputVerificationViewModel : ViewModelBase
{
    private readonly EmailStore _emailStore;
    private string _email;
    [RegularExpression("^[a-z0-9_\\+-]+(\\.[a-z0-9_\\+-]+)*@[a-z0-9-]+(\\.[a-z0-9]+)*\\.([a-z]{2,4})$", ErrorMessage = "Invalid email format.")]
    [EmailAddress]
    public string Email
    {
        get => _email;
        set
        {
            _email = value;
            CanClick = false;
            OnPropertyChanged(nameof(CanClick));
            ValidateProperty(value, nameof(Email));
            CanClick = true;
            OnPropertyChanged(nameof(CanClick));
        }
    }
    public bool CanClick { get; set; }
    public ICommand VerifyCommand { get; }
    public InputVerificationViewModel(EmailStore emailStore, INavigationService verifyNavigationService)
    {
        CanClick = false;
        _emailStore = emailStore;
        VerifyCommand = new RelayCommand<object>(_ => { _emailStore.Email = Email; verifyNavigationService.Navigate(); });
    }

    private void ValidateProperty<T>(T value, string name)
    {
        Validator.ValidateProperty(value, new ValidationContext(this, null, null)
        {
            MemberName = name
        });
    }
}
