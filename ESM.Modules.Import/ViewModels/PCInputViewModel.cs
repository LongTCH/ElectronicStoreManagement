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
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace ESM.Modules.Import.ViewModels
{
    public class PCInputViewModel : BaseProductInputViewModel<Pc>
    {
        public PCInputViewModel(IUnitOfWork unitOfWork, IOpenDialogService openDialogService, IModalService modalService) : base(unitOfWork, openDialogService, modalService)
        {
            
        }
        HashSet<string> NotInDatabase;
        public string Header => "PC";
        private string cpu;
        public string Cpu
        {
            get => cpu;
            set => SetProperty(ref cpu, value);
        }
        private string ram;
        public string Ram
        {
            get => ram;
            set => SetProperty(ref ram, value);
        }
        private string series;
        public string Series
        {
            get => series;
            set => SetProperty(ref series, value);
        }
        /*public string Cpu { get; set; } = null!;

        public string? Ram { get; set; }


        public string? Series { get; set; }


        public string? Need { get; set; }*/
        protected override async void addCommand()
        {
            /*if (Id == null || Company == null || Unit == null ||
               Name == null|| Series == null ||
               Cpu == null || Ram == null)
            {
                _modalService.ShowModal(ModalType.Error, "Nhập tất cả thông tin cần thiết", "Cảnh báo");
                return;
            }
            Pc pcDTO = new()
            {
                Id = Id,
                Cpu = Cpu,
                Company = Company,
                Unit = Unit,
                Series = Series,
                Name = Name,
                Price = Price,
                Discount = Discount,
                DetailPath = DetailPath,
                AvatarPath = AvatarPath,
                ImagePath = ImagePath,
                Ram = Ram,
                
                
                Remain = Remain,
                
            };
            ProductList.Add(pcDTO);*/
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
               Name == null || Series == null ||
               Cpu == null || Ram == null)
                {
                    _modalService.ShowModal(ModalType.Error, "Nhập tất cả thông tin cần thiết", "Cảnh báo");
                    return;
                }
                Pc pcDTO = new()
                {
                    Id = Id,
                    Cpu = Cpu,
                    Company = Company,
                    Unit = Unit,
                    Series = Series,
                    Name = Name,
                    Price = Price,
                    Discount = Discount,
                    DetailPath = DetailPath,
                    AvatarPath = AvatarPath,
                    ImagePath = ImagePath,
                    Ram = Ram,


                    Remain = 0,
                };
                ProductList.Add(pcDTO);
                NotInDatabase.Add(Id);
                clearCommand();
            }
            else if (SelectedWorkType == "SỬA")
            {
                if (Id == null || Company == null || Unit == null ||
               Name == null || Series == null ||
               Cpu == null || Ram == null)
                {
                    _modalService.ShowModal(ModalType.Error, "Nhập tất cả thông tin cần thiết", "Cảnh báo");
                    return;
                }
                Pc pcDTO = new()
                {
                    Id = Id,
                    Cpu = Cpu,
                    Company = Company,
                    Unit = Unit,
                    Series = Series,
                    Name = Name,
                    Price = Price,
                    Discount = Discount,
                    DetailPath = DetailPath,
                    AvatarPath = AvatarPath,
                    ImagePath = ImagePath,
                    Ram = Ram,


                    Remain = Remain,
                };
                var res = await _unitOfWork.Pcs.Update(pcDTO);
                if ((bool)res)
                    _modalService.ShowModal(ModalType.Information, "Cập nhật thành công", "Thông báo");
                else _modalService.ShowModal(ModalType.Error, "Có lỗi xảy ra", "Thông báo");
                // Clear
                Id = null;
                    Cpu = null;
                Company = null;
                Unit = null;
                Series = null;
                Name = null;
                Price = 0;
                Discount = 0;
                    DetailPath = null;
                AvatarPath = null;
                ImagePath = null;
                Ram = null;


                Remain = 0;
                RaisePropertyChanged(nameof(IsDefault));
                var list = await _unitOfWork.Pcs.GetAll();
                ProductList = new(list);
            }
        }
        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
            //Id = _unitOfWork.Pcharddisks.GetSuggestID();
            ProductList = new();
            NotInDatabase = new();
        }
        protected override void clearCommand()
        {
            if (SelectedWorkType == "THÊM")
            {
                Id = null;
                Cpu = null;
                Company = null;
                Unit = null;
                Series = null;
                Name = null;
                Price = 0;
                Discount = 0;
                DetailPath = null;
                AvatarPath = null;
                ImagePath = null;
                Ram = null;


                Remain = 0;
                RaisePropertyChanged(nameof(IsDefault));
            }
            else if (SelectedWorkType == "SỬA")
            {
                Price = 0; Discount = null;
                if (Product != null) findCommand(Product);
            }
        }

        protected override async void CurrentWorkTypeChanged()
        {
            if (SelectedWorkType == "THÊM")
            {
                ProductList = new();
            }
            else
            {
                var list = await _unitOfWork.Pcs.GetAll();
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
                    await _unitOfWork.Pcs.Delete(productDTO.Id);
                    var list = await _unitOfWork.Pcs.GetAll();
                    ProductList = new(list);
                }
            }
        }

        protected override void findCommand(ProductDTO productDTO)
        {
            Product = (Pc)productDTO;
            Id = Product.Id;
            Company = Product.Company;
            Unit = Product.Unit;
            Series = Product.Series;
            Name = Product.Name;
            Price = Product.Price;
            Discount = Product.Discount;
            DetailPath = Product.DetailPath;
            AvatarPath = Product.AvatarPath;
            ImagePath = Product.ImagePath;
            Cpu = Product.Cpu;
            Ram = Product.Ram;
            RaisePropertyChanged(nameof(IsDefault));
        }

        protected override async Task saveCommand()
        {
            var res = await _unitOfWork.Pcs.AddList(ProductList);
            if ((bool)res)
            {
                _modalService.ShowModal(ModalType.Information, "Đã lưu", "Thông báo");
                ProductList = new();
            }
            else _modalService.ShowModal(ModalType.Error, "Lưu không thành công", "Lỗi");
            await Task.CompletedTask;
        }
    }
}
