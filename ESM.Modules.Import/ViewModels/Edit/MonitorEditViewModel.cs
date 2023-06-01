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
    public class MonitorEditViewModel : CommonInputViewModal<Monitor>
    {
        private string size;
        public string Size
        {
            get => size;
            set => SetProperty(ref size, value);
        }
        private string panel;
        public string Panel
        {
            get => panel;
            set => SetProperty(ref panel, value);
        }
        private string refreshRate;
        public string RefreshRate
        {
            get => refreshRate;
            set => SetProperty(ref refreshRate, value);
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
        public MonitorEditViewModel(IUnitOfWork unitOfWork, IOpenDialogService openDialogService, IModalService modalService, ViewModelStore viewModelStore) : base(unitOfWork, openDialogService, modalService, viewModelStore)
        {
        }

        public override async void OnNavigatedTo(NavigationContext navigationContext)
        {
            _viewModelStore.CurrentViewModel = this;
            Id = navigationContext.Parameters["monitorId"].ToString();
            var p = await _unitOfWork.Monitors.GetById(Id);
            AvatarPath = p.AvatarPath;
            Company = p.Company;
            DetailPath = p.DetailPath;
            Discount = p.Discount;
            ImagePath = p.ImagePath;
            Name = p.Name;
            Need = p.Need;
            Panel = p.Panel;
            Price = p.Price;
            RefreshRate = p.RefreshRate;
            Remain = p.Remain;
            Series = p.Series;
            Size = p.Size;
            Unit = p.Unit;
        }

        public override async Task save()
        {
            if (Company == null || Unit == null ||
                Name == null || Size == null || Panel == null)
            {
                _modalService.ShowModal(ModalType.Error, "Nhập tất cả thông tin cần thiết", "Cảnh báo");
                return;
            }
            MetroWindow metroWindow = (MetroWindow)Application.Current.MainWindow;
            if (metroWindow.ShowModalMessageExternal("Thông báo", "Bạn có chắc chắn lưu?", MessageDialogStyle.AffirmativeAndNegative) == MessageDialogResult.Affirmative)
            {
                Monitor p = new()
                {
                    Id = Id,
                    AvatarPath = AvatarPath,
                    Company = Company,
                    DetailPath = DetailPath,
                    Discount = Discount,
                    ImagePath = ImagePath,
                    Name = Name,
                    Need = Need,
                    Panel = Panel,
                    Price = Price,
                    RefreshRate = RefreshRate,
                    Remain = Remain,
                    Series = Series,
                    Size = Size,
                    Unit = Unit
                };
                var res = (bool)await _unitOfWork.Monitors.Update(p);
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
