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
        public HashSet<ProductAttributeStore> CompanyList { get; set; } = new();
        public HashSet<ProductAttributeStore> NeedList { get; set; }=new();
        public HashSet<ProductAttributeStore> PanelList { get; set; } = new();
        public HashSet<ProductAttributeStore> SeriesList { get; set; } = new();
        public HashSet<ProductAttributeStore> RefreshRateList { get; set; } = new();
        public HashSet<ProductAttributeStore> SizeList { get; set; } = new();
        public MonitorViewModel(IUnitOfWork unitOfWork, IModalService modalService)
            : base(unitOfWork, modalService)
        {
            Action += OnIsCheckedChanged;
            getCompanyList();
            getPanelList();
            getNeedList();
            getRefreshRateList();
            getSeriesList();
            getSizeList();
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
            if (ListNeed.Count != 0) ProductList = ((List<Monitor>)ProductList).Where(x => ListNeed.Contains(x.Need)).ToList();
            if (ListRefreshRate.Count != 0) ProductList = ((List<Monitor>)ProductList).Where(x => ListRefreshRate.Contains(x.RefreshRate.ToString())).ToList();
            if (ListSeries.Count != 0) ProductList = ((List<Monitor>)ProductList).Where(x => ListSeries.Contains(x.Series)).ToList();
            if (ListSize.Count != 0) ProductList = ((List<Monitor>)ProductList).Where(x => ListSize.Contains(x.Size)).ToList();
        }
        private void getCompanyList()
        {
            if (_productDTOs == null) return;
            CompanyList = new();
            foreach (var monitor in _productDTOs)
            {
                ProductAttributeStore monitorCompany = new() { Name = monitor.Company };
                monitorCompany.CurrentStoreChanged += FilterProduct;
                CompanyList.Add(monitorCompany);
            }
        }
        private void getPanelList()
        {
            if (_productDTOs == null) return;
            PanelList = new();
            foreach (var monitor in _productDTOs)
            {
                ProductAttributeStore monitorCPU = new() { Name = monitor.Panel };
                monitorCPU.CurrentStoreChanged += OnIsCheckedChanged;
                PanelList.Add(monitorCPU);
            }
        }
        private void getNeedList()
        {
            if (_productDTOs == null) return;
            NeedList = new();
            foreach (var monitor in _productDTOs)
            {
                if (string.IsNullOrWhiteSpace(monitor.Need)) continue;
                ProductAttributeStore monitorNeed = new() { Name = monitor.Need };
                monitorNeed.CurrentStoreChanged += FilterProduct;
                NeedList.Add(monitorNeed);
            }
        }
        private void getRefreshRateList()
        {
            if (_productDTOs == null) return;
            RefreshRateList = new();
            foreach (var monitor in _productDTOs)
            {
                ProductAttributeStore monitorRAM = new() { Name = monitor.RefreshRate.ToString() };
                monitorRAM.CurrentStoreChanged += FilterProduct;
                RefreshRateList.Add(monitorRAM);
            }
        }
        private void getSeriesList()
        {
            if (_productDTOs == null) return;
            SeriesList = new();
            foreach (var monitor in _productDTOs)
            {
                if (string.IsNullOrWhiteSpace(monitor.Series)) continue;
                ProductAttributeStore monitorSeries = new() { Name = monitor.Series };
                monitorSeries.CurrentStoreChanged += FilterProduct;
                SeriesList.Add(monitorSeries);
            }
        }
        private void getSizeList()
        {
            if (_productDTOs == null) return;
            SizeList = new();
            foreach (var monitor in _productDTOs)
            {
                ProductAttributeStore monitorStorage = new() { Name = monitor.Size };
                monitorStorage.CurrentStoreChanged += FilterProduct;
                SizeList.Add(monitorStorage);
            }
        }
    }
}
