using ESM.Core.ShareServices;
using ESM.Modules.DataAccess.Infrastructure;
using ESM.Modules.DataAccess.Models;
using System.Threading.Tasks;
using Prism.Regions;
using ESM.Core.ShareStores;
using ESM.Core;
using ESM.Modules.Import.Utilities;

namespace ESM.Modules.Import.ViewModels
{
    public class CPUInputViewModel : BaseProductInputViewModel<Pccpu>
    {
        public CPUInputViewModel(IUnitOfWork unitOfWork, IRegionManager regionManager, IOpenDialogService openDialogService, IModalService modalService, ViewModelStore viewModelStore) : base(unitOfWork, regionManager, openDialogService, modalService, viewModelStore)
        {
        }

        public string Header => "CPU";
        protected override async void GetProductList()
        {
            ProductList = new(await _unitOfWork.Pccpus.GetAll());
        }

        protected override async Task searchCommand(string keyword)
        {
            ProductList = new(await _unitOfWork.Pccpus.SearchProduct(keyword));
        }

        protected override void addCommand()
        {
            IsEditMode = true;
            _regionManager.RequestNavigate(RegionNames.InnerCPUManageRegion, ViewNames.CPUAdd);
        }

        protected override void editCommand()
        {
            if (SelectedProduct != null)
            {
                IsEditMode = true;
                _regionManager.RequestNavigate(RegionNames.InnerCPUManageRegion, ViewNames.CPUEdit, new NavigationParameters()
                {
                    {"cpuId", SelectedProduct.Id }
                });
            }
            else _modalService.ShowModal(ModalType.Error, "Bạn chưa chọn sản phẩm", "Thông báo");
        }
    }
}