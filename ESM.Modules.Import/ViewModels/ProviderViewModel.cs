using ESM.Core;
using ESM.Core.ShareServices;
using ESM.Modules.DataAccess.Infrastructure;
using ESM.Modules.DataAccess.Models;
using MahApps.Metro.Controls.Dialogs;
using MahApps.Metro.Controls;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using ESM.Core.ShareStores;
using ESM.Modules.Import.Utilities;

namespace ESM.Modules.Import.ViewModels
{
    [RegionMemberLifetime(KeepAlive = true)]
    public class ProviderViewModel : BindableBase, INavigationAware, IParentViewModel, ISearchBar
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IModalService _modalService;
        private readonly IRegionManager _regionManager;
        private readonly ViewModelStore _viewModelStore;
        public ProviderViewModel(IUnitOfWork unitOfWork, IModalService modalService, IRegionManager regionManager, ViewModelStore viewModelStore)
        {
            _unitOfWork = unitOfWork;
            _modalService = modalService;
            _regionManager = regionManager;
            _viewModelStore = viewModelStore;
            AddCommand = new(addCommand);
            CancelCommand = new(clearCommand);
            SaveCommand = new(async () => await viewModelStore.CurrentViewModel.save());
            DeleteCommand = new(async () => await deleteCommand());
            EditCommand = new(editCommand);
            GetAllCommand = new(getProviderList);
            SearchCommand = new(searchCommand);
        }
        private Provider selectedProvider;
        public Provider SelectedProvider
        {
            get => selectedProvider;
            set => SetProperty(ref selectedProvider, value);
        }
        private ObservableCollection<Provider> providers;
        public ObservableCollection<Provider> Providers
        {
            get => providers;
            set => SetProperty(ref providers, value);
        }
        private bool isEditMode;
        public bool IsEditMode
        {
            get => isEditMode;
            set => SetProperty(ref isEditMode, value);
        }
        public DelegateCommand AddCommand { get; }
        public DelegateCommand EditCommand { get; }
        public DelegateCommand SaveCommand { get; }
        public DelegateCommand CancelCommand { get; }
        public DelegateCommand DeleteCommand { get; }

        public DelegateCommand GetAllCommand { get; }

        public DelegateCommand<string> SearchCommand { get; }

        private void editCommand()
        {
            if (SelectedProvider == null)
            {
                _modalService.ShowModal(ModalType.Error, "Bạn chưa chọn nhà cung cấp", "Thông báo");
                return;
            }
            IsEditMode = true;
            _regionManager.RequestNavigate(RegionNames.InnerProviderManageRegion, ViewNames.ProviderEdit, new NavigationParameters(){
                {"providerId", SelectedProvider.Id }
            });
        }
        private void addCommand()
        {
            IsEditMode = true;
            _regionManager.RequestNavigate(RegionNames.InnerProviderManageRegion, ViewNames.ProviderAdd);
        }
        private void clearCommand()
        {
            MetroWindow metroWindow = (MetroWindow)Application.Current.MainWindow;
            if (metroWindow.ShowModalMessageExternal("Cảnh báo", "Bạn có chắc chắn hủy bỏ thông tin?", MessageDialogStyle.AffirmativeAndNegative) == MessageDialogResult.Affirmative)
            {
                SelectedProvider = null;
                IsEditMode = false;
                getProviderList();
            }
        }
        private async void searchCommand(string keyword)
        {
            Providers = new(await _unitOfWork.Providers.SearchProviders(keyword));
        }
        private async void getProviderList()
        {
            Providers = new(await _unitOfWork.Providers.GetAll());
        }
        private async Task deleteCommand()
        {
            if (SelectedProvider is null) return;
            MetroWindow metroWindow = (MetroWindow)Application.Current.MainWindow;
            if (metroWindow.ShowModalMessageExternal("Cảnh báo", "Bạn có chắc chắn xóa?", MessageDialogStyle.AffirmativeAndNegative) == MessageDialogResult.Affirmative)
            {
                await _unitOfWork.Providers.Delete(SelectedProvider.Id + "");
                _modalService.ShowModal(ModalType.Information, "Đã xóa", "Thông báo");
                getProviderList();
            }
        }
        public async void OnNavigatedTo(NavigationContext navigationContext)
        {
            Providers = new();
            var list = await _unitOfWork.Providers.GetAll();
            foreach (var item in list) Providers.Add(item);
            _viewModelStore.ParentViewModal = this;
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
            getProviderList();
        }
    }
}
