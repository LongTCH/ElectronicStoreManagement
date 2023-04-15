using System.Windows.Controls;
using System.Windows.Input;

namespace ESM.Modules.Import.Views
{
    /// <summary>
    /// Interaction logic for ComboInputView
    /// </summary>
    public partial class ComboInputView : UserControl
    {
        public ComboInputView()
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
