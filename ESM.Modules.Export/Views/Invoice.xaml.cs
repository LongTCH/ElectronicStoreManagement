using ESM.Core.ShareServices;
using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ESM.Modules.Export.Views
{
    /// <summary>
    /// Interaction logic for Invoice.xaml
    /// </summary>
    public partial class Invoice : MetroWindow
    {
        private readonly IModalService _modalService;
        public Invoice(IModalService modalService)
        {
            _modalService = modalService;
            InitializeComponent();
        }

        private void btnPrint_Click(object sender, RoutedEventArgs e)
        {
            PrintDialog printDialog = new PrintDialog();
            if (printDialog.ShowDialog() == true)
            {
                btnPrint.Visibility = Visibility.Hidden;
                printDialog.PrintVisual(print, "In Hóa đơn");
                _modalService.ShowModal(ModalType.Information, "In thành công", "Thông báo");
            }
        }
    }
}
