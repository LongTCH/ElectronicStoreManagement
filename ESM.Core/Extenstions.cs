using ESM.Core.ShareServices;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

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

    }
}
