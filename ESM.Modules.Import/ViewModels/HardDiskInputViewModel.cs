using ESM.Core;
using ESM.Core.ShareServices;
using ESM.Modules.DataAccess.Infrastructure;
using ESM.Modules.DataAccess.Models;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using Prism.Regions;
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
        
        protected override async Task saveCommand(Pcharddisk product)
        {
            if (product.Company == null || product.Unit == null ||
                product.Name == null || product.Storage == null || product.Connect == null || product.Type == null
                || product.Price == 0)
            {
                _modalService.ShowModal(ModalType.Error, "Nhập tất cả thông tin cần thiết", "Cảnh báo");
                return;
            }
            MetroWindow metroWindow = (MetroWindow)Application.Current.MainWindow;
            if (metroWindow.ShowModalMessageExternal("Thông báo", "Bạn có chắc chắn lưu?", MessageDialogStyle.AffirmativeAndNegative) == MessageDialogResult.Affirmative)
            {
                bool res;
                if (product.InMemory)
                {
                    res = (bool)await _unitOfWork.Pcharddisks.Update(product);
                    if (res)
                    {
                        _modalService.ShowModal(ModalType.Information, "Cập nhật thành công", "Thông báo");
                        product.InMemory = true;
                    }
                    else _modalService.ShowModal(ModalType.Error, "Có lỗi xảy ra", "Thông báo");
                }
                else
                {
                    res = (bool)await _unitOfWork.Pcharddisks.Add(product);
                    if (res)
                    {
                        _modalService.ShowModal(ModalType.Information, "Đã lưu", "Thông báo");
                        product.IdExist = true;
                        product.InMemory = true;
                    }
                    else _modalService.ShowModal(ModalType.Error, "Lưu không thành công", "Lỗi");
                }
                if (res)
                {
                    ProductList.Refresh();
                }
            }
        }
       
        public override async void OnNavigatedTo(NavigationContext navigationContext)
        {
            ProductList = new(await _unitOfWork.Pcharddisks.GetAll());
        }
    }
}
