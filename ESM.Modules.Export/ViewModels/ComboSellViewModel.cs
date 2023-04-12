﻿using ESM.Core;
using ESM.Core.ShareServices;
using ESM.Core.ShareStores;
using ESM.Modules.DataAccess;
using ESM.Modules.DataAccess.Infrastructure;
using ESM.Modules.DataAccess.Models;
using ESM.Modules.Export.Utilities;
using ESM.Modules.Export.Views;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ESM.Modules.Export.ViewModels
{
    public class ComboSellViewModel : BindableBase, INavigationAware
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IModalService _modalService;
        private readonly AccountStore _accountStore;
        public ComboSellViewModel(IUnitOfWork unitOfWork, IModalService modalService, AccountStore accountStore)
        {
            _unitOfWork = unitOfWork;
            _modalService = modalService;
            _accountStore = accountStore;
            AddCommand = new(async(x) => await addCommand(x));
            CancelCommand = new(cancelCommand);
            PayCommand = new(ExecutePay);
        }
        public string Header => "Combo";
        private Combo SelectedCombo;
        private IEnumerable<ProductDTO> productList;
        public IEnumerable<ProductDTO> ProductList
        {
            get => productList;
            set => SetProperty(ref productList, value);
        }
        private IEnumerable<Combo> comboList;
        public IEnumerable<Combo> ComboList
        {
            get => comboList;
            set => SetProperty(ref comboList, value);
        }
        private ICollection<ProductBill> ProductBillList;
        public decimal SellPrice => (SelectedCombo == null)? 0: SelectedCombo.SellPrice;
        public double Discount => (SelectedCombo == null)? 0: SelectedCombo.Discount;
        public string TextFormPrice => NumberToText.FuncNumberToText((double)SellPrice);
        public string CustomerName { get; set; }
        public string CustomerPhone { get; set; }
        public DelegateCommand<Combo> AddCommand { get; }
        public DelegateCommand CancelCommand { get; }
        public DelegateCommand PayCommand { get; }
        private void ExecutePay()
        {
            if (SelectedCombo == null) return;
            ProductBillList = new List<ProductBill>();
            foreach (var product in ProductList)
            {
                ProductBillList.Add(new(null)
                {
                    Name = product.Name,
                    SellPrice = product.SellPrice,
                    Remain = product.Remain,
                    Number = "1",
                });
            };
            var Result = new InvoiceCombo(_modalService, _unitOfWork, _accountStore, CustomerName, CustomerPhone, ProductBillList, SelectedCombo).ShowDialog();
            if (Result == true)
            {
                ProductBillList.Clear();
                CustomerName = null;
                CustomerPhone = null;
            }
        }
        private async Task addCommand(Combo combo)
        {
            if (combo.Remain == 0)
            {
                _modalService.ShowModal(ModalType.Information, "Sản phẩm tạm hết hàng", "Xin lỗi");
                return;
            }
            SelectedCombo = combo;
            ProductList = await _unitOfWork.Combos.GetListProduct(SelectedCombo);
            RaisePropertyChanged(nameof(SellPrice));
            RaisePropertyChanged(nameof(TextFormPrice));
            RaisePropertyChanged(nameof(Discount));
        }
        private void cancelCommand()
        {
            SelectedCombo = null;
            ProductList = null;
            RaisePropertyChanged(nameof(SellPrice));
            RaisePropertyChanged(nameof(TextFormPrice));
            RaisePropertyChanged(nameof(Discount));
            RaisePropertyChanged(nameof(CustomerName));
            RaisePropertyChanged(nameof(CustomerPhone));
        }
        public async void OnNavigatedTo(NavigationContext navigationContext)
        {
            ComboList = await _unitOfWork.Combos.GetAll();
            cancelCommand();
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