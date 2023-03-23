using Prism.Regions;
using System.Windows.Controls;

namespace ESM.Modules.Import.Views
{
    /// <summary>
    /// Interaction logic for RegisterView
    /// </summary>
    [RegionMemberLifetime(KeepAlive = false)]
    public partial class RegisterView : UserControl
    {
        public RegisterView()
        {
            InitializeComponent();
        }
    }
}
