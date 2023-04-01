using ESM.Core;
using ESM.Core.ShareServices;
using ESM.Modules.Authentication.Views;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ESM.Modules.Authentication.ViewModels
{
    public class VerifyEmailViewModel : BindableBase, INavigationAware, IModal
    {
        private readonly IModalService _modalService;
        private readonly IRegionManager _regionManager;
        public DelegateCommand CloseCommand { get; }
        public string EmailMark { get; set; }
        private string ReceivedCode;
        private string Id;
        public VerifyEmailViewModel(IModalService modalService, IRegionManager regionManager)
        {
            _modalService = modalService;
            _regionManager = regionManager;
            CloseCommand = new(() => modalService.CloseModal());
        }
        public int MaxLengthCode { get; } = 6;
        private int counter = 2;
        private string _verifyCode;
        public string VerifyCode
        {
            get => _verifyCode;
            set
            {
                if (value?.Length <= MaxLengthCode) _verifyCode = value;
                if (_verifyCode?.Length == MaxLengthCode && VerifyCode != ReceivedCode)
                {
                    if (counter == 0)
                    {
                        CloseCommand.Execute();
                        _regionManager.RequestNavigate(RegionNames.ContentRegion, ViewNames.HomeView);
                        _regionManager.ResetTrace();
                        _modalService.ShowModal(ModalType.Error, "Nhập sai nhiều lần", "Thất bại");
                    }
                    else --counter;
                    throw new ValidationException("Verify Failed");
                }
                else if (VerifyCode == ReceivedCode)
                {
                    CloseCommand.Execute();
                    NavigationParameters parameter = new()
                    {
                        { "Id", Id },
                    };
                    _regionManager.RequestNavigate(RegionNames.ContentRegion, ViewNames.ResetPasswordView, parameter);
                    _modalService.ShowModal(ModalType.Information, "Đặt lại mật khẩu", "Đã xác minh");
                }
            }
        }
        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            var mailMark = navigationContext.Parameters["MailMark"] as string;
            ReceivedCode = navigationContext.Parameters["Code"] as string;
            Id = navigationContext.Parameters["Id"] as string;
            EmailMark = mailMark ?? string.Empty;
            RaisePropertyChanged(nameof(EmailMark));
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {

        }
    }
}
