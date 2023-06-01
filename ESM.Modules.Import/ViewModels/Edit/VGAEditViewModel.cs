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
    public class VGAEditViewModel : CommonInputViewModal<Vga>
    {
        private string chip;
        public string Chip
        {
            get => chip; 
            set => SetProperty(ref chip, value);
        }
        private string chipSet;
        public string Chipset
        {
            get => chipSet;
            set => SetProperty(ref chipSet, value);
        }
        private string vram;
        public string Vram
        {
            get => vram;
            set => SetProperty(ref vram, value);
        }
        private string gen;
        public string Gen
        {
            get => gen; 
            set => SetProperty(ref gen, value);
        }
        private string series;
        public string Series
        {
            get => series;
            set => SetProperty(ref series, value);
        }
        public VGAEditViewModel(IUnitOfWork unitOfWork, IOpenDialogService openDialogService, IModalService modalService, ViewModelStore viewModelStore) : base(unitOfWork, openDialogService, modalService, viewModelStore)
        {
        }

        public override async void OnNavigatedTo(NavigationContext navigationContext)
        {
            _viewModelStore.CurrentViewModel = this;
            Id = navigationContext.Parameters["vgaId"].ToString();
            var p = await _unitOfWork.Vgas.GetById(Id);
            Name = p.Name;
            AvatarPath = p.AvatarPath;
            Chip = p.Chip;
            Chipset = p.Chipset;
            Company = p.Company;
            DetailPath = p.DetailPath;
            Discount = p.Discount;
            Gen = p.Gen;
            ImagePath = p.ImagePath;
            Price = p.Price;
            Remain = p.Remain;
            Series = p.Series;
            Unit = p.Unit;
            Vram = p.Vram;
        }

        public override async Task save()
        {
            if (Company == null || Unit == null || Name == null ||
                   Chip == null || Chipset == null || Vram == null || Gen == null)
            {
                _modalService.ShowModal(ModalType.Error, "Nhập tất cả thông tin cần thiết", "Cảnh báo");
                return;
            }
            MetroWindow metroWindow = (MetroWindow)Application.Current.MainWindow;
            if (metroWindow.ShowModalMessageExternal("Thông báo", "Bạn có chắc chắn lưu?", MessageDialogStyle.AffirmativeAndNegative) == MessageDialogResult.Affirmative)
            {
                Vga p = new()
                {
                    Id = Id,
                    Name = Name,
                    AvatarPath = AvatarPath,
                    Chip = Chip,
                    Chipset = Chipset,
                    Company = Company,
                    DetailPath = DetailPath,
                    Discount = Discount,
                    Gen = Gen,
                    ImagePath = ImagePath,
                    Price = Price,
                    Remain = Remain,
                    Series = Series,
                    Unit = Unit,
                    Vram = Vram,
                };
                bool res = (bool)await _unitOfWork.Vgas.Update(p);
                if (res)
                {
                    _modalService.ShowModal(ModalType.Information, "Cập nhật thành công", "Thông báo");
                    _viewModelStore.ParentViewModal.OnChildViewNotify();
                }
                else _modalService.ShowModal(ModalType.Error, "Lưu không thành công", "Lỗi");
            }
        }
    }
}
