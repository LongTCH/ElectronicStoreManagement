using ESM.Core.ShareServices;
using ESM.Modules.Export.Views;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Media;

namespace ESM.Modules.Export.ViewModels
{
    public class InvoiceViewModel : BindableBase
    {
        private readonly IModalService _modalService;

        public InvoiceViewModel(IModalService modalService)
        {
            _modalService = modalService;
            PrintCommand = new(print);
            IsPrintShow = true;
        }
        public bool IsPrintShow { get;set; }
        public DelegateCommand<Visual> PrintCommand { get; }
        private void print(Visual print)
        {
            PrintDialog printDialog = new PrintDialog();
            if (printDialog.ShowDialog() == true)
            {
                IsPrintShow = false;
                RaisePropertyChanged(nameof(IsPrintShow));
                printDialog.PrintVisual(print, "In Hóa đơn");
                _modalService.ShowModal(ModalType.Information, "In thành công", "Thông báo");
            }
        }
    }
}
