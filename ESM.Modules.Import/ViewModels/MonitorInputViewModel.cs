using ControlzEx.Standard;
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
    public class MonitorInputViewModel : BaseProductInputViewModel<Monitor>
    {
        public string Header => "Monitor";
        public MonitorInputViewModel(IUnitOfWork unitOfWork, IOpenDialogService openDialogService, IModalService modalService) : base(unitOfWork, openDialogService, modalService)
        {

        }
        HashSet<string> NotInDatabase;
        public string size;
        public string Size
        {
            get => size;
            set => SetProperty(ref size, value);
        }

        public string panel;
        public string Panel
        {
            get => panel;
            set => SetProperty(ref panel, value);
        }

        public short refreshRate;
        public short RefreshRate
        {
            get => refreshRate;
            set => SetProperty(ref refreshRate, value);
        }

        public string series;
        public string Series
        {
            get => series;
            set => SetProperty(ref series, value);
        }

        public string need;
        public string Need
        {
            get => need;
            set => SetProperty(ref need, value);
        }
        protected override async void CurrentWorkTypeChanged()
        {
            if (SelectedWorkType == "THÊM")
            {
                ProductList = new();
            }
            else
            {
                var list = await _unitOfWork.Monitors.GetAll();
                ProductList = new(list);
            }
            clearCommand();
        }

        protected override async Task saveCommand()
        {
            var res = await _unitOfWork.Monitors.AddList(ProductList);
            if ((bool)res)
            {
                _modalService.ShowModal(ModalType.Information, "Đã lưu", "Thông báo");
                ProductList = new();
            }
            else _modalService.ShowModal(ModalType.Error, "Lưu không thành công", "Lỗi");
            await Task.CompletedTask;
        }

        protected override void clearCommand()
        {
            if (SelectedWorkType == "THÊM")
            {
                Product = null;
                Id = null;
                Company = null; Unit = null; Series = null;
                Name = null; size = null; panel = null; RefreshRate = 0;
                AvatarPath = null; Price = 0; Discount = 0;
                ImagePath = null; DetailPath = null; Remain = 0; need = null;
                RaisePropertyChanged(nameof(IsDefault));
            }
            else if (SelectedWorkType == "SỬA")
            {
                Price = 0; Discount = null;
                if (Product != null) findCommand(Product);
            }
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
                if (Company == null || Unit == null ||
                Name == null || size == null || panel == null
                || Price == 0)
                {
                    _modalService.ShowModal(ModalType.Error, "Nhập tất cả thông tin cần thiết", "Cảnh báo");
                    return;
                }
                Monitor monitorsDTO = new()
                {
                    Name = Name,
                    Size = Size,
                    Series = Series,
                    Panel = Panel,
                    Company = Company,
                    DetailPath = DetailPath,
                    Discount = Discount,
                    Id = Id,
                    ImagePath = ImagePath,
                    AvatarPath = AvatarPath,
                    Price = Price,
                    Unit = Unit,
                    Remain = 0,
                    RefreshRate = RefreshRate,
                    Need = null
                };
                ProductList.Add(monitorsDTO);
                NotInDatabase.Add(Id);
                clearCommand();
            }
            else if (SelectedWorkType == "SỬA")
            {
                if (Company == null || Unit == null ||
                Name == null || size == null || panel == null
                || Price == 0)
                {
                    _modalService.ShowModal(ModalType.Error, "Nhập tất cả thông tin cần thiết", "Cảnh báo");
                    return;
                }
                Monitor monitorsDTO = new()
                {
                    Name = Name,
                    Size = Size,
                    Series = Series,
                    Panel = Panel,
                    Company = Company,
                    DetailPath = DetailPath,
                    Discount = Discount,
                    Id = Id,
                    ImagePath = ImagePath,
                    AvatarPath = AvatarPath,
                    Price = Price,
                    Unit = Unit,
                    Remain = 0,
                    RefreshRate = RefreshRate,
                    Need = null
                };
                var res = await _unitOfWork.Monitors.Update(monitorsDTO);
                if ((bool)res)
                    _modalService.ShowModal(ModalType.Information, "Cập nhật thành công", "Thông báo");
                else _modalService.ShowModal(ModalType.Error, "Có lỗi xảy ra", "Thông báo");
                // Clear
                Product = null;
                Id = null;
                Company = null; Unit = null; Series = null;
                Name = null; size = null; panel = null; RefreshRate = 0;
                AvatarPath = null; Price = 0; Discount = 0;
                ImagePath = null; DetailPath = null; Remain = 0; need = null;
                RaisePropertyChanged(nameof(IsDefault));
                var list = await _unitOfWork.Monitors.GetAll();
                ProductList = new(list);
            }
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
                    await _unitOfWork.Monitors.Delete(productDTO.Id);
                    var list = await _unitOfWork.Monitors.GetAll();
                    ProductList = new(list);
                }
            }
        }

        protected override void findCommand(ProductDTO productDTO)
        {
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


    }
}
