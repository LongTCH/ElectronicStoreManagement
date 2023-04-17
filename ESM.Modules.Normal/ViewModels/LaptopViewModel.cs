using ESM.Core.ShareServices;
using ESM.Core.ShareStores;
using ESM.Modules.DataAccess.DTOs;
using ESM.Modules.DataAccess.Infrastructure;
using ESM.Modules.DataAccess.Models;
using System.Collections.Generic;
using System.Linq;

namespace ESM.Modules.Normal.ViewModels
{
    public class LaptopViewModel : BaseProductViewModel<Laptop>
    {
        public LaptopViewModel(IUnitOfWork unitOfWork, IModalService modalService)
            : base(unitOfWork, modalService)
        {
            Action += OnIsCheckedChanged;
            LoadAttribute += () =>
            {
                getCompanyList();
                getCPUList();
                getGraphicList();
                getNeedList();
                getRAMList();
                getSeriesList();
                getStorageList();
            };

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
            if (ListCompany.Count != 0) ProductList = ((List<Laptop>)ProductList).Where(x => ListCompany.Contains(x.Company)).ToList();
            if (ListCPU.Count != 0) ProductList = ((List<Laptop>)ProductList).Where(x => ListCPU.Contains(x.Cpu)).ToList();
            if (ListGraphic.Count != 0) ProductList = ((List<Laptop>)ProductList).Where(x => ListGraphic.Contains(x.Graphic)).ToList();
            if (ListRAM.Count != 0) ProductList = ((List<Laptop>)ProductList).Where(x => ListRAM.Contains(x.Ram)).ToList();
            if (ListSeries.Count != 0) ProductList = ((List<Laptop>)ProductList).Where(x => ListSeries.Contains(x.Series)).ToList();
            if (ListStorage.Count != 0) ProductList = ((List<Laptop>)ProductList).Where(x => ListStorage.Contains(x.Storage)).ToList();
            if (ListNeed.Count != 0) ProductList = ((List<Laptop>)ProductList).Where(x => IsInListNeed(ListNeed, x.Need)).ToList();
        }
    }
}