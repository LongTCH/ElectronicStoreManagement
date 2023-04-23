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
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using System.Windows;
using System.Linq;

namespace ESM.Modules.Import.ViewModels
{
    public abstract class BaseProductInputViewModel<T> : BindableBase, INavigationAware where T : ProductDTO
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
            SelectFolder = new(getFolderPath);
            SelectDetail = new(getDetailPath);
            AddAvatarCommand = new(addAvatarCommand);
            ClearCommand = new(clearCommand);
            FindCommand = new(findCommand);
            AddCommand = new(addCommand);
            DeleteCommand = new(() => { if (Id != null) deleteCommand(); });
            TurnOnEditCommand = new(() => IsEditable = true);
        }
        private ObservableCollection<T> productList;
        public ObservableCollection<T> ProductList
        {
            get => productList;
            set => SetProperty(ref productList, value);
        }
        private T product;
        public T Product
        {
            get => product;
            set => SetProperty(ref product, value);
        }
        private bool isEditable;
        public bool IsEditable
        {
            get => isEditable;
            set => SetProperty(ref isEditable, value);
        }
        private string id;
        public string Id
        {
            get => id?.Trim();
            set => SetProperty(ref id, value);
        }
        private string name;
        public string Name
        {
            get => name?.Trim();
            set => SetProperty(ref name, value);
        }
        protected decimal price;
        public decimal Price
        {
            get => price;
            set => SetProperty(ref price, value);
        }
        protected double? discount;
        [Range(0, 100)]
        public double? Discount
        {
            get => discount;
            set => SetProperty(ref discount, value, () => this.ValidateProperty(value, nameof(Discount)));
        }
        private int remain;
        public int Remain
        {
            get => remain;
            set => SetProperty(ref remain, value);
        }
        private string detailPath;
        public string DetailPath
        {
            get => detailPath?.Trim();
            set => SetProperty(ref detailPath, value);
        }
        private string avatarPath;
        public string AvatarPath
        {
            get => avatarPath?.Trim();
            set => SetProperty(ref avatarPath, value);
        }
        private string company;
        public string Company
        {
            get => company?.Trim();
            set => SetProperty(ref company, value);
        }
        private string unit;
        public string Unit
        {
            get => unit?.Trim();
            set => SetProperty(ref unit, value);
        }
        public bool IsDefault => AvatarPath == null;
        private string imagePath;
        public string ImagePath
        {
            get => imagePath?.Trim();
            set => SetProperty(ref imagePath, value);
        }
        public DelegateCommand SelectFolder { get; }
        public DelegateCommand SelectDetail { get; }
        public DelegateCommand AddAvatarCommand { get; }
        public DelegateCommand SaveCommand { get; }
        public DelegateCommand ClearCommand { get; }
        public DelegateCommand AddCommand { get; }
        public DelegateCommand TurnOnEditCommand { get; }
        public DelegateCommand DeleteCommand { get; }
        public DelegateCommand<ProductDTO> FindCommand { get; }
        protected abstract Task saveCommand();
        protected abstract void addCommand();
        protected abstract void deleteCommand();
        protected abstract void findCommand(ProductDTO productDTO);
        protected abstract void clear();
        private void clearCommand()
        {
            MetroWindow metroWindow = (MetroWindow)Application.Current.MainWindow;
            if (metroWindow.ShowModalMessageExternal("Cảnh báo", "Bạn có chắc chắn hủy bỏ thông tin?", MessageDialogStyle.AffirmativeAndNegative) == MessageDialogResult.Affirmative)
            {
                clear();
            }
        }
        private void getFolderPath()
        {
            ImagePath = _openDialogService.FolderDialog();
        }
        private void getDetailPath()
        {
            DetailPath = _openDialogService.FileDialog(FileType.Excel);
        }
        private void addAvatarCommand()
        {
            var avatar = _openDialogService.FileDialog(FileType.Image);
            if (avatar != null)
            {
                AvatarPath = avatar;
                RaisePropertyChanged(nameof(IsDefault));
            }
        }
        protected void ValidateProperty<TProp>(TProp value, string name)
        {
            Validator.ValidateProperty(value, new ValidationContext(this, null, null)
            {
                MemberName = name
            });
        }
        protected string GetNewID(ProductType type)
        {
            var previousID = ProductList.OrderBy(x => x.Id).LastOrDefault()?.Id;
            if (previousID == null) return DAStaticData.IdPrefix[type] + "0000000";
            int counter = Convert.ToInt32(previousID[2..]);
            ++counter;
            return DAStaticData.IdPrefix[type] + counter.ToString().PadLeft(7, '0');
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
