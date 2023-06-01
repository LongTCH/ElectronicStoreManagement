using ESM.Core.ShareServices;
using ESM.Modules.DataAccess;
using ESM.Modules.DataAccess.Infrastructure;
using MahApps.Metro.Controls.Dialogs;
using MahApps.Metro.Controls;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using ESM.Modules.DataAccess.Models;
using ESM.Core.ShareStores;

namespace ESM.Modules.Import.Utilities
{
    public abstract class BaseProductInputViewModel<T> : BindableBase, INavigationAware, IParentViewModel where T : ProductDTO, new()
    {
        protected readonly IUnitOfWork _unitOfWork;
        protected readonly IOpenDialogService _openDialogService;
        protected readonly IModalService _modalService;
        protected readonly ViewModelStore _viewModelStore;
        protected readonly IRegionManager _regionManager;

        public BaseProductInputViewModel(IUnitOfWork unitOfWork, IRegionManager regionManager,
            IOpenDialogService openDialogService, IModalService modalService, ViewModelStore viewModelStore)
        {
            _unitOfWork = unitOfWork;
            _openDialogService = openDialogService;
            _modalService = modalService;
            _viewModelStore = viewModelStore;
            _regionManager = regionManager;
            SaveCommand = new(async () => await viewModelStore.CurrentViewModel.save());
            EditCommand = new(editCommand);
            ClearCommand = new(clearCommand);
            AddCommand = new(addCommand);
            DeleteCommand = new(deleteCommand);
            SearchCommand = new(async (s) => await searchCommand(s));
            GetAllCommand = new(GetProductList);
        }
        private bool isEditMode;
        public bool IsEditMode
        {
            get => isEditMode;
            set => SetProperty(ref isEditMode, value);
        }
        private T selectedProduct;
        public T SelectedProduct
        {
            get => selectedProduct;
            set => SetProperty(ref selectedProduct, value);
        }
        private ObservableCollection<T> productList;
        public ObservableCollection<T> ProductList
        {
            get => productList;
            set => SetProperty(ref productList, value);
        }

        public DelegateCommand SaveCommand { get; }
        public DelegateCommand ClearCommand { get; }
        public DelegateCommand AddCommand { get; }
        public DelegateCommand EditCommand { get; }
        public DelegateCommand DeleteCommand { get; }
        public DelegateCommand GetAllCommand { get; }
        public DelegateCommand<string> SearchCommand { get; }
        protected abstract Task searchCommand(string keyword);
        protected abstract void editCommand();
        private void clearCommand()
        {
            MetroWindow metroWindow = (MetroWindow)Application.Current.MainWindow;
            if (metroWindow.ShowModalMessageExternal("Cảnh báo", "Bạn có chắc chắn hủy bỏ thông tin?", MessageDialogStyle.AffirmativeAndNegative) == MessageDialogResult.Affirmative)
            {
                SelectedProduct = null;
                IsEditMode = false;
                GetProductList();
            }

        }
        private async void deleteCommand()
        {
            if (SelectedProduct is null)
            {
                _modalService.ShowModal(ModalType.Error, "Bạn chưa chọn sản phẩm", "Thông báo"); return;
            }
            MetroWindow metroWindow = (MetroWindow)Application.Current.MainWindow;
            if (metroWindow.ShowModalMessageExternal("Cảnh báo", "Bạn có chắc chắn xóa?", MessageDialogStyle.AffirmativeAndNegative) == MessageDialogResult.Affirmative)
            {
                if (SelectedProduct is Laptop)
                    await _unitOfWork.Laptops.Delete(SelectedProduct.Id);
                else if (SelectedProduct is Monitor)
                    await _unitOfWork.Monitors.Delete(SelectedProduct.Id);
                else if (SelectedProduct is Pcharddisk)
                    await _unitOfWork.Pcharddisks.Delete(SelectedProduct.Id);
                else if (SelectedProduct is Pc)
                    await _unitOfWork.Pcs.Delete(SelectedProduct.Id);
                else if (SelectedProduct is Pccpu)
                    await _unitOfWork.Pccpus.Delete(SelectedProduct.Id);
                else if (SelectedProduct is Smartphone)
                    await _unitOfWork.Smartphones.Delete(SelectedProduct.Id);
                else if (SelectedProduct is Vga)
                    await _unitOfWork.Vgas.Delete(SelectedProduct.Id);
                _modalService.ShowModal(ModalType.Information, "Đã xóa", "Thông báo");
            }
        }
        protected abstract void GetProductList();
        protected abstract void addCommand();
        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            _viewModelStore.ParentViewModal = this;
            GetProductList();
            IsEditMode = false;
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {

        }

        public void OnChildViewNotify()
        {
            IsEditMode = false;
            GetProductList();
        }
    }
}
