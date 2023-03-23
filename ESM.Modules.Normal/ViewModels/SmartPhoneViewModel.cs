using ESM.Core.ShareServices;
using ESM.Core.ShareStores;
using ESM.Modules.DataAccess.DTOs;
using ESM.Modules.DataAccess.Infrastructure;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ESM.Modules.Normal.ViewModels
{
    public class SmartPhoneViewModel : BaseProductViewModel<SmartphoneDTO>
    {
        public HashSet<ProductAttributeStore> CompanyList { get; set; }
        public HashSet<ProductAttributeStore> CPUList { get; set; }
        public HashSet<ProductAttributeStore> SeriesList { get; set; }
        public HashSet<ProductAttributeStore> RAMList { get; set; }
        public HashSet<ProductAttributeStore> StorageList { get; set; }
        public SmartPhoneViewModel(IUnitOfWork unitOfWork, IModalService modalService)
            : base(unitOfWork, modalService)
        {
            Action += OnIsCheckedChanged;
            getCompanyList();
            getCPUList();
            getRAMList();
            getSeriesList();
            getStorageList();
        }
        private void OnIsCheckedChanged()
        {
            List<string> ListCompany = new();
            List<string> ListCPU = new();
            List<string> ListRAM = new();
            List<string> ListSeries = new();
            List<string> ListStorage = new();
            foreach (var e in CompanyList)
                if (e.IsChecked) ListCompany.Add(e.Name);
            foreach (var e in CPUList)
                if (e.IsChecked) ListCPU.Add(e.Name);
            foreach (var e in RAMList)
                if (e.IsChecked) ListRAM.Add(e.Name);
            foreach (var e in SeriesList)
                if (e.IsChecked) ListSeries.Add(e.Name);
            foreach (var e in StorageList)
                if (e.IsChecked) ListStorage.Add(e.Name);
            if (ListCompany.Count != 0) ProductList = ((List<SmartphoneDTO>)_productDTOs).Where(x => ListCompany.Contains(x.Company)).ToList();
            else ProductList = (List<SmartphoneDTO>?)_productDTOs;
            if (ListCPU.Count != 0) ProductList = ((List<SmartphoneDTO>)ProductList).Where(x => ListCPU.Contains(x.Cpu)).ToList();
            if (ListRAM.Count != 0) ProductList = ((List<SmartphoneDTO>)ProductList).Where(x => ListRAM.Contains(x.Ram)).ToList();
            if (ListSeries.Count != 0) ProductList = ((List<SmartphoneDTO>)ProductList).Where(x => ListSeries.Contains(x.Series)).ToList();
            if (ListStorage.Count != 0) ProductList = ((List<SmartphoneDTO>)ProductList).Where(x => ListStorage.Contains(x.Storage)).ToList();
            RaisePropertyChanged(nameof(ProductList));
        }
        private void getCompanyList()
        {
            if (_productDTOs == null) return;
            CompanyList = new();
            foreach (var smartphone in _productDTOs)
            {
                ProductAttributeStore smartphoneCompany = new() { Name = smartphone.Company };
                smartphoneCompany.CurrentStoreChanged += OnIsCheckedChanged;
                CompanyList.Add(smartphoneCompany);
            }
        }
        private void getCPUList()
        {
            if (_productDTOs == null) return;
            CPUList = new();
            foreach (var smartphone in _productDTOs)
            {
                ProductAttributeStore smartphoneCPU = new() { Name = smartphone.Cpu };
                smartphoneCPU.CurrentStoreChanged += OnIsCheckedChanged;
                CPUList.Add(smartphoneCPU);
            }
        }
        private void getRAMList()
        {
            if (_productDTOs == null) return;
            RAMList = new();
            foreach (var smartphone in _productDTOs)
            {
                ProductAttributeStore smartphoneRAM = new() { Name = smartphone.Ram };
                smartphoneRAM.CurrentStoreChanged += OnIsCheckedChanged;
                RAMList.Add(smartphoneRAM);
            }
        }
        private void getSeriesList()
        {
            if (_productDTOs == null) return;
            SeriesList = new();
            foreach (var smartphone in _productDTOs)
            {
                if (string.IsNullOrWhiteSpace(smartphone.Series)) continue;
                ProductAttributeStore smartphoneSeries = new() { Name = smartphone.Series };
                smartphoneSeries.CurrentStoreChanged += OnIsCheckedChanged;
                SeriesList.Add(smartphoneSeries);
            }
        }
        private void getStorageList()
        {
            if (_productDTOs == null) return;
            StorageList = new();
            foreach (var smartphone in _productDTOs)
            {
                ProductAttributeStore smartphoneStorage = new() { Name = smartphone.Storage };
                smartphoneStorage.CurrentStoreChanged += OnIsCheckedChanged;
                StorageList.Add(smartphoneStorage);
            }
        }
    }
}
