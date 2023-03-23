using MahApps.Metro.Controls;
using Prism.Regions;
using System.Collections.Specialized;
using System.Linq;
using System.Windows.Controls;

namespace ESM.Core.ShareStores
{
    public class TabControlAdapter : RegionAdapterBase<TabControl>
    {
        public TabControlAdapter(IRegionBehaviorFactory regionBehaviorFactory) : base(regionBehaviorFactory)
        {
        }

        protected override void Adapt(IRegion region, TabControl regionTarget)
        {
            region.Views.CollectionChanged += (s, e) =>
            {
                if (e.Action == NotifyCollectionChangedAction.Add)
                    foreach (UserControl item in e.NewItems)
                    {
                        regionTarget.Items.Add(new TabItem { Header = item.Tag, Content = item });
                    }
                else if (e.Action == NotifyCollectionChangedAction.Remove)
                    foreach (UserControl item in e.OldItems)
                    {
                        var tabTodelete = regionTarget.Items.OfType<TabItem>().FirstOrDefault(n => n.Content == item);
                        regionTarget.Items.Remove(tabTodelete);
                    }

            };
        }


        protected override IRegion CreateRegion()
        {
            return new SingleActiveRegion();
        }
    }

}
