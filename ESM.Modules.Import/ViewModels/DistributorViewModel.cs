using ESM.Core;
using ESM.Core.ShareServices;
using ESM.Modules.DataAccess.Infrastructure;
using ESM.Modules.DataAccess.Models;
using MahApps.Metro.Controls.Dialogs;
using MahApps.Metro.Controls;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace ESM.Modules.Import.ViewModels
{
    public class DistributorViewModel : BindableBase, INavigationAware
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IModalService _modalService;

        public DistributorViewModel(IUnitOfWork unitOfWork, IModalService modalService)
        {
            _unitOfWork = unitOfWork;
            _modalService = modalService;
            AddCommand = new(async () => await addCommand());
            CancelCommand = new(async (p) => await clearCommand(p));
            SaveCommand = new(async (p) => await saveCommand(p));
            DeleteCommand = new(async (p) => await deleteCommand(p));
        }
        private ObservableCollection<Provider> providers;
        public ObservableCollection<Provider> Providers
        {
            get => providers;
            set => SetProperty(ref providers, value);
        }
        public DelegateCommand AddCommand { get; }
        public DelegateCommand<Provider> SaveCommand { get; }
        public DelegateCommand<Provider> CancelCommand { get; }
        public DelegateCommand<Provider> DeleteCommand { get; }
        private async Task addCommand()
        {
            if (Providers == null) return;
            var id1 = await _unitOfWork.Providers.GetSuggestId();
            var id2 = Providers.OrderBy(x => x.Id).LastOrDefault()?.Id;
            if (id2 != null) id1 = Math.Max(id1, id2.Value + 1);
            Providers.Add(new()
            {
                Id = id1,
            });
        }
        private async Task clearCommand(Provider provider)
        {
            if (provider is null) return;
            MetroWindow metroWindow = (MetroWindow)Application.Current.MainWindow;
            if (metroWindow.ShowModalMessageExternal("Cảnh báo", "Bạn có chắc chắn hủy?", MessageDialogStyle.AffirmativeAndNegative) == MessageDialogResult.Affirmative)
            {
                if (await _unitOfWork.Providers.IsIdExist(provider.Id.ToString()))
                {
                    Providers[Providers.IndexOf(provider)] = await _unitOfWork.Providers.GetById(provider.Id.ToString());
                }
                else Providers[Providers.IndexOf(provider)] = new() { Id = provider.Id };
                Providers.Refresh();
            }
        }
        private async Task saveCommand(Provider provider)
        {
            if (provider is null) return;
            MetroWindow metroWindow = (MetroWindow)Application.Current.MainWindow;
            if (metroWindow.ShowModalMessageExternal("Cảnh báo", "Bạn có chắc chắn lưu?", MessageDialogStyle.AffirmativeAndNegative) == MessageDialogResult.Affirmative)
            {
                if (string.IsNullOrWhiteSpace(provider.ProviderName))
                {
                    _modalService.ShowModal(ModalType.Error, "Nhập tên nhà cung cấp", "Thông báo");
                    return;
                }
                bool res;
                if (await _unitOfWork.Providers.IsIdExist(provider.Id.ToString()))
                {
                    res = (bool)await _unitOfWork.Providers.Update(provider);
                    if (res) _modalService.ShowModal(ModalType.Information, "Cập nhật thành công", "Thông báo");
                    else _modalService.ShowModal(ModalType.Error, "Có lỗi xảy ra", "Thất bại");
                }
                else
                {
                    res = (bool)await _unitOfWork.Providers.Add(provider);
                    if (res) _modalService.ShowModal(ModalType.Information, "Thêm thành công", "Thông báo");
                    else _modalService.ShowModal(ModalType.Error, "Có lỗi xảy ra", "Thất bại");
                }
                if (res)
                {
                    Providers.Refresh();
                }
            }
        }

        private async Task deleteCommand(Provider provider)
        {
            if (provider is null) return;
            MetroWindow metroWindow = (MetroWindow)Application.Current.MainWindow;
            if (metroWindow.ShowModalMessageExternal("Cảnh báo", "Bạn có chắc chắn xóa?", MessageDialogStyle.AffirmativeAndNegative) == MessageDialogResult.Affirmative)
            {
                if (await _unitOfWork.Providers.IsIdExist(provider.Id.ToString()))
                {
                    await _unitOfWork.Providers.Delete(provider.Id.ToString());
                }
                Providers.Remove(provider);
                Providers.Refresh();
            }
        }
            public async void OnNavigatedTo(NavigationContext navigationContext)
            {
                Providers = new();
                var list = await _unitOfWork.Providers.GetAll();
                foreach (var item in list) Providers.Add(item);
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
