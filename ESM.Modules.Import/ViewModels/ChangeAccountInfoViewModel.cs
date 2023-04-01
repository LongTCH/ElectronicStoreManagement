﻿using ESM.Core;
using ESM.Core.ShareServices;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using System;
using ESM.Modules.DataAccess.Infrastructure;
using ESM.Core.ShareStores.Address;
using System.Linq;
using ESM.Modules.DataAccess.DTOs;
using System.IO;
using static ESM.Modules.Import.Utilities.StaticData;

namespace ESM.Modules.Import.ViewModels
{
    public class ChangeAccountInfoViewModel : BindableBase, INavigationAware, IRegionMemberLifetime
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRegionManager _regionManager;
        private readonly IModalService _modalService;
        private readonly IOpenDialogService _openDialogService;

        public ChangeAccountInfoViewModel(IUnitOfWork unitOfWork,
            IRegionManager regionManager,
            IModalService modalService,
            ICityListService cityListService,
            IOpenDialogService openDialogService)
        {
            _unitOfWork = unitOfWork;
            _regionManager = regionManager;
            _modalService = modalService;
            _openDialogService = openDialogService;
            GetCityList(cityListService).Await();
            SaveChangeCommand = new DelegateCommand(async () => await saveChange());
            AddAvatarCommand = new DelegateCommand(addAvatarCommand);
            FindAccountCommand = new DelegateCommand(async () => await findAccountCommand());
        }
        private string CurrentAccoutId;
        private IEnumerable<City>? cities;
        public IEnumerable<City>? Cities
        {
            get => cities;
            set => SetProperty(ref cities, value);
        }
        private IEnumerable<District>? districts;
        public IEnumerable<District>? Districts
        {
            get => districts;
            set => SetProperty(ref districts, value);
        }
        private IEnumerable<Sub_district>? sub_districts;
        public IEnumerable<Sub_district>? Sub_districts
        {
            get => sub_districts;
            set => SetProperty(ref sub_districts, value);
        }
        public IEnumerable<string> Gender { get; } = StaticData.GenderList;
        public IEnumerable<string> ListRole { get; } = RoleList;
        public string? Avatar_Path { get; set; } = null;
        private int role = 0;
        public int Role
        {
            get => role;
            set => SetProperty(ref role, value);
        }
        private string id = "";
        public string Id
        {
            get => id;
            set => SetProperty(ref id, value);
        }
        private string? _firstName;
        [Required]
        public string? FirstName
        {
            get => _firstName;
            set => SetProperty(ref _firstName, value);
        }
        private string? _lastName;
        [Required]
        public string? LastName
        {
            get => _lastName;
            set => SetProperty(ref _lastName, value);
        }

        private string? _email;
        [RegularExpression(@"^[a-z0-9_\+-]+(\.[a-z0-9_\+-]+)*@[a-z0-9-]+(\.[a-z0-9]+)*\.([a-z]{2,4})$", ErrorMessage = "Invalid email format.")]
        [EmailAddress]
        public string? Email
        {
            get => _email;
            set => SetProperty(ref _email, value, () => ValidateProperty(value, nameof(Email)));
        }
        private string? _phone;
        [Phone]
        public string? Phone
        {
            get => _phone;
            set
            => SetProperty(ref _phone, value, () => ValidateProperty(value, nameof(Phone)));
        }
        public string SelectedGender { get; set; }
        private City? selectedCity;
        public City? SelectedCity
        {
            get => selectedCity;
            set => SetProperty(ref selectedCity, value, getDistricts);
        }
        private District? selectedDistrict;

        public District? SelectedDistrict
        {
            get => selectedDistrict;
            set => SetProperty(ref selectedDistrict, value, getSubDistricts);
        }
        private Sub_district? selectedSubDistrict;
        public Sub_district? SelectedSub_district
        {
            get => selectedSubDistrict;
            set => SetProperty(ref selectedSubDistrict, value);
        }
        public string? Street { get; set; }
        public DateTime BirthDay { get; set; }
        public bool IsDefault => !File.Exists(Avatar_Path);
        public DelegateCommand AddAvatarCommand { get; }
        public DelegateCommand SaveChangeCommand { get; }
        public DelegateCommand FindAccountCommand { get; }

        public bool KeepAlive => false;

