using ESM.Core;
using ESM.Core.ShareServices;
using ESM.Core.ShareStores.Address;
using ESM.Modules.DataAccess.DTOs;
using ESM.Modules.DataAccess.Infrastructure;
using ESM.Modules.DataAccess.Models;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using static ESM.Modules.Import.Utilities.StaticData;

namespace ESM.Modules.Import.ViewModels
{
    public class RegisterViewModel : BindableBase, INavigationAware, IRegionMemberLifetime
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRegionManager _regionManager;
        private readonly IModalService _modalService;
        private readonly IOpenDialogService _openDialogService;

        public RegisterViewModel(IUnitOfWork unitOfWork,
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
            SignUpCommand = new DelegateCommand(async () => await signUp());
            AddAvatarCommand = new DelegateCommand(addAvatarCommand);
            getSuggestID();
        }
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
            set => SetProperty(ref role, value, getSuggestID);
        }
        private void getSuggestID()
        {
            SuggestID = _unitOfWork.Accounts.GetSuggestAccountId(Role + DateTime.UtcNow.Year.ToString());
            RaisePropertyChanged(nameof(SuggestID));
        }
        public string SuggestID { get; set; }
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
        public DateTime BirthDay { get; set; } = DateTime.Today.AddYears(-18);
        public bool IsDefault => !File.Exists(Avatar_Path);
        public DelegateCommand AddAvatarCommand { get; }
        public DelegateCommand SignUpCommand { get; } = null!;

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
        private async Task signUp()
        {
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
            Account accountDTO = new()
            {
                Id = SuggestID!,
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
                _unitOfWork.Accounts.Add(accountDTO);
                await _unitOfWork.SaveChangesAsync();
                _modalService.ShowModal(ModalType.Information, "New account created", "Success");
                _regionManager.RequestNavigate(RegionNames.ContentRegion, ViewNames.RegisterView);
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
