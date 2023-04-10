using ESM.Core.ShareServices;
using ESM.Modules.DataAccess;
using ESM.Modules.DataAccess.Infrastructure;
using ESM.Modules.DataAccess.Models;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace ESM.Modules.Import.ViewModels
{
    public class ComboInputViewModel : BindableBase, INavigationAware
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IModalService modalService;

        public ComboInputViewModel(IUnitOfWork unitOfWork, IModalService modalService)
        {
            this.unitOfWork = unitOfWork;
            this.modalService = modalService;
            WorkType = new[] { "THÊM", "SỬA", "XÓA" };
            ProductType = new[] { "LAPTOP", "PC", "HARD DISK", "CPU", "MONITOR", "SMARTPHONE", "VGA" };
            AddToComboCommand = new(addToComboCommand);
            RemoveFromComboDetailCommand = new(executeRemove);
        }
        public string Header => "COMBO";
        public IEnumerable<string> WorkType { get; }
        public IEnumerable<string> ProductType { get; }
        public ICollection<SelectableViewModel> productList;
        public ICollection<SelectableViewModel> ProductList
        {
            get => productList;
            set => SetProperty(ref productList, value);
        }
        private ObservableCollection<SelectableViewModel> comboDetail;
        public ObservableCollection<SelectableViewModel> ComboDetail
        {
            get => comboDetail;
            set => SetProperty(ref comboDetail, value);
        }
        private ObservableCollection<Combo> comboList;
        public ObservableCollection<Combo> ComboList
        {
            get => comboList;
            set => SetProperty(ref comboList, value);
        }
        public string SelectedWorkType { get; set; }
        private string selectedProductType;
        public string SelectedProductType
        {
            get => selectedProductType;
            set
            {
                selectedProductType = value;
                SetProductList();
            }
        }
        public DelegateCommand AddToComboCommand { get; }
        public DelegateCommand<SelectableViewModel> RemoveFromComboDetailCommand { get; }
        private void addToComboCommand()
        {
            foreach (var item in ProductList) {
                if (item.IsSelected && !ComboDetail.Any(x => x.Id == item.Id)) ComboDetail.Add(item);
            }
        }
        private void SetProductList()
        {
            switch (SelectedProductType)
            {
                case "LAPTOP":
                    ProductList = Laptops.Select(x => new SelectableViewModel()
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Discount = x.Discount,
                        Price = x.Price,
                        Unit = x.Unit,
                        IsSelected = ComboDetail.Any(p => p.Id == x.Id),
                    }).ToList(); break;
                case "PC":
                    ProductList = Pcs.Select(x => new SelectableViewModel()
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Discount = x.Discount,
                        Price = x.Price,
                        Unit = x.Unit,
                        IsSelected = ComboDetail.Any(p => p.Id == x.Id),
                    }).ToList(); break;
                case "HARD DISK":
                    ProductList = HardDisks.Select(x => new SelectableViewModel()
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Discount = x.Discount,
                        Price = x.Price,
                        Unit = x.Unit,
                        IsSelected = ComboDetail.Any(p => p.Id == x.Id),
                    }).ToList(); break;
                case "CPU":
                    ProductList = CPUs.Select(x => new SelectableViewModel()
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Discount = x.Discount,
                        Price = x.Price,
                        Unit = x.Unit,
                        IsSelected = ComboDetail.Any(p => p.Id == x.Id),
                    }).ToList(); break;
                case "MONITOR":
                    ProductList = Monitors.Select(x => new SelectableViewModel()
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Discount = x.Discount,
                        Price = x.Price,
                        Unit = x.Unit,
                        IsSelected = ComboDetail.Any(p => p.Id == x.Id),
                    }).ToList(); break;
                case "SMARTPHONE":
                    ProductList = Smartphones.Select(x => new SelectableViewModel()
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Discount = x.Discount,
                        Price = x.Price,
                        Unit = x.Unit,
                        IsSelected = ComboDetail.Any(p => p.Id == x.Id),
                    }).ToList(); break;
                default:
                    ProductList = Vgas.Select(x => new SelectableViewModel()
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Discount = x.Discount,
                        Price = x.Price,
                        Unit = x.Unit,
                        IsSelected = ComboDetail.Any(p => p.Id == x.Id),
                    }).ToList(); break;
            }
        }
        private void executeRemove(SelectableViewModel obj) {
            ComboDetail.Remove(obj);
        }
        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {

        }

        public async void OnNavigatedTo(NavigationContext navigationContext)
        {
            Laptops = await unitOfWork.Laptops.GetAll();
            Monitors = await unitOfWork.Monitors.GetAll();
            Pcs = await unitOfWork.Pcs.GetAll();
            HardDisks = await unitOfWork.Pcharddisks.GetAll();
            Smartphones = await unitOfWork.Smartphones.GetAll();
            Vgas = await unitOfWork.Vgas.GetAll();
            CPUs = await unitOfWork.Pccpus.GetAll();
            ComboDetail = new();
            ProductList = Array.Empty<SelectableViewModel>();
            ComboList = new();
        }
        private IEnumerable<Laptop> Laptops;
        private IEnumerable<DataAccess.Models.Monitor> Monitors;
        private IEnumerable<Pc> Pcs;
        private IEnumerable<Pcharddisk> HardDisks;
        private IEnumerable<Vga> Vgas;
        private IEnumerable<Smartphone> Smartphones;
        private IEnumerable<Pccpu> CPUs;
    }
    public class SelectableViewModel : BindableBase
    {
        public bool IsSelected { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public double? Discount { get; set; }
        public decimal SellPrice => Discount == null || Discount == 0 ? Price : Price * (1 - (decimal)Discount / 100);
        public string Unit { get; set; }
        public override bool Equals(object? obj)
        {
            return Id == (obj as SelectableViewModel)?.Id;
        }
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
