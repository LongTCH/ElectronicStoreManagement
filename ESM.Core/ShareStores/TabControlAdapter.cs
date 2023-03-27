using MahApps.Metro.Controls;
using Prism.Regions;
using System.Collections.Specialized;
using System.Linq;
using System.Windows;
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
                {
                    foreach (FrameworkElement view in e.NewItems)
                    {
                        var tabItem = new TabItem();
                        tabItem.Header = view.Tag;
                        tabItem.Content = view;
                        regionTarget.Items.Add(tabItem);

                        var navigationAware = view.DataContext as INavigationAware;
                        if (navigationAware != null)
                        {
                            navigationAware.OnNavigatedTo(new NavigationContext(region.NavigationService, null));
                        }
                    }
                }
                else if (e.Action == NotifyCollectionChangedAction.Remove)
                {
                    foreach (FrameworkElement view in e.OldItems)
                    {
                        var tabItem = regionTarget.Items.Cast<TabItem>().FirstOrDefault(ti => ti.Content == view);
                        if (tabItem != null)
                        {
                            var navigationAware = view.DataContext as INavigationAware;
                            if (navigationAware != null)
                            {
                                navigationAware.OnNavigatedFrom(new NavigationContext(region.NavigationService, null));
                            }

                            regionTarget.Items.Remove(tabItem);
                        }
                    }
                }
            };

            regionTarget.SelectionChanged += (s, e) =>
            {
                if (e.Source is TabControl tabControl)
                {
                    var deselectedTabItem = e.RemovedItems.Cast<TabItem>().FirstOrDefault();
                    if (deselectedTabItem != null && deselectedTabItem.Content is FrameworkElement deselectedView)
                    {
                        var deselectedNavigationAware = deselectedView.DataContext as INavigationAware;
                        if (deselectedNavigationAware != null)
                        {
                            deselectedNavigationAware.OnNavigatedFrom(new NavigationContext(region.NavigationService, null));
                        }
                    }

                    var selectedTabItem = e.AddedItems.Cast<TabItem>().FirstOrDefault();
                    if (selectedTabItem != null && selectedTabItem.Content is FrameworkElement selectedView)
                    {
                        var selectedNavigationAware = selectedView.DataContext as INavigationAware;
                        if (selectedNavigationAware != null)
                        {
                            selectedNavigationAware.OnNavigatedTo(new NavigationContext(region.NavigationService, null));
                        }
                    }
                }
            };
        }

        protected override IRegion CreateRegion()
        {
            return new AllActiveRegion();
        }
    }
}
