using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace ESM.Modules.Import.Views
{
    /// <summary>
    /// Interaction logic for AccountManage
    /// </summary>
    public partial class AccountManage : UserControl
    {
        public AccountManage()
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
    public class GenderConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var selectedGender = value as bool?;
            if (selectedGender == null)
                return null;
            return selectedGender.Value ? "Nam" : "Nữ";
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value.ToString() == "Nam";
        }
    }
}
