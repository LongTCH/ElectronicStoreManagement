using Microsoft.Win32;
using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Net;
using System.Net.Mail;
using System.Windows;
using System.Windows.Input;
using ViewModels.Commands;
using ViewModels.Services;
using ViewModels.Stores;

namespace ViewModels.NotifyControlViewModels;

public class VerifyEmailViewModel : ViewModelBase
{
    private readonly EmailStore _emailStore;
    
    public ICommand CloseCommand { get; }
    public int MaxLengthCode { get; init; } = 6;
    public VerifyEmailViewModel(EmailStore emailStore, CloseModalNavigationService closeModalNavigationService)
    {
        _emailStore = emailStore;
        CloseCommand = new RelayCommand<object>(_ => closeModalNavigationService.Navigate());
    }
    private string _verifyCode;
    public string VerifyCode
    {
        get => _verifyCode;
        set
        {
            _verifyCode = value;
            if (VerifyCode != _emailStore.VerificationCode) throw new ValidationException("Verify Failed");
            MessageBox.Show("Verifed");
            CloseCommand.Execute(new {});
        }
    }
}
