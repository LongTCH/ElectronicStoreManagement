using ESM.Core.ShareServices;
using ESM.Modules.DataAccess;
using ESM.Modules.DataAccess.Infrastructure;
using ESM.Modules.DataAccess.Models;
using MahApps.Metro.Controls.Dialogs;
using MahApps.Metro.Controls;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Net.Sockets;

namespace ESM.Modules.Import.ViewModels
{
    public class PhoneInputViewModel : BaseProductInputViewModel<Smartphone>
    {
        public PhoneInputViewModel(IUnitOfWork unitOfWork, IOpenDialogService openDialogService, IModalService modalService) : base(unitOfWork, openDialogService, modalService)
        {
        }
        public string Header => "Phone";
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
        private string storage;


        public string Storage
        {
            get => storage?.Trim();
            set => SetProperty(ref storage, value);
        }

        protected override void findCommand(ProductDTO productDTO)
        {
            if (productDTO == null) return;
            Product = (Smartphone)productDTO;
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
            Storage = Product.Storage;
            RaisePropertyChanged(nameof(IsDefault));
            RaisePropertyChanged(nameof(IsEditable));
        }
        protected override void addCommand()
        {
            Smartphone p = new();
            p.Id = GetNewID(ProductType.SMARTPHONE);
            p.InMemory = false;
            ProductList.Add(p);
            findCommand(p);
            TurnOnEditCommand.Execute();
        }
        protected override async Task saveCommand()
        {
            if (Company == null || Unit == null || Name == null ||
                   Cpu == null || Ram == null || Storage == null)
            {
                _modalService.ShowModal(ModalType.Error, "Nhập tất cả thông tin cần thiết", "Cảnh báo");
                return;
            }
            MetroWindow metroWindow = (MetroWindow)Application.Current.MainWindow;
            if (metroWindow.ShowModalMessageExternal("Thông báo", "Bạn có chắc chắn lưu?", MessageDialogStyle.AffirmativeAndNegative) == MessageDialogResult.Affirmative)
            {
                var p = GetPhone(Id, Remain);
                if (Product.InMemory)
                {
                    var res = await _unitOfWork.Smartphones.Update(p);
                    if ((bool)res)
                    {
                        _modalService.ShowModal(ModalType.Information, "Cập nhật thành công", "Thông báo");
                        // Clear
                        var index = ProductList.IndexOf(Product);
                        ProductList.RemoveAt(index);
                        ProductList.Insert(index, p);
                        Empty();
                    }
                    else _modalService.ShowModal(ModalType.Error, "Có lỗi xảy ra", "Thông báo");
                }
                else
                {
                    var res = await _unitOfWork.Smartphones.Add(p);
                    if ((bool)res)
                    {
                        _modalService.ShowModal(ModalType.Information, "Đã lưu", "Thông báo");
                        // Clear
                        Empty();
                        var index = ProductList.IndexOf(ProductList.Where(x => x.Id == p.Id).First());
                        ProductList.RemoveAt(index);
                        ProductList.Insert(index, p);
                    }
                    else _modalService.ShowModal(ModalType.Error, "Lưu không thành công", "Lỗi");
                    await Task.CompletedTask;
                }
            }
        }

        protected override async void deleteCommand()
        {
            MetroWindow metroWindow = (MetroWindow)Application.Current.MainWindow;
            if (metroWindow.ShowModalMessageExternal("Cảnh báo", "Bạn có chắc chắn xóa?", MessageDialogStyle.AffirmativeAndNegative) == MessageDialogResult.Affirmative)
            {
                if (Product.InMemory) await _unitOfWork.Smartphones.Delete(Product.Id);
                ProductList.Remove(Product);
                Empty();
            }
        }
        private Smartphone GetPhone(string id, int remain)
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
                Storage = Storage,
                Remain = remain,
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
            Storage = null;
            Remain = 0;
            RaisePropertyChanged(nameof(IsEditable));
            RaisePropertyChanged(nameof(IsDefault));
        }

        protected override void clear()
        {
            findCommand(Product);
        }
    }
}
