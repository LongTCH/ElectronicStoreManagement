using ESM.Core.ShareServices;
using ESM.Modules.DataAccess;
using ESM.Modules.DataAccess.Infrastructure;
using ESM.Modules.DataAccess.Models;
using MahApps.Metro.Controls.Dialogs;
using MahApps.Metro.Controls;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

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

        protected override void clearCommand()
        {
            if (SelectedWorkType == "THÊM")
            {
                Empty();
            }
            else if (SelectedWorkType == "SỬA")
            {
                Price = 0; Discount = null;
                if (Product != null) findCommand(Product);
            }
        }

        protected override void editCommand(ProductDTO productDTO)
        {
            if (SelectedWorkType == "THÊM")
            {
                ProductList.Remove((Laptop)productDTO);
                NotInDatabase.Remove(productDTO.Id);
            }
            findCommand(productDTO);
        }
        protected override void findCommand(ProductDTO productDTO)
        {
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

        protected async override void addCommand()
        {
            if (SelectedWorkType == "THÊM")
            {
                if (Id == null || Id.Length != 9 || !Id.StartsWith(DAStaticData.IdPrefix[ProductType.LAPTOP]) || !Id.All(x => char.IsDigit(x)))
                {
                    _modalService.ShowModal(ModalType.Error, "Sai định dạng ID", "Cảnh báo");
                    return;
                }

                //make the two calls to IsProductExistInBill and IsIdExist concurrently
                var task1 = _unitOfWork.Bills.IsProductExistInBill(Id);
                var task2 = _unitOfWork.Laptops.IsIdExist(Id);

                await Task.WhenAll(task1, task2);

                bool exist = task1.Result || task2.Result;

                if (ProductList.Any(x => x.Id == Id) || exist)
                {
                    _modalService.ShowModal(ModalType.Error, "ID đã tồn tại", "Cảnh báo");
                    return;
                }
                if (Company == null || Unit == null ||
               Name == null || Storage == null || Graphic == null ||
               Cpu == null || Ram == null)
                {
                    _modalService.ShowModal(ModalType.Error, "Nhập tất cả thông tin cần thiết", "Cảnh báo");
                    return;
                }
                Laptop laptopDTO = GetLaptop();
                ProductList.Add(laptopDTO);
                NotInDatabase.Add(Id);
                clearCommand();
            }
            else if (SelectedWorkType == "SỬA")
            {
                if (Company == null || Unit == null ||
               Name == null || Storage == null || Graphic == null ||
               Cpu == null || Ram == null)
                {
                    _modalService.ShowModal(ModalType.Error, "Nhập tất cả thông tin cần thiết", "Cảnh báo");
                    return;
                }
                Laptop laptopDTO = GetLaptop();
                var res = await _unitOfWork.Laptops.Update(laptopDTO);
                if ((bool)res)
                    _modalService.ShowModal(ModalType.Information, "Cập nhật thành công", "Thông báo");
                else _modalService.ShowModal(ModalType.Error, "Có lỗi xảy ra", "Thông báo");
                // Clear
                Empty();
                var list = await _unitOfWork.Laptops.GetAll();
                ProductList = new(list);
                laptopDTO.Remain = Remain;
            }

        }
        protected override async Task saveCommand()
        {
            var res = await _unitOfWork.Laptops.AddList(ProductList);
            if ((bool)res)
            {
                _modalService.ShowModal(ModalType.Information, "Đã lưu", "Thông báo");
                ProductList = new();
                NotInDatabase = new();
            }
            else _modalService.ShowModal(ModalType.Error, "Lưu không thành công", "Lỗi");
            await Task.CompletedTask;
        }

        protected override async void CurrentWorkTypeChanged()
        {
            if (SelectedWorkType == "THÊM")
            {
                ProductList = new();
            }
            else
            {
                var list = await _unitOfWork.Laptops.GetAll();
                ProductList = new(list);
            }
            clearCommand();
        }

        protected override async void deleteCommand(ProductDTO productDTO)
        {
            if (SelectedWorkType == "THÊM")
            {
                if (NotInDatabase.Contains(productDTO.Id))
                {
                    var p = ProductList.Single(x => x.Id == productDTO.Id);
                    ProductList.Remove(p);
                    NotInDatabase.Remove(productDTO.Id);
                }
            }
            else
            {
                MetroWindow metroWindow = (MetroWindow)Application.Current.MainWindow;
                if (metroWindow.ShowModalMessageExternal("Cảnh báo", "Bạn có chắc chắn xóa?", MessageDialogStyle.AffirmativeAndNegative) == MessageDialogResult.Affirmative)
                {
                    await _unitOfWork.Laptops.Delete(productDTO.Id);
                    var list = await _unitOfWork.Laptops.GetAll();
                    ProductList = new(list);
                }
            }
        }
        private Laptop GetLaptop()
        {
            return new()
            {
                Name = Name,
                Storage = Storage,
                Series = Series,
                Company = Company,
                DetailPath = DetailPath,
                Discount = Discount,
                Id = Id,
                ImagePath = ImagePath,
                AvatarPath = AvatarPath,
                Price = Price,
                Unit = Unit,
                Remain = 0,
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
            RaisePropertyChanged(nameof(IsDefault));
        }
    }
}
