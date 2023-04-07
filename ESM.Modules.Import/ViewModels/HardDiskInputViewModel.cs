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

namespace ESM.Modules.Import.ViewModels
{
    public class HardDiskInputViewModel : BaseProductInputViewModel<Pcharddisk>
    {
        public HardDiskInputViewModel(IUnitOfWork unitOfWork, IOpenDialogService openDialogService, IModalService modalService) : base(unitOfWork, openDialogService, modalService)
        {

        }
        HashSet<string> NotInDatabase;
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
        protected override void addCommand()
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
            Name == null || Storage == null || Connect == null || Type == null
            || Price == 0)
            {
                _modalService.ShowModal(ModalType.Error, "Nhập tất cả thông tin cần thiết", "Cảnh báo");
                return;
            }
            Pcharddisk pcharddiskDTO = new()
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
                Remain = Remain,
            };
            ProductList.Add(pcharddiskDTO);
            NotInDatabase.Add(Id);
            clearCommand();
        }
        protected override void saveCommand()
        {
            //Task<bool> task = new(() =>
            //{

            //    try
            //    {
            //        if (Product == null)
            //            _unitOfWork.Pcharddisks.Add(pcharddiskDTO);
            //        else
            //        {
            //            _unitOfWork.Pcharddisks.Update(pcharddiskDTO);
            //            Product = pcharddiskDTO;
            //        }
            //        _unitOfWork.SaveChange();
            //        ProductList = _unitOfWork.Pcharddisks.GetAll();
            //        return true;
            //    }
            //    catch (Exception)
            //    {
            //        return false;
            //    }
            //});
            //task.Start();
            //task.Await();
            //var res = task.Result;
            //if (res)
            //{
            //    _modalService.ShowModal(ModalType.Information, "Đã lưu", "Thông báo");
            //    clearCommand();
            //}
            //else _modalService.ShowModal(ModalType.Error, "Lưu không thành công", "Lỗi");
        }
        protected override void clearCommand()
        {
            Product = null;
            Id = null;
            Company = null; Unit = null; Series = null;
            Name = null; Storage = null; Connect = null; Type = null;
            AvatarPath = null; Price = 0; Discount = 0;
            ImagePath = null; DetailPath = null;
            IsNotInDatabase = true;
            RaisePropertyChanged(nameof(IsDefault));
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
            IsNotInDatabase = NotInDatabase.Contains(Id);
            RaisePropertyChanged(nameof(IsDefault));
        }

        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
            //Id = _unitOfWork.Pcharddisks.GetSuggestID();
            ProductList = new(_unitOfWork.Pcharddisks.GetAll());
            NotInDatabase = new();
        }

        protected override void deleteCommand()
        {
            if (NotInDatabase.Contains(Id))
            {
                var p = ProductList.Single(x => x.Id == Id);
                ProductList.Remove(p);
                NotInDatabase.Remove(Id);
            }
        }
    }
}
