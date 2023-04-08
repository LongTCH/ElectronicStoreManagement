using ESM.Core.ShareServices;
using ESM.Modules.DataAccess;
using ESM.Modules.DataAccess.DTOs;
using ESM.Modules.DataAccess.Infrastructure;
using ESM.Modules.DataAccess.Models;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
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
        private string storage;
        public string Storage
        {
            get => storage;
            set => SetProperty(ref storage, value);
        }
        private string connect;
        public string Connect
        {
            get => connect;
            set => SetProperty(ref connect, value);
        }
        private string series;
        public string Series
        {
            get => series;
            set => SetProperty(ref series, value);
        }
        private string type;
        public string Type
        {
            get => type;
            set => SetProperty(ref type, value);
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
                var exist = await _unitOfWork.Bills.IsProductExistInBill(Id);
                if (ProductList.Any(x => x.Id == Id) || exist)
                {
                    _modalService.ShowModal(ModalType.Error, "ID đã tồn tại", "Cảnh báo");
                    return;
                }
                if (Company == null || Unit == null ||
                Name == null || Storage == null || Connect == null || Type == null
                || Price == 0)
                {
                    _modalService.ShowModal(ModalType.Error, "Nhập tất cả thông tin cần thiết", "Cảnh báo");
                    return;
                }
                Pcharddisk pcharddiskDTO = GetPcharddisk();
                ProductList.Add(pcharddiskDTO);
                NotInDatabase.Add(Id);
                clearCommand();
            }
            else if (SelectedWorkType == "SỬA")
            {
                if (Company == null || Unit == null ||
                Name == null || Storage == null || Connect == null || Type == null
                || Price == 0)
                {
                    _modalService.ShowModal(ModalType.Error, "Nhập tất cả thông tin cần thiết", "Cảnh báo");
                    return;
                }
                var pcharddiskDTO = GetPcharddisk();
                pcharddiskDTO.Remain = Remain;
                var res = await _unitOfWork.Pcharddisks.Update(pcharddiskDTO);
                if ((bool)res)
                _modalService.ShowModal(ModalType.Information, "Cập nhật thành công", "Thông báo");
                else _modalService.ShowModal(ModalType.Error, "Có lỗi xảy ra", "Thông báo");
                // Clear
                Empty();
                 var list = await _unitOfWork.Pcharddisks.GetAll();
                ProductList = new(list);
            }
        }
        protected override async Task saveCommand()
        {
            var res = await _unitOfWork.Pcharddisks.AddList(ProductList);
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
                Empty();
            }
            else if (SelectedWorkType == "SỬA")
            {
                Price = 0; Discount = null;
                if (Product != null) findCommand(Product);
            }
        }

        protected override void findCommand(ProductDTO productDTO)
        {
            Product = (Pcharddisk)productDTO;
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
            Connect = Product.Connect;
            Storage = Product.Storage;
            Type = Product.Type;
            Remain = Product.Remain;
            RaisePropertyChanged(nameof(IsDefault));
        }
        protected override async void CurrentWorkTypeChanged()
        {
            if (SelectedWorkType == "THÊM")
            {
                ProductList = new();
            }
            else
            {
                var list = await _unitOfWork.Pcharddisks.GetAll();
                ProductList = new(list);
            }
            clearCommand();
        }
        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
            //Id = _unitOfWork.Pcharddisks.GetSuggestID();
            ProductList = new();
            NotInDatabase = new();
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
                    await _unitOfWork.Pcharddisks.Delete(productDTO.Id);
                    var list = await _unitOfWork.Pcharddisks.GetAll();
                    ProductList = new(list);
                }
            }

        }
        private Pcharddisk GetPcharddisk()
        {
            return new()
            {
                Name = Name,
                Storage = Storage,
                Connect = Connect,
                Series = Series,
                Type = Type,
                Company = Company,
                DetailPath = DetailPath,
                Discount = Discount,
                Id = Id,
                ImagePath = ImagePath,
                AvatarPath = AvatarPath,
                Price = Price,
                Unit = Unit,
                Remain = 0,
            };
        }
        private void Empty()
        {
            Product = null;
            Id = null;
            Company = null; Unit = null; Series = null;
            Name = null; Storage = null; Connect = null; Type = null;
            AvatarPath = null; Price = 0; Discount = 0;
            ImagePath = null; DetailPath = null; Remain = 0;
            RaisePropertyChanged(nameof(IsDefault));
        }
    }
}
