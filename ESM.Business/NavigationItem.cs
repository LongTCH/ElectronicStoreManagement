using System.Collections.ObjectModel;

namespace ESM.Business
{
    public class NavigationItem
    {
        public string Caption { get;set; }
        public string NavigationPath { get;set; }
        public ObservableCollection<NavigationItem> NavigationItems { get; set; }
    }
}
