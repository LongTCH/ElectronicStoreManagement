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
    public class VGAViewModel : BaseProductViewModel<Vga>
    {
        public VGAViewModel(IUnitOfWork unitOfWork, IModalService modalService)
            : base(unitOfWork, modalService)
        {
            Action += OnIsCheckedChanged;
            LoadAttribute += () =>
            {
                getCompanyList();
                getChipList();
                getChipsetList();
                getVramList();
                getSeriesList();
                getGenList();
            };
        }
        private void OnIsCheckedChanged()
        {
            List<string> ListCompany = new();
            List<string> ListChip = new();
            List<string> ListChipset = new();
            List<string> ListVram = new();
            List<string> ListSeries = new();
            List<string> ListGen = new();
            foreach (var e in CompanyList)
                if (e.IsChecked) ListCompany.Add(e.Name);
            foreach (var e in ChipList)
                if (e.IsChecked) ListChip.Add(e.Name);
            foreach (var e in ChipsetList)
                if (e.IsChecked) ListChipset.Add(e.Name);
            foreach (var e in VramList)
                if (e.IsChecked) ListVram.Add(e.Name);
            foreach (var e in SeriesList)
                if (e.IsChecked) ListSeries.Add(e.Name);
            foreach (var e in GenList)
                if (e.IsChecked) ListGen.Add(e.Name);
            if (ListCompany.Count != 0) ProductList = ((List<Vga>?)ProductList).Where(x => ListCompany.Contains(x.Company)).ToList();
            if (ListChip.Count != 0) ProductList = ((List<Vga>?)ProductList).Where(x => ListChip.Contains(x.Chip)).ToList();
            if (ListChipset.Count != 0) ProductList = ((List<Vga>?)ProductList).Where(x => ListChipset.Contains(x.Chipset)).ToList();
            if (ListVram.Count != 0) ProductList = ((List<Vga>?)ProductList).Where(x => ListVram.Contains(x.Vram)).ToList();
            if (ListSeries.Count != 0) ProductList = ((List<Vga>?)ProductList).Where(x => ListSeries.Contains(x.Series)).ToList();
            if (ListGen.Count != 0) ProductList = ((List<Vga>?)ProductList).Where(x => ListGen.Contains(x.Gen)).ToList();
        }
    }
}
