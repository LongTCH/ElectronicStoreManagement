using ESM.Core.ShareServices;
using ESM.Core.ShareStores;
using ESM.Modules.DataAccess.Infrastructure;
using ESM.Modules.DataAccess.Models;
using MahApps.Metro.Controls.Dialogs;
using MahApps.Metro.Controls;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using ESM.Core;
using System.ComponentModel.DataAnnotations;

namespace ESM.Modules.Import.ViewModels.Edit
{
    [RegionMemberLifetime(KeepAlive = false)]
    public class ProviderEditViewModel : BindableBase, IViewModel, INavigationAware
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IModalService _modalService;
        private readonly ViewModelStore _viewModelStore;
        private string id;
        public string Id
        {
            get => id;
            set => SetProperty(ref id, value);
        }
        private string phone;
        [Phone(ErrorMessage ="Số điện thoại không hợp lệ")]
        public string Phone
        {
            get => phone;
            set => SetProperty(ref phone, value, () => this.ValidateProperty(value, nameof(Phone)));
        }
        private string name;
        [Required]
        public string Name
        {
            get => name;
            set => SetProperty(ref name, value, () => this.ValidateProperty(value, nameof (Name)));
        }
        private string website;
        public string Website
        {
            get => website;
            set => SetProperty(ref website, value);
        }
        private string note;
        public string Note
        {
            get => website;
            set => SetProperty(ref website, value);
        }
        private Provider provider;

        public ProviderEditViewModel(IUnitOfWork unitOfWork, IModalService modalService, ViewModelStore viewModelStore)
        {
            _unitOfWork = unitOfWork;
            _modalService = modalService;
            _viewModelStore = viewModelStore;
        }

        public async Task save()
        {
            if (string.IsNullOrWhiteSpace(provider.ProviderName))
            {
                _modalService.ShowModal(ModalType.Error, "Nhập tên nhà cung cấp", "Thông báo");
                return;
            }
     
            MetroWindow metroWindow = (MetroWindow)Application.Current.MainWindow;
            if (metroWindow.ShowModalMessageExternal("Cảnh báo", "Bạn có chắc chắn lưu?", MessageDialogStyle.AffirmativeAndNegative) == MessageDialogResult.Affirmative)
            {
                var res = (bool)await _unitOfWork.Providers.Update(provider);
                if (res)
                {
                    _modalService.ShowModal(ModalType.Information, "Cập nhật thành công", "Thông báo");
                    _viewModelStore.ParentViewModal.OnChildViewNotify();
                }
                else _modalService.ShowModal(ModalType.Error, "Có lỗi xảy ra", "Thất bại");
            }
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return false;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {

        }

        public async void OnNavigatedTo(NavigationContext navigationContext)
        {
            _viewModelStore.CurrentViewModel = this;
            Id = navigationContext.Parameters["providerId"].ToString();
            provider = await _unitOfWork.Providers.GetById(Id);
            _viewModelStore.CurrentViewModel = this;
        }


    }
}
