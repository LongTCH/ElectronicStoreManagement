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
    public class PCInputViewModel : BaseProductInputViewModel<Pc>
    {
        public PCInputViewModel(IUnitOfWork unitOfWork, IRegionManager regionManager, IOpenDialogService openDialogService, IModalService modalService, ViewModelStore viewModelStore) : base(unitOfWork, regionManager, openDialogService, modalService, viewModelStore)
        {
        }

        public string Header => "PC";

        protected override async void GetProductList()
        {
            ProductList = new(await _unitOfWork.Pcs.GetAll());
        }
        protected override async Task searchCommand(string keyword)
        {
            ProductList = new(await _unitOfWork.Pcs.SearchProduct(keyword));
        }

        protected override void addCommand()
        {
            IsEditMode = true;
            _regionManager.RequestNavigate(RegionNames.InnerPCManageRegion, ViewNames.PCAdd);
        }

        protected override void editCommand()
        {
            if (SelectedProduct != null)
            {
                IsEditMode = true;
                _regionManager.RequestNavigate(RegionNames.InnerPCManageRegion, ViewNames.PCEdit, new NavigationParameters()
                {
                    {"pcId", SelectedProduct.Id }
                });
            }
            else _modalService.ShowModal(ModalType.Error, "Bạn chưa chọn sản phẩm", "Thông báo");
        }
    }
}
