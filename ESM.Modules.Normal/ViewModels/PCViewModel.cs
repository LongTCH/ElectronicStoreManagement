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
    public class PCViewModel : BaseProductViewModel<Pc>
    {
        public PCViewModel(IUnitOfWork unitOfWork, IModalService modalService)
            : base(unitOfWork, modalService)
        {
            Action += OnIsCheckedChanged;
            LoadAttribute += () =>
            {
                getCompanyList();
                getCPUList();
                getNeedList();
                getRAMList();
                getSeriesList();
            };
        }
        private void OnIsCheckedChanged()
        {
            List<string> ListCompany = new();
            List<string> ListCPU = new();
            List<string> ListNeed = new();
            List<string> ListRAM = new();
            List<string> ListSeries = new();
            foreach (var e in CompanyList)
                if (e.IsChecked) ListCompany.Add(e.Name);
            foreach (var e in CPUList)
                if (e.IsChecked) ListCPU.Add(e.Name);
            foreach (var e in NeedList)
                if (e.IsChecked) ListNeed.Add(e.Name);
            foreach (var e in RAMList)
                if (e.IsChecked) ListRAM.Add(e.Name);
            foreach (var e in SeriesList)
                if (e.IsChecked) ListSeries.Add(e.Name);
            if (ListCompany.Count != 0) ProductList = ((List<Pc>)ProductList)!.Where(x => ListCompany.Contains(x.Company)).ToList();
            if (ListCPU.Count != 0) ProductList = ((List<Pc>)ProductList)!.Where(x => ListCPU.Contains(x.Cpu)).ToList();
            if (ListNeed.Count != 0) ProductList = ((List<Pc>)ProductList)!.Where(x => IsInListNeed(ListNeed, x.Need)).ToList();
            if (ListRAM.Count != 0) ProductList = ((List<Pc>)ProductList)!.Where(x => ListRAM.Contains(x.Ram)).ToList();
            if (ListSeries.Count != 0) ProductList = ((List<Pc>)ProductList)!.Where(x => ListSeries.Contains(x.Series)).ToList();
        }
    }
}
