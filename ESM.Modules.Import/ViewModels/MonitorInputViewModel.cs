using ESM.Core.ShareServices;
using ESM.Modules.DataAccess;
using ESM.Modules.DataAccess.Infrastructure;
using ESM.Modules.DataAccess.Models;
using MahApps.Metro.Controls.Dialogs;
using MahApps.Metro.Controls;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Prism.Regions;

namespace ESM.Modules.Import.ViewModels
{
    public class MonitorInputViewModel : BaseProductInputViewModel<Monitor>
    {
        public string Header => "Monitor";
        public MonitorInputViewModel(IUnitOfWork unitOfWork, IOpenDialogService openDialogService, IModalService modalService) : base(unitOfWork, openDialogService, modalService)
        {

        }
        private string size;
        public string Size
        {
            get => size?.Trim();
            set => SetProperty(ref size, value);
        }

        private string panel;
        public string Panel
        {
            get => panel?.Trim();
            set => SetProperty(ref panel, value);
        }

        private string refreshRate;
        public string RefreshRate
        {
            get => refreshRate?.Trim();
            set => SetProperty(ref refreshRate, value);
        }

        private string series;
        public string Series
        {
            get => series?.Trim();
            set => SetProperty(ref series, value);
        }

        private string need;
        public string Need
        {
            get => need?.Trim();
            set => SetProperty(ref need, value);
        }

        protected override async Task saveCommand()
        {
            if (Company == null || Unit == null ||
                Name == null || size == null || panel == null
                || Price == 0)
            {
                _modalService.ShowModal(ModalType.Error, "Nhập tất cả thông tin cần thiết", "Cảnh báo");
                return;
            }
            MetroWindow metroWindow = (MetroWindow)Application.Current.MainWindow;
            if (metroWindow.ShowModalMessageExternal("Thông báo", "Bạn có chắc chắn lưu?", MessageDialogStyle.AffirmativeAndNegative) == MessageDialogResult.Affirmative)
            {
                var p = GetMonitor(Id, Remain);
                if (Product.InMemory)
                {
                    var res = await _unitOfWork.Monitors.Update(p);
                    if ((bool)res)
                    {
                        _modalService.ShowModal(ModalType.Information, "Cập nhật thành công", "Thông báo");
                        // Clear
                        Empty();
                        var index = ProductList.IndexOf(Product);
                        ProductList.RemoveAt(index);
                        ProductList.Insert(index, Product);
                    }
                    else _modalService.ShowModal(ModalType.Error, "Có lỗi xảy ra", "Thông báo");
                }
                else
                {
                    var res = await _unitOfWork.Monitors.Add(p);
                    if ((bool)res)
                    {
                        _modalService.ShowModal(ModalType.Information, "Đã lưu", "Thông báo");
                        // Clear
                        Empty();
                        ProductList.Remove(ProductList.Last());
                        ProductList.Add(p);
                    }
                    else _modalService.ShowModal(ModalType.Error, "Lưu không thành công", "Lỗi");
                    await Task.CompletedTask;
                }
            }
        }

        protected override void addCommand()
        {
            Monitor pccpuDTO = GetMonitor(GetNewID(ProductType.MONITOR), 0);
            pccpuDTO.InMemory = false;
            ProductList.Add(pccpuDTO);
            findCommand(pccpuDTO);
            TurnOnEditCommand.Execute();
        }

        protected override async void deleteCommand()
        {
            MetroWindow metroWindow = (MetroWindow)Application.Current.MainWindow;
            if (metroWindow.ShowModalMessageExternal("Cảnh báo", "Bạn có chắc chắn xóa?", MessageDialogStyle.AffirmativeAndNegative) == MessageDialogResult.Affirmative)
            {
                if (Product.InMemory) await _unitOfWork.Monitors.Delete(Product.Id);
                ProductList.Remove(Product);
                Empty();
            }
        }

        protected override void findCommand(ProductDTO productDTO)
        {
            if (productDTO == null) return;
            Product = (Monitor)productDTO;
            Name = Product.Name;
            Size = Product.Size;
            Series = Product.Series;
            Panel = Product.Panel;
            Company = Product.Company;
            DetailPath = Product.DetailPath;
            Discount = Product.Discount;
            Id = Product.Id;
            ImagePath = Product.ImagePath;
            AvatarPath = Product.AvatarPath;
            Price = Product.Price;
            Unit = Product.Unit;
            Remain = Product.Remain;
            RefreshRate = Product.RefreshRate;
            Need = Product.Need;
            RaisePropertyChanged(nameof(IsDefault));
        }
        protected override void clear()
        {
            findCommand(Product);
        }
        private Monitor GetMonitor(string id, int remain)
        {
            return new()
            {
                Name = Name,
                Size = Size,
                Series = Series,
                Panel = Panel,
                Company = Company,
                DetailPath = DetailPath,
                Discount = Discount,
                Id = id,
                ImagePath = ImagePath,
                AvatarPath = AvatarPath,
                Price = Price,
                Unit = Unit,
                Remain = remain,
                RefreshRate = RefreshRate,
                Need = null
            };
        }
        private void Empty()
        {
            Product = null;
            Id = null;
            Company = null; Unit = null; Series = null;
            Name = null; Size = null; Panel = null; RefreshRate = null;
            AvatarPath = null; Price = 0; Discount = 0;
            ImagePath = null; DetailPath = null; Remain = 0; Need = null;
            RaisePropertyChanged(nameof(IsDefault));
        }

        public override async void OnNavigatedTo(NavigationContext navigationContext)
        {
            clear();
            ProductList = new(await _unitOfWork.Monitors.GetAll());
        }
    }
}
