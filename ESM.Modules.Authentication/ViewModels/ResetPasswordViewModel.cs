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
    [RegionMemberLifetime(KeepAlive = false)]
    public class ResetPasswordViewModel : BindableBase, INavigationAware
    {
        private readonly AccountStore _accountStore;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRegionManager _regionManager;
        private readonly IModalService _modalService;
        private string Id;
        public ResetPasswordViewModel(AccountStore accountStore,
            IUnitOfWork unitOfWork, IRegionManager regionManager,
            IModalService modalService)
        {
            _accountStore = accountStore;
            _unitOfWork = unitOfWork;
            _regionManager = regionManager;
            _modalService = modalService;
            ResetCommand = new DelegateCommand(async () => await reset());
        }
        public DelegateCommand ResetCommand { get; }
        public string? OldPassword { get; set; }
        public string? NewPassword { get; set; }
        public string? ReTypeNewPassword { get; set; }
        public bool IsOldPasswordType { get; set; }
        private async Task reset()
        {
            if (IsOldPasswordType)
            {
                ScryptEncoder encoder = new ScryptEncoder();
                if (encoder.Compare(OldPassword, _accountStore.CurrentAccount!.PasswordHash) == false)
                {
                    _modalService.ShowModal(ModalType.Error, "Sai mật khẩu", "Thất bại");
                    return;
                }
                Id = _accountStore.CurrentAccount.Id;
            }
            if (NewPassword != ReTypeNewPassword || NewPassword == null)
            {
                _modalService.ShowModal(ModalType.Error, "Mật khẩu không hợp lệ", "Thất bại");
                return;
            }
            try
            {
                Task t = new(() =>
                {
                    ScryptEncoder encoder = new ScryptEncoder();
                    _unitOfWork.Accounts.ResetPassword(Id, encoder.Encode(NewPassword!));
                    _unitOfWork.SaveChange();
                });
                t.Start();
                await t;
                _modalService.ShowModal(ModalType.Information, "Đăng nhập lại", "Thành công");
            }
            catch (Exception)
            {
                _modalService.ShowModal(ModalType.Error, "Đặt lại mật khẩu không thành công", "Lỗi");
            }
            finally
            {
                _accountStore.Logout();
                _regionManager.RequestNavigate(RegionNames.ContentRegion, ViewNames.LoginView);
            }
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {

        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            IsOldPasswordType = _accountStore.CurrentAccount != null;
            if (!IsOldPasswordType) Id = navigationContext.Parameters["Id"].ToString();
            RaisePropertyChanged(nameof(IsOldPasswordType));
        }
    }
}
