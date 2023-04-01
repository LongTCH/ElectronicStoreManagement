using ESM.Core;
using ESM.Core.ShareServices;
using ESM.Modules.DataAccess.DTOs;
using ESM.Modules.DataAccess.Infrastructure;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESM.Modules.Authentication.ViewModels
{
    public class InputVerificationViewModel : BindableBase
    {
        private readonly IRegionManager _regionManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISendEmailService _sendEmailService;
        private readonly IModalService _modalService;

        public InputVerificationViewModel(IModalService modalService,
            IRegionManager regionManager,
            IUnitOfWork unitOfWork,
            ISendEmailService sendEmailService)
        {
            _regionManager = regionManager;
            _unitOfWork = unitOfWork;
            _sendEmailService = sendEmailService;
            _modalService = modalService;
            VerifyCommand = new(async () => await ExecuteVerifyCommand());
        }
        private string id;
        public string Id
        {
            get => id;
            set => SetProperty(ref id, value);
        }
        private bool isBusy;
        public bool IsBusy
        {
            get => isBusy;
            set => SetProperty(ref isBusy, value);
        }
        private string busyContent;
        public string BusyContent
        {
            get => busyContent;
            set => SetProperty(ref busyContent, value);
        }
        public DelegateCommand VerifyCommand { get; }

        #region Send email
        private async Task ExecuteVerifyCommand()
        {
            if (string.IsNullOrWhiteSpace(Id))
            {
                _modalService.ShowModal(ModalType.Error, "Nhập ID", "Cảnh báo");
                return;
            }
            IsBusy = true;
            BusyContent = "Đang kiểm tra tài khoản...";
            Task<AccountDTO> CheckAccountExist = new(() => _unitOfWork.Accounts.GetById(Id));
            CheckAccountExist.Start();
            await CheckAccountExist;
            var account = CheckAccountExist.Result;
            if (account == null)
            {
                _modalService.ShowModal(ModalType.Error, "Tài khoản không tồn tại", "Lỗi");
            }
            else
            {
                BusyContent = "Đang gửi email...";
                Random rnd = new();
                int ran = rnd.Next(100_000, 999_999);
                string _randomVerifyCode = ran.ToString();
                StringBuilder builder = new();
                int i = account.EmailAddress.IndexOf("@");
                if (i / 2 > 2)
                {
                    builder.Append(account.EmailAddress[..2]);
                    builder.Append(new string('*', i - 2));
                }
                else
                {
                    builder.Append(account.EmailAddress.ElementAt(0));
                    builder.Append(new string('*', i - 1));
                }
                builder.Append(account.EmailAddress[i..]);
                NavigationParameters parameter = new()
                {
                    { "MailMark", builder.ToString() },
                    {"Code", _randomVerifyCode },
                    { "Id", Id }
                };

                var result = await _sendEmailService.BeginSendEmail(account.EmailAddress, StaticData.EmailVerificationPrefix + _randomVerifyCode, true);
                if (result)
                    _modalService.ShowModal(ViewNames.VerifyEmailView, parameter);
                else
                    _modalService.ShowModal(ModalType.Error, "Không thể gửi email", "Lỗi");


            }
            IsBusy = false;
        }
        #endregion
    }
}
