using ESM.Core.ShareServices;
using ESM.Modules.DataAccess.Models;
using ESM.Modules.Import.Utilities;
using MahApps.Metro.Controls.Dialogs;
using MahApps.Metro.Controls;
using Prism.Regions;
using System.Threading.Tasks;
using System.Windows;
using ESM.Modules.DataAccess.Infrastructure;
using ESM.Core.ShareStores;

namespace ESM.Modules.Import.ViewModels.Edit
{
    public class HardDiskEditViewModel: CommonInputViewModal<Pcharddisk>
    {
        private string storage;
        public string Storage
        {
            get => storage;
            set => SetProperty(ref storage, value);
        }
        private string connect;
        public string Connect
        {
            get => connect;
            set => SetProperty(ref connect, value);
        }
        private string type;
        public string Type
        {
            get => type;
            set => SetProperty(ref type, value);
        }
        private string series;

        public HardDiskEditViewModel(IUnitOfWork unitOfWork, IOpenDialogService openDialogService, IModalService modalService, ViewModelStore viewModelStore) : base(unitOfWork, openDialogService, modalService, viewModelStore)
        {
        }

        public string Series
        {
            get => series;
            set => SetProperty(ref series, value);
        }

        public override async Task save()
        {
            if (Company == null || Unit == null ||
                Name == null || Storage == null || Connect == null || Type == null
                || Price == 0)
            {
                _modalService.ShowModal(ModalType.Error, "Nhập tất cả thông tin cần thiết", "Cảnh báo");
                return;
            }
            MetroWindow metroWindow = (MetroWindow)Application.Current.MainWindow;
            if (metroWindow.ShowModalMessageExternal("Thông báo", "Bạn có chắc chắn lưu?", MessageDialogStyle.AffirmativeAndNegative) == MessageDialogResult.Affirmative)
            {
                Pcharddisk p = new()
                {
                    Id = Id,
                    Name = Name,
                    AvatarPath = AvatarPath,
                    Company = Company,
                    Price = Price,
                    Connect = Connect,
                    Type = Type,
                    DetailPath = DetailPath,
                    Discount = Discount,
                    ImagePath = ImagePath,
                    Remain = Remain,
                    Series = Series,
                    Storage = Storage,
                    Unit = Unit,
                };
                bool res = (bool)await _unitOfWork.Pcharddisks.Update(p);
                if (res)
                {
                    _modalService.ShowModal(ModalType.Information, "Cập nhật thành công", "Thông báo");
                    _viewModelStore.ParentViewModal.OnChildViewNotify();

                }
                else _modalService.ShowModal(ModalType.Error, "Lưu không thành công", "Lỗi");
            }
        }

        public override async void OnNavigatedTo(NavigationContext navigationContext)
        {
            _viewModelStore.CurrentViewModel = this;
            Id = navigationContext.Parameters["harddiskId"].ToString();
            var p = await _unitOfWork.Pcharddisks.GetById(Id);
            Name = p.Name;
            AvatarPath = p.AvatarPath;
            Company = p.Company;
            Price = p.Price;
            Connect = p.Connect;
            Type = p.Type;
            DetailPath = p.DetailPath;
            Discount = p.Discount;
            ImagePath = p.ImagePath;
            Remain = p.Remain;
            Series = p.Series;
            Storage = p.Storage;
            Unit = p.Unit;
        }
    }
}
