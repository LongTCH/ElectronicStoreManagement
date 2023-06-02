using ESM.Core.ShareServices;
using ESM.Core.ShareStores;
using ESM.Modules.DataAccess.Models;
using MahApps.Metro.Controls.Dialogs;
using MahApps.Metro.Controls;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Prism.Regions;
using ESM.Core;
using ESM.Modules.DataAccess.Infrastructure;
using System.ComponentModel.DataAnnotations;
using System.Windows;

namespace ESM.Modules.Import.ViewModels.Add
{
    [RegionMemberLifetime(KeepAlive = false)]
    public class ProviderAddViewModel : BindableBase, IViewModel, INavigationAware
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
        [Phone(ErrorMessage = "Số điện thoại không hợp lệ")]
        public string Phone
        {
            get => phone;
            set => SetProperty(ref phone, value, () => this.ValidateProperty(value, nameof(Phone)));
        }
        private string name;
        [Required(ErrorMessage = "Không được để trống")]
        public string Name
        {
            get => name;
            set => SetProperty(ref name, value, () => this.ValidateProperty(value, nameof(Name)));
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
            get => note;
            set => SetProperty(ref note, value);
        }

        public ProviderAddViewModel(IUnitOfWork unitOfWork, IModalService modalService, ViewModelStore viewModelStore)
        {
            _unitOfWork = unitOfWork;
            _modalService = modalService;
            _viewModelStore = viewModelStore;
        }

        public async Task save()
        {
            if (string.IsNullOrWhiteSpace(Name))
            {
                _modalService.ShowModal(ModalType.Error, "Nhập tên nhà cung cấp", "Thông báo");
                return;
            }

            MetroWindow metroWindow = (MetroWindow)Application.Current.MainWindow;
            if (metroWindow.ShowModalMessageExternal("Cảnh báo", "Bạn có chắc chắn lưu?", MessageDialogStyle.AffirmativeAndNegative) == MessageDialogResult.Affirmative)
            {
                Provider p = new()
                {
                    Id = Convert.ToInt32(Id),
                    ProviderName = Name,
                    Phone = Phone,
                    Note = Note,
                    Website = Website,
                };
                var res = (bool)await _unitOfWork.Providers.Add(p);
                if (res)
                {
                    _modalService.ShowModal(ModalType.Information, "Thêm thành công", "Thông báo");
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
            Id = await _unitOfWork.Providers.GetSuggestID();
        }


    }
}
