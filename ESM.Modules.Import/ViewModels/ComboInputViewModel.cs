using ESM.Core;
using ESM.Core.ShareServices;
using ESM.Modules.DataAccess;
using ESM.Modules.DataAccess.Infrastructure;
using ESM.Modules.DataAccess.Models;
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
            WorkType = new[] { "THÊM", "SỬA", "XÓA" };
            ProductType = new[] { "LAPTOP", "PC", "HARD DISK", "CPU", "MONITOR", "SMARTPHONE", "VGA" };
            AddToComboCommand = new(addToComboCommand);
            RemoveFromComboDetailCommand = new(executeRemove);
            AddToComboListCommand = new(async () => await addToComboListCommand());
            CancelCommand = new(clearCommand);
            DeleteCommand = new(deleteCommand);
            AddCommand = new(async () => await addCommand());
            EditCommand = new(editCommand);
        }
        HashSet<string> NotInDatabase;
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
            NotInDatabase = new();
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
        public IEnumerable<string> WorkType { get; }
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
        private string selectedWorkType;
        public string SelectedWorkType
        {
            get => selectedWorkType;
            set
            {
                SetProperty(ref selectedWorkType, value);
                OnSelectedWorkTypeChanged();
            }
        }
        private async void OnSelectedWorkTypeChanged()
        {
            if (SelectedWorkType == "THÊM")
            {
                ComboList = new();
            }
            else
            {
                var list = await unitOfWork.Combos.GetAll();
                ComboList = new(list);
            }
            clear();
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
        public DelegateCommand AddToComboCommand { get; }
        public DelegateCommand AddToComboListCommand { get; }
        public DelegateCommand CancelCommand { get; }
        public DelegateCommand AddCommand { get; }
        public DelegateCommand<Combo> DeleteCommand { get; }
        public DelegateCommand<Combo> EditCommand { get; }
        public DelegateCommand<SelectableViewModel> RemoveFromComboDetailCommand { get; }
        protected async void deleteCommand(Combo combo)
        {
            MetroWindow metroWindow = (MetroWindow)Application.Current.MainWindow;
            if (metroWindow.ShowModalMessageExternal("Cảnh báo", "Bạn có chắc chắn xóa?", MessageDialogStyle.AffirmativeAndNegative) == MessageDialogResult.Affirmative)
            {
                if (SelectedWorkType == "THÊM")
                {
                    if (NotInDatabase.Contains(combo.Id))
                    {
                        var p = ComboList.Single(x => x.Id == combo.Id);
                        ComboList.Remove(p);
                        NotInDatabase.Remove(combo.Id);
                    }
                }
                else
                {
                    var res = await unitOfWork.Combos.Delete(combo.Id);
                    if ((bool)res == false) modalService.ShowModal(ModalType.Error, "Không thể xóa", "Lỗi");
                    var list = await unitOfWork.Combos.GetAll();
                    ComboList = new(list);
                }
            }

        }
        private void addToComboCommand()
        {
            if (!string.IsNullOrEmpty(SelectedWorkType))
            {
                foreach (var item in ProductList)
                {
                    if (item.IsSelected && !ComboDetail.Any(x => x.Id == item.Id)) ComboDetail.Add(item);
                }
            }
        }
        private async Task addCommand()
        {
            MetroWindow metroWindow = (MetroWindow)Application.Current.MainWindow;
            if (metroWindow.ShowModalMessageExternal("Thông báo", "Bạn có chắc chắn lưu?", MessageDialogStyle.AffirmativeAndNegative) == MessageDialogResult.Affirmative)
            {
                var res = await unitOfWork.Combos.AddList(ComboList);
                if ((bool)res)
                {
                    modalService.ShowModal(ModalType.Information, "Đã lưu", "Thông báo");
                    ComboList = new();
                    NotInDatabase = new();
                }
                else modalService.ShowModal(ModalType.Error, "Lưu không thành công", "Lỗi");
                await Task.CompletedTask;
            }
        }
        private async Task addToComboListCommand()
        {
            if (SelectedWorkType == "THÊM")
            {
                if (ComboId == null || ComboId.Length != 9 || !ComboId.StartsWith(DAStaticData.IdPrefix[DataAccess.ProductType.COMBO]) || !ComboId.All(x => char.IsDigit(x)))
                {
                    modalService.ShowModal(ModalType.Error, "Sai định dạng ID", "Cảnh báo");
                    return;
                }
                //make the two calls to IsProductExistInBill and IsIdExist concurrently
                var task1 = unitOfWork.BillCombos.IsComboExistInBill(ComboId);
                var task2 = unitOfWork.Combos.IsIdExist(ComboId);

                await Task.WhenAll(task1, task2);

                bool exist = task1.Result || task2.Result;

                if (ComboList.Any(x => x.Id == ComboId) || exist)
                {
                    modalService.ShowModal(ModalType.Error, "ID đã tồn tại", "Cảnh báo");
                    return;
                }
                if (string.IsNullOrWhiteSpace(ComboName) || string.IsNullOrWhiteSpace(ComboUnit))
                {
                    modalService.ShowModal(ModalType.Error, "Điền tất cả thông tin cần thiết", "Thông báo");
                    return;
                }
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
                ComboList.Add(combo);
                NotInDatabase.Add(ComboId);
                clear();
            }
            else if (SelectedWorkType == "SỬA")
            {
                if (CurrentCombo == null) return;
                List<string> ids = new();
                foreach (var item in ComboDetail)
                {
                    ids.Add(item.Id);
                }
                CurrentCombo.Discount = Discount;
                CurrentCombo.ProductIdlist = string.Join(" ", ids);
                var res = await unitOfWork.Combos.Update(CurrentCombo);
                if ((bool)res)
                    modalService.ShowModal(ModalType.Information, "Cập nhật thành công", "Thông báo");
                else modalService.ShowModal(ModalType.Error, "Có lỗi xảy ra", "Thông báo");
                // Clear
                Empty();
                var list = await unitOfWork.Combos.GetAll();
                ComboList = new(list);
            }
        }
        private void editCommand(Combo combo)
        {
            if (SelectedWorkType == "THÊM")
            {
                ComboList.Remove(combo);
                NotInDatabase.Remove(combo.Id);
            }
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
        private void clear()
        {
            if (SelectedWorkType == "THÊM")
            {
                Empty();
            }
            else if (SelectedWorkType == "SỬA")
            {
                Discount = 0;
                if (CurrentCombo != null) findCommand(CurrentCombo);
            }
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
            SelectedWorkType = "THÊM";
            await Init();
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
        public int Remain { get; set; }
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
