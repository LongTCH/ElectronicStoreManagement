using ESM.Core.ShareServices;
using ESM.Core.ShareStores;
using ESM.Modules.DataAccess.DTOs;
using ESM.Modules.DataAccess.Infrastructure;
using ESM.Modules.DataAccess.Models;
using Prism.Regions;
using System.Collections.Generic;
using System.Linq;

namespace ESM.Modules.Normal.ViewModels
{
    [RegionMemberLifetime(KeepAlive = false)]
    public class MonitorViewModel : BaseProductViewModel<Monitor>
    {
        public MonitorViewModel(IUnitOfWork unitOfWork, IModalService modalService)
            : base(unitOfWork, modalService)
        {
            Action += OnIsCheckedChanged;
            LoadAttribute += () =>
            {
                getCompanyList();
                getPanelList();
                getNeedList();
                getRefreshRateList();
                getSeriesList();
                getSizeList();
            };
        }
        private void OnIsCheckedChanged()
        {
            List<string> ListCompany = new();
            List<string> ListPanel = new();
            List<string> ListNeed = new();
            List<string> ListRefreshRate = new();
            List<string> ListSeries = new();
            List<string> ListSize = new();
            foreach (var e in CompanyList)
                if (e.IsChecked) ListCompany.Add(e.Name);
            foreach (var e in PanelList)
                if (e.IsChecked) ListPanel.Add(e.Name);
            foreach (var e in NeedList)
                if (e.IsChecked) ListNeed.Add(e.Name);
            foreach (var e in RefreshRateList)
                if (e.IsChecked) ListRefreshRate.Add(e.Name);
            foreach (var e in SeriesList)
                if (e.IsChecked) ListSeries.Add(e.Name);
            foreach (var e in SizeList)
                if (e.IsChecked) ListSize.Add(e.Name);
            if (ListCompany.Count != 0) ProductList = ((List<Monitor>)ProductList).Where(x => ListCompany.Contains(x.Company)).ToList();
            if (ListPanel.Count != 0) ProductList = ((List<Monitor>)ProductList).Where(x => ListPanel.Contains(x.Panel)).ToList();
            if (ListNeed.Count != 0) ProductList = ((List<Monitor>)ProductList).Where(x => IsInListNeed(ListNeed, x.Need)).ToList();
            if (ListRefreshRate.Count != 0) ProductList = ((List<Monitor>)ProductList).Where(x => ListRefreshRate.Contains(x.RefreshRate.ToString())).ToList();
            if (ListSeries.Count != 0) ProductList = ((List<Monitor>)ProductList).Where(x => ListSeries.Contains(x.Series)).ToList();
            if (ListSize.Count != 0) ProductList = ((List<Monitor>)ProductList).Where(x => ListSize.Contains(x.Size)).ToList();
        }
    }
}
