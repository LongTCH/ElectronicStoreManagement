using ESM.Core.ShareServices;
using ESM.Modules.DataAccess.Infrastructure;
using ESM.Modules.DataAccess.Models;
using MahApps.Metro.Controls.Dialogs;
using MahApps.Metro.Controls;
using System.Threading.Tasks;
using System.Windows;
using Prism.Regions;
using ESM.Core;

namespace ESM.Modules.Import.ViewModels
{
    public class VGAInputViewModel : BaseProductInputViewModel<Vga>
    {
        public VGAInputViewModel(IUnitOfWork unitOfWork, IOpenDialogService openDialogService, IModalService modalService) : base(unitOfWork, openDialogService, modalService)
        {
        }
        public string Header => "VGA";
       
        protected override async Task saveCommand()
        {
            if (SelectedProduct.Company == null || SelectedProduct.Unit == null || SelectedProduct.Name == null ||
                   SelectedProduct.Chip == null || SelectedProduct.Chipset == null || SelectedProduct.Vram == null || SelectedProduct.Gen == null)
            {
                _modalService.ShowModal(ModalType.Error, "Nhập tất cả thông tin cần thiết", "Cảnh báo");
                return;
            }
            MetroWindow metroWindow = (MetroWindow)Application.Current.MainWindow;
            if (metroWindow.ShowModalMessageExternal("Thông báo", "Bạn có chắc chắn lưu?", MessageDialogStyle.AffirmativeAndNegative) == MessageDialogResult.Affirmative)
            {
                bool res;
                if (await _unitOfWork.Vgas.IsIdExist(SelectedProduct.Id))
                {
                    res = (bool)await _unitOfWork.Vgas.Update(SelectedProduct);
                    if (res)
                    {
                        _modalService.ShowModal(ModalType.Information, "Cập nhật thành công", "Thông báo");
                        
                    }
                    else _modalService.ShowModal(ModalType.Error, "Có lỗi xảy ra", "Thông báo");
                }
                else
                {
                    res = (bool)await _unitOfWork.Vgas.Add(SelectedProduct);
                    if (res)
                    {
                        _modalService.ShowModal(ModalType.Information, "Đã lưu", "Thông báo");
                        SelectedProduct.IdExist = true;
                        
                    }
                    else _modalService.ShowModal(ModalType.Error, "Lưu không thành công", "Lỗi");
                }
                if (res)
                {
                    GetProductList();
                    IsEditMode = false;
                }
            }
        }
      
        protected override async void GetProductList()
        {
            ProductList = new(await _unitOfWork.Vgas.GetAll());
        }
    }
}
