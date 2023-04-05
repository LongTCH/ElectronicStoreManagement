using ESM.Core.ShareServices;
using ESM.Modules.DataAccess.DTOs;
using ESM.Modules.DataAccess.Infrastructure;
using Microsoft.Identity.Client.Extensions.Msal;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
        public string Ram { 
            get => ram;
            set => SetProperty(ref ram, value);
        }
        private string cpu;
        public string Cpu {
            get => cpu;
            set => SetProperty(ref cpu, value);
        }
        private string screen;
        public string Screen {
            get => screen;
            set => SetProperty(ref screen, value);
        }

        private string need;
        public string Need {
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
            Name = null; Cpu = null; Screen = null; Need = null;
            AvatarPath = null; Price = 0; Discount = 0; Remain = 0;
            Ram = null; ImagePath = null; DetailPath = null; Series = null;
            RaisePropertyChanged(nameof(IsDefault));
        }

        protected override void findCommand()
        {
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
            Screen = Product.Screen;
            Remain = Product.Remain;
            Series = Product.Series;

        }

        protected override void saveCommand()
        {
            if (Id == null || Company == null || Unit == null ||
                Name == null || Storage == null || Graphic == null || 
                Cpu == null || Ram == null || Screen == null)
            {
                _modalService.ShowModal(ModalType.Error, "Enter all required value", "Warning");
                return;
            }
            Task<bool> task = new(() =>
            {
                LaptopDTO laptopDTO = new()
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
                Screen = Screen,
                Remain = Remain,
                Series = Series,
            };
                try
                {
                    if (Product == null)
                        _unitOfWork.LaptopDTO.Add(laptopDTO);
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
        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
            {
                ProductList = _unitOfWork.Laptops.GetAll();
            }
    }
}
