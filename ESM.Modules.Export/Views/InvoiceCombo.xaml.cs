using ESM.Core.ShareServices;
using ESM.Core.ShareStores;
using ESM.Modules.DataAccess.Infrastructure;
using ESM.Modules.DataAccess.Models;
using ESM.Modules.Export.Utilities;
using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace ESM.Modules.Export.Views
{
    /// <summary>
    /// Interaction logic for Invoice.xaml
    /// </summary>
    public partial class InvoiceCombo : MetroWindow, INotifyPropertyChanged
    {
        private readonly IModalService _modalService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly AccountStore _accountStore;
        private Combo _combo;
        public InvoiceCombo(IModalService modalService, IUnitOfWork unitOfWork, AccountStore accountStore,
            string customerName, string phone, ICollection<ProductBill> list, Combo combo, int number)
        {
            _modalService = modalService;
            _unitOfWork = unitOfWork;
            _accountStore = accountStore;
            InitializeComponent();
            DataContext = this;
            CustomerPhone = phone;
            CustomerName = customerName;
            ListProduct = list;
            Number = number;
            TotalAmount = combo.SellPrice * Convert.ToDecimal(number);
            ComboName = combo.Name;
            _combo = combo;
            SellDay = DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString();
            this.Height = 600 + 70 * ListProduct.Count;
        }
        private int Number;
        public string BillId { get; set; } = null;
        public string SellDay { get; }
        public string CustomerName { get; }
        public string CustomerPhone { get; }
        public decimal TotalAmount { get; }
        public string ComboName { get; }
        public ICollection<ProductBill> ListProduct { get; set; }
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
                try
                {
                    BillCombo bill = new()
                    {
                        ComboId = _combo.Id,
                        StaffId = _accountStore.CurrentAccount.Id,
                        CustomerName = CustomerName,
                        Phone = CustomerPhone,
                        PurchasedTime = DateTime.Now,
                        TotalAmount = TotalAmount,
                        Number = Number
                    };
                    BillId = (await _unitOfWork.BillCombos.Add(bill)).ToString();

                    OnPropertyChanged(nameof(BillId));
                    btnPrint.Visibility = Visibility.Hidden;
                    printDialog.PrintVisual(print, "In Hóa đơn");
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
