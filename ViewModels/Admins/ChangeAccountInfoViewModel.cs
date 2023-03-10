using Microsoft.Win32;
using Models.DTOs;
using Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Markup;
using ViewModels.Commands;
using ViewModels.MyMessageBox;
using ViewModels.Services;
using ViewModels.Stores.Address;

namespace ViewModels.Admins;

public class ChangeAccountInfoViewModel : ViewModelBase
{
    private readonly INavigationService _navigationService;
    private readonly IUnitOfWork _unitOfWork;

    private ObservableCollection<string> _error = new ObservableCollection<string>();

    public List<City>? Cities { get; set; }
    public IEnumerable<District>? Districts { get; set; }
    public IEnumerable<Sub_district>? Sub_districts { get; set; }
    public List<string> Gender { get; } = new GetGenderListCommand().Execute();
    private bool UpdateAddress = false;
    public string? Avatar_Path { get; set; } = null;
    private string? _id;
    [Required]
    [RegularExpression(@"\d{9}", ErrorMessage = "ID must contain 9 digital characters")]
    public string? Id
    {
        get => _id;
        set
        {
            _id = value;
            if (!_error.Contains(nameof(Id)))
                _error.Add(nameof(Id));
            ValidateProperty(value, nameof(Id));
            _error.Remove(nameof(Id));
        }
    }
    private string? _firstName;
    [Required]
    public string? FirstName
    {
        get => _firstName;
        set
        {
            _firstName = value;
            if (!_error.Contains(nameof(FirstName)))
                _error.Add(nameof(FirstName));
            ValidateProperty(value, nameof(FirstName));
            _error.Remove(nameof(FirstName));
        }
    }
    private string? _lastName;
    [Required]
    public string? LastName
    {
        get => _lastName;
        set
        {
            _lastName = value;
            if (!_error.Contains(nameof(LastName)))
                _error.Add(nameof(LastName));
            ValidateProperty(value, nameof(LastName));
            _error.Remove(nameof(LastName));
        }
    }

