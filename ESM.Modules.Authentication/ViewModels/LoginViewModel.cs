using ESM.Core;
using ESM.Core.ShareServices;
using ESM.Core.ShareStores;
using ESM.Modules.Authentication.Views;
using ESM.Modules.DataAccess.DTOs;
using ESM.Modules.DataAccess.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Prism.Commands;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Regions;
using Scrypt;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;

namespace ESM.Modules.Authentication.ViewModels
{
    public class LoginViewModel : BindableBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRegionManager _regionManager;
        private readonly AccountStore _accountStore;
        private readonly IModalService _modalService;
        private string _id;
        public string Id
        {
            get { return _id; }
            set { _id = value; }
        }

        private string _password;
        public string Password
        {
            get { return _password; }
            set { SetProperty(ref _password, value); }
        }
        private bool isBusy;
        public bool IsBusy
        {
            get => isBusy;
            set => SetProperty(ref isBusy, value);
        }
        public LoginViewModel(IUnitOfWork unitOfWork,
            AccountStore accountStore,
            IRegionManager regionManager,
            IModalService modalService)
        {
            _unitOfWork = unitOfWork;
            _accountStore = accountStore;
            _regionManager = regionManager;
            _modalService = modalService;
            LoginCommand = new (async () => await ExecuteLogin());
            ForgotPasswordNavigationCommand = new(() => _regionManager.RequestNavigate(RegionNames.ContentRegion, ViewNames.InputVerificationView));
        }

        public DelegateCommand LoginCommand { get; }
        public DelegateCommand ForgotPasswordNavigationCommand { get; }

        AccountDTO Login()
        {
            IsBusy = true;
            ScryptEncoder encoder = new();
            var account = _unitOfWork.Accounts.GetById(Id);
            if (account == null || !encoder.Compare(Password, account.PasswordHash))
                account = null;
            _accountStore.CurrentAccount = account;
            IsBusy = false;
            return account;
        }
        private async Task ExecuteLogin()
        {
            
            if (string.IsNullOrWhiteSpace(Id))
            {
                _modalService.ShowModal(ModalType.Error, "Enter ID", "Error");
                await Task.CompletedTask;
            }
            else if (string.IsNullOrWhiteSpace(Password))
            {
                _modalService.ShowModal(ModalType.Error, "Enter Password", "Error");
                await Task.CompletedTask;
            }
            else
            {
                Task<AccountDTO> task = new(Login);
                task.Start();
                var account = await task;
                if (account == null)
                    _modalService.ShowModal(ModalType.Error, "Cannot find your account", "Error");
                else _regionManager.RequestNavigate(RegionNames.ContentRegion, ViewNames.AccountView);
            }
            
        }
    }
}
