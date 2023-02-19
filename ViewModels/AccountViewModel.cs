using Microsoft.Win32;
using Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Input;
using System.Windows.Markup;
using ViewModels.Commands;
using ViewModels.Services;
using ViewModels.Stores.Accounts;

namespace ViewModels;

public class AccountViewModel : ViewModelBase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly AccountStore _accountStore;
    private readonly IEnumerable<string> GenderList;
    public string? Id => _accountStore.CurrentAccount!.Id;
    public string? FirstName => _accountStore.CurrentAccount!.FirstName;
    public string? LastName => _accountStore.CurrentAccount!.LastName;
    public string? Email => _accountStore.CurrentAccount!.EmailAddress;
    public string? Phone => _accountStore.CurrentAccount?.Phone;
    public string? City => _accountStore.CurrentAccount!.City;
    public string? District => _accountStore.CurrentAccount!.District;
    public string? SubDistrict => _accountStore.CurrentAccount!.SubDistrict;
    public string? Street => _accountStore.CurrentAccount!.Street;
    public string? Gender => _accountStore.CurrentAccount!.Sex ? GenderList.ElementAt(0) : GenderList.ElementAt(1);
    public string? Avatar_Path
    {
        get => _accountStore.CurrentAccount?.AvatarPath;
        set
        {
            _accountStore.CurrentAccount!.AvatarPath = value;
        }
    }
    public DateTime BirthDay { get; set; }
    public string DateFormat => CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern;
    public XmlLanguage Language => XmlLanguage.GetLanguage(CultureInfo.CurrentCulture.IetfLanguageTag);
    public bool IsDefault => !File.Exists(Avatar_Path);
    public bool IsAdmin => _accountStore.IsAdmin;
    public ICommand ResetPasswordCommand { get; }
    public ICommand AddAvatarCommand { get; }
    public ICommand AddUserCommand { get; }
    public ICommand ChangeUserInfoCommand { get; }
    public AccountViewModel(IUnitOfWork unitOfWork,
        AccountStore accountStore,
        INavigationService resetPasswordNavigationService,
        INavigationService registerNavigationService,
        INavigationService changeUserInfoNavigationService)
    {
        _unitOfWork = unitOfWork;
        _accountStore = accountStore;
        GenderList = new GetGenderListCommand().Execute();
        ResetPasswordCommand = new RelayCommand<object>(_ => resetPasswordNavigationService.Navigate());
        AddAvatarCommand = new RelayCommand<object>(_ => addAvatarCommand());
        AddUserCommand = new RelayCommand<object>(_ => registerNavigationService.Navigate());
        ChangeUserInfoCommand = new RelayCommand<object>(_ => changeUserInfoNavigationService.Navigate());
        //_accountStore.CurrentStoreChanged += OnCurrentAccountChanged;
    }
    private void addAvatarCommand()
    {
        OpenFileDialog openFileDialog = new();
        openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;...";
        openFileDialog.Title = "Direct to your avartar";
        if (openFileDialog.ShowDialog() == true)
        {
            Avatar_Path = openFileDialog.FileName;
            OnPropertyChanged(nameof(IsDefault));
            OnPropertyChanged(nameof(Avatar_Path));
        }
        _unitOfWork.Accounts.Update(_accountStore.CurrentAccount!);
        _unitOfWork.Complete();
    }
    public override void Dispose()
    {
        //_accountStore.CurrentStoreChanged -= OnCurrentAccountChanged;

        base.Dispose();
    }
}
