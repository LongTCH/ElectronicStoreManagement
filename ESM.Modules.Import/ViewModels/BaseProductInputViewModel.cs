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
using System.ComponentModel;

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
            SaveCommand = new(async (p) =>
            {
                if (p == null) return;
                await saveCommand(p);
            });
            SelectFolder = new(getFolderPath);
            SelectDetail = new(getDetailPath);
            AddAvatarCommand = new(addAvatarCommand);
            ClearCommand = new(async (p) => await clearCommand(p));
            AddCommand = new(addCommand);
            DeleteCommand = new(deleteCommand);
        }
        private ObservableCollection<T> productList;
        public ObservableCollection<T> ProductList
        {
            get => productList;
            set => SetProperty(ref productList, value);
        }
        public DelegateCommand<T> SelectFolder { get; }
        public DelegateCommand<T> SelectDetail { get; }
        public DelegateCommand<T> AddAvatarCommand { get; }
        public DelegateCommand<T> SaveCommand { get; }
        public DelegateCommand<T> ClearCommand { get; }
        public DelegateCommand AddCommand { get; }
        public DelegateCommand<T> DeleteCommand { get; }
        protected abstract Task saveCommand(T product);
        private async Task clearCommand(T product)
        {
            MetroWindow metroWindow = (MetroWindow)Application.Current.MainWindow;
            if (metroWindow.ShowModalMessageExternal("Cảnh báo", "Bạn có chắc chắn hủy bỏ thông tin?", MessageDialogStyle.AffirmativeAndNegative) == MessageDialogResult.Affirmative)
            {
                if (product is Laptop)
                    product = (T)(object)await _unitOfWork.Laptops.GetById(product.Id);
                else if (product is Monitor)
                    product = (T)(object)await _unitOfWork.Monitors.GetById(product.Id);
                else if (product is Pcharddisk)
                    product = (T)(object)await _unitOfWork.Pcharddisks.GetById(product.Id);
                else if (product is Pc)
                    product = (T)(object)await _unitOfWork.Pcs.GetById(product.Id);
                else if (product is Pccpu)
                    product = (T)(object)await _unitOfWork.Pccpus.GetById(product.Id);
                else if (product is Smartphone)
                    product = (T)(object)await _unitOfWork.Smartphones.GetById(product.Id);
                else if (product is Vga)
                    product = (T)(object)await _unitOfWork.Vgas.GetById(product.Id);
                ProductList[ProductList.IndexOf(product)] = product;
                ProductList.Refresh();
            }
        }
        private async void deleteCommand(T product)
        {
            if (product is null) return;
            MetroWindow metroWindow = (MetroWindow)Application.Current.MainWindow;
            if (metroWindow.ShowModalMessageExternal("Cảnh báo", "Bạn có chắc chắn xóa?", MessageDialogStyle.AffirmativeAndNegative) == MessageDialogResult.Affirmative)
            {
                if (product.IdExist)
                {
                    if (product is Laptop)
                        await _unitOfWork.Laptops.Delete(product.Id);
                    else if (product is Monitor)
                        await _unitOfWork.Monitors.Delete(product.Id);
                    else if (product is Pcharddisk)
                        await _unitOfWork.Pcharddisks.Delete(product.Id);
                    else if (product is Pc)
                        await _unitOfWork.Pcs.Delete(product.Id);
                    else if (product is Pccpu)
                        await _unitOfWork.Pccpus.Delete(product.Id);
                    else if (product is Smartphone)
                        await _unitOfWork.Smartphones.Delete(product.Id);
                    else if (product is Vga)
                        await _unitOfWork.Vgas.Delete(product.Id);
                }
                ProductList.Remove(product);
            }
        }
        private void getFolderPath(T product)
        {
            product.ImagePath = _openDialogService.FolderDialog();
            ProductList.Refresh();
        }
        private void getDetailPath(T product)
        {
            product.DetailPath = _openDialogService.FileDialog(FileType.Excel);
            ProductList.Refresh();
        }
        private void addAvatarCommand(T product)
        {
            if (product == null) return;
            var avatar = _openDialogService.FileDialog(FileType.Image);
            if (avatar != null)
            {
                product.AvatarPath = avatar;
                
            }
            ProductList.Refresh();
        }
        protected string GetNewID(ProductType type)
        {
            var previousID = ProductList.OrderBy(x => x.Id).LastOrDefault()?.Id;
            if (previousID == null) return DAStaticData.IdPrefix[type] + "0000000";
            int counter = Convert.ToInt32(previousID[2..]);
            ++counter;
            return DAStaticData.IdPrefix[type] + counter.ToString().PadLeft(7, '0');
        }
        private void addCommand()
        {
            T p = new()
            {
                IdExist = false,
                Id = GetNewID(ProductType.CPU),
                InMemory = false
            };
            ProductList.Add(p);
        }
        public virtual void OnNavigatedTo(NavigationContext navigationContext)
        {
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
