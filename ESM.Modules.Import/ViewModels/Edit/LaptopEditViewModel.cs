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
    public class LaptopEditViewModel : CommonInputViewModal<Laptop>
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
        public string Need
        {
            get => need;
            set => SetProperty(ref need, value);
        }
        public LaptopEditViewModel(IUnitOfWork unitOfWork, IOpenDialogService openDialogService, IModalService modalService, ViewModelStore viewModelStore) : base(unitOfWork, openDialogService, modalService, viewModelStore)
        {
        }

        public override async void OnNavigatedTo(NavigationContext navigationContext)
        {
            _viewModelStore.CurrentViewModel = this;
            Id = navigationContext.Parameters["laptopId"].ToString();
            var p = await _unitOfWork.Laptops.GetById(Id);
            Name = p.Name;
            AvatarPath = p.AvatarPath;
            Storage = p.Storage;
            Cpu = p.Cpu;
            Ram = p.Ram;
            Company = p.Company;
            DetailPath = p.DetailPath;
            ImagePath = p.ImagePath;
            Discount = p.Discount;
            Graphic = p.Graphic;
            Need = p.Need;
            Price = p.Price;
            Remain = p.Remain;
            Series = p.Series;
            Unit = p.Unit;
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
                var res = (bool)await _unitOfWork.Laptops.Update(p);
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