        private void getDistricts()
        {
            if (SelectedCity != null)
            { Districts = SelectedCity.level2s; }
            else
            {
                Districts = null;
            }
        }
        private void getSubDistricts()
        {
            if (SelectedDistrict != null) { Sub_districts = SelectedDistrict.level3s; }
            else Sub_districts = null;
        }
        private async Task GetCityList(ICityListService cityListService)
        {
            Task<IEnumerable<City>?> task = new(() => cityListService.GetList());
            task.Start();
            await task;
            Cities = task.Result;
        }
        private async Task saveChange()
        {
            if (Id != CurrentAccoutId)
            {
                _modalService.ShowModal(ModalType.Error, "You changed ID", "Warning");
                return;
            }

            if (SelectedCity == null || SelectedDistrict == null || SelectedSub_district == null || Street == null)
            {
                _modalService.ShowModal(ModalType.Error, "Enter full address", "Warning");
                return;
            }
            if (SelectedGender == null)
            {
                _modalService.ShowModal(ModalType.Error, "Choose your gender", "Warning");
                return;
            }
            if (DateTime.Compare(BirthDay!, DateTime.Today) >= 0)
            {
                _modalService.ShowModal(ModalType.Error, "Must be earlier than current day", "Birthday Invalid");
                return;
            }
            AccountDTO accountDTO = new()
            {
                Id = Id!,
                PasswordHash = "00000000000000000",
                FirstName = FirstName!,
                LastName = LastName!,
                EmailAddress = Email!,
                Phone = Phone!,
                Gender = SelectedGender!.Equals(Gender.ElementAt(0))!,
                Birthday = DateTime.SpecifyKind(BirthDay, DateTimeKind.Utc),
                City = SelectedCity!.ToString(),
                District = SelectedDistrict!.ToString(),
                SubDistrict = SelectedSub_district!.ToString(),
                Street = Street!.ToString(),
                AvatarPath = Avatar_Path
            };
            try
            {
                _unitOfWork.Accounts.Update(accountDTO);
                await _unitOfWork.SaveChangesAsync();

                _modalService.ShowModal(ModalType.Information, "Saved change", "Success");
                _regionManager.RequestNavigate(RegionNames.ContentRegion, ViewNames.ChangeAccountInfoView);

            }
            catch (Exception ex) { _modalService.ShowModal(ModalType.Error, ex.Message, "Error"); }
        }
        private void addAvatarCommand()
        {
            Avatar_Path = _openDialogService.FileDialog(FileType.Image);
            if (Avatar_Path != null)
            {
                RaisePropertyChanged(nameof(IsDefault));
                RaisePropertyChanged(nameof(Avatar_Path));
            }
        }
        private async Task findAccountCommand()
        {
            Task<AccountDTO> task = new(() => _unitOfWork.Accounts.GetById(Id));
            task.Start();
            await task;
            var CurrentAccout = task.Result;
            if (CurrentAccout == null)
            {
                _modalService.ShowModal(ModalType.Error, "ID does not exist", "Error");
                return;
            }
            CurrentAccoutId = CurrentAccout.Id;
            FirstName = CurrentAccout.FirstName;
            LastName = CurrentAccout.LastName;
            Email = CurrentAccout.EmailAddress;
            Phone = CurrentAccout.Phone;
            SelectedGender = (CurrentAccout.Gender) ? Gender.ElementAt(0) : Gender.ElementAt(1);
            BirthDay = DateTime.SpecifyKind(CurrentAccout.Birthday, DateTimeKind.Local);
            try
            {
                SelectedCity = Cities.FirstOrDefault(s => s.name == CurrentAccout.City);
                Districts = SelectedCity.level2s;
                SelectedDistrict = SelectedCity.level2s.FirstOrDefault(s => s.name == CurrentAccout.District);
                Sub_districts = SelectedDistrict.level3s;
                SelectedSub_district = SelectedDistrict.level3s.FirstOrDefault(s => s.name == CurrentAccout.SubDistrict);
            }
            catch (Exception)
            {

            }
            Street = CurrentAccout.Street;
            Avatar_Path = CurrentAccout.AvatarPath;
            RaisePropertyChanged(nameof(SelectedGender));
            RaisePropertyChanged(nameof(Street));
            RaisePropertyChanged(nameof(IsDefault));
            RaisePropertyChanged(nameof(Avatar_Path));
            RaisePropertyChanged(nameof(BirthDay));
        }
        private void ValidateProperty<T>(T value, string name)
        {
            Validator.ValidateProperty(value, new ValidationContext(this, null, null)
            {
                MemberName = name
            });
        }
        public void OnNavigatedTo(NavigationContext navigationContext)
        {

        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return false;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {

        }
    }
}