using ESM.Core;
using ESM.Core.ShareServices;
using ESM.Modules.DataAccess;
using ESM.Modules.DataAccess.Infrastructure;
using ESM.Modules.DataAccess.Models;
using ESM.Modules.Import.Utilities;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

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
            ProductType = new[] { "LAPTOP", "PC", "HARD DISK", "CPU", "MONITOR", "SMARTPHONE", "VGA" };
            AddCommand = new(addToComboListCommand);
            AddToComboCommand = new(addToComboCommand);
            RemoveFromComboDetailCommand = new(executeRemove);
            AddToComboListCommand = new(addToComboListCommand);
            CancelCommand = new(clearCommand);
            DeleteCommand = new(deleteCommand);
            FindCommand = new(findCommand);
            SaveCommand = new(async () => await saveCommand());
        }
        async Task Init()
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
        public string Header => "COMBO";
        private string comboId;
        [StringLength(9, MinimumLength = 9)]
        [Phone(ErrorMessage = "Not all digits")]
        public string ComboId
        {
            get => comboId?.Trim();
            set => SetProperty(ref comboId, value, () => this.ValidateProperty(value, nameof(ComboId)));
        }
        private double discount;
        [Range(0, 100)]
        public double Discount
        {
            get => discount;
            set => SetProperty(ref discount, value, () => this.ValidateProperty(value, nameof(Discount)));
        }
        private string comboName;
        public string ComboName
        {
            get => comboName?.Trim();
            set => SetProperty(ref comboName, value);
        }
        private string comboUnit;
        public string ComboUnit
        {
            get => comboUnit?.Trim();
            set => SetProperty(ref comboUnit, value);
        }
        public IEnumerable<string> ProductType { get; }
        public Combo CurrentCombo { get; set; }
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
        public DelegateCommand AddCommand { get; }
        public DelegateCommand AddToComboCommand { get; }
        public DelegateCommand AddToComboListCommand { get; }
        public DelegateCommand CancelCommand { get; }
        public DelegateCommand SaveCommand { get; }
        public DelegateCommand DeleteCommand { get; }
        public DelegateCommand<Combo> FindCommand { get; }
        public DelegateCommand<SelectableViewModel> RemoveFromComboDetailCommand { get; }
        private async void deleteCommand()
        {
            MetroWindow metroWindow = (MetroWindow)Application.Current.MainWindow;
            if (metroWindow.ShowModalMessageExternal("Cảnh báo", "Bạn có chắc chắn xóa?", MessageDialogStyle.AffirmativeAndNegative) == MessageDialogResult.Affirmative)
            {
                if (CurrentCombo.InMemory) await unitOfWork.Combos.Delete(CurrentCombo.Id);
                ComboList.Remove(CurrentCombo);
                Empty();
            }
        }
        private void addToComboCommand()
        {
            foreach (var item in ProductList)
            {
                if (item.IsSelected && !ComboDetail.Any(x => x.Id == item.Id)) ComboDetail.Add(item);
            }
        }
        private async Task saveCommand()
        {
            if (CurrentCombo == null) return;
            if (ComboName == null || ComboUnit == null)
            {
                modalService.ShowModal(ModalType.Error, "Nhập tất cả thông tin cần thiết", "Cảnh báo");
                return;
            }
            MetroWindow metroWindow = (MetroWindow)Application.Current.MainWindow;
            if (metroWindow.ShowModalMessageExternal("Thông báo", "Bạn có chắc chắn lưu?", MessageDialogStyle.AffirmativeAndNegative) == MessageDialogResult.Affirmative)
            {
                var p = await GetCombo();
                if (CurrentCombo.InMemory)
                {
                    var res = await unitOfWork.Combos.Update(p);
                    if ((bool)res)
                    {
                        modalService.ShowModal(ModalType.Information, "Cập nhật thành công", "Thông báo");

                        var index = ComboList.IndexOf(CurrentCombo);
                        ComboList.RemoveAt(index);
                        ComboList.Insert(index, p);
                        // Clear
                        Empty();
                    }
                    else modalService.ShowModal(ModalType.Error, "Có lỗi xảy ra", "Thông báo");
                }
                else
                {
                    var res = await unitOfWork.Combos.Add(p);
                    if ((bool)res)
                    {
                        modalService.ShowModal(ModalType.Information, "Đã lưu", "Thông báo");
                        // Clear
                        Empty();
                        var index = ComboList.IndexOf(ComboList.Where(x => x.Id == p.Id).First());
                        ComboList.RemoveAt(index);
                        ComboList.Insert(index, p);
                    }
                    else modalService.ShowModal(ModalType.Error, "Lưu không thành công", "Lỗi");
                    await Task.CompletedTask;
                }
            }
        }
        private void addToComboListCommand()
        {
            Combo combo = new();
            combo.Id = GetNewID(DataAccess.ProductType.COMBO);
            combo.InMemory = false;
            ComboList.Add(combo);
            findCommand(combo);
        }
        private void clearCommand()
        {
            MetroWindow metroWindow = (MetroWindow)Application.Current.MainWindow;
            if (metroWindow.ShowModalMessageExternal("Cảnh báo", "Bạn có chắc chắn hủy bỏ thông tin?", MessageDialogStyle.AffirmativeAndNegative) == MessageDialogResult.Affirmative)
            {
                clear();
            }
        }
        private async void clear()
        {
            Combo combo = null;
            if (CurrentCombo != null)
            {
                combo = await unitOfWork.Combos.GetById(CurrentCombo.Id);
            }

            Empty();
            if (combo != null) findCommand(combo);
        }
        private void Empty()
        {
            CurrentCombo = null;
            Discount = 0;
            ComboDetail = new();
            ComboId = null;
            ComboName = null;
            Discount = 0;
            ComboUnit = null;
        }
        private async void findCommand(Combo combo)
        {
            if (combo == null) return;
            CurrentCombo = combo;
            ComboDetail.Clear();
            var list = await unitOfWork.Combos.GetListProduct(combo);
            foreach (var item in list)
            {
                ComboDetail.Add(new()
                {
                    Id = item.Id,
                    Discount = item.Discount,
                    IsSelected = true,
                    Name = item.Name,
                    Price = item.Price,
                    Unit = item.Unit,
                    Remain = item.Remain,
                });
            }
            ComboId = combo.Id;
            ComboName = combo.Name;
            Discount = combo.Discount;
            ComboUnit = combo.Unit;
        }
        private async Task<Combo> GetCombo()
        {
            Combo combo = new();
            List<string> ids = new();
            foreach (var item in ComboDetail)
            {
                ids.Add(item.Id);
            }
            combo.Id = ComboId;
            combo.Discount = Discount;
            combo.Name = ComboName;
            combo.Unit = ComboUnit;
            combo.ProductIdlist = string.Join(' ', ids);
            combo.Price = await unitOfWork.Combos.GetComboPrice(combo);
            return combo;
        }
        protected string GetNewID(ProductType type)
        {
            var previousID = ComboList.OrderBy(x => x.Id).LastOrDefault()?.Id;
            if (previousID == null) return DAStaticData.IdPrefix[type] + "0000000";
            int counter = Convert.ToInt32(previousID[2..]);
            ++counter;
            return DAStaticData.IdPrefix[type] + counter.ToString().PadLeft(7, '0');
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
                        Remain = x.Remain,
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
                        Remain = x.Remain,
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
                        Remain = x.Remain,
                        IsSelected = ComboDetail.Any(p => p.Id == x.Id),
                    }).ToList(); break;
                case "CPU":
                    ProductList = CPUs.Select(x => new SelectableViewModel()
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Discount = x.Discount,
                        Price = x.Price,
                        Remain = x.Remain,
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
                        Remain = x.Remain,
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
                        Remain = x.Remain,
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
                        Remain = x.Remain,
                        IsSelected = ComboDetail.Any(p => p.Id == x.Id),
                    }).ToList(); break;
            }
        }
        private void executeRemove(SelectableViewModel obj)
        {
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
            clear();
            await Init();
            ComboList = new(await unitOfWork.Combos.GetAll());
        }
        private IEnumerable<Laptop> Laptops;
        private IEnumerable<Monitor> Monitors;
        private IEnumerable<Pc> Pcs;
        private IEnumerable<Pcharddisk> HardDisks;
        private IEnumerable<Vga> Vgas;
        private IEnumerable<Smartphone> Smartphones;
        private IEnumerable<Pccpu> CPUs;
    }
}
