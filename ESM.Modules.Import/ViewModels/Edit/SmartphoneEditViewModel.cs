using ESM.Core.ShareServices;
using ESM.Core.ShareStores;
using ESM.Modules.DataAccess.Infrastructure;
using ESM.Modules.DataAccess.Models;
using ESM.Modules.Import.Utilities;
using MahApps.Metro.Controls.Dialogs;
using MahApps.Metro.Controls;
using Prism.Regions;
using System.Threading.Tasks;
using System.Windows;

namespace ESM.Modules.Import.ViewModels.Edit
{
    public class SmartphoneEditViewModel : CommonInputViewModal<Smartphone>
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
        private string series;
        public string Series
        {
            get => series;
            set => SetProperty(ref series, value);
        }
        public SmartphoneEditViewModel(IUnitOfWork unitOfWork, IOpenDialogService openDialogService, IModalService modalService, ViewModelStore viewModelStore) : base(unitOfWork, openDialogService, modalService, viewModelStore)
        {
        }

        public override async void OnNavigatedTo(NavigationContext navigationContext)
        {
            _viewModelStore.CurrentViewModel = this;
            Id = navigationContext.Parameters["smartphoneId"].ToString();
            var p = await _unitOfWork.Smartphones.GetById(Id);
            Id = p.Id;
            AvatarPath = p.AvatarPath;
            Company = p.Company;
            Name = p.Name;
            Cpu = p.Cpu;
            Ram = p.Ram;
            Storage = p.Storage;
            DetailPath = p.DetailPath;
            Discount = p.Discount;
            ImagePath = p.ImagePath;
            Price = p.Price;
            Remain = p.Remain;
            Series = p.Series;
            Unit = p.Unit;
        }

        public override async Task save()
        {
            if (Company == null || Unit == null || Name == null ||
                   Cpu == null || Ram == null || Storage == null)
            {
                _modalService.ShowModal(ModalType.Error, "Nhập tất cả thông tin cần thiết", "Cảnh báo");
                return;
            }
            MetroWindow metroWindow = (MetroWindow)Application.Current.MainWindow;
            if (metroWindow.ShowModalMessageExternal("Thông báo", "Bạn có chắc chắn lưu?", MessageDialogStyle.AffirmativeAndNegative) == MessageDialogResult.Affirmative)
            {
                Smartphone p = new()
                {
                    Id = Id,
                    AvatarPath = AvatarPath,
                    Company = Company,
                    Name = Name,
                    Cpu = Cpu,
                    Ram = Ram,
                    Storage = Storage,
                    DetailPath = DetailPath,
                    Discount = Discount,
                    ImagePath = ImagePath,
                    Price = Price,
                    Remain = Remain,
                    Series = Series,
                    Unit = Unit,
                };
                bool res = (bool)await _unitOfWork.Smartphones.Update(p);
                if (res)
                {
                    _modalService.ShowModal(ModalType.Information, "Cập nhật thành công", "Thông báo");
                    _viewModelStore.ParentViewModal.OnChildViewNotify();
                }
                else _modalService.ShowModal(ModalType.Error, "Có lỗi xảy ra", "Thông báo");
            }
        }
    }
}
