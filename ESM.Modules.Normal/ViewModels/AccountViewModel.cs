using ESM.Core;
using ESM.Core.ShareServices;
using ESM.Core.ShareStores;
using ESM.Modules.DataAccess.Infrastructure;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Markup;

namespace ESM.Modules.Normal.ViewModels
{
    public class AccountViewModel : BindableBase, INavigationAware
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly AccountStore _accountStore;
        private readonly IEnumerable<string> GenderList;
        private readonly IOpenDialogService _openDialogService;
        public AccountViewModel(IUnitOfWork unitOfWork,
            AccountStore accountStore,
            IOpenDialogService openDialogService)
        {
            _unitOfWork = unitOfWork;
            _accountStore = accountStore;
            GenderList = StaticData.GenderList;
            AddAvatarCommand = new(addAvatarCommand);
            _openDialogService = openDialogService;
        }

        public string Id => _accountStore.CurrentAccount?.Id;
        public string FirstName => _accountStore.CurrentAccount?.FirstName;
        public string LastName => _accountStore.CurrentAccount?.LastName;
        public string Email => _accountStore.CurrentAccount?.EmailAddress;
        public string Phone => _accountStore.CurrentAccount?.Phone;
        public string City => _accountStore.CurrentAccount?.City;
        public string District => _accountStore.CurrentAccount?.District;
        public string SubDistrict => _accountStore.CurrentAccount?.SubDistrict;
        public string Street => _accountStore.CurrentAccount?.Street;
        public string Gender => _accountStore.CurrentAccount.Gender ? GenderList.ElementAt(0) : GenderList.ElementAt(1);
        public string Avatar_Path
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
        public bool IsSellStaff => _accountStore.IsSellStaff || IsAdmin;
        public bool IsTypingStaff => _accountStore.IsTypingStaff || IsAdmin;
        public DelegateCommand ResetPasswordCommand { get; }
        public DelegateCommand AddAvatarCommand { get; }
        public DelegateCommand AddUserCommand { get; }
        public DelegateCommand ChangeUserInfoCommand { get; }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            RaisePropertyChanged(nameof(Id));
            RaisePropertyChanged(nameof(FirstName));
            RaisePropertyChanged(nameof(LastName));
            RaisePropertyChanged(nameof(Email));
            RaisePropertyChanged(nameof(Phone));
            RaisePropertyChanged(nameof(City));
            RaisePropertyChanged(nameof(District));
            RaisePropertyChanged(nameof(SubDistrict));
            RaisePropertyChanged(nameof(Street));
            RaisePropertyChanged(nameof(Gender));
            RaisePropertyChanged(nameof(Avatar_Path));
            RaisePropertyChanged(nameof(BirthDay));
            RaisePropertyChanged(nameof(DateFormat));
            RaisePropertyChanged(nameof(Language));
            RaisePropertyChanged(nameof(IsDefault));
            RaisePropertyChanged(nameof(IsAdmin));
            RaisePropertyChanged(nameof(IsSellStaff));
            RaisePropertyChanged(nameof(IsTypingStaff));
        }

        private void addAvatarCommand()
        {
            Avatar_Path = _openDialogService.FileDialog(FileType.Image);
            if (Avatar_Path != null)
            {
                RaisePropertyChanged(nameof(IsDefault));
                RaisePropertyChanged(nameof(Avatar_Path));
            }
            _unitOfWork.Accounts.Update(_accountStore.CurrentAccount!);
            _unitOfWork.SaveChangesAsync();
        }
    }
}
