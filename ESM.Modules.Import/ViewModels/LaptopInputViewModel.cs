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
    public class LaptopInputViewModel : BaseProductInputViewModel<Laptop>
    {
        public LaptopInputViewModel(IUnitOfWork unitOfWork, IOpenDialogService openDialogService, IModalService modalService) : base(unitOfWork, openDialogService, modalService)
        {

        }

        public string Header => "Laptop";
        private string graphic;
        public string Graphic
        {
            get => graphic?.Trim();
            set => SetProperty(ref graphic, value);
        }
        private string ram;
        public string Ram
        {
            get => ram?.Trim();
            set => SetProperty(ref ram, value);
        }
        private string cpu;
        public string Cpu
        {
            get => cpu?.Trim();
            set => SetProperty(ref cpu, value);
        }

        private string need;
        public string Need
        {
            get => need?.Trim();
            set => SetProperty(ref need, value);
        }

        private string series;
        public string Series
        {
            get => series?.Trim();
            set => SetProperty(ref series, value);
        }
        private string storage;
        public string Storage
        {
            get => storage?.Trim();
            set => SetProperty(ref storage, value);
        }
        protected override void findCommand(ProductDTO productDTO)
        {
            if (productDTO == null) return;
            Product = (Laptop)productDTO;
            Id = Product.Id;
            Cpu = Product.Cpu;
            Company = Product.Company;
            Unit = Product.Unit;
            Graphic = Product.Graphic;
            Name = Product.Name;
            Price = Product.Price;
            Discount = Product.Discount;
            DetailPath = Product.DetailPath;
            AvatarPath = Product.AvatarPath;
            ImagePath = Product.ImagePath;
            Ram = Product.Ram;
            Need = Product.Need;
            Remain = Product.Remain;
            Series = Product.Series;
            Storage = Product.Storage;
            RaisePropertyChanged(nameof(IsDefault));
        }

        protected override void addCommand()
        {
            Laptop p = GetLaptop(GetNewID(ProductType.LAPTOP), 0);
            p.InMemory = false;
            ProductList.Add(p);
            findCommand(p);
            TurnOnEditCommand.Execute();
        }
        protected override async Task saveCommand()
        {
            if (Company == null || Unit == null ||
               Name == null || Storage == null || Graphic == null ||
               Cpu == null || Ram == null)
            {
                _modalService.ShowModal(ModalType.Error, "Nhập tất cả thông tin cần thiết", "Cảnh báo");
                return;
            }
            MetroWindow metroWindow = (MetroWindow)Application.Current.MainWindow;
            if (metroWindow.ShowModalMessageExternal("Thông báo", "Bạn có chắc chắn lưu?", MessageDialogStyle.AffirmativeAndNegative) == MessageDialogResult.Affirmative)
            {
                var p = GetLaptop(Id, Remain);
                if (Product.InMemory)
                {
                    var res = await _unitOfWork.Laptops.Update(p);
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
                    var res = await _unitOfWork.Laptops.Add(p);
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
        protected override void clear()
        {
            findCommand(Product);
        }
        protected override async void deleteCommand()
        {
            MetroWindow metroWindow = (MetroWindow)Application.Current.MainWindow;
            if (metroWindow.ShowModalMessageExternal("Cảnh báo", "Bạn có chắc chắn xóa?", MessageDialogStyle.AffirmativeAndNegative) == MessageDialogResult.Affirmative)
            {
                if (Product.InMemory) await _unitOfWork.Laptops.Delete(Product.Id);
                ProductList.Remove(Product);
                Empty();
            }
        }
        private Laptop GetLaptop(string id, int remain)
        {
            return new()
            {
                Name = Name,
                Storage = Storage,
                Series = Series,
                Company = Company,
                DetailPath = DetailPath,
                Discount = Discount,
                Id = id,
                ImagePath = ImagePath,
                AvatarPath = AvatarPath,
                Price = Price,
                Unit = Unit,
                Remain = remain,
                Cpu = Cpu,
                Graphic = Graphic,
                Need = Need,
                Ram = Ram
            };
        }
        private void Empty()
        {
            Product = null;
            Id = null;
            Company = null; Unit = null; Series = null;
            Name = null; Storage = null; Graphic = null;
            Cpu = null; Need = null; Ram = null;
            AvatarPath = null; Price = 0; Discount = 0;
            ImagePath = null; DetailPath = null; Remain = 0;
            IsEditable = false;
            RaisePropertyChanged(nameof(IsDefault));
        }

        public override async void OnNavigatedTo(NavigationContext navigationContext)
        {
            clear();
            ProductList = new(await _unitOfWork.Laptops.GetAll());
        }
    }
}
