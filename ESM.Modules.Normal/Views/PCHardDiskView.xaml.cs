using System.Windows.Controls;
using System.Windows.Input;

namespace ESM.Modules.Normal.Views
{
    /// <summary>
    /// Interaction logic for PCHardDiskView
    /// </summary>
    public partial class PCHardDiskView : UserControl
    {
        public PCHardDiskView()
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
