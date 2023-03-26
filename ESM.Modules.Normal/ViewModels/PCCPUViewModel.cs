using ESM.Core.ShareServices;
using ESM.Core.ShareStores;
using ESM.Modules.DataAccess.DTOs;
using ESM.Modules.DataAccess.Infrastructure;
using Prism.Regions;
using System.Collections.Generic;
using System.Linq;

namespace ESM.Modules.Normal.ViewModels
{
    [RegionMemberLifetime(KeepAlive = false)]
    public class PCCPUViewModel : BaseProductViewModel<PccpuDTO>
    {
        public HashSet<ProductAttributeStore> CompanyList { get; set; } = new();
        public HashSet<ProductAttributeStore> NeedList { get; set; } = new();
        public HashSet<ProductAttributeStore> SocketList { get; set; } = new();
        public HashSet<ProductAttributeStore> SeriesList { get; set; } = new();
        public PCCPUViewModel(IUnitOfWork unitOfWork, IModalService modalService)
            : base(unitOfWork, modalService)
        {
            Action += OnIsCheckedChanged;
            getCompanyList();
            getSocketList();
            getNeedList();
            getSeriesList();
        }
        private void OnIsCheckedChanged()
        {
            List<string> ListCompany = new();
            List<string> ListSocket = new();
            List<string> ListNeed = new();
            List<string> ListSeries = new();
            foreach (var e in CompanyList)
                if (e.IsChecked) ListCompany.Add(e.Name);
            foreach (var e in SocketList)
                if (e.IsChecked) ListSocket.Add(e.Name);
            foreach (var e in NeedList)
                if (e.IsChecked) ListNeed.Add(e.Name);
            foreach (var e in SeriesList)
                if (e.IsChecked) ListSeries.Add(e.Name);
            if (ListCompany.Count != 0) ProductList = ((List<PccpuDTO>)ProductList).Where(x => ListCompany.Contains(x.Company)).ToList();
            if (ListSocket.Count != 0) ProductList = ((List<PccpuDTO>)ProductList).Where(x => ListSocket.Contains(x.Socket)).ToList();
            if (ListNeed.Count != 0) ProductList = ((List<PccpuDTO>)ProductList).Where(x => ListNeed.Contains(x.Need)).ToList();
            if (ListSeries.Count != 0) ProductList = ((List<PccpuDTO>)ProductList).Where(x => ListSeries.Contains(x.Series)).ToList();
        }
        private void getCompanyList()
        {
            if (_productDTOs == null) return;
            CompanyList = new();
            foreach (var pccpu in _productDTOs)
            {
                ProductAttributeStore pccpuCompany = new() { Name = pccpu.Company };
                pccpuCompany.CurrentStoreChanged += FilterProduct;
                CompanyList.Add(pccpuCompany);
            }
        }
        private void getSocketList()
        {
            if (_productDTOs == null) return;
            SocketList = new();
            foreach (var pccpu in _productDTOs)
            {
                ProductAttributeStore pccpusocket = new() { Name = pccpu.Socket };
                pccpusocket.CurrentStoreChanged += FilterProduct;
                SocketList.Add(pccpusocket);
            }
        }
        private void getNeedList()
        {
            if (_productDTOs == null) return;
            NeedList = new();
            foreach (var pccpu in _productDTOs)
            {
                if (string.IsNullOrWhiteSpace(pccpu.Need)) continue;
                ProductAttributeStore pccpuNeed = new() { Name = pccpu.Need };
                pccpuNeed.CurrentStoreChanged += FilterProduct;
                NeedList.Add(pccpuNeed);
            }
        }
        private void getSeriesList()
        {
            if (_productDTOs == null) return;
            SeriesList = new();
            foreach (var pccpu in _productDTOs)
            {
                if (string.IsNullOrWhiteSpace(pccpu.Series)) continue;
                ProductAttributeStore pccpuSeries = new() { Name = pccpu.Series };
                pccpuSeries.CurrentStoreChanged += FilterProduct;
                SeriesList.Add(pccpuSeries);
            }
        }
    }
}
