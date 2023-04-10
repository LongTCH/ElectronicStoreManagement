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
            Id = _unitOfWork.Laptops.GetSuggestID();
        }

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

        }

        protected override void addCommand()
        {
            if (Id == null || Company == null || Unit == null ||
               Name == null || Storage == null || Graphic == null ||
               Cpu == null || Ram == null)
            {
                _modalService.ShowModal(ModalType.Error, "Nhập tất cả thông tin cần thiết", "Cảnh báo");
                return;
            }
            Laptop laptopDTO = GetLaptop();
            laptopDTO.Remain = Remain;
            ProductList.Add(laptopDTO);
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
        public override async void OnNavigatedTo(NavigationContext navigationContext)
        {
            var list = await _unitOfWork.Laptops.GetAll();
            ProductList = new(list);
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
                if (MessageBox.Show("Bạn có chắc chắn xóa?", "Cảnh báo", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
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
                Graphic =Graphic,
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
