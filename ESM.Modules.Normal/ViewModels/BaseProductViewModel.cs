using ESM.Core;
using ESM.Modules.DataAccess;
using ESM.Modules.DataAccess.DTOs;
using ESM.Modules.DataAccess.Infrastructure;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace ESM.Modules.Normal.ViewModels
{
    public abstract class BaseProductViewModel<T> : BindableBase
    where T : ProductDTO
    {
        protected IUnitOfWork _unitOfWork;
        protected dynamic _productDTOs;
        public dynamic ProductList { get; set; }
        public List<string> Conditions { get; } = StaticData.Conditions;
        protected string selectedCondition;
        public double MaxPrice { get; set; } = 0;
        public double TickFrequency { get; } = 500_000;
        private double currentPrice;
        public double CurrentPrice
        {
            get => currentPrice;
            set
            {
                currentPrice = value;
                priceRangeCommand();
            }
        }
        protected BaseProductViewModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            GetProductList();
        }
        private void GetProductList()
        {
            dynamic list = null;
            if (typeof(T).Equals(typeof(LaptopDTO)))
                list = _unitOfWork.Laptops.GetAll();
            else if (typeof(T).Equals(typeof(MonitorDTO)))
                list = _unitOfWork.Monitors.GetAll();
            else if(typeof(T).Equals(typeof(PccpuDTO)))
                list = _unitOfWork.Pccpus.GetAll();
            else if (typeof(T).Equals(typeof(PcDTO)))
                list = _unitOfWork.Pcs.GetAll();
            else if(typeof(T).Equals(typeof(SmartphoneDTO)))
                list = _unitOfWork.Smartphones.GetAll();
            else if (typeof(T).Equals(typeof(VgaDTO)))
                list = _unitOfWork.Vgas.GetAll();
            else if (typeof(T).Equals(typeof(PcharddiskDTO)))
                list = _unitOfWork.Pcharddisks.GetAll();
            if (list != null && list.Count > 0)
            {
                ProductList = list;
                _productDTOs = list;
                MaxPrice = Math.Ceiling((double)((List<T>)list).Max(x => x.SellPrice) / TickFrequency) * TickFrequency;
                CurrentPrice = MaxPrice;
            }
        }
        private void priceRangeCommand()
        {
            Action?.Invoke();
            ProductList = ((List<T>)ProductList).Where(x => (double)x.SellPrice <= CurrentPrice).ToList();
            RaisePropertyChanged(nameof(ProductList));
        }
        public string? SelectedCondition
        {
            get => selectedCondition;
            set
            {
                selectedCondition = value;
                RaisePropertyChanged(nameof(SelectedCondition));
                SelectedConditionChanged();
            }
        }
        public ICommand ProductDetailNavigateCommand { get; set; }
        protected Action? Action { get; set; }
        protected void SelectedConditionChanged()
        {
            if (SelectedCondition == null) return;
            if (SelectedCondition == Conditions[0])
            {
                SelectedCondition = null;
                Action?.Invoke();
            }
            else if (SelectedCondition == Conditions[1])
            {
                ProductList = ((List<T>)ProductList)?.OrderBy(x => x.SellPrice).ToList();
            }
            else if (SelectedCondition == Conditions[2])
            {
                ProductList = ((List<T>)ProductList)?.OrderByDescending(x => x.SellPrice).ToList();
            }
            RaisePropertyChanged(nameof(ProductList));
        }
    }
}
