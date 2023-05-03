using ESM.Core.ShareServices;
using ESM.Modules.DataAccess.Infrastructure;
using ESM.Modules.DataAccess.Models;
using MahApps.Metro.Controls.Dialogs;
using MahApps.Metro.Controls;
using System.Windows;
using Prism.Regions;
using System.Threading.Tasks;
using ESM.Core;

namespace ESM.Modules.Import.ViewModels
{
    public class CPUInputViewModel : BaseProductInputViewModel<Pccpu>
    {
        public CPUInputViewModel(IUnitOfWork unitOfWork, IOpenDialogService openDialogService, IModalService modalService) : base(unitOfWork, openDialogService, modalService)
        {
        }
        public string Header => "CPU";
        protected override async Task saveCommand(Pccpu product)
        {
            if (product.Company == null || product.Unit == null || product.Name == null || product.Socket == null)
            {
                _modalService.ShowModal(ModalType.Error, "Nhập tất cả thông tin cần thiết", "Cảnh báo");
                return;
            }
            MetroWindow metroWindow = (MetroWindow)Application.Current.MainWindow;
            if (metroWindow.ShowModalMessageExternal("Thông báo", "Bạn có chắc chắn lưu?", MessageDialogStyle.AffirmativeAndNegative) == MessageDialogResult.Affirmative)
            {
                bool res;
                if (product.IdExist)
                {
                    res = (bool)await _unitOfWork.Pccpus.Update(product);
                    if (res)
                    {
                        _modalService.ShowModal(ModalType.Information, "Cập nhật thành công", "Thông báo");
                        product.InMemory = true;
                    }
                    else _modalService.ShowModal(ModalType.Error, "Có lỗi xảy ra", "Thông báo");
                }
                else
                {
                    res = (bool)await _unitOfWork.Pccpus.Add(product);
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
            ProductList = new(await _unitOfWork.Pccpus.GetAll());
        }
    }
}