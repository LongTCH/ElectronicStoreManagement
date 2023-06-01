using ESM.Core.ShareServices;
using ESM.Modules.DataAccess.Infrastructure;
using ESM.Modules.DataAccess.Models;
using MahApps.Metro.Controls.Dialogs;
using MahApps.Metro.Controls;
using System.Threading.Tasks;
using System.Windows;
using ESM.Core;
using Prism.Regions;
using ESM.Modules.Import.Utilities;
using ESM.Core.ShareStores;

namespace ESM.Modules.Import.ViewModels
{
    public class PhoneInputViewModel : BaseProductInputViewModel<Smartphone>
    {
        public PhoneInputViewModel(IUnitOfWork unitOfWork, IRegionManager regionManager, IOpenDialogService openDialogService, IModalService modalService, ViewModelStore viewModelStore) : base(unitOfWork, regionManager, openDialogService, modalService, viewModelStore)
        {
        }

        public string Header => "Phone";
        
        protected override async void GetProductList()
        {
            ProductList = new(await _unitOfWork.Smartphones.GetAll());
        }
        protected override async Task searchCommand(string keyword)
        {
            ProductList = new(await _unitOfWork.Smartphones.SearchProduct(keyword));
        }

        protected override void addCommand()
        {
            IsEditMode = true;
            _regionManager.RequestNavigate(RegionNames.InnerSmartphoneManageRegion, ViewNames.SmartphoneAdd);
        }

        protected override void editCommand()
        {
            if (SelectedProduct != null)
            {
                IsEditMode = true;
                _regionManager.RequestNavigate(RegionNames.InnerSmartphoneManageRegion, ViewNames.SmartphoneEdit, new NavigationParameters()
                {
                    {"smartphoneId", SelectedProduct.Id }
                });
            }
            else _modalService.ShowModal(ModalType.Error, "Bạn chưa chọn sản phẩm", "Thông báo");
        }
    }
}
