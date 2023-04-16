using ESM.Modules.Normal.ViewModels;
using MahApps.Metro.Controls;
using System.Windows.Controls;
using System.Windows.Input;

namespace ESM.Modules.Normal.Views
{
    /// <summary>
    /// Interaction logic for ProductDetailView
    /// </summary>
    public partial class ProductDetailView : MetroWindow
    {
        public ProductDetailView(ProductDetailViewModel context)
        {
            InitializeComponent();
            DataContext = context;
        }
        private void ScrollViewer_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            ScrollViewer viewer = sender as ScrollViewer;
            viewer.ScrollToVerticalOffset(viewer.VerticalOffset - e.Delta);
        }
    }
}
