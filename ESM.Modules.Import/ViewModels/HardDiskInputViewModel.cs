using ESM.Core.ShareServices;
using ESM.Modules.DataAccess;
using ESM.Modules.DataAccess.Infrastructure;
using ESM.Modules.DataAccess.Models;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using Prism.Regions;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace ESM.Modules.Import.ViewModels
{
    public class HardDiskInputViewModel : BaseProductInputViewModel<Pcharddisk>
    {
        public HardDiskInputViewModel(IUnitOfWork unitOfWork, IOpenDialogService openDialogService, IModalService modalService) : base(unitOfWork, openDialogService, modalService)
        {

        }
        public string Header => "Hard Disk";
        private string storage;
        public string Storage
        {
            get => storage?.Trim();
            set => SetProperty(ref storage, value);
        }
        private string connect;
        public string Connect
        {
            get => connect?.Trim();
            set => SetProperty(ref connect, value);
        }
        private string series;
        public string Series
        {
            get => series?.Trim();
            set => SetProperty(ref series, value);
        }
        private string type;
        public string Type
        {
            get => type?.Trim();
            set => SetProperty(ref type, value);
        }
        protected override void addCommand()
        {
            Pcharddisk p = new();
            p.Id = GetNewID(ProductType.HARDDISK);
            p.InMemory = false;
            ProductList.Add(p);
            findCommand(p);
            TurnOnEditCommand.Execute();
        }
        protected override void clear()
        {
            findCommand(Product);
        }
        protected override async Task saveCommand()
        {
            if (Company == null || Unit == null ||
                Name == null || Storage == null || Connect == null || Type == null
                || Price == 0)
            {
                _modalService.ShowModal(ModalType.Error, "Nhập tất cả thông tin cần thiết", "Cảnh báo");
                return;
            }
            MetroWindow metroWindow = (MetroWindow)Application.Current.MainWindow;
            if (metroWindow.ShowModalMessageExternal("Thông báo", "Bạn có chắc chắn lưu?", MessageDialogStyle.AffirmativeAndNegative) == MessageDialogResult.Affirmative)
            {
                var p = GetPcharddisk(Id, Remain);
                if (Product.InMemory)
                {
                    var res = await _unitOfWork.Pcharddisks.Update(p);
                    if ((bool)res)
                    {
                        _modalService.ShowModal(ModalType.Information, "Cập nhật thành công", "Thông báo");
                        var index = ProductList.IndexOf(Product);
                        ProductList.RemoveAt(index);
                        ProductList.Insert(index, p);
                        // Clear
                        Empty();
                    }
                    else _modalService.ShowModal(ModalType.Error, "Có lỗi xảy ra", "Thông báo");
                }
                else
                {
                    var res = await _unitOfWork.Pcharddisks.Add(p);
                    if ((bool)res)
                    {
                        _modalService.ShowModal(ModalType.Information, "Đã lưu", "Thông báo");
                        var index = ProductList.IndexOf(ProductList.Where(x => x.Id == p.Id).First());
                        ProductList.RemoveAt(index);
                        ProductList.Insert(index, p);
                        // Clear
                        Empty();
                    }
                    else _modalService.ShowModal(ModalType.Error, "Lưu không thành công", "Lỗi");
                    await Task.CompletedTask;
                }
            }
        }
        protected override void findCommand(ProductDTO productDTO)
        {
            if (productDTO == null) return;
            Product = (Pcharddisk)productDTO;
            Id = Product.Id;
            Company = Product.Company;
            Unit = Product.Unit;
            Series = Product.Series;
            Name = Product.Name;
            Price = Product.Price;
            Discount = Product.Discount;
            DetailPath = Product.DetailPath;
            AvatarPath = Product.AvatarPath;
            ImagePath = Product.ImagePath;
            Connect = Product.Connect;
            Storage = Product.Storage;
            Type = Product.Type;
            Remain = Product.Remain;
            RaisePropertyChanged(nameof(IsDefault));
            RaisePropertyChanged(nameof(IsEditable));
        }

        protected override async void deleteCommand()
        {
            MetroWindow metroWindow = (MetroWindow)Application.Current.MainWindow;
            if (metroWindow.ShowModalMessageExternal("Cảnh báo", "Bạn có chắc chắn xóa?", MessageDialogStyle.AffirmativeAndNegative) == MessageDialogResult.Affirmative)
            {
                if (Product.InMemory) await _unitOfWork.Pcharddisks.Delete(Product.Id);
                ProductList.Remove(Product);
                Empty();
            }
        }
        private Pcharddisk GetPcharddisk(string id, int remain)
        {
            return new()
            {
                Name = Name,
                Storage = Storage,
                Connect = Connect,
                Series = Series,
                Type = Type,
                Company = Company,
                DetailPath = DetailPath,
                Discount = Discount,
                Id = id,
                ImagePath = ImagePath,
                AvatarPath = AvatarPath,
                Price = Price,
                Unit = Unit,
                Remain = remain,
            };
        }
        private void Empty()
        {
            Product = null;
            Id = null;
            Company = null; Unit = null; Series = null;
            Name = null; Storage = null; Connect = null; Type = null;
            AvatarPath = null; Price = 0; Discount = 0;
            ImagePath = null; DetailPath = null; Remain = 0;
            RaisePropertyChanged(nameof(IsEditable));
            RaisePropertyChanged(nameof(IsDefault));
        }
        public override async void OnNavigatedTo(NavigationContext navigationContext)
        {
            clear();
            ProductList = new(await _unitOfWork.Pcharddisks.GetAll());
        }
    }
}
