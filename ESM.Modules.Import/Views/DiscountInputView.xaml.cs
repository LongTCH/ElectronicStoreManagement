using System.Windows.Controls;
using System.Windows.Input;

namespace ESM.Modules.Import.Views
{
    /// <summary>
    /// Interaction logic for DiscountInputView
    /// </summary>
    public partial class DiscountInputView : UserControl
    {
        public DiscountInputView()
        {
            InitializeComponent();
        }
        private void ScrollViewer_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            ScrollViewer viewer = sender as ScrollViewer;
            viewer.ScrollToVerticalOffset(viewer.VerticalOffset - e.Delta);
        }
    }
}
