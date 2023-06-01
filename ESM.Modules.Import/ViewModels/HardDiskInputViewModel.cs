using ESM.Core;
using ESM.Core.ShareServices;
using ESM.Core.ShareStores;
using ESM.Modules.DataAccess.Infrastructure;
using ESM.Modules.DataAccess.Models;
using ESM.Modules.Import.Utilities;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using Prism.Regions;
using System.Threading.Tasks;
using System.Windows;

namespace ESM.Modules.Import.ViewModels
{
    public class HardDiskInputViewModel : BaseProductInputViewModel<Pcharddisk>
    {
        public HardDiskInputViewModel(IUnitOfWork unitOfWork, IRegionManager regionManager, IOpenDialogService openDialogService, IModalService modalService, ViewModelStore viewModelStore) : base(unitOfWork, regionManager, openDialogService, modalService, viewModelStore)
        {
        }

        public string Header => "Hard Disk";
       

        protected override async void GetProductList()
        {
            ProductList = new(await _unitOfWork.Pcharddisks.GetAll());
        }
        protected override async Task searchCommand(string keyword)
        {
            ProductList = new(await _unitOfWork.Pcharddisks.SearchProduct(keyword));
        }

        protected override void addCommand()
        {
            IsEditMode = true;
            _regionManager.RequestNavigate(RegionNames.InnerHardDiskManageRegion, ViewNames.HardDiskAdd);
        }

        protected override void editCommand()
        {
            if (SelectedProduct != null)
            {
                IsEditMode = true;
                _regionManager.RequestNavigate(RegionNames.InnerHardDiskManageRegion, ViewNames.HardDiskEdit, new NavigationParameters()
                {
                    {"harddiskId", SelectedProduct.Id }
                });
            }
            else _modalService.ShowModal(ModalType.Error, "Bạn chưa chọn sản phẩm", "Thông báo");
        }
    }
}
