using ESM.Core.ShareServices;
using ESM.Modules.DataAccess.Infrastructure;
using ESM.Modules.DataAccess.Models;
using System.Threading.Tasks;
using Prism.Regions;
using ESM.Core;
using ESM.Core.ShareStores;
using ESM.Modules.Import.Utilities;

namespace ESM.Modules.Import.ViewModels
{
    public class LaptopInputViewModel : BaseProductInputViewModel<Laptop>
    {
        public LaptopInputViewModel(IUnitOfWork unitOfWork, IRegionManager regionManager, IOpenDialogService openDialogService, IModalService modalService, ViewModelStore viewModelStore) : base(unitOfWork, regionManager, openDialogService, modalService, viewModelStore)
        {
        }

        public string Header => "Laptop";
        
        protected override async void GetProductList()
        {
            ProductList = new(await _unitOfWork.Laptops.GetAll());
        }

        protected override async Task searchCommand(string keyword)
        {
            ProductList = new(await _unitOfWork.Laptops.SearchProduct(keyword));
        }


        protected override void addCommand()
        {
            IsEditMode = true;
            _regionManager.RequestNavigate(RegionNames.InnerLaptopManageRegion, ViewNames.LaptopAdd);
        }

        protected override void editCommand()
        {
            if (SelectedProduct != null)
            {
                IsEditMode = true;
                _regionManager.RequestNavigate(RegionNames.InnerLaptopManageRegion, ViewNames.LaptopEdit, new NavigationParameters() {
                    {"laptopId", SelectedProduct.Id } });
            }
            else _modalService.ShowModal(ModalType.Error, "Bạn chưa chọn sản phẩm", "Thông báo");
        }
    }
}
