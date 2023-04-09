using ESM.Core.ShareServices;
using ESM.Modules.DataAccess;
using ESM.Modules.DataAccess.DTOs;
using ESM.Modules.DataAccess.Infrastructure;
using ESM.Modules.DataAccess.Models;
using Microsoft.Identity.Client.Extensions.Msal;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
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
        HashSet<string> NotInDatabase;
        public string Header => "Laptop";
        private string graphic;
        public string Graphic
        {
            get => graphic;
            set => SetProperty(ref graphic, value);
        }
        private string ram;
        public string Ram
        {
            get => ram;
            set => SetProperty(ref ram, value);
        }
        private string cpu;
        public string Cpu
        {
            get => cpu;
            set => SetProperty(ref cpu, value);
        }

        private string need;
        public string Need
        {
            get => need;
            set => SetProperty(ref need, value);
        }

        private string series;
        public string Series
        {
            get => series;
            set => SetProperty(ref series, value);
        }
        private string storage;
        public string Storage
        {
            get => storage;
            set => SetProperty(ref storage, value);
        }

        protected override void clearCommand()
        {
            Product = null;
            Id = _unitOfWork.Pcharddisks.GetSuggestID();
            Company = null; Unit = null; Graphic = null;
            Name = null; Cpu = null; Need = null;
            AvatarPath = null; Price = 0; Discount = 0; Remain = 0;
            Ram = null; ImagePath = null; DetailPath = null; Series = null;
            RaisePropertyChanged(nameof(IsDefault));
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
            RaisePropertyChanged(nameof(IsDefault));
        }

        protected override async void addCommand()
        {
            if (SelectedWorkType == "THÊM")
            {
                if (Id == null || Id.Length != 9 || !Id.StartsWith(DAStaticData.IdPrefix[ProductType.HARDDISK]) || !Id.All(x => char.IsDigit(x)))
                {
                    _modalService.ShowModal(ModalType.Error, "Sai định dạng ID", "Cảnh báo");
                    return;
                }
                if (ProductList.Any(x => x.Id == Id))
                {
                    _modalService.ShowModal(ModalType.Error, "ID đã tồn tại", "Cảnh báo");
                    return;
                }
                if (Id == null || Company == null || Unit == null ||
               Name == null || Storage == null || Graphic == null ||
               Cpu == null || Ram == null)
                {
                    _modalService.ShowModal(ModalType.Error, "Nhập tất cả thông tin cần thiết", "Cảnh báo");
                    return;
                }
                Laptop laptopDTO = new()
                {
                    Id = Id,
                    Cpu = Cpu,
                    Company = Company,
                    Unit = Unit,
                    Graphic = Graphic,
                    Name = Name,
                    Price = Price,
                    Discount = Discount,
                    DetailPath = DetailPath,
                    AvatarPath = AvatarPath,
                    ImagePath = ImagePath,
                    Ram = Ram,
                    Need = Need,
                    Storage = Storage,
                    Remain = Remain,
                    Series = Series,
                };
                ProductList.Add(laptopDTO);
                NotInDatabase.Add(Id);
                clearCommand();
            }
            else if (SelectedWorkType == "SỬA")
            {
                if (Id == null || Company == null || Unit == null ||
               Name == null || Storage == null || Graphic == null ||
               Cpu == null || Ram == null)
                {
                    _modalService.ShowModal(ModalType.Error, "Nhập tất cả thông tin cần thiết", "Cảnh báo");
                    return;
                }
                Laptop laptopDTO = new()
                {
                    Id = Id,
                    Cpu = Cpu,
                    Company = Company,
                    Unit = Unit,
                    Graphic = Graphic,
                    Name = Name,
                    Price = Price,
                    Discount = Discount,
                    DetailPath = DetailPath,
                    AvatarPath = AvatarPath,
                    ImagePath = ImagePath,
                    Ram = Ram,
                    Need = Need,
                    Storage = Storage,
                    Remain = Remain,
                    Series = Series,
                };
                var res = await _unitOfWork.Laptops.Update(laptopDTO);
                if ((bool)res)
                    _modalService.ShowModal(ModalType.Information, "Cập nhật thành công", "Thông báo");
                else _modalService.ShowModal(ModalType.Error, "Có lỗi xảy ra", "Thông báo");
                Id = null;
                Cpu = null;
                Company = null;
                Unit = null;
                Graphic = null;
                Name = null;
                Price = 0;
                Discount = 0;
                DetailPath = null;
                AvatarPath = null;
                ImagePath = null;
                Ram = null;
                Need = null;
                Storage = null;
                Remain = 0;
                Series = null;
                RaisePropertyChanged(nameof(IsDefault));
                var list = await _unitOfWork.Laptops.GetAll();
                ProductList = new(list);
            }
        }
        protected override async Task saveCommand()
        {
            var res = await _unitOfWork.Laptops.AddList(ProductList);

            if ((bool)res)
            {
                _modalService.ShowModal(ModalType.Information, "Đã lưu", "Thông báo");
                ProductList = new();
            }
            else _modalService.ShowModal(ModalType.Error, "Lưu không thành công", "Lỗi");
            await Task.CompletedTask;
        }
        public override async void OnNavigatedTo(NavigationContext navigationContext)
        {
            ProductList = new();
            NotInDatabase = new();
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
            clearCommand(); ;
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
                if (MessageBox.Show("Bạn có chắc chắn xóa?", "Cảnh báo", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    await _unitOfWork.Laptops.Delete(productDTO.Id);
                    var list = await _unitOfWork.Laptops.GetAll();
                    ProductList = new(list);
                }
            }
        }
    }
}
