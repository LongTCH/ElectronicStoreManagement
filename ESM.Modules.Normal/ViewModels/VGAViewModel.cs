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
    public class VGAViewModel : BaseProductViewModel<VgaDTO>
    {
        public HashSet<ProductAttributeStore> CompanyList { get; set; } = new();
        public HashSet<ProductAttributeStore> ChipList { get; set; } = new();
        public HashSet<ProductAttributeStore> ChipsetList { get; set; } = new();
        public HashSet<ProductAttributeStore> NeedList { get; set; } = new();
        public HashSet<ProductAttributeStore> SeriesList { get; set; } = new();
        public HashSet<ProductAttributeStore> VramList { get; set; } = new();
        public HashSet<ProductAttributeStore> GenList { get; set; } = new();

        public VGAViewModel(IUnitOfWork unitOfWork, IModalService modalService)
            : base(unitOfWork, modalService)
        {
            Action += OnIsCheckedChanged;
            getCompanyList();
            getChipList();
            getChipsetList();
            getNeedList();
            getVramList();
            getSeriesList();
            getGenList();
        }
        private void OnIsCheckedChanged()
        {
            List<string> ListCompany = new();
            List<string> ListChip = new();
            List<string> ListChipset = new();
            List<string> ListNeed = new();
            List<string> ListVram = new();
            List<string> ListSeries = new();
            List<string> ListGen = new();
            foreach (var e in CompanyList)
                if (e.IsChecked) ListCompany.Add(e.Name);
            foreach (var e in ChipList)
                if (e.IsChecked) ListChip.Add(e.Name);
            foreach (var e in ChipsetList)
                if (e.IsChecked) ListChipset.Add(e.Name);
            foreach (var e in NeedList)
                if (e.IsChecked) ListNeed.Add(e.Name);
            foreach (var e in VramList)
                if (e.IsChecked) ListVram.Add(e.Name);
            foreach (var e in SeriesList)
                if (e.IsChecked) ListSeries.Add(e.Name);
            foreach (var e in GenList)
                if (e.IsChecked) ListGen.Add(e.Name);
            if (ListCompany.Count != 0) ProductList = ((List<VgaDTO>?)ProductList).Where(x => ListCompany.Contains(x.Company)).ToList();
            if (ListChip.Count != 0) ProductList = ((List<VgaDTO>?)ProductList).Where(x => ListChip.Contains(x.Chip)).ToList();
            if (ListChipset.Count != 0) ProductList = ((List<VgaDTO>?)ProductList).Where(x => ListChipset.Contains(x.Chipset)).ToList();
            if (ListNeed.Count != 0) ProductList = ((List<VgaDTO>?)ProductList).Where(x => ListNeed.Contains(x.Need)).ToList();
            if (ListVram.Count != 0) ProductList = ((List<VgaDTO>?)ProductList).Where(x => ListVram.Contains(x.Vram)).ToList();
            if (ListSeries.Count != 0) ProductList = ((List<VgaDTO>?)ProductList).Where(x => ListSeries.Contains(x.Series)).ToList();
            if (ListGen.Count != 0) ProductList = ((List<VgaDTO>?)ProductList).Where(x => ListGen.Contains(x.Gen)).ToList();
        }
        private void getCompanyList()
        {
            if (_productDTOs == null) return;
            CompanyList = new();
            foreach (var vga in _productDTOs)
            {
                ProductAttributeStore vgaCompany = new() { Name = vga.Company };
                vgaCompany.CurrentStoreChanged += FilterProduct;
                CompanyList.Add(vgaCompany);
            }
        }
        private void getChipList()
        {
            if (_productDTOs == null) return;
            ChipList = new();
            foreach (var vga in _productDTOs)
            {
                ProductAttributeStore vgaCPU = new() { Name = vga.Chip };
                vgaCPU.CurrentStoreChanged += FilterProduct;
                ChipList.Add(vgaCPU);
            }
        }
        private void getChipsetList()
        {
            if (_productDTOs == null) return;
            ChipsetList = new();
            foreach (var vga in _productDTOs)
            {
                ProductAttributeStore vgaGraphic = new() { Name = vga.Chipset };
                vgaGraphic.CurrentStoreChanged += FilterProduct;
                ChipsetList.Add(vgaGraphic);
            }
        }
        private void getNeedList()
        {
            if (_productDTOs == null) return;
            NeedList = new();
            foreach (var vga in _productDTOs)
            {
                if (string.IsNullOrWhiteSpace(vga.Need)) continue;
                ProductAttributeStore vgaNeed = new() { Name = vga.Need };
                vgaNeed.CurrentStoreChanged += FilterProduct;
                NeedList.Add(vgaNeed);
            }
        }
        private void getVramList()
        {
            if (_productDTOs == null) return;
            VramList = new();
            foreach (var vga in _productDTOs)
            {
                ProductAttributeStore vgaVram = new() { Name = vga.Vram };
                vgaVram.CurrentStoreChanged += FilterProduct;
                VramList.Add(vgaVram);
            }
        }
        private void getSeriesList()
        {
            if (_productDTOs == null) return;
            SeriesList = new();
            foreach (var vga in _productDTOs)
            {
                if (string.IsNullOrWhiteSpace(vga.Series)) continue;
                ProductAttributeStore vgaSeries = new() { Name = vga.Series };
                vgaSeries.CurrentStoreChanged += FilterProduct;
                SeriesList.Add(vgaSeries);
            }
        }
        private void getGenList()
        {
            if (_productDTOs == null) return;
            GenList = new();
            foreach (var vga in _productDTOs)
            {
                ProductAttributeStore vgaGen = new() { Name = vga.Gen };
                vgaGen.CurrentStoreChanged += FilterProduct;
                GenList.Add(vgaGen);
            }
        }
    }
}
