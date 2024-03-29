﻿using ESM.Core.ShareServices;
using ESM.Core.ShareStores;
using ESM.Modules.DataAccess.Infrastructure;
using ESM.Modules.DataAccess.Models;
using ESM.Modules.Export.Utilities;
using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace ESM.Modules.Export.Views
{
    /// <summary>
    /// Interaction logic for Invoice.xaml
    /// </summary>
    public partial class Invoice : MetroWindow, INotifyPropertyChanged
    {
        private readonly IModalService _modalService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly AccountStore _accountStore;
        public Invoice(IModalService modalService, IUnitOfWork unitOfWork, AccountStore accountStore,
            string customerName, string phone, ObservableCollection<ProductBill> list, decimal totalAmount)
        {
            _modalService = modalService;
            _unitOfWork = unitOfWork;
            _accountStore = accountStore;
            InitializeComponent();
            DataContext = this;
            StaffId = accountStore.CurrentAccount.Id;
            CustomerPhone = phone;
            CustomerName = customerName;
            ListProduct = list;
            TotalAmount = totalAmount;
            SellDay = DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString();
            this.Height = 650 + 70 * ListProduct.Count;
        }
        public string StaffId { get;set; } 
        public string BillId { get; set; } = null;
        public string SellDay { get; set; }
        public string CustomerName { get; set; }
        public string CustomerPhone { get; set; }
        public decimal TotalAmount { get; set; }
        public ObservableCollection<ProductBill> ListProduct { get; set; }
        void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public event PropertyChangedEventHandler PropertyChanged;

        private async void btnPrint_Click(object sender, RoutedEventArgs e)
        {
            PrintDialog printDialog = new PrintDialog();
            if (printDialog.ShowDialog() == true)
            {
                ICollection<BillProduct> billProducts = new List<BillProduct>();
                foreach (var item in ListProduct)
                {
                    billProducts.Add(new BillProduct()
                    {
                        Amount = item.Amount,
                        Number = Convert.ToInt32(item.Number),
                        SellPrice = item.SellPrice,
                        Warranty = item.Warranty,
                        ProductId = item.Id,
                        Unit = item.Unit,
                    });
                }
                try
                {
                    Bill bill = new()
                    {
                        CustomerName = CustomerName,
                        Phone = CustomerPhone,
                        BillProducts = billProducts,
                        PurchasedTime = DateTime.Now,
                        StaffId = _accountStore.CurrentAccount.Id,
                        TotalAmount = TotalAmount,
                    };
                    BillId = (await _unitOfWork.Bills.Add(bill)).ToString();
                   
                    OnPropertyChanged(nameof(BillId));
                    btnPrint.Visibility = Visibility.Hidden;
                    try
                    {
                        printDialog.PrintVisual(print, "In Hóa đơn");
                    }
                    catch { }
                    _modalService.ShowModal(ModalType.Information, "In thành công", "Thông báo");
                    DialogResult = true;
                }
                catch (Exception)
                {
                    _modalService.ShowModal(ModalType.Error, "Đơn hàng không thành công", "Thông báo");
                    DialogResult = false;
                }
                finally
                {
                    this.Close();
                }
            }
        }
    }
}
