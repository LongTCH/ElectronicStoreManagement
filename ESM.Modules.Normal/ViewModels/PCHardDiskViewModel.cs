using ESM.Core.ShareStores;
using ESM.Modules.DataAccess.DTOs;
using ESM.Modules.DataAccess.Infrastructure;
using System.Collections.Generic;
using System.Linq;

namespace ESM.Modules.Normal.ViewModels
{
    public class PCHardDiskViewModel : BaseProductViewModel<PcharddiskDTO>
    {
        public HashSet<ProductAttributeStore> CompanyList { get; set; }
        public HashSet<ProductAttributeStore> ConnectList { get; set; }
        public HashSet<ProductAttributeStore> TypeList { get; set; }
        public HashSet<ProductAttributeStore> SeriesList { get; set; }
        public HashSet<ProductAttributeStore> StorageList { get; set; }
        public PCHardDiskViewModel(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
            Action += OnIsCheckedChanged;
            getCompanyList();
            getConnectList();
            getTypeList();
            getStorageList();
            getSeriesList();
        }
        private void OnIsCheckedChanged()
        {
            List<string> ListCompany = new();
            List<string> ListConnect = new();
            List<string> ListType = new();
            List<string> ListStorage = new();
            List<string> ListSeries = new();
            foreach (var e in CompanyList)
                if (e.IsChecked) ListCompany.Add(e.Name);
            foreach (var e in ConnectList)
                if (e.IsChecked) ListConnect.Add(e.Name);
            foreach (var e in TypeList)
                if (e.IsChecked) ListType.Add(e.Name);
            foreach (var e in StorageList)
                if (e.IsChecked) ListStorage.Add(e.Name);
            foreach (var e in SeriesList)
                if (e.IsChecked) ListSeries.Add(e.Name);
            if (ListCompany.Count != 0) ProductList = ((List<PcharddiskDTO>)_productDTOs)!.Where(x => ListCompany.Contains(x.Company)).ToList();
            else ProductList = (List<PcharddiskDTO>)_productDTOs;
            if (ListConnect.Count != 0) ProductList = ((List<PcharddiskDTO>)ProductList)!.Where(x => ListConnect.Contains(x.Connect)).ToList();
            if (ListType.Count != 0) ProductList = ((List<PcharddiskDTO>)ProductList)!.Where(x => ListType.Contains(x.Type)).ToList();
            if (ListStorage.Count != 0) ProductList = ((List<PcharddiskDTO>)ProductList)!.Where(x => ListStorage.Contains(x.Storage)).ToList();
            if (ListSeries.Count != 0) ProductList = ((List<PcharddiskDTO>)ProductList)!.Where(x => ListSeries.Contains(x.Series)).ToList();
            RaisePropertyChanged(nameof(ProductList));
        }
        private void getCompanyList()
        {
            if (_productDTOs == null) return;
            CompanyList = new();
            foreach (var pcharddisk in _productDTOs)
            {
                ProductAttributeStore pcharddiskCompany = new() { Name = pcharddisk.Company };
                pcharddiskCompany.CurrentStoreChanged += OnIsCheckedChanged;
                CompanyList.Add(pcharddiskCompany);
            }
        }
        private void getConnectList()
        {
            if (_productDTOs == null) return;
            ConnectList = new();
            foreach (var pcharddisk in _productDTOs)
            {
                ProductAttributeStore pcharddiskCPU = new() { Name = pcharddisk.Connect };
                pcharddiskCPU.CurrentStoreChanged += OnIsCheckedChanged;
                ConnectList.Add(pcharddiskCPU);
            }
        }
        private void getTypeList()
        {
            if (_productDTOs == null) return;
            TypeList = new();
            foreach (var pcharddisk in _productDTOs)
            {
                if (string.IsNullOrWhiteSpace(pcharddisk.Type)) continue;
                ProductAttributeStore pcharddiskNeed = new() { Name = pcharddisk.Type };
                pcharddiskNeed.CurrentStoreChanged += OnIsCheckedChanged;
                TypeList.Add(pcharddiskNeed);
            }
        }
        private void getStorageList()
        {
            if (_productDTOs == null) return;
            StorageList = new();
            foreach (var pcharddisk in _productDTOs)
            {
                ProductAttributeStore pcharddiskRAM = new() { Name = pcharddisk.Storage };
                pcharddiskRAM.CurrentStoreChanged += OnIsCheckedChanged;
                StorageList.Add(pcharddiskRAM);
            }
        }
        private void getSeriesList()
        {
            if (_productDTOs == null) return;
            SeriesList = new();
            foreach (var pcharddisk in _productDTOs)
            {
                if (string.IsNullOrWhiteSpace(pcharddisk.Series)) continue;
                ProductAttributeStore pcharddiskSeries = new() { Name = pcharddisk.Series };
                pcharddiskSeries.CurrentStoreChanged += OnIsCheckedChanged;
                SeriesList.Add(pcharddiskSeries);
            }
        }
    }
}
