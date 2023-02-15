using Models;
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
using ViewModels.Services;
using ViewModels.Stores.Accounts;
using ViewModels.Stores.Address;

namespace ViewModels;

public class RegisterViewModel : ViewModelBase
{
    private readonly AccountStore? _accountStore;
    private readonly INavigationService _navigationService;
    private readonly INavigationService _openNotifyView;
    private ObservableCollection<string> _error = new ObservableCollection<string>() {
        nameof(FirstName), nameof(LastName), nameof(Email), nameof(Phone)};

    public List<City> Cities { get; set; }
    public IEnumerable<District>? Districts { get; set; }
    public IEnumerable<Sub_district>? Sub_districts { get; set; }
    public List<string> Gender { get; } = new() { "Nam", "Nữ" };

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
    [RegularExpression("^[a-z0-9_\\+-]+(\\.[a-z0-9_\\+-]+)*@[a-z0-9-]+(\\.[a-z0-9]+)*\\.([a-z]{2,4})$", ErrorMessage = "Invalid email format.")]
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
    private City? SelectedCity { get; set; }
    public District? SelectedDistrict { get; set; }
    private Sub_district? SelectedSub_district { get; set; }
    public string? Street { get; set; }
    private string? SelectedGender { get; set; }
    public DateTime? BirthDay { get; set; }
    public string DateFormat => CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern;
    public XmlLanguage Language => XmlLanguage.GetLanguage(CultureInfo.CurrentCulture.IetfLanguageTag);
    public bool CanClick => _error.Count == 0;
    public ICommand GetDistricts { get; }
    public ICommand GetSub_districts { get; }
    public ICommand Sub_districtChanged { get; }
    public ICommand GenderChanged { get; }

    public ICommand SignUpCommand { get; } = null!;
    public RegisterViewModel(AccountStore? accountStore,
        INavigationService navigationService,
        INavigationService openNotifyView)
    {
        _accountStore = accountStore;
        _navigationService = navigationService;
        _openNotifyView = openNotifyView;

        Cities = new CitiesSortCommand(new GetCitiesCommand().GetCitiesList().ToList()).GetSortedCities();
        GetDistricts = new RelayCommand<City>(getDistricts);
        GetSub_districts = new RelayCommand<District>(getSubDistricts);
        Sub_districtChanged = new RelayCommand<Sub_district>(p => SelectedSub_district = p);
        GenderChanged = new RelayCommand<string>(p => SelectedGender = p);
        SignUpCommand = new RelayCommand<object>(_ => signUp());

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
        if (SelectedCity == null || SelectedDistrict == null || SelectedSub_district == null || Street == null)
        {
            ErrorNotifyViewModel.Instance!.Show("Enter full address", "Warning");
            return;
        }
        if (SelectedGender == null)
            ErrorNotifyViewModel.Instance!.Show("Choose your gender", "Warning");
        if (BirthDay == null)
            ErrorNotifyViewModel.Instance!.Show("Choose your birthday", "Warning");

    }
    private void ValidateProperty<T>(T value, string name)
    {
        Validator.ValidateProperty(value, new ValidationContext(this, null, null)
        {
            MemberName = name
        });
    }
}