using ESM.Core.ShareServices;
using ESM.Modules.DataAccess.DTOs;
using ESM.Modules.DataAccess.Infrastructure;
using System;
using System.Threading.Tasks;

namespace ESM.Modules.Import.ViewModels
{
    public class HardDiskInputViewModel : BaseProductInputViewModel<PcharddiskDTO>
    {
        public HardDiskInputViewModel(IUnitOfWork unitOfWork, IOpenDialogService openDialogService, IModalService modalService) : base(unitOfWork, openDialogService, modalService)
        {
            Id = _unitOfWork.Pcharddisks.GetSuggestID();
            ProductList = _unitOfWork.Pcharddisks.GetAll();
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
        protected override void saveCommand()
        {
            if (Id == null || Company == null || Unit == null ||
                Name == null || Storage == null || Connect == null || Type == null)
            {
                _modalService.ShowModal(ModalType.Error, "Enter all required value", "Warning");
                return;
            }
            Task<bool> task = new(() =>
            {
                PcharddiskDTO pcharddiskDTO = new()
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
                    Unit = Unit
                };
                try
                {
                    if (Product == null)
                        _unitOfWork.Pcharddisks.Add(pcharddiskDTO);
                    else
                    {
                        _unitOfWork.Pcharddisks.Update(pcharddiskDTO);
                        Product = pcharddiskDTO;
                    }
                    _unitOfWork.SaveChange();
                    ProductList = _unitOfWork.Pcharddisks.GetAll();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            });
            task.Start();
            task.Await();
            var res = task.Result;
            if (res)
            {
                _modalService.ShowModal(ModalType.Information, "Saved", "Success");
                clearCommand();
            }
            else _modalService.ShowModal(ModalType.Error, "Failed to save", "Error");
        }
        protected override void clearCommand()
        {
            Product = null;
            Id = _unitOfWork.Pcharddisks.GetSuggestID();
            Company = null; Unit = null; Series = null;
            Name = null; Storage = null; Connect = null; Type = null;
            AvatarPath = null; Price = 0; Discount = 0;
            ImagePath = null; DetailPath = null;
            RaisePropertyChanged(nameof(IsDefault));
        }

        protected override void findCommand()
        {
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
        }
    }
}
