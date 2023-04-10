using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ESM.Modules.Import.Views
{
    /// <summary>
    /// Interaction logic for VGAInputView
    /// </summary>
    public partial class VGAInputView : UserControl
    {
        public VGAInputView()
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
