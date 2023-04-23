using ESM.Core.ShareServices;
using ESM.Modules.DataAccess;
using ESM.Modules.DataAccess.Infrastructure;
using ESM.Modules.DataAccess.Models;
using MahApps.Metro.Controls.Dialogs;
using MahApps.Metro.Controls;
using System.Threading.Tasks;
using System.Windows;
using Prism.Regions;
using System.Linq;

namespace ESM.Modules.Import.ViewModels
{
    public class CPUInputViewModel : BaseProductInputViewModel<Pccpu>
    {
        public CPUInputViewModel(IUnitOfWork unitOfWork, IOpenDialogService openDialogService, IModalService modalService) : base(unitOfWork, openDialogService, modalService)
        {
        }
        public string Header => "CPU";
        private string socket;
        public string Socket
        {
            get => socket?.Trim();
            set => SetProperty(ref socket, value);
        }

        private string need;
        public string Need
        {
            get => need?.Trim();
            set => SetProperty(ref need, value);
        }

        private string series;
        public string Series
        {
            get => series?.Trim();
            set => SetProperty(ref series, value);
        }
        protected override void addCommand()
        {
            Pccpu pccpuDTO = Getcpu(GetNewID(ProductType.CPU), 0);
            pccpuDTO.InMemory = false;
            ProductList.Add(pccpuDTO);
            findCommand(pccpuDTO);
            TurnOnEditCommand.Execute();
        }

        protected override void clear()
        {
            findCommand(Product);
        }

        protected override async void deleteCommand()
        {
            MetroWindow metroWindow = (MetroWindow)Application.Current.MainWindow;
            if (metroWindow.ShowModalMessageExternal("Cảnh báo", "Bạn có chắc chắn xóa?", MessageDialogStyle.AffirmativeAndNegative) == MessageDialogResult.Affirmative)
            {
                if (Product.InMemory) await _unitOfWork.Pccpus.Delete(Product.Id);
                ProductList.Remove(Product);
                Empty();
            }
        }

        protected override void findCommand(ProductDTO productDTO)
        {
            if (productDTO == null) return;
            Product = (Pccpu)productDTO;
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
            Socket = Product.Socket;
            Need = Product.Need;
            Remain = Product.Remain;
            RaisePropertyChanged(nameof(IsDefault));
        }

        protected override async Task saveCommand()
        {
            if (Company == null || Unit == null || Name == null || Socket == null)
            {
                _modalService.ShowModal(ModalType.Error, "Nhập tất cả thông tin cần thiết", "Cảnh báo");
                return;
            }
            MetroWindow metroWindow = (MetroWindow)Application.Current.MainWindow;
            if (metroWindow.ShowModalMessageExternal("Thông báo", "Bạn có chắc chắn lưu?", MessageDialogStyle.AffirmativeAndNegative) == MessageDialogResult.Affirmative)
            {
                var p = Getcpu(Id, Remain);
                if (Product.InMemory)
                {
                    var res = await _unitOfWork.Pccpus.Update(p);
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
                    var res = await _unitOfWork.Pccpus.Add(p);
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
        private Pccpu Getcpu(string id, int remain)
        {
            return new()
            {
                Id = id,
                Company = Company,
                Unit = Unit,
                Series = Series,
                Name = Name,
                Price = Price,
                Discount = Discount,
                DetailPath = DetailPath,
                AvatarPath = AvatarPath,
                ImagePath = ImagePath,
                Socket = Socket,
                Need = Need,
                Remain = remain,
            };
        }
        private void Empty()
        {
            Product = null;
            Id = null;
            Company = null;
            Unit = null;
            Series = null;
            Name = null;
            Price = 0;
            Discount = 0;
            DetailPath = null;
            AvatarPath = null;
            ImagePath = null;
            Socket = null;
            Need = null;
            Remain = 0;
            IsEditable = false;
            RaisePropertyChanged(nameof(IsDefault));
        }
        public override async void OnNavigatedTo(NavigationContext navigationContext)
        {
            clear();
            ProductList = new(await _unitOfWork.Pccpus.GetAll());
        }
    }
}