using ESM.Core.ShareServices;
using ESM.Modules.DataAccess;
using ESM.Modules.DataAccess.Infrastructure;
using MahApps.Metro.Controls.Dialogs;
using MahApps.Metro.Controls;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Linq;
using ESM.Modules.DataAccess.Models;
using ESM.Core;

namespace ESM.Modules.Import.ViewModels
{
    public abstract class BaseProductInputViewModel<T> : BindableBase, INavigationAware where T : ProductDTO, new()
    {
        protected readonly IUnitOfWork _unitOfWork;
        protected readonly IOpenDialogService _openDialogService;
        protected readonly IModalService _modalService;

        public BaseProductInputViewModel(IUnitOfWork unitOfWork,
            IOpenDialogService openDialogService, IModalService modalService)
        {
            _unitOfWork = unitOfWork;
            _openDialogService = openDialogService;
            _modalService = modalService;
            SaveCommand = new(async () => await saveCommand());
            EditCommand = new(editCommand);
            SelectFolder = new(getFolderPath);
            SelectDetail = new(getDetailPath);
            AddAvatarCommand = new(addAvatarCommand);
            ClearCommand = new(clearCommand);
            AddCommand = new(addCommand);
            DeleteCommand = new(deleteCommand);
        }
        private bool isEditMode;
        public bool IsEditMode
        {
            get => isEditMode;
            set => SetProperty(ref isEditMode, value);
        }
        private T selectedProduct;
        public T SelectedProduct
        {
            get => selectedProduct;
            set => SetProperty(ref selectedProduct, value);
        }
        private ObservableCollection<T> productList;
        public ObservableCollection<T> ProductList
        {
            get => productList;
            set => SetProperty(ref productList, value);
        }
        public DelegateCommand SelectFolder { get; }
        public DelegateCommand SelectDetail { get; }
        public DelegateCommand AddAvatarCommand { get; }
        public DelegateCommand SaveCommand { get; }
        public DelegateCommand ClearCommand { get; }
        public DelegateCommand AddCommand { get; }
        public DelegateCommand EditCommand { get; }
        public DelegateCommand DeleteCommand { get; }
        protected abstract Task saveCommand();
        private void editCommand()
        {
            if (SelectedProduct != null)
                IsEditMode = true;
            else _modalService.ShowModal(ModalType.Error, "Bạn chưa chọn sản phẩm", "Thông báo");
        }
        private void clearCommand()
        {
            if (SelectedProduct == null) return;
            MetroWindow metroWindow = (MetroWindow)Application.Current.MainWindow;
            if (metroWindow.ShowModalMessageExternal("Cảnh báo", "Bạn có chắc chắn hủy bỏ thông tin?", MessageDialogStyle.AffirmativeAndNegative) == MessageDialogResult.Affirmative)
            {
                SelectedProduct = null;
                IsEditMode = false;
                GetProductList();
            }

        }
        private async void deleteCommand()
        {
            if (SelectedProduct is null) return;
            MetroWindow metroWindow = (MetroWindow)Application.Current.MainWindow;
            if (metroWindow.ShowModalMessageExternal("Cảnh báo", "Bạn có chắc chắn xóa?", MessageDialogStyle.AffirmativeAndNegative) == MessageDialogResult.Affirmative)
            {
                if (SelectedProduct is Laptop)
                    await _unitOfWork.Laptops.Delete(SelectedProduct.Id);
                else if (SelectedProduct is Monitor)
                    await _unitOfWork.Monitors.Delete(SelectedProduct.Id);
                else if (SelectedProduct is Pcharddisk)
                    await _unitOfWork.Pcharddisks.Delete(SelectedProduct.Id);
                else if (SelectedProduct is Pc)
                    await _unitOfWork.Pcs.Delete(SelectedProduct.Id);
                else if (SelectedProduct is Pccpu)
                    await _unitOfWork.Pccpus.Delete(SelectedProduct.Id);
                else if (SelectedProduct is Smartphone)
                    await _unitOfWork.Smartphones.Delete(SelectedProduct.Id);
                else if (SelectedProduct is Vga)
                    await _unitOfWork.Vgas.Delete(SelectedProduct.Id);
                GetProductList();
                ProductList.Refresh();
            }
        }
        protected abstract void GetProductList();
        private void getFolderPath()
        {
            if (SelectedProduct == null) return;
            SelectedProduct.ImagePath = _openDialogService.FolderDialog();
        }
        private void getDetailPath()
        {
            if (SelectedProduct == null) return;
            SelectedProduct.DetailPath = _openDialogService.FileDialog(FileType.Excel);
        }
        private void addAvatarCommand()
        {
            if (SelectedProduct == null) return;
            var avatar = _openDialogService.FileDialog(FileType.Image);
            if (avatar != null)
            {
                SelectedProduct.AvatarPath = avatar;

            }
        }
        protected string GetNewID()
        {
            ProductType type = ProductType.LAPTOP;
            if (typeof(T).Equals(typeof(Laptop))) type = ProductType.LAPTOP;
            else if (typeof(T).Equals(typeof(Monitor))) type = ProductType.MONITOR;
            else if (typeof(T).Equals(typeof(Pcharddisk))) type = ProductType.HARDDISK;
            else if (typeof(T).Equals(typeof(Pccpu))) type = ProductType.CPU;
            else if (typeof(T).Equals(typeof(Pc))) type = ProductType.PC;
            else if (typeof(T).Equals(typeof(Smartphone))) type = ProductType.SMARTPHONE;
            else if (typeof(T).Equals(typeof(Vga))) type = ProductType.VGA;
            var previousID = ProductList.OrderBy(x => x.Id).LastOrDefault()?.Id;
            if (previousID == null) return DAStaticData.IdPrefix[type] + "0000000";
            int counter = Convert.ToInt32(previousID[2..]);
            //string cs = "0";
            //if (type == ProductType.LAPTOP) cs = await _unitOfWork.Laptops.GetSuggestID();
            //else if (type == ProductType.MONITOR) cs = await _unitOfWork.Monitors.GetSuggestID();
            //else if (type == ProductType.HARDDISK) cs = await _unitOfWork.Pcharddisks.GetSuggestID();
            //else if (type == ProductType.CPU) cs = await _unitOfWork.Pccpus.GetSuggestID();
            //else if (type == ProductType.PC) cs = await _unitOfWork.Pcs.GetSuggestID();
            //else if (type == ProductType.SMARTPHONE) cs = await _unitOfWork.Smartphones.GetSuggestID();
            //else if (type == ProductType.VGA) cs = await _unitOfWork.Vgas.GetSuggestID();
            //counter = Math.Max(counter, int.Parse(cs[2..]));
            ++counter;
            return DAStaticData.IdPrefix[type] + counter.ToString().PadLeft(7, '0');
        }
        private void addCommand()
        {
            IsEditMode = true;
            var p = new T();
            p.Id = GetNewID();
            SelectedProduct = p;
        }
        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            GetProductList();
            IsEditMode = false;
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {

        }
    }
}