    private string? _email;
    [RegularExpression(@"^[a-z0-9_\+-]+(\.[a-z0-9_\+-]+)*@[a-z0-9-]+(\.[a-z0-9]+)*\.([a-z]{2,4})$", ErrorMessage = "Invalid email format.")]
    [EmailAddress]
    public string? Email
    {
        get => _email;
        set
        {
            _email = value;
            if (!_error.Contains(nameof(Email)))
                _error.Add(nameof(Email));
            ValidateProperty(value, nameof(Email));
            _error.Remove(nameof(Email));
        }
    }
    private string? _phone;
    [Phone]
    public string? Phone
    {
        get => _phone;
        set
        {
            _phone = value;
            if (!_error.Contains(nameof(Phone)))
                _error.Add(nameof(Phone));
            ValidateProperty(value, nameof(Phone));
            _error.Remove(nameof(Phone));
        }
    }
    public City? SelectedCity { get; set; }
    public District? SelectedDistrict { get; set; }
    public Sub_district? SelectedSub_district { get; set; }
    public string? Street { get; set; }
    public string? SelectedGender { get; set; }
    public DateTime BirthDay { get; set; }
    public string DateFormat => CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern;
    public XmlLanguage Language => XmlLanguage.GetLanguage(CultureInfo.CurrentCulture.IetfLanguageTag);
    public bool CanClick => _error.Count == 0;
    public bool IsDefault => !File.Exists(Avatar_Path);
    public ICommand GetDistricts { get; }
    public ICommand GetSub_districts { get; }
    public ICommand AddAvatarCommand { get; }
    public ICommand SignUpCommand { get; } = null!;
    public ICommand FindAccountCommand { get; }
    public ChangeAccountInfoViewModel(IUnitOfWork unitOfWork, INavigationService navigationSerVice)
    {
        _navigationService = navigationSerVice;
        _unitOfWork = unitOfWork;
        Cities = new CitiesSortCommand(new GetCitiesCommand().GetCitiesList()?.ToList())?.GetSortedCities();
        GetDistricts = new RelayCommand<City>(getDistricts);
        GetSub_districts = new RelayCommand<District>(getSubDistricts);
        SignUpCommand = new RelayCommand<object>(_ => signUp());
        AddAvatarCommand = new RelayCommand<object>(_ => addAvatarCommand());
        FindAccountCommand = new RelayCommand<object>(_ => findAccountCommand());

        _error.CollectionChanged += new System.Collections.Specialized.NotifyCollectionChangedEventHandler((_, _) => OnPropertyChanged(nameof(CanClick)));
    }
    private void getDistricts(City p)
    {
        if (UpdateAddress == false) return;
        if (p != null)
        {
            Districts = p.level2s;
            SelectedCity = p;
        }
        else Districts = null;
        SelectedDistrict = null;
        OnPropertyChanged(nameof(Districts));
        OnPropertyChanged(nameof(SelectedDistrict));

    }
    private void getSubDistricts(District p)
    {
        if (UpdateAddress == false) return;
        if (p != null)
        {
            Sub_districts = p.level3s;
            SelectedDistrict = p;
        }
        else Sub_districts = null;
        SelectedSub_district = null;
        OnPropertyChanged(nameof(Sub_districts));
        OnPropertyChanged(nameof(SelectedSub_district));
    }
    private void signUp()
    {
        if (Id == null || _unitOfWork.Accounts.Get(Id) == null)
        {
            ErrorNotifyViewModel.Instance!.Show("ID does not exist", "Error");
            return;
        }
        if (SelectedCity == null || SelectedDistrict == null || SelectedSub_district == null || Street == null)
        {
            ErrorNotifyViewModel.Instance!.Show("Enter full address", "Warning");
            return;
        }
        if (SelectedGender == null)
        {
            ErrorNotifyViewModel.Instance!.Show("Choose your gender", "Warning");
            return;
        }
        if (DateTime.Compare(BirthDay, DateTime.Today) >= 0)
        {
            ErrorNotifyViewModel.Instance!.Show("Must be earlier than current day", "Birthday Invalid");
            return;
        }
        AccountDTO accountDTO = new AccountDTO()
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
        Task task = new(() => changeUser(accountDTO));
        task.Start();
    }
    private void changeUser(AccountDTO accountDTO)
    {
        _unitOfWork.Accounts.Update(accountDTO);
        _unitOfWork.Complete();
        InformationViewModel.Instance!.Show("Saved change", "Success");
        _navigationService.Navigate();
    }
    private void addAvatarCommand()
    {
        Avatar_Path = new FileCommand().Set(FileType.Image);
        if (Avatar_Path != null)
        {
            OnPropertyChanged(nameof(IsDefault));
            OnPropertyChanged(nameof(Avatar_Path));
        }
    }
    private void findAccountCommand()
    {
        AccountDTO? accountDTO = _unitOfWork.Accounts.Get(Id);
        if (accountDTO == null)
        {
            ErrorNotifyViewModel.Instance!.Show("ID does not exist", "Error");
            return;
        }
        FirstName = accountDTO.FirstName;
        LastName = accountDTO.LastName;
        Email = accountDTO.EmailAddress;
        Phone = accountDTO.Phone;
        SelectedGender = (accountDTO.Gender) ? Gender.ElementAt(0) : Gender.ElementAt(1);
        BirthDay = DateTime.SpecifyKind(accountDTO.Birthday, DateTimeKind.Local);
        try
        {
            SelectedCity = Cities.FirstOrDefault(s => s.name == accountDTO.City);
            Districts = SelectedCity.level2s;
            SelectedDistrict = SelectedCity.level2s.FirstOrDefault(s => s.name == accountDTO.District);
            Sub_districts = SelectedDistrict.level3s;
            SelectedSub_district = SelectedDistrict.level3s.FirstOrDefault(s => s.name == accountDTO.SubDistrict);
        }
        catch (Exception)
        {

        }
        Street = accountDTO.Street;
        Avatar_Path = accountDTO.AvatarPath;
        OnPropertyChanged(nameof(FirstName));
        OnPropertyChanged(nameof(LastName));
        OnPropertyChanged(nameof(Email));
        OnPropertyChanged(nameof(Phone));
        OnPropertyChanged(nameof(SelectedGender));
        OnPropertyChanged(nameof(BirthDay));
        OnPropertyChanged(nameof(SelectedCity));
        OnPropertyChanged(nameof(SelectedDistrict));
        OnPropertyChanged(nameof(SelectedSub_district));
        OnPropertyChanged(nameof(Street));
        OnPropertyChanged(nameof(IsDefault));
        OnPropertyChanged(nameof(Avatar_Path));
        OnPropertyChanged(nameof(Districts));
        OnPropertyChanged(nameof(Sub_districts));
        UpdateAddress = true;
    }
}
