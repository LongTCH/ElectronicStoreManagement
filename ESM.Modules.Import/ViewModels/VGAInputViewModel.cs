﻿using ESM.Core.ShareServices;
using ESM.Modules.DataAccess;
using ESM.Modules.DataAccess.Infrastructure;
using ESM.Modules.DataAccess.Models;
using MahApps.Metro.Controls.Dialogs;
using MahApps.Metro.Controls;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Prism.Regions;
using System.Net.Sockets;

namespace ESM.Modules.Import.ViewModels
{
    public class VGAInputViewModel : BaseProductInputViewModel<Vga>
    {
        public VGAInputViewModel(IUnitOfWork unitOfWork, IOpenDialogService openDialogService, IModalService modalService) : base(unitOfWork, openDialogService, modalService)
        {
        }
        public string Header => "VGA";
        private string chip;
        public string Chip
        {
            get => chip?.Trim();
            set => SetProperty(ref chip, value);
        }
        private string chipset;
        public string Chipset
        {
            get => chipset?.Trim();
            set => SetProperty(ref chipset, value);
        }
        private string vram;
        public string Vram
        {
            get => vram?.Trim();
            set => SetProperty(ref vram, value);
        }
        private string gen;
        public string Gen
        {
            get => gen?.Trim();
            set => SetProperty(ref gen, value);
        }
        private string series;
        public string Series
        {
            get => series?.Trim();
            set => SetProperty(ref series, value);
        }
        protected override void addCommand()
        {
            Vga p = GetVga(GetNewID(ProductType.VGA), 0);
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
                if (Product.InMemory) await _unitOfWork.Vgas.Delete(Product.Id);
                ProductList.Remove(Product);
                Empty();
            }
        }

        protected override void findCommand(ProductDTO productDTO)
        {
            if (productDTO == null) return;
            Product = (Vga)productDTO;
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
            Chip = Product.Chip;
            Chipset = Product.Chipset;
            Vram = Product.Vram;
            Gen = Product.Gen;
            RaisePropertyChanged(nameof(IsDefault));
        }
        protected override void clear()
        {
            findCommand(Product);
        }
        protected override async Task saveCommand()
        {
            if (Company == null || Unit == null || Name == null ||
                   Chip == null || Chipset == null || Vram == null || Gen == null)
            {
                _modalService.ShowModal(ModalType.Error, "Nhập tất cả thông tin cần thiết", "Cảnh báo");
                return;
            }
            MetroWindow metroWindow = (MetroWindow)Application.Current.MainWindow;
            if (metroWindow.ShowModalMessageExternal("Thông báo", "Bạn có chắc chắn lưu?", MessageDialogStyle.AffirmativeAndNegative) == MessageDialogResult.Affirmative)
            {
                var p = GetVga(Id, Remain);
                if (Product.InMemory)
                {
                    var res = await _unitOfWork.Vgas.Update(p);
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
                    var res = await _unitOfWork.Vgas.Add(p);
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
        private Vga GetVga(string id, int remain)
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
                Chip = Chip,
                Chipset = Chipset,
                Gen = Gen,
                Vram = Vram,
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
            Chip = null;
            Chipset = null;
            Gen = null;
            Vram = null;
            Remain = 0;
            IsEditable = false;
            RaisePropertyChanged(nameof(IsDefault));
        }

        public override async void OnNavigatedTo(NavigationContext navigationContext)
        {
            clear();
            ProductList = new(await _unitOfWork.Vgas.GetAll());
        }
    }
}
