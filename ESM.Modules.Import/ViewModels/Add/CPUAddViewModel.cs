using ESM.Core.ShareServices;
using ESM.Core.ShareStores;
using ESM.Modules.DataAccess.Infrastructure;
using ESM.Modules.DataAccess.Models;
using ESM.Modules.Import.Utilities;
using MahApps.Metro.Controls.Dialogs;
using MahApps.Metro.Controls;
using System.Threading.Tasks;
using System.Windows;
using Prism.Regions;

namespace ESM.Modules.Import.ViewModels.Add
{
    public class CPUAddViewModel : CommonInputViewModal<Pccpu>
    {
        public CPUAddViewModel(IUnitOfWork unitOfWork, IOpenDialogService openDialogService, IModalService modalService, ViewModelStore viewModelStore) : base(unitOfWork, openDialogService, modalService, viewModelStore)
        {
        }
        private string socket;
        public string Socket
        {
            get => socket;
            set => SetProperty(ref socket, value);
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
        public override async Task save()
        {
            if (Company == null || Unit == null || Name == null || Socket == null)
            {
                _modalService.ShowModal(ModalType.Error, "Nhập tất cả thông tin cần thiết", "Cảnh báo");
                return;
            }
            MetroWindow metroWindow = (MetroWindow)Application.Current.MainWindow;
            if (metroWindow.ShowModalMessageExternal("Thông báo", "Bạn có chắc chắn lưu?", MessageDialogStyle.AffirmativeAndNegative) == MessageDialogResult.Affirmative)
            {
                Pccpu p = new()
                {
                    Id = Id,
                    AvatarPath = AvatarPath,
                    Company = Company,
                    DetailPath = DetailPath,
                    Discount = Discount,
                    ImagePath = ImagePath,
                    Name = Name,
                    Need = Need,
                    Price = Price,
                    Remain = Remain,
                    Series = Series,
                    Socket = Socket,
                    Unit = Unit,
                };
                bool res = (bool)await _unitOfWork.Pccpus.Add(p);
                if (res)
                {
                    _modalService.ShowModal(ModalType.Information, "Đã lưu", "Thông báo");
                    _viewModelStore.ParentViewModal.OnChildViewNotify();
                }
                else _modalService.ShowModal(ModalType.Error, "Lưu không thành công", "Lỗi");
            }
        }

        public override async void OnNavigatedTo(NavigationContext navigationContext)
        {
            _viewModelStore.CurrentViewModel = this;
            Id = await _unitOfWork.Pccpus.GetSuggestID();
        }
    }
}

