using ESM.Core;
using ESM.Core.ShareServices;
using ESM.Core.ShareStores;
using ESM.Modules.DataAccess;
using ESM.Modules.DataAccess.Infrastructure;
using ESM.Modules.DataAccess.Models;
using ESM.Modules.Normal.Views;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ESM.Modules.Normal.ViewModels
{
    [RegionMemberLifetime(KeepAlive = false)]
    public abstract class BaseProductViewModel<T> : BindableBase, INavigationAware
    where T : ProductDTO
    {
        protected readonly IUnitOfWork _unitOfWork;
        protected readonly IModalService _modalService;
        protected dynamic _productDTOs;
        public dynamic ProductList { get; set; }
        public HashSet<ProductAttributeStore> CompanyList { get; set; } = new();
        public HashSet<ProductAttributeStore> CPUList { get; set; } = new();
        public HashSet<ProductAttributeStore> GraphicList { get; set; } = new();
        public HashSet<ProductAttributeStore> NeedList { get; set; } = new();
        public HashSet<ProductAttributeStore> SeriesList { get; set; } = new();
        public HashSet<ProductAttributeStore> RAMList { get; set; } = new();
        public HashSet<ProductAttributeStore> StorageList { get; set; } = new();
        public HashSet<ProductAttributeStore> PanelList { get; set; } = new();
        public HashSet<ProductAttributeStore> RefreshRateList { get; set; } = new();
        public HashSet<ProductAttributeStore> SizeList { get; set; } = new(); 
        public HashSet<ProductAttributeStore> SocketList { get; set; } = new();
        public HashSet<ProductAttributeStore> ConnectList { get; set; } = new();
        public HashSet<ProductAttributeStore> TypeList { get; set; } = new();
        public HashSet<ProductAttributeStore> ChipList { get; set; } = new();
        public HashSet<ProductAttributeStore> ChipsetList { get; set; } = new();
        public HashSet<ProductAttributeStore> VramList { get; set; } = new();
        public HashSet<ProductAttributeStore> GenList { get; set; } = new();
        public List<string> Conditions { get; } = StaticData.Conditions;
        private double maxPrice;
        public double MaxPrice
        {
            get => maxPrice;
            set => SetProperty(ref maxPrice, value);
        }
        public double TickFrequency { get; } = 500_000;
        private double upperValue;
        public double UpperValue
        {
            get => upperValue;
            set => SetProperty(ref upperValue, value, FilterProduct);
        }
        private double lowerValue;
        public double LowerValue
        {
            get => lowerValue;
            set => SetProperty(ref lowerValue, value, FilterProduct);
        }
        protected BaseProductViewModel(IUnitOfWork unitOfWork, IModalService modalService)
        {
            _unitOfWork = unitOfWork;
            _modalService = modalService;

            ProductDetailNavigateCommand = new(navigate);
        }
        protected Action LoadAttribute;
        private async Task GetProductList()
        {
            dynamic list = null;
            if (typeof(T).Equals(typeof(Laptop)))
                list = await _unitOfWork.Laptops.GetAll();
            else if (typeof(T).Equals(typeof(Monitor)))
                list = await _unitOfWork.Monitors.GetAll();
            else if (typeof(T).Equals(typeof(Pccpu)))
                list = await _unitOfWork.Pccpus.GetAll();
            else if (typeof(T).Equals(typeof(Pc)))
                list = await _unitOfWork.Pcs.GetAll();
            else if (typeof(T).Equals(typeof(Smartphone)))
                list = await _unitOfWork.Smartphones.GetAll();
            else if (typeof(T).Equals(typeof(Vga)))
                list = await _unitOfWork.Vgas.GetAll();
            else if (typeof(T).Equals(typeof(Pcharddisk)))
                list = await _unitOfWork.Pcharddisks.GetAll();
            if (list != null && list.Count > 0)
            {
                ProductList = list;
                _productDTOs = list;
                MaxPrice = Math.Ceiling((double)((List<T>)list).Max(x => x.SellPrice) / TickFrequency) * TickFrequency;
                UpperValue = MaxPrice;
                LoadAttribute?.Invoke();
            }
        }
        protected string selectedCondition;
        public string? SelectedCondition
        {
            get => selectedCondition;
            set => SetProperty(ref selectedCondition, value, FilterProduct);
        }
        public DelegateCommand<ProductDTO> ProductDetailNavigateCommand { get; set; }
        private void navigate(ProductDTO product)
        {
            //NavigationParameters parameter = new()
            //{
            //    {"Product" , product}
            //};
            //_modalService.ShowModal(ViewNames.ProductDetailView, parameter);
            new ProductDetailView(new ProductDetailViewModel(product)).Show();
        }
        protected Action Action { get; set; }
        protected void FilterProduct()
        {
            ProductList = _productDTOs;
            if (SelectedCondition == Conditions[1])
            {
                ProductList = ((List<T>)ProductList)?.OrderBy(x => x.SellPrice).ToList();
            }
            else if (SelectedCondition == Conditions[2])
            {
                ProductList = ((List<T>)ProductList)?.OrderByDescending(x => x.SellPrice).ToList();
            }
            else if (SelectedCondition == Conditions[3])
            {
                ProductList = ((List<T>)ProductList)?.OrderByDescending(x => x.Discount).ToList();
            }
            Action?.Invoke();
            ProductList = ((List<T>)ProductList)?
                .Where(x => (double)x.SellPrice <= UpperValue && (double)x.SellPrice >= LowerValue)
                .ToList();
            RaisePropertyChanged(nameof(ProductList));
        }
        protected bool IsInListNeed(List<string> listNeed, string need)
        {
            if (string.IsNullOrWhiteSpace(need)) return false;
            foreach (var item in listNeed)
            {
                if (need.Contains(item)) return true;
            }
            return false;
        }
        protected void getNeedList()
        {
            if (_productDTOs == null) return;
            NeedList = new();
            foreach (var laptop in _productDTOs)
            {
                if (string.IsNullOrWhiteSpace(laptop.Need)) continue;
                var listNeed = laptop.Need.Split(", ");
                foreach (var item in listNeed)
                {
                    ProductAttributeStore laptopNeed = new() { Name = item };
                    laptopNeed.CurrentStoreChanged += FilterProduct;
                    NeedList.Add(laptopNeed);
                }
            }
            RaisePropertyChanged(nameof(NeedList));
        }
        protected void getCompanyList()
        {
            if (_productDTOs == null) return;
            CompanyList = new();
            foreach (var laptop in _productDTOs)
            {
                ProductAttributeStore laptopCompany = new() { Name = laptop.Company };
                laptopCompany.CurrentStoreChanged += FilterProduct;
                CompanyList.Add(laptopCompany);
            }
            RaisePropertyChanged(nameof(CompanyList));
        }
        protected void getCPUList()
        {
            if (_productDTOs == null) return;
            CPUList = new();
            foreach (var laptop in _productDTOs)
            {
                ProductAttributeStore laptopCPU = new() { Name = laptop.Cpu };
                laptopCPU.CurrentStoreChanged += FilterProduct;
                CPUList.Add(laptopCPU);
            }
            RaisePropertyChanged(nameof(CPUList));
        }
        protected void getGraphicList()
        {
            if (_productDTOs == null) return;
            GraphicList = new();
            foreach (var laptop in _productDTOs)
            {
                ProductAttributeStore laptopGraphic = new() { Name = laptop.Graphic };
                laptopGraphic.CurrentStoreChanged += FilterProduct;
                GraphicList.Add(laptopGraphic);
            }
            RaisePropertyChanged(nameof(GraphicList));
        }
        protected void getRAMList()
        {
            if (_productDTOs == null) return;
            RAMList = new();
            foreach (var laptop in _productDTOs)
            {
                ProductAttributeStore laptopRAM = new() { Name = laptop.Ram };
                laptopRAM.CurrentStoreChanged += FilterProduct;
                RAMList.Add(laptopRAM);
            }
            RaisePropertyChanged(nameof(RAMList));
        }
        protected void getSeriesList()
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
            RaisePropertyChanged(nameof(SeriesList));
        }
        protected void getStorageList()
        {
            if (_productDTOs == null) return;
            StorageList = new();
            foreach (var laptop in _productDTOs)
            {
                ProductAttributeStore laptopStorage = new() { Name = laptop.Storage };
                laptopStorage.CurrentStoreChanged += FilterProduct;
                StorageList.Add(laptopStorage);
            }
            RaisePropertyChanged(nameof(StorageList));
        }
        protected void getPanelList()
        {
            if (_productDTOs == null) return;
            PanelList = new();
            foreach (var monitor in _productDTOs)
            {
                ProductAttributeStore monitorCPU = new() { Name = monitor.Panel };
                monitorCPU.CurrentStoreChanged += FilterProduct;
                PanelList.Add(monitorCPU);
            }
            RaisePropertyChanged(nameof(PanelList));
        }
        protected void getRefreshRateList()
        {
            if (_productDTOs == null) return;
            RefreshRateList = new();
            foreach (var monitor in _productDTOs)
            {
                ProductAttributeStore monitorRAM = new() { Name = monitor.RefreshRate.ToString() };
                monitorRAM.CurrentStoreChanged += FilterProduct;
                RefreshRateList.Add(monitorRAM);
            }
            RaisePropertyChanged(nameof(RefreshRateList));
        }
        protected void getSizeList()
        {
            if (_productDTOs == null) return;
            SizeList = new();
            foreach (var monitor in _productDTOs)
            {
                ProductAttributeStore monitorStorage = new() { Name = monitor.Size };
                monitorStorage.CurrentStoreChanged += FilterProduct;
                SizeList.Add(monitorStorage);
            }
            RaisePropertyChanged(nameof(SizeList));
        }
        protected void getSocketList()
        {
            if (_productDTOs == null) return;
            SocketList = new();
            foreach (var pccpu in _productDTOs)
            {
                ProductAttributeStore pccpusocket = new() { Name = pccpu.Socket };
                pccpusocket.CurrentStoreChanged += FilterProduct;
                SocketList.Add(pccpusocket);
            }
            RaisePropertyChanged(nameof(SocketList));
        }
        protected void getConnectList()
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
        protected void getTypeList()
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
        protected void getChipList()
        {
            if (_productDTOs == null) return;
            ChipList = new();
            foreach (var vga in _productDTOs)
            {
                ProductAttributeStore vgaCPU = new() { Name = vga.Chip };
                vgaCPU.CurrentStoreChanged += FilterProduct;
                ChipList.Add(vgaCPU);
            }
            RaisePropertyChanged(nameof(ChipList));
        }
        protected void getChipsetList()
        {
            if (_productDTOs == null) return;
            ChipsetList = new();
            foreach (var vga in _productDTOs)
            {
                ProductAttributeStore vgaGraphic = new() { Name = vga.Chipset };
                vgaGraphic.CurrentStoreChanged += FilterProduct;
                ChipsetList.Add(vgaGraphic);
            }
            RaisePropertyChanged(nameof(ChipsetList));
        }
        protected void getVramList()
        {
            if (_productDTOs == null) return;
            VramList = new();
            foreach (var vga in _productDTOs)
            {
                ProductAttributeStore vgaVram = new() { Name = vga.Vram };
                vgaVram.CurrentStoreChanged += FilterProduct;
                VramList.Add(vgaVram);
            }
            RaisePropertyChanged(nameof(VramList));
        }
        protected void getGenList()
        {
            if (_productDTOs == null) return;
            GenList = new();
            foreach (var vga in _productDTOs)
            {
                ProductAttributeStore vgaGen = new() { Name = vga.Gen };
                vgaGen.CurrentStoreChanged += FilterProduct;
                GenList.Add(vgaGen);
            }
            RaisePropertyChanged(nameof(GenList));
        }
        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            GetProductList().Await();
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return false;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {

        }
    }
}
