using ESM.Core.ShareServices;
using ESM.Modules.DataAccess.Infrastructure;
using ESM.Modules.DataAccess.Models;
using MahApps.Metro.Controls.Dialogs;
using MahApps.Metro.Controls;
using System.Threading.Tasks;
using System.Windows;
using Prism.Regions;
using ESM.Core;
using ESM.Modules.Import.Utilities;
using ESM.Core.ShareStores;

namespace ESM.Modules.Import.ViewModels
{
    public class VGAInputViewModel : BaseProductInputViewModel<Vga>
    {
        public VGAInputViewModel(IUnitOfWork unitOfWork, IRegionManager regionManager, IOpenDialogService openDialogService, IModalService modalService, ViewModelStore viewModelStore) : base(unitOfWork, regionManager, openDialogService, modalService, viewModelStore)
        {
        }

        public string Header => "VGA";

        protected override async void GetProductList()
        {
            ProductList = new(await _unitOfWork.Vgas.GetAll());
        }
        protected override async Task searchCommand(string keyword)
        {
            ProductList = new(await _unitOfWork.Vgas.SearchProduct(keyword));
        }

        protected override void addCommand()
        {
            IsEditMode = true;
            _regionManager.RequestNavigate(RegionNames.InnerVGAManageRegion, ViewNames.VGAAdd);
        }

        protected override void editCommand()
        {
            if (SelectedProduct != null)
            {
                IsEditMode = true;
                _regionManager.RequestNavigate(RegionNames.InnerVGAManageRegion, ViewNames.VGAEdit, new NavigationParameters()
                {
                    {"vgaId", SelectedProduct.Id }
                });
            }
            else _modalService.ShowModal(ModalType.Error, "Bạn chưa chọn sản phẩm", "Thông báo");
        }
    }
}
