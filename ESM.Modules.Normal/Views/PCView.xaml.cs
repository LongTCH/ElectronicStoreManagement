using System.Windows.Controls;
using System.Windows.Input;

namespace ESM.Modules.Normal.Views
{
    /// <summary>
    /// Interaction logic for PCView
    /// </summary>
    public partial class PCView : UserControl
    {
        public PCView()
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
