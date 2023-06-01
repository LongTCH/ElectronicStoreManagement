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

namespace ESM.Modules.Import.Views
{
    /// <summary>
    /// Interaction logic for ProviderView.xaml
    /// </summary>
    public partial class ProviderView : UserControl
    {
        public ProviderView()
        {
            InitializeComponent();
        }
        bool IsControlPressed = false;
        private void DataGridCell_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            DataGridCell cell = sender as DataGridCell;
            if (cell != null && cell.IsSelected == true)
            {
                if (e.Key == Key.LeftCtrl || e.Key == Key.RightCtrl)
                {
                    IsControlPressed = true;
                }
                if (IsControlPressed && e.Key == Key.C)
                {
                    if (cell.Content is TextBlock)
                        Clipboard.SetText((cell.Content as TextBlock).Text);
                    IsControlPressed = false;
                    e.Handled = true;
                }
            }
        }
    }
}
