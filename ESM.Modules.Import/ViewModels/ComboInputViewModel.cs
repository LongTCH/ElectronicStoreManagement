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
            ComboDetail = new();
            ProductType = new[] { "LAPTOP", "PC", "HARD DISK", "CPU", "MONITOR", "SMARTPHONE", "VGA" };
            AddCommand = new(addToComboListCommand);
            AddToComboCommand = new(addToComboCommand);
            RemoveFromComboDetailCommand = new(executeRemove);
            AddToComboListCommand = new(addToComboListCommand);
            CancelCommand = new(clearCommand);
            DeleteCommand = new(async (p) => await deleteCommand(p));
            FindCommand = new(findCommand);
            SaveCommand = new(async (p) => await saveCommand(p));
        }
        public string Header => "COMBO";
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
        public DelegateCommand<Combo> CancelCommand { get; }
        public DelegateCommand<Combo> SaveCommand { get; }
        public DelegateCommand<Combo> DeleteCommand { get; }
        public DelegateCommand<Combo> FindCommand { get; }
        public DelegateCommand<SelectableViewModel> RemoveFromComboDetailCommand { get; }
        private async Task deleteCommand(Combo combo)
        {
            if (combo == null) return;
            MetroWindow metroWindow = (MetroWindow)Application.Current.MainWindow;
            if (metroWindow.ShowModalMessageExternal("Cảnh báo", "Bạn có chắc chắn xóa?", MessageDialogStyle.AffirmativeAndNegative) == MessageDialogResult.Affirmative)
            {
                if (combo.InMemory) await unitOfWork.Combos.Delete(combo.Id);
                ComboList.Remove(combo);
                ComboDetail.Clear();
                ComboList.Refresh();
            }
        }
        private void addToComboCommand()
        {
            if (ProductList == null) return;
            foreach (var item in ProductList)
            {
                if (item.IsSelected && !ComboDetail.Any(x => x.Id == item.Id)) ComboDetail.Add(item);
            }
        }
        private async Task saveCommand(Combo combo)
        {
            if (combo == null) return;
            if (combo.Name == null || combo.Unit == null)
            {
                modalService.ShowModal(ModalType.Error, "Nhập tất cả thông tin cần thiết", "Cảnh báo");
                return;
            }
            MetroWindow metroWindow = (MetroWindow)Application.Current.MainWindow;
            if (metroWindow.ShowModalMessageExternal("Thông báo", "Bạn có chắc chắn lưu?", MessageDialogStyle.AffirmativeAndNegative) == MessageDialogResult.Affirmative)
            {
                FillCombo(combo);
                bool res;
                if (combo.InMemory)
                {
                    res = (bool)await unitOfWork.Combos.Update(combo);
                    if (res)
                    {
                        modalService.ShowModal(ModalType.Information, "Cập nhật thành công", "Thông báo");
                    }
                    else modalService.ShowModal(ModalType.Error, "Có lỗi xảy ra", "Thông báo");
                }
                else
                {
                    res = (bool)await unitOfWork.Combos.Add(combo);
                    if (res)
                    {
                        modalService.ShowModal(ModalType.Information, "Đã lưu", "Thông báo");
                    }
                    else modalService.ShowModal(ModalType.Error, "Lưu không thành công", "Lỗi");
                }
                if (res)
                {
                    combo.InMemory = true;
                    ComboList.Refresh();
                }
            }
        }
        private async void addToComboListCommand()
        {
            Combo combo = new()
            {
                Id = await GetNewID(),
                InMemory = false
            };
            ComboList.Add(combo);
        }
        private async void clearCommand(Combo combo)
        {
            MetroWindow metroWindow = (MetroWindow)Application.Current.MainWindow;
            if (metroWindow.ShowModalMessageExternal("Cảnh báo", "Bạn có chắc chắn hủy bỏ thông tin?", MessageDialogStyle.AffirmativeAndNegative) == MessageDialogResult.Affirmative)
            {
                if (combo.InMemory) combo = await unitOfWork.Combos.GetById(combo.Id);
                else combo = new()
                {
                    Id = combo.Id,
                    InMemory = false
                };
                ComboList[ComboList.IndexOf(combo)] = combo;
                ComboDetail?.Clear();
                ComboList.Refresh(); ;
            }
        }
        private async void findCommand(Combo combo)
        {
            if (combo == null) return;
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
        }
        private async void FillCombo(Combo combo)
        {
            List<string> ids = new();
            foreach (var item in ComboDetail)
            {
                ids.Add(item.Id);
            }
            combo.ProductIdlist =  string.Join(' ', ids);
            combo.Price = await unitOfWork.Combos.GetComboPrice(combo);
        }
        protected async Task<string> GetNewID()
        {
            var previousID = ComboList.OrderBy(x => x.Id).LastOrDefault()?.Id;
            if (previousID == null) return DAStaticData.IdPrefix[DataAccess.ProductType.COMBO] + "0000000";
            int counter = Convert.ToInt32(previousID[2..]);
            string cs = await unitOfWork.Combos.GetSuggestID();
            counter = Math.Max(counter, int.Parse(cs[2..]));
            ++counter;
            return DAStaticData.IdPrefix[DataAccess.ProductType.COMBO] + counter.ToString().PadLeft(7, '0');
        }
        private async void SetProductList()
        {
            switch (SelectedProductType)
            {
                case "LAPTOP":
                    ProductList = (await unitOfWork.Laptops.GetAll()).Select(x => new SelectableViewModel()
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
                    ProductList = (await unitOfWork.Pcs.GetAll()).Select(x => new SelectableViewModel()
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
                    ProductList = (await unitOfWork.Pcharddisks.GetAll()).Select(x => new SelectableViewModel()
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
                    ProductList = (await unitOfWork.Pccpus.GetAll()).Select(x => new SelectableViewModel()
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
                    ProductList = (await unitOfWork.Monitors.GetAll()).Select(x => new SelectableViewModel()
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
                    ProductList = (await unitOfWork.Smartphones.GetAll()).Select(x => new SelectableViewModel()
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
                    ProductList = (await unitOfWork.Vgas.GetAll()).Select(x => new SelectableViewModel()
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
            ComboList = new(await unitOfWork.Combos.GetAll());
        }
    }
}
