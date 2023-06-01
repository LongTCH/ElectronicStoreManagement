using ESM.Core.ShareServices;
using ESM.Modules.DataAccess.Infrastructure;
using ESM.Modules.DataAccess.Models;
using Prism.Mvvm;
using System.Threading.Tasks;
using System.Collections.Generic;
using ESM.Modules.Import.Utilities;
using System.Collections.ObjectModel;
using System.Linq;
using Prism.Commands;
using MahApps.Metro.Controls.Dialogs;
using MahApps.Metro.Controls;
using System.Windows;
using Prism.Regions;
using ESM.Core.ShareStores;
using ESM.Core;

namespace ESM.Modules.Import.ViewModels
{
    public class DiscountInputViewModel : BindableBase, INavigationAware, IParentViewModel, ISearchBar
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IModalService _modalService;
        private readonly IRegionManager _regionManager;
        private readonly ViewModelStore _viewModelStore;
        public DiscountInputViewModel(IUnitOfWork unitOfWork, IModalService modalService, IRegionManager regionManager, ViewModelStore viewModelStore)
        {
            _unitOfWork = unitOfWork;
            _modalService = modalService;
            _regionManager = regionManager;
            _viewModelStore = viewModelStore;
            DiscountDetail = new();

            AddCommand = new(addToDiscountListCommand);
            EditCommand = new(editCommand);
            CancelCommand = new(clearCommand);
            DeleteCommand = new(async () => await deleteCommand());
            FindCommand = new(findCommand);
            SaveCommand = new(async () => await _viewModelStore.CurrentViewModel.save());
            SearchCommand = new(async (s) => await searchCommand(s));
            GetAllCommand = new(async () => await getAll());
        }
        public void OnChildViewNotify()
        {
            IsEditMode = false;
            reset();
        }
        public DelegateCommand AddCommand { get; }
        public DelegateCommand EditCommand { get; }
        public DelegateCommand CancelCommand { get; }
        public DelegateCommand SaveCommand { get; }
        public DelegateCommand DeleteCommand { get; }
        public DelegateCommand FindCommand { get; }
        public DelegateCommand GetAllCommand { get; }
        public DelegateCommand<string> SearchCommand { get; }
        private Discount selectedDiscount;
        public Discount SelectedDiscount
        {
            get => selectedDiscount;
            set => SetProperty(ref selectedDiscount, value);
        }
        private bool isEditMode;
        public bool IsEditMode
        {
            get => isEditMode;
            set => SetProperty(ref isEditMode, value);
        }

        public ICollection<SelectableViewModel> productList;
        public ICollection<SelectableViewModel> ProductList
        {
            get => productList;
            set => SetProperty(ref productList, value);
        }
        private ObservableCollection<SelectableViewModel> discountDetail;
        public ObservableCollection<SelectableViewModel> DiscountDetail
        {
            get => discountDetail;
            set => SetProperty(ref discountDetail, value);
        }
        private ObservableCollection<Discount> discountList;
        public ObservableCollection<Discount> DiscountList
        {
            get => discountList;
            set => SetProperty(ref discountList, value);
        }
        private async Task searchCommand(string keyword)
        {
            DiscountList = new(await _unitOfWork.Discounts.SearchDiscount(keyword));
        }

        private void editCommand()
        {
            if (SelectedDiscount != null)
            {
                IsEditMode = true;
                _regionManager.RequestNavigate(RegionNames.InnerDiscountManageRegion, ViewNames.DiscountEdit, new NavigationParameters()
                {
                    {"discountId", SelectedDiscount.Id }
                });
            }
            else _modalService.ShowModal(ModalType.Error, "Bạn chưa chọn Khuyến mãi", "Thông báo");
        }
        private async Task deleteCommand()
        {
            if (SelectedDiscount == null) return;
            bool res = false;
            MetroWindow metroWindow = (MetroWindow)Application.Current.MainWindow;
            if (metroWindow.ShowModalMessageExternal("Cảnh báo", "Bạn có chắc chắn xóa?", MessageDialogStyle.AffirmativeAndNegative) == MessageDialogResult.Affirmative)
            {
                res = (bool)await _unitOfWork.Discounts.Delete(SelectedDiscount.Id + "");
            }
            if (res)
            {
                _modalService.ShowModal(ModalType.Information, "Đã xóa", "Thông báo");
                reset();
            }
            else _modalService.ShowModal(ModalType.Information, "Xóa không thành công", "Thông báo");
        }


        private void addToDiscountListCommand()
        {
            IsEditMode = true;
            _regionManager.RequestNavigate(RegionNames.InnerDiscountManageRegion, ViewNames.DiscountAdd);
        }
        private void clearCommand()
        {
            MetroWindow metroWindow = (MetroWindow)Application.Current.MainWindow;
            if (metroWindow.ShowModalMessageExternal("Cảnh báo", "Bạn có chắc chắn hủy bỏ thông tin?", MessageDialogStyle.AffirmativeAndNegative) == MessageDialogResult.Affirmative)
            {
                IsEditMode = false;
                reset();
            }
        }
        private async void findCommand()
        {
            if (SelectedDiscount == null) return;
            DiscountDetail?.Clear();
            var list = await _unitOfWork.Discounts.GetListProduct(SelectedDiscount);
            foreach (var item in list)
            {
                DiscountDetail.Add(new()
                {
                    Id = item.Id,
                    Discount = item.Discount,
                    IsSelected = true,
                    Name = item.Name,
                    Price = item.Price,
                    Unit = item.Unit,
                    Remain = item.Remain,
                });
            }
        }

        private async void reset()
        {
            SelectedDiscount = null;
            DiscountDetail.Clear();
            IsEditMode = false;
            await getAll();
        }
        private async Task getAll()
        {
            DiscountList = new(await _unitOfWork.Discounts.GetAll());
        }
        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {

        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            _viewModelStore.ParentViewModal = this;
            reset();
        }
    }
}
