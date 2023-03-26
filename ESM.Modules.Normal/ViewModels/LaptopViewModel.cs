using ESM.Core.ShareServices;
using ESM.Core.ShareStores;
using ESM.Modules.DataAccess.DTOs;
using ESM.Modules.DataAccess.Infrastructure;
using System.Collections.Generic;
using System.Linq;

namespace ESM.Modules.Normal.ViewModels
{
    public class LaptopViewModel : BaseProductViewModel<LaptopDTO>
    {
        public HashSet<ProductAttributeStore> CompanyList { get; set; } = new();
        public HashSet<ProductAttributeStore> CPUList { get; set; }=new();
        public HashSet<ProductAttributeStore> GraphicList { get; set; } = new();
        public HashSet<ProductAttributeStore> NeedList { get; set; } = new();
        public HashSet<ProductAttributeStore> SeriesList { get; set; } = new();
        public HashSet<ProductAttributeStore> RAMList { get; set; } = new();
        public HashSet<ProductAttributeStore> StorageList { get; set; } = new();

        public LaptopViewModel(IUnitOfWork unitOfWork, IModalService modalService)
            : base(unitOfWork, modalService)
        {
                Action += OnIsCheckedChanged;
                getCompanyList();
                getCPUList();
                getGraphicList();
                getNeedList();
                getRAMList();
                getSeriesList();
                getStorageList();

        }
        private void OnIsCheckedChanged()
        {
            List<string> ListCompany = new();
            List<string> ListCPU = new();
            List<string> ListGraphic = new();
            List<string> ListNeed = new();
            List<string> ListRAM = new();
            List<string> ListSeries = new();
            List<string> ListStorage = new();
            foreach (var e in CompanyList)
                if (e.IsChecked) ListCompany.Add(e.Name);
            foreach (var e in CPUList)
                if (e.IsChecked) ListCPU.Add(e.Name);
            foreach (var e in GraphicList)
                if (e.IsChecked) ListGraphic.Add(e.Name);
            foreach (var e in NeedList)
                if (e.IsChecked) ListNeed.Add(e.Name);
            foreach (var e in RAMList)
                if (e.IsChecked) ListRAM.Add(e.Name);
            foreach (var e in SeriesList)
                if (e.IsChecked) ListSeries.Add(e.Name);
            foreach (var e in StorageList)
                if (e.IsChecked) ListStorage.Add(e.Name);
            if (ListCompany.Count != 0) ProductList = ((List<LaptopDTO>)ProductList).Where(x => ListCompany.Contains(x.Company)).ToList();
            if (ListCPU.Count != 0) ProductList = ((List<LaptopDTO>)ProductList).Where(x => ListCPU.Contains(x.Cpu)).ToList();
            if (ListGraphic.Count != 0) ProductList = ((List<LaptopDTO>)ProductList).Where(x => ListGraphic.Contains(x.Graphic)).ToList();
            if (ListNeed.Count != 0) ProductList = ((List<LaptopDTO>)ProductList).Where(x => ListNeed.Contains(x.Need)).ToList();
            if (ListRAM.Count != 0) ProductList = ((List<LaptopDTO>)ProductList).Where(x => ListRAM.Contains(x.Ram)).ToList();
            if (ListSeries.Count != 0) ProductList = ((List<LaptopDTO>)ProductList).Where(x => ListSeries.Contains(x.Series)).ToList();
            if (ListStorage.Count != 0) ProductList = ((List<LaptopDTO>)ProductList).Where(x => ListStorage.Contains(x.Storage)).ToList();
        }
        private void getCompanyList()
        {
            if (_productDTOs == null) return;
            CompanyList = new();
            foreach (var laptop in _productDTOs)
            {
                ProductAttributeStore laptopCompany = new() { Name = laptop.Company };
                laptopCompany.CurrentStoreChanged += FilterProduct;
                CompanyList.Add(laptopCompany);
            }
        }
        private void getCPUList()
        {
            if (_productDTOs == null) return;
            CPUList = new();
            foreach (var laptop in _productDTOs)
            {
                ProductAttributeStore laptopCPU = new() { Name = laptop.Cpu };
                laptopCPU.CurrentStoreChanged += FilterProduct;
                CPUList.Add(laptopCPU);
            }
        }
        private void getGraphicList()
        {
            if (_productDTOs == null) return;
            GraphicList = new();
            foreach (var laptop in _productDTOs)
            {
                ProductAttributeStore laptopGraphic = new() { Name = laptop.Graphic };
                laptopGraphic.CurrentStoreChanged += FilterProduct;
                GraphicList.Add(laptopGraphic);
            }
        }
        private void getNeedList()
        {
            if (_productDTOs == null) return;
            NeedList = new();
            foreach (var laptop in _productDTOs)
            {
                if (string.IsNullOrWhiteSpace(laptop.Need)) continue;
                ProductAttributeStore laptopNeed = new() { Name = laptop.Need };
                laptopNeed.CurrentStoreChanged += OnIsCheckedChanged;
                NeedList.Add(laptopNeed);
            }
        }
        private void getRAMList()
        {
            if (_productDTOs == null) return;
            RAMList = new();
            foreach (var laptop in _productDTOs)
            {
                ProductAttributeStore laptopRAM = new() { Name = laptop.Ram };
                laptopRAM.CurrentStoreChanged += FilterProduct;
                RAMList.Add(laptopRAM);
            }
        }
        private void getSeriesList()
        {
            if (_productDTOs == null) return;
            SeriesList = new();
            foreach (var laptop in _productDTOs)
            {
                if (string.IsNullOrWhiteSpace(laptop.Series)) continue;
                ProductAttributeStore laptopSeries = new() { Name = laptop.Series };
                laptopSeries.CurrentStoreChanged += FilterProduct;
                SeriesList.Add(laptopSeries);
            }
        }
        private void getStorageList()
        {
            if (_productDTOs == null) return;
            StorageList = new();
            foreach (var laptop in _productDTOs)
            {
                ProductAttributeStore laptopStorage = new() { Name = laptop.Storage };
                laptopStorage.CurrentStoreChanged += FilterProduct;
                StorageList.Add(laptopStorage);
            }
        }
    }
}
