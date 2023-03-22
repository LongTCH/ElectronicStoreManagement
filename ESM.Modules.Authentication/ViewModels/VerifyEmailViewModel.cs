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
    public class VerifyEmailViewModel : BindableBase, INavigationAware
    {
        private readonly IModalService _modalService;
        private readonly IRegionManager _regionManager;
        public DelegateCommand CloseCommand { get; }
        public string EmailMark { get; set; }
        private string ReceivedCode;
        public VerifyEmailViewModel(IModalService modalService, IRegionManager regionManager)
        {
            _modalService = modalService;
            _regionManager = regionManager;
            CloseCommand = new(() => modalService.CloseModal(ViewNames.VerifyEmailView));
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
                        _modalService.ShowModal(ModalType.Error, "Verification Fail", "Error");
                    }
                    else --counter;
                    throw new ValidationException("Verify Failed");
                }
                else if (VerifyCode == ReceivedCode)
                {
                    CloseCommand.Execute();
                    _regionManager.RequestNavigate(RegionNames.ContentRegion, ViewNames.ResetPasswordView);
                    _modalService.ShowModal(ModalType.Information, "Please reset your password", "Verified");
                }
            }
        }
        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            var mailMark = navigationContext.Parameters["MailMark"] as string;
            ReceivedCode = navigationContext.Parameters["Code"] as string;
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
