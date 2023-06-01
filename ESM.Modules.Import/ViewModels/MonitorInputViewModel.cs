using ESM.Core.ShareServices;
using ESM.Modules.DataAccess.Infrastructure;
using ESM.Modules.DataAccess.Models;
using System.Threading.Tasks;
using Prism.Regions;
using ESM.Core;
using ESM.Modules.Import.Utilities;
using ESM.Core.ShareStores;

namespace ESM.Modules.Import.ViewModels
{
    public class MonitorInputViewModel : BaseProductInputViewModel<Monitor>
    {
        public MonitorInputViewModel(IUnitOfWork unitOfWork, IRegionManager regionManager, IOpenDialogService openDialogService, IModalService modalService, ViewModelStore viewModelStore) : base(unitOfWork, regionManager, openDialogService, modalService, viewModelStore)
        {
        }

        public string Header => "Monitor";

        protected override async void GetProductList()
        {
            ProductList = new(await _unitOfWork.Monitors.GetAll());
        }
        protected override async Task searchCommand(string keyword)
        {
            ProductList = new(await _unitOfWork.Monitors.SearchProduct(keyword));
        }

        protected override void addCommand()
        {
            IsEditMode = true;
            _regionManager.RequestNavigate(RegionNames.InnerMonitorManageRegion, ViewNames.MonitorAdd);
        }

        protected override void editCommand()
        {
            if (SelectedProduct != null)
            {
                IsEditMode = true;
                _regionManager.RequestNavigate(RegionNames.InnerMonitorManageRegion, ViewNames.MonitorEdit, new NavigationParameters()
                {
                    { "monitorId", SelectedProduct.Id } 
                });
            }
            else _modalService.ShowModal(ModalType.Error, "Bạn chưa chọn sản phẩm", "Thông báo");
        }
    }
}
