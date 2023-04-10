using ESM.Core;
using ESM.Core.ShareServices;
using ESM.Core.ShareStores;
using ESM.Modules.DataAccess.Infrastructure;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using Scrypt;
using System;
using System.Threading.Tasks;

namespace ESM.Modules.Authentication.ViewModels
{
    public class LoginViewModel : BindableBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRegionManager _regionManager;
        private readonly AccountStore _accountStore;
        private readonly IModalService _modalService;
        private readonly IApplicationCommand _applicationCommand;
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
            set { _password = value; }
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
            IModalService modalService,
            IApplicationCommand applicationCommand)
        {
            _unitOfWork = unitOfWork;
            _accountStore = accountStore;
            _regionManager = regionManager;
            _modalService = modalService;
            _applicationCommand = applicationCommand;
            LoginCommand = new(async () => await ExecuteLogin());
            ForgotPasswordNavigationCommand = new(forgotPasswordNavigationCommand);
        }

        public DelegateCommand LoginCommand { get; }
        public DelegateCommand ForgotPasswordNavigationCommand { get; }
        private async Task ExecuteLogin()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(Id))
                {
                    _modalService.ShowModal(ModalType.Error, "Nhập ID", "Cảnh báo");
                    await Task.CompletedTask;
                }
                else if (string.IsNullOrWhiteSpace(Password))
                {
                    _modalService.ShowModal(ModalType.Error, "Nhập mật khẩu", "Cảnh báo");
                    await Task.CompletedTask;
                }
                else
                {
                    IsBusy = true;
                    ScryptEncoder encoder = new();
                    var account = await _unitOfWork.Accounts.GetById(Id);
                    if (account == null || !encoder.Compare(Password, account.PasswordHash))
                        account = null;
                    _accountStore.CurrentAccount = account;
                    IsBusy = false;
                    if (account == null)
                        _modalService.ShowModal(ModalType.Error, "Sai ID hoặc mật khẩu", "Thất bại");
                    else
                    {
                        _regionManager.RequestNavigateContentRegionWithTrace(ViewNames.AccountView);
                        _applicationCommand.ResetIndexCommand.Execute(true);
                    }
                }
            }

            catch (Exception) { _modalService.ShowModal(ModalType.Error, "Không thể kết nối dữ liệu", "Lỗi"); }
        }
        private void forgotPasswordNavigationCommand()
        {
            _regionManager.RequestNavigateContentRegionWithTrace(ViewNames.InputVerificationView);
            _applicationCommand.ResetIndexCommand.Execute(true);
        }
    }
}
