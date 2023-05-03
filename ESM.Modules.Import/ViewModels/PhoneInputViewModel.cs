using ESM.Core.ShareServices;
using ESM.Modules.DataAccess.Infrastructure;
using ESM.Modules.DataAccess.Models;
using MahApps.Metro.Controls.Dialogs;
using MahApps.Metro.Controls;
using System.Threading.Tasks;
using System.Windows;
using ESM.Core;
using Prism.Regions;

namespace ESM.Modules.Import.ViewModels
{
    public class PhoneInputViewModel : BaseProductInputViewModel<Smartphone>
    {
        public PhoneInputViewModel(IUnitOfWork unitOfWork, IOpenDialogService openDialogService, IModalService modalService) : base(unitOfWork, openDialogService, modalService)
        {
        }
        public string Header => "Phone";
        
        protected override async Task saveCommand(Smartphone product)
        {
            if (product.Company == null || product.Unit == null || product.Name == null ||
                   product.Cpu == null || product.Ram == null || product.Storage == null)
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
                    res = (bool)await _unitOfWork.Smartphones.Update(product);
                    if (res)
                    {
                        _modalService.ShowModal(ModalType.Information, "Cập nhật thành công", "Thông báo");
                        product.InMemory = true;
                    }
                    else _modalService.ShowModal(ModalType.Error, "Có lỗi xảy ra", "Thông báo");
                }
                else
                {
                    res = (bool)await _unitOfWork.Smartphones.Add(product);
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
            ProductList = new(await _unitOfWork.Smartphones.GetAll());
        }
    }
}
