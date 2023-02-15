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
    private readonly VerificationStore _store;
    public string Id { get; set; }
    public ICommand VerifyCommand { get; }
    public InputVerificationViewModel(VerificationStore Store, INavigationService verifyNavigationService)
    {
        _store = Store;
        VerifyCommand = new RelayCommand<object>(_ => { _store.Id = Id; verifyNavigationService.Navigate(); });
    }
}
