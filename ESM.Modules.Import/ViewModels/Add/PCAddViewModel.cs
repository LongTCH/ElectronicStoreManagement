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

namespace ESM.Modules.Import.ViewModels.Add
{
    public class PCAddViewModel : CommonInputViewModal<Pc>
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
        public PCAddViewModel(IUnitOfWork unitOfWork, IOpenDialogService openDialogService, IModalService modalService, ViewModelStore viewModelStore) : base(unitOfWork, openDialogService, modalService, viewModelStore)
        {
        }

        public override async void OnNavigatedTo(NavigationContext navigationContext)
        {
            _viewModelStore.CurrentViewModel = this;
            Id = await _unitOfWork.Pcs.GetSuggestID();
        }

        public override async Task save()
        {
            if (Company == null || Unit == null || Name == null ||
                    Cpu == null || Ram == null)
            {
                _modalService.ShowModal(ModalType.Error, "Nhập tất cả thông tin cần thiết", "Cảnh báo");
                return;
            }
            MetroWindow metroWindow = (MetroWindow)Application.Current.MainWindow;
            if (metroWindow.ShowModalMessageExternal("Thông báo", "Bạn có chắc chắn lưu?", MessageDialogStyle.AffirmativeAndNegative) == MessageDialogResult.Affirmative)
            {
                Pc p = new()
                {
                    Id = Id,
                    AvatarPath = AvatarPath,
                    Company = Company,
                    Name = Name,
                    Cpu = Cpu,
                    Ram = Ram,
                    DetailPath = DetailPath,
                    Discount = Discount,
                    ImagePath = ImagePath,
                    Need = Need,
                    Price = Price,
                    Remain = Remain,
                    Series = Series,
                    Unit = Unit
                };
                bool res = (bool)await _unitOfWork.Pcs.Add(p);
                if (res)
                {
                    _modalService.ShowModal(ModalType.Information, "Đã lưu", "Thông báo");
                    _viewModelStore.ParentViewModal.OnChildViewNotify();
                }
                else _modalService.ShowModal(ModalType.Error, "Lưu không thành công", "Lỗi");
            }
        }
    }
}
