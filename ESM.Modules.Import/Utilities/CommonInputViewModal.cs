using ESM.Core;
using ESM.Core.ShareServices;
using ESM.Core.ShareStores;
using ESM.Modules.DataAccess;
using ESM.Modules.DataAccess.Infrastructure;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Threading.Tasks;

namespace ESM.Modules.Import.Utilities
{
    public abstract class CommonInputViewModal<T> : BindableBase, IViewModel, INavigationAware where T : ProductDTO, new()
    {
        protected readonly IUnitOfWork _unitOfWork;
        protected readonly IOpenDialogService _openDialogService;
        protected readonly IModalService _modalService;
        protected readonly ViewModelStore _viewModelStore;
        protected CommonInputViewModal(IUnitOfWork unitOfWork, IOpenDialogService openDialogService, IModalService modalService, ViewModelStore viewModelStore)
        {
            _unitOfWork = unitOfWork;
            _openDialogService = openDialogService;
            _modalService = modalService;
            _viewModelStore = viewModelStore;
            SelectFolder = new(getFolderPath);
            SelectDetail = new(getDetailPath);
            AddAvatarCommand = new(addAvatarCommand);
        }
        private string id;
        public string Id
        {
            get => id;
            set => SetProperty(ref id, value);
        }
        private string name;
        [Required(ErrorMessage = "Không được để trống")]
        public string Name
        {
            get => name;
            set => SetProperty(ref name, value, () => this.ValidateProperty(value, nameof(Name)));
        }
        public decimal price;
        public decimal Price
        {
            get => price;
            set => SetProperty(ref price, value, () => this.ValidateProperty(value, nameof(Price)));
        }
        private double? discount;
        public double? Discount
        {
            get => discount;
            set => SetProperty(ref discount, value);
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
            get => detailPath;
            set => SetProperty(ref detailPath, value);
        }
        private string imagePath;
        public string ImagePath
        {
            get => imagePath;
            set => SetProperty(ref imagePath, value);
        }
        private string avatarPath;
        public string AvatarPath
        {
            get => avatarPath;
            set
            {
                SetProperty(ref avatarPath, value);
                RaisePropertyChanged(nameof(IsDefault));
            }
        }
        private string company;
        [Required(ErrorMessage = "Không được để trống")]
        public string Company
        {
            get => company?.Trim();
            set => SetProperty(ref company, value);
        }
        public decimal SellPrice => Discount == null || Discount == 0 ? Price : Price * (1 - (decimal)Discount / 100);
        public bool DiscountShow => SellPrice < Price;
        public bool IsDefault => !File.Exists(AvatarPath);
        private string unit;
        public string Unit
        {
            get => unit?.Trim();
            set => SetProperty(ref unit, value);
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

            }
        }
        private void getFolderPath()
        {
            ImagePath = _openDialogService.FolderDialog();
        }

        public abstract Task save();

        public abstract void OnNavigatedTo(NavigationContext navigationContext);

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return false;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            
        }

        public DelegateCommand SelectFolder { get; }
        public DelegateCommand SelectDetail { get; }
        public DelegateCommand AddAvatarCommand { get; }

    }
}
