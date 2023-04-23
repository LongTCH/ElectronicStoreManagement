using ESM.Core.ShareServices;
using ESM.Modules.DataAccess;
using ESM.Modules.DataAccess.Infrastructure;
using ESM.Modules.DataAccess.Models;
using MahApps.Metro.Controls.Dialogs;
using MahApps.Metro.Controls;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Prism.Regions;

namespace ESM.Modules.Import.ViewModels
{
    public class PCInputViewModel : BaseProductInputViewModel<Pc>
    {
        public PCInputViewModel(IUnitOfWork unitOfWork, IOpenDialogService openDialogService, IModalService modalService) : base(unitOfWork, openDialogService, modalService)
        {

        }
        public string Header => "PC";
        private string cpu;
        public string Cpu
        {
            get => cpu?.Trim();
            set => SetProperty(ref cpu, value);
        }
        private string ram;
        public string Ram
        {
            get => ram?.Trim();
            set => SetProperty(ref ram, value);
        }
        private string series;
        public string Series
        {
            get => series?.Trim();
            set => SetProperty(ref series, value);
        }
        private string need;
        public string Need
        {
            get => need?.Trim();
            set => SetProperty(ref need, value);
        }
        protected override void addCommand()
        {
            Pc p = GetPc(GetNewID(ProductType.PC), 0);
            p.InMemory = false;
            ProductList.Add(p);
            findCommand(p);
            TurnOnEditCommand.Execute();
        }

        protected override async void deleteCommand()
        {
            MetroWindow metroWindow = (MetroWindow)Application.Current.MainWindow;
            if (metroWindow.ShowModalMessageExternal("Cảnh báo", "Bạn có chắc chắn xóa?", MessageDialogStyle.AffirmativeAndNegative) == MessageDialogResult.Affirmative)
            {
                if (Product.InMemory) await _unitOfWork.Pcs.Delete(Product.Id);
                ProductList.Remove(Product);
                Empty();
            }
        }
        protected override void clear()
        {
            findCommand(Product);
        }
        protected override void findCommand(ProductDTO productDTO)
        {
            if (productDTO == null) return;
            Product = (Pc)productDTO;
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
            Cpu = Product.Cpu;
            Ram = Product.Ram;
            Need = Product.Need;
            RaisePropertyChanged(nameof(IsDefault));
        }

        protected override async Task saveCommand()
        {
            if (Company == null || Unit == null || Name == null ||
                    Cpu == null || Ram == null)
            {
                _modalService.ShowModal(ModalType.Error, "Nhập tất cả thông tin cần thiết", "Cảnh báo");
                return;
            }
            MetroWindow metroWindow = (MetroWindow)Application.Current.MainWindow;
            if (metroWindow.ShowModalMessageExternal("Thông báo", "Bạn có chắc chắn lưu?", MessageDialogStyle.AffirmativeAndNegative) == MessageDialogResult.Affirmative)
            {
                var p = GetPc(Id, Remain);
                if (Product.InMemory)
                {
                    var res = await _unitOfWork.Pcs.Update(p);
                    if ((bool)res)
                    {
                        _modalService.ShowModal(ModalType.Information, "Cập nhật thành công", "Thông báo");
                        // Clear
                        Empty();
                        var index = ProductList.IndexOf(Product);
                        ProductList.RemoveAt(index);
                        ProductList.Insert(index, Product);
                    }
                    else _modalService.ShowModal(ModalType.Error, "Có lỗi xảy ra", "Thông báo");
                }
                else
                {
                    var res = await _unitOfWork.Pcs.Add(p);
                    if ((bool)res)
                    {
                        _modalService.ShowModal(ModalType.Information, "Đã lưu", "Thông báo");
                        // Clear
                        Empty();
                        ProductList.Remove(ProductList.Last());
                        ProductList.Add(p);
                    }
                    else _modalService.ShowModal(ModalType.Error, "Lưu không thành công", "Lỗi");
                    await Task.CompletedTask;
                }
            }
        }


        private Pc GetPc(string id, int remain)
        {
            return new()
            {
                Id = id,
                Cpu = Cpu,
                Company = Company,
                Unit = Unit,
                Series = Series,
                Name = Name,
                Price = Price,
                Discount = Discount,
                DetailPath = DetailPath,
                AvatarPath = AvatarPath,
                ImagePath = ImagePath,
                Ram = Ram,
                Need = Need,
                Remain = remain
            };
        }
        private void Empty()
        {
            Product = null;
            Id = null;
            Cpu = null;
            Company = null;
            Unit = null;
            Series = null;
            Name = null;
            Price = 0;
            Discount = 0;
            DetailPath = null;
            AvatarPath = null;
            ImagePath = null;
            Ram = null;
            Need = null;
            Remain = 0;
            IsEditable = false;
            RaisePropertyChanged(nameof(IsDefault));
        }

        public override async void OnNavigatedTo(NavigationContext navigationContext)
        {
            clear();
            ProductList = new(await _unitOfWork.Pcs.GetAll());
        }
    }
}
