using ESM.Core.ShareServices;
using ESM.Modules.DataAccess.Infrastructure;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace ESM.Modules.Import.ViewModels
{
    public abstract class BaseProductInputViewModel<T> : BindableBase
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
            SaveCommand = new(saveCommand);
            SelectFolder = new(getFolderPath);
            SelectDetail = new(getDetailPath);
            AddAvatarCommand = new(addAvatarCommand);
            ClearCommand = new(clearCommand);
            FindCommand = new(findCommand);
        }
        private IEnumerable<T> productList;
        public IEnumerable<T> ProductList
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
        private string id;
        public string Id
        {
            get => id;
            set => SetProperty(ref id, value);
        }
        private string name;
        public string Name
        {
            get => name;
            set => SetProperty(ref name, value);
        }
        protected decimal price;
        [Required]
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
            set => SetProperty(ref discount, value, () => ValidateProperty(value, nameof(Discount)));
        }
        private short remain;
        public short Remain
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
        private string avatarPath;
        public string AvatarPath
        {
            get => avatarPath;
            set => SetProperty(ref avatarPath, value);
        }
        private string company;
        public string Company
        {
            get => company;
            set => SetProperty(ref company, value);
        }
        private string unit;
        public string Unit
        {
            get => unit;
            set => SetProperty(ref unit, value);
        }
        public bool IsDefault => AvatarPath == null;
        private string imagePath;
        public string ImagePath
        {
            get => imagePath;
            set => SetProperty(ref imagePath, value);
        }
        public DelegateCommand SelectFolder { get; }
        public DelegateCommand SelectDetail { get; }
        public DelegateCommand AddAvatarCommand { get; }
        public DelegateCommand SaveCommand { get; }
        public DelegateCommand ClearCommand { get; }
        public DelegateCommand FindCommand { get; }
        protected abstract void saveCommand();
        protected abstract void clearCommand();
        protected abstract void findCommand();
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
    }
}
