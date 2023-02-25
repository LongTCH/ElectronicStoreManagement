using Microsoft.IdentityModel.Tokens;
using Microsoft.Win32;
using Models.DTOs;
using Models.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Input;
using System.Windows.Markup;
using ViewModels.Commands;
using ViewModels.MyMessageBox;
using ViewModels.Stores.Address;

namespace ViewModels;

public class RegisterViewModel : ViewModelBase
{
    private readonly IUnitOfWork _unitOfWork;
    private ObservableCollection<string> _error = new() {
        nameof(Id), nameof(FirstName), nameof(LastName), nameof(Email), nameof(Phone)};

    public List<City>? Cities { get; set; }
    public IEnumerable<District>? Districts { get; set; }
    public IEnumerable<Sub_district>? Sub_districts { get; set; }
    public List<string> Gender { get; } = new GetGenderListCommand().Execute();

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
    public string SuggestID => "1" + DateTime.UtcNow.Year.ToString() + _unitOfWork.Accounts.GetSuggestAccountIdCounter();
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
    public DateTime BirthDay { get; set; } = DateTime.Today;
    public string DateFormat => CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern;
    public XmlLanguage Language => XmlLanguage.GetLanguage(CultureInfo.CurrentCulture.IetfLanguageTag);
    public bool CanClick => _error.Count == 0;
    public bool IsDefault => !File.Exists(Avatar_Path);
    public ICommand GetDistricts { get; }
    public ICommand GetSub_districts { get; }
    public ICommand Sub_districtChanged { get; }
    public ICommand GenderChanged { get; }
    public ICommand SuggestCommand { get; }
    public ICommand AddAvatarCommand { get; }
    public ICommand SignUpCommand { get; } = null!;
    public RegisterViewModel(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;

        Cities = new CitiesSortCommand(new GetCitiesCommand().GetCitiesList()?.ToList()).GetSortedCities();
        GetDistricts = new RelayCommand<City>(getDistricts);
        GetSub_districts = new RelayCommand<District>(getSubDistricts);
        Sub_districtChanged = new RelayCommand<Sub_district>(p => SelectedSub_district = p);
        GenderChanged = new RelayCommand<string>(p => SelectedGender = p);
        SignUpCommand = new RelayCommand<object>(_ => signUp());
        SuggestCommand = new RelayCommand<object>(_ => OnPropertyChanged(nameof(SuggestID)));
        AddAvatarCommand = new RelayCommand<object>(_ => addAvatarCommand());

        _error.CollectionChanged += new System.Collections.Specialized.NotifyCollectionChangedEventHandler((_, _) => OnPropertyChanged(nameof(CanClick)));
    }
    private void getDistricts(City p)
    {
        if (p != null)
        { Districts = p.level2s; }
        else
        {
            Districts = null;
        }
        Sub_districts = null;
        OnPropertyChanged(nameof(Districts));
        OnPropertyChanged(nameof(Sub_districts));
        SelectedCity = p;
    }
    private void getSubDistricts(District p)
    {
        if (p != null) { Sub_districts = p.level3s; }
        else Sub_districts = null;
        OnPropertyChanged(nameof(Sub_districts));
        SelectedDistrict = p;
    }
    private void signUp()
    {
        if (string.IsNullOrWhiteSpace(Id))
        {
            ErrorNotifyViewModel.Instance!.Show("Enter ID", "Warning");
            return;
        }
        try
        {
            if (_unitOfWork.Accounts.Get(Id!) != null)
            {
                ErrorNotifyViewModel.Instance!.Show("ID has existed", "Error");
                return;
            }
        }
        catch (Exception ex)
        {
            ErrorNotifyViewModel.Instance!.Show(ex.Message, "Error");
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
        if (DateTime.Compare(BirthDay!, DateTime.Today) >= 0)
        {
            ErrorNotifyViewModel.Instance!.Show("Must be earlier than current day", "Birthday Invalid");
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
            _unitOfWork.Accounts.Add(accountDTO);
            _unitOfWork.Complete();
        }
        catch (Exception ex) { ErrorNotifyViewModel.Instance!.Show(ex.Message, "Error"); }
    }
    private void addAvatarCommand()
    {
        Avatar_Path = new ImagePathCommand().Set();
        if (Avatar_Path != null)
        {
            OnPropertyChanged(nameof(IsDefault));
            OnPropertyChanged(nameof(Avatar_Path));
        }
    }

}