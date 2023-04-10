using ESM.Core;
using ESM.Core.ShareServices;
using ESM.Modules.DataAccess;
using ESM.Modules.DataAccess.DTOs;
using ESM.Modules.DataAccess.Infrastructure;
using ESM.Modules.DataAccess.Models;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ESM.Modules.Normal.ViewModels
{
    public abstract class BaseProductViewModel<T> : BindableBase, INavigationAware
    where T : ProductDTO
    {
        protected readonly IUnitOfWork _unitOfWork;
        protected readonly IModalService _modalService;
        protected dynamic _productDTOs;
        public dynamic ProductList { get; set; }
        public List<string> Conditions { get; } = StaticData.Conditions;
        private double maxPrice;
        public double MaxPrice
        {
            get => maxPrice;
            set => SetProperty(ref maxPrice, value);
        }
        public double TickFrequency { get; } = 500_000;
        private double upperValue;
        public double UpperValue
        {
            get => upperValue;
            set => SetProperty(ref upperValue, value, FilterProduct);
        }
        private double lowerValue;
        public double LowerValue
        {
            get => lowerValue;
            set => SetProperty(ref lowerValue, value, FilterProduct);
        }
        protected BaseProductViewModel(IUnitOfWork unitOfWork, IModalService modalService)
        {
            _unitOfWork = unitOfWork;
            _modalService = modalService;
            
            ProductDetailNavigateCommand = new(navigate);
        }
        protected Action LoadAttribute;
        private async Task GetProductList()
        {
            dynamic list = null;
            if (typeof(T).Equals(typeof(Laptop)))
                list = await _unitOfWork.Laptops.GetAll();
            else if (typeof(T).Equals(typeof(Monitor)))
                list = await _unitOfWork.Monitors.GetAll();
            else if (typeof(T).Equals(typeof(Pccpu)))
                list = await _unitOfWork.Pccpus.GetAll();
            else if (typeof(T).Equals(typeof(Pc)))
                list = await _unitOfWork.Pcs.GetAll();
            else if (typeof(T).Equals(typeof(Smartphone)))
                list = await _unitOfWork.Smartphones.GetAll();
            else if (typeof(T).Equals(typeof(Vga)))
                list = await _unitOfWork.Vgas.GetAll();
            else if (typeof(T).Equals(typeof(Pcharddisk)))
                list = await _unitOfWork.Pcharddisks.GetAll();
            if (list != null && list.Count > 0)
            {
                ProductList = list;
                _productDTOs = list;
                MaxPrice = Math.Ceiling((double)((List<T>)list).Max(x => x.SellPrice) / TickFrequency) * TickFrequency;
                UpperValue = MaxPrice;
                LoadAttribute?.Invoke();
            }
        }
        protected string selectedCondition;
        public string? SelectedCondition
        {
            get => selectedCondition;
            set => SetProperty(ref selectedCondition, value, FilterProduct);
        }
        public DelegateCommand<ProductDTO> ProductDetailNavigateCommand { get; set; }
        private void navigate(ProductDTO product)
        {
            NavigationParameters parameter = new()
            {
                {"Product" , product}
            };
            _modalService.ShowModal(ViewNames.ProductDetailView, parameter);
        }
        protected Action Action { get; set; }
        protected void FilterProduct()
        {
            ProductList = _productDTOs;
            if (SelectedCondition == Conditions[1])
            {
                ProductList = ((List<T>)ProductList)?.OrderBy(x => x.SellPrice).ToList();
            }
            else if (SelectedCondition == Conditions[2])
            {
                ProductList = ((List<T>)ProductList)?.OrderByDescending(x => x.SellPrice).ToList();
            }
            else if (SelectedCondition == Conditions[3])
            {
                ProductList = ((List<T>)ProductList)?.OrderByDescending(x => x.Discount).ToList();
            }
            Action?.Invoke();
            ProductList = ((List<T>)ProductList)?
                .Where(x => (double)x.SellPrice <= UpperValue && (double)x.SellPrice >= LowerValue)
                .ToList();
            RaisePropertyChanged(nameof(ProductList));
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            GetProductList().Await();
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {

        }
    }
}
