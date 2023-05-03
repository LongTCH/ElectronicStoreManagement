using Prism.Mvvm;
using Prism.Regions;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Windows.Data;

namespace ESM.Core
{
    class NavigationJournal
    {
        public string CurrentView;
        public NavigationJournal? back = null, forward = null;
    }
    public static class Extenstions
    {
        private static NavigationJournal? journal = null;
        public static void RequestNavigateContentRegionWithTrace(this IRegionManager regionManager, string view)
        {
            NavigationJournal node = new() { CurrentView = view };
            if (journal == null) journal = node;
            else
            {
                node.back = journal;
                journal.forward = node;
                journal = journal.forward;
            }
            regionManager.RequestNavigate(RegionNames.ContentRegion, view);
        }
        public static void RequestNavigateContentRegionWithTrace(this IRegionManager regionManager, string view, NavigationParameters parameter)
        {
            NavigationJournal node = new() { CurrentView = view };
            if (journal == null) journal = node;
            else
            {
                node.back = journal;
                journal.forward = node;
                journal = journal.forward;
            }
            regionManager.RequestNavigate(RegionNames.ContentRegion, view, parameter);
        }
        public static void GoBack(this IRegionManager regionManager)
        {
            if (journal == null || journal.back == null) return;
            journal = journal.back;
            regionManager.RequestNavigate(RegionNames.ContentRegion, journal.CurrentView);
        }
        public static void GoForward(this IRegionManager regionManager)
        {
            if (journal == null || journal.forward == null) return;
            journal = journal.forward;
            regionManager.RequestNavigate(RegionNames.ContentRegion, journal.CurrentView);
        }
        public static void ResetTrace(this IRegionManager regionManager)
        {
            journal = null;
        }
        public static void RegisterViewWithContentRegion<T>(this IRegionManager regionManager)
        {
            regionManager.RegisterViewWithRegion(RegionNames.ContentRegion, typeof(T));
            var view = regionManager.Regions[RegionNames.ContentRegion].Views.First(v => v.GetType().Equals(typeof(T)));
            regionManager.Regions[RegionNames.ContentRegion].Deactivate(view);
        }
        public static void ValidateProperty<TProp>(this BindableBase BASE ,TProp value, string name)
        {
            Validator.ValidateProperty(value, new ValidationContext(BASE, null, null)
            {
                MemberName = name
            });
        }
        public static void Refresh<T>(this ObservableCollection<T> value)
        {
            CollectionViewSource.GetDefaultView(value).Refresh();
        }
    }
}
