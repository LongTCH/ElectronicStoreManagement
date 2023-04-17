using ESM.Core.ShareServices;
using ESM.Core.ShareStores;
using ESM.Modules.DataAccess.DTOs;
using ESM.Modules.DataAccess.Infrastructure;
using ESM.Modules.DataAccess.Models;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ESM.Modules.Normal.ViewModels
{
    [RegionMemberLifetime(KeepAlive = false)]
    public class SmartPhoneViewModel : BaseProductViewModel<Smartphone>
    {
        public SmartPhoneViewModel(IUnitOfWork unitOfWork, IModalService modalService)
            : base(unitOfWork, modalService)
        {
            Action += OnIsCheckedChanged;
            LoadAttribute += () =>
            {
                getCompanyList();
                getCPUList();
                getRAMList();
                getSeriesList();
                getStorageList();
            };
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
            if (ListCompany.Count != 0) ProductList = ((List<Smartphone>)ProductList).Where(x => ListCompany.Contains(x.Company)).ToList();
            if (ListCPU.Count != 0) ProductList = ((List<Smartphone>)ProductList).Where(x => ListCPU.Contains(x.Cpu)).ToList();
            if (ListRAM.Count != 0) ProductList = ((List<Smartphone>)ProductList).Where(x => ListRAM.Contains(x.Ram)).ToList();
            if (ListSeries.Count != 0) ProductList = ((List<Smartphone>)ProductList).Where(x => ListSeries.Contains(x.Series)).ToList();
            if (ListStorage.Count != 0) ProductList = ((List<Smartphone>)ProductList).Where(x => ListStorage.Contains(x.Storage)).ToList();
        }
    }
}
