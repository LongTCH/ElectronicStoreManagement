using ESM.Core.ShareServices;
using ESM.Modules.DataAccess.Models;
using System;
using ESM.Modules.Import.Utilities;
using System.Threading.Tasks;
using Prism.Regions;
using ESM.Modules.DataAccess.Infrastructure;
using ESM.Core.ShareStores;
using MahApps.Metro.Controls.Dialogs;
using MahApps.Metro.Controls;
using Microsoft.Identity.Client.Extensions.Msal;
using System.Drawing;
using System.Runtime.Intrinsics.Arm;
using System.Windows;

namespace ESM.Modules.Import.ViewModels.Add
{
    public class LaptopAddViewModel : CommonInputViewModal<Laptop>
    {
        private string cpu;
        public string Cpu
        {
            get => cpu;
            set => SetProperty(ref cpu, value);
        }
        private string ram;
        public string Ram
        {
            get => ram;
            set => SetProperty(ref ram, value);
        }
        private string storage;
        public string Storage
        {
            get => storage;
            set => SetProperty(ref storage, value);
        }
        private string graphic;
        public string Graphic
        {
            get => graphic;
            set => SetProperty(ref graphic, value);
        }
        private string series;
        public string Series
        {
            get => series;
            set => SetProperty(ref series, value);
        }
        private string need;

        public LaptopAddViewModel(IUnitOfWork unitOfWork, IOpenDialogService openDialogService, IModalService modalService, ViewModelStore viewModelStore) : base(unitOfWork, openDialogService, modalService, viewModelStore)
        {
        }

        public string Need
        {
            get => need;
            set => SetProperty(ref need, value);
        }
        public override async Task save()
        {
            if (Company == null || Unit == null ||
              Name == null || Storage == null || Graphic == null ||
              Cpu == null || Ram == null)
            {
                _modalService.ShowModal(ModalType.Error, "Nhập tất cả thông tin cần thiết", "Cảnh báo");
                return;
            }
            MetroWindow metroWindow = (MetroWindow)Application.Current.MainWindow;
            if (metroWindow.ShowModalMessageExternal("Thông báo", "Bạn có chắc chắn lưu", MessageDialogStyle.AffirmativeAndNegative) == MessageDialogResult.Affirmative)
            {
                Laptop p = new()
                {
                    Id = Id,
                    Name = Name,
                    Storage = Storage,
                    Cpu = Cpu,
                    Ram = Ram,
                    AvatarPath = AvatarPath,
                    Company = Company,
                    DetailPath = DetailPath,
                    Discount = Discount,
                    Graphic = Graphic,
                    ImagePath = ImagePath,
                    Need = Need,
                    Price = Price,
                    Remain = Remain,
                    Series = Series,
                    Unit = Unit,
                };
                var res = (bool)await _unitOfWork.Laptops.Add(p);
                if (res)
                {
                    _modalService.ShowModal(ModalType.Information, "Đã lưu", "Thông báo");
                    _viewModelStore.ParentViewModal.OnChildViewNotify();
                }
                else _modalService.ShowModal(ModalType.Error, "Có lỗi xảy ra", "Thông báo");
            }
        }

        public override async void OnNavigatedTo(NavigationContext navigationContext)
        {
            _viewModelStore.CurrentViewModel = this;
            Id = await _unitOfWork.Laptops.GetSuggestID();
        }
    }
}
