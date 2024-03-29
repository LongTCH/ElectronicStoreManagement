﻿using ESM.Core.ShareServices;
using Prism.Mvvm;
using System;

namespace ESM.Modules.Export.Utilities
{
    public class ProductBill : BindableBase
    {
        private readonly IModalService _modalService;

        public ProductBill(IModalService modalService)
        {
            _modalService = modalService;
        }

        public string Id { get; set; }
        public int Remain { get; set; }
        private string? number;
        public string? Number
        {
            get => number;
            set
            {
                number = checkValidNumber(value).ToString();
                RaisePropertyChanged(nameof(Number));
                RaisePropertyChanged(nameof(Amount));
                Action?.Invoke();
            }
        }
        public string Name { get; set; }
        public decimal SellPrice { get; set; }
        private string importPrice;
        public string ImportPrice
        {
            get => importPrice;
            set
            {
                importPrice = checkValidPrice(value).ToString();
                RaisePropertyChanged(nameof(ImportPrice));
                RaisePropertyChanged(nameof(ImportAmount));
                Action?.Invoke();
            }
        }
        public string Unit { get; set; }
        public decimal Amount => SellPrice * Convert.ToInt32(Number);
        public decimal ImportAmount => Convert.ToDecimal(ImportPrice) * Convert.ToInt32(Number);
        public string? Warranty { get; set; }
        public Action? Action { get; set; }
        public int checkValidNumber(string? s)
        {
            if (!int.TryParse(s, out int n))
            {
                _modalService.ShowModal(ModalType.Error, "Không đúng định dạng", "Lỗi");
            }
            else if (n <= 0)
            {
                _modalService.ShowModal(ModalType.Error, "Số lượng phải lớn hơn 0", "Lỗi");
            }
            else if (n > Remain)
            {
                _modalService.ShowModal(ModalType.Error, $"Chỉ còn {Remain} sản phẩm", "Không đủ sản phẩm");
                return Remain;
            }
            return (n == 0) ? 1 : n;
        }
        public decimal checkValidPrice(string? s)
        {
            if (!decimal.TryParse(s, out decimal n))
            {
                _modalService.ShowModal(ModalType.Error, "Không đúng định dạng", "Lỗi");
            }
            return n;
        }
    }
}
