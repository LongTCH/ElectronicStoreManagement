using ESM.Core.ShareServices;
using ESM.Modules.DataAccess.Infrastructure;
using ESM.Modules.DataAccess.Models;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using System.Threading.Tasks;
using System.Windows;

namespace ESM.Modules.Import.ViewModels
{
    public class HardDiskInputViewModel : BaseProductInputViewModel<Pcharddisk>
    {
        public HardDiskInputViewModel(IUnitOfWork unitOfWork, IOpenDialogService openDialogService, IModalService modalService) : base(unitOfWork, openDialogService, modalService)
        {

        }
        public string Header => "Hard Disk";
        
        protected override async Task saveCommand()
        {
            if (SelectedProduct.Company == null || SelectedProduct.Unit == null ||
                SelectedProduct.Name == null || SelectedProduct.Storage == null || SelectedProduct.Connect == null || SelectedProduct.Type == null
                || SelectedProduct.Price == 0)
            {
                _modalService.ShowModal(ModalType.Error, "Nhập tất cả thông tin cần thiết", "Cảnh báo");
                return;
            }
            MetroWindow metroWindow = (MetroWindow)Application.Current.MainWindow;
            if (metroWindow.ShowModalMessageExternal("Thông báo", "Bạn có chắc chắn lưu?", MessageDialogStyle.AffirmativeAndNegative) == MessageDialogResult.Affirmative)
            {
                bool res;
                if (await _unitOfWork.Pcharddisks.IsIdExist(SelectedProduct.Id))
                {
                    res = (bool)await _unitOfWork.Pcharddisks.Update(SelectedProduct);
                    if (res)
                    {
                        _modalService.ShowModal(ModalType.Information, "Cập nhật thành công", "Thông báo");
                        
                    }
                    else _modalService.ShowModal(ModalType.Error, "Có lỗi xảy ra", "Thông báo");
                }
                else
                {
                    res = (bool)await _unitOfWork.Pcharddisks.Add(SelectedProduct);
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
            ProductList = new(await _unitOfWork.Pcharddisks.GetAll());
        }
    }
}
