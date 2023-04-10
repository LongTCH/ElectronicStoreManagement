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
    public class PCHardDiskViewModel : BaseProductViewModel<Pcharddisk>
    {
        public HashSet<ProductAttributeStore> CompanyList { get; set; } = new();
        public HashSet<ProductAttributeStore> ConnectList { get; set; }=new();
        public HashSet<ProductAttributeStore> TypeList { get; set; } = new();
        public HashSet<ProductAttributeStore> SeriesList { get; set; } = new();
        public HashSet<ProductAttributeStore> StorageList { get; set; } = new();
        public PCHardDiskViewModel(IUnitOfWork unitOfWork, IModalService modalService)
            : base(unitOfWork, modalService)
        {
            Action += OnIsCheckedChanged;
            LoadAttribute += () =>
            {
                getCompanyList();
                getConnectList();
                getTypeList();
                getStorageList();
                getSeriesList();
            };
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
            if (ListCompany.Count != 0) ProductList = ((List<Pcharddisk>)ProductList)!.Where(x => ListCompany.Contains(x.Company)).ToList();
            if (ListConnect.Count != 0) ProductList = ((List<Pcharddisk>)ProductList)!.Where(x => ListConnect.Contains(x.Connect)).ToList();
            if (ListType.Count != 0) ProductList = ((List<Pcharddisk>)ProductList)!.Where(x => ListType.Contains(x.Type)).ToList();
            if (ListStorage.Count != 0) ProductList = ((List<Pcharddisk>)ProductList)!.Where(x => ListStorage.Contains(x.Storage)).ToList();
            if (ListSeries.Count != 0) ProductList = ((List<Pcharddisk>)ProductList)!.Where(x => ListSeries.Contains(x.Series)).ToList();
        }
        private void getCompanyList()
        {
            if (_productDTOs == null) return;
            CompanyList = new();
            foreach (var pcharddisk in _productDTOs)
            {
                ProductAttributeStore pcharddiskCompany = new() { Name = pcharddisk.Company };
                pcharddiskCompany.CurrentStoreChanged += FilterProduct;
                CompanyList.Add(pcharddiskCompany);
            }
            RaisePropertyChanged(nameof(CompanyList));
        }
        private void getConnectList()
        {
            if (_productDTOs == null) return;
            ConnectList = new();
            foreach (var pcharddisk in _productDTOs)
            {
                ProductAttributeStore pcharddiskCPU = new() { Name = pcharddisk.Connect };
                pcharddiskCPU.CurrentStoreChanged += FilterProduct;
                ConnectList.Add(pcharddiskCPU);
            }
            RaisePropertyChanged(nameof(ConnectList));
        }
        private void getTypeList()
        {
            if (_productDTOs == null) return;
            TypeList = new();
            foreach (var pcharddisk in _productDTOs)
            {
                if (string.IsNullOrWhiteSpace(pcharddisk.Type)) continue;
                ProductAttributeStore pcharddiskNeed = new() { Name = pcharddisk.Type };
                pcharddiskNeed.CurrentStoreChanged += FilterProduct;
                TypeList.Add(pcharddiskNeed);
            }
            RaisePropertyChanged(nameof(TypeList));
        }
        private void getStorageList()
        {
            if (_productDTOs == null) return;
            StorageList = new();
            foreach (var pcharddisk in _productDTOs)
            {
                ProductAttributeStore pcharddiskRAM = new() { Name = pcharddisk.Storage };
                pcharddiskRAM.CurrentStoreChanged += FilterProduct;
                StorageList.Add(pcharddiskRAM);
            }
            RaisePropertyChanged(nameof(StorageList));
        }
        private void getSeriesList()
        {
            if (_productDTOs == null) return;
            SeriesList = new();
            foreach (var pcharddisk in _productDTOs)
            {
                if (string.IsNullOrWhiteSpace(pcharddisk.Series)) continue;
                ProductAttributeStore pcharddiskSeries = new() { Name = pcharddisk.Series };
                pcharddiskSeries.CurrentStoreChanged += FilterProduct;
                SeriesList.Add(pcharddiskSeries);
            }
            RaisePropertyChanged(nameof(SeriesList));
        }
    }
}
