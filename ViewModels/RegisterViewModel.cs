using Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Windows.Input;
using ViewModels.Commands;
using ViewModels.Services;
using ViewModels.Stores.Accounts;
using ViewModels.Stores.Address;

namespace ViewModels;

public class RegisterViewModel : ViewModelBase
{
    private readonly ESMDbContext _esmDbContext;
    private readonly AccountStore? _accountStore;
    private readonly INavigationService _navigationService;
    private readonly INavigationService _openNotifyView;
    private HashSet<string> _error = new HashSet<string>() { nameof(FirstName), nameof(LastName), nameof(Email), nameof(Phone) };

    public List<City> Cities { get; set; }
    public IEnumerable<District> Districts { get; set; }
    public IEnumerable<Sub_district> Sub_districts { get; set; }

    private string _firstName;
    [Required]
    public string FirstName
    {
        get => _firstName;
        set
        {
            _firstName = value;
            _error.Add(nameof(FirstName));
            OnPropertyChanged(nameof(CanClick));
            ValidateProperty(value, nameof(FirstName));
            OnPropertyChanged(nameof(FirstName));
            _error.Remove(nameof(FirstName));
            OnPropertyChanged(nameof(CanClick));
        }
    }
    private string _lastName;
    [Required]
    public string LastName
    {
        get => _lastName;
        set
        {
            _lastName = value;
            _error.Add(nameof(LastName));
            OnPropertyChanged(nameof(CanClick));
            ValidateProperty(value, nameof(LastName));
            OnPropertyChanged(nameof(LastName));
            _error.Remove(nameof(LastName));
            OnPropertyChanged(nameof(CanClick));
        }
    }

    private string _email;
    [RegularExpression("^[a-z0-9_\\+-]+(\\.[a-z0-9_\\+-]+)*@[a-z0-9-]+(\\.[a-z0-9]+)*\\.([a-z]{2,4})$", ErrorMessage = "Invalid email format.")]
    [EmailAddress]
    public string Email
    {
        get => _email;
        set
        {
            _email = value;
            _error.Add(nameof(Email));
            OnPropertyChanged(nameof(CanClick));
            ValidateProperty(value, nameof(Email));
            OnPropertyChanged(nameof(Email));
            _error.Remove(nameof(Email));
            OnPropertyChanged(nameof(CanClick));
        }
    }
    private string _phone;
    [Phone]
    public string Phone
    {
        get => _phone;
        set
        {
            _phone = value;
            _error.Add(nameof(Phone));
            OnPropertyChanged(nameof(CanClick));
            ValidateProperty(value, nameof(Phone));
            OnPropertyChanged(nameof(Phone));
            _error.Remove(nameof(Phone));
            OnPropertyChanged(nameof(CanClick));
        }
    }
    private City _selectedCity;
    [Required]
    private City SelectedCity
    {
        get => _selectedCity;
        set
        {
            _selectedCity = value;
            OnPropertyChanged(nameof(Districts));
        }
    }
    private District _selectedDistrict;
    [Required]
    public District SelectedDistrict
    {
        get => _selectedDistrict;
        set
        {
            _selectedDistrict = value;
            OnPropertyChanged(nameof(Sub_districts));
        }
    }
    private Sub_district _selectedSubDistrict;
    [Required]
    private Sub_district SelectedSub_district
    {
        get => _selectedSubDistrict;
        set
        {
            _selectedSubDistrict = value;

        }
    }
    [Required]
    public string Street { get; set; }
    public bool CanClick => _error.Count == 0;
    public ICommand GetDistricts { get; }
    public ICommand GetSub_districts { get; }
    public ICommand Sub_districtChanged { get; }

    public ICommand SignUpCommand { get; } = null!;
    public RegisterViewModel(ESMDbContext esmDbContext,
        AccountStore? accountStore,
        INavigationService navigationService,
        INavigationService openNotifyView)
    {
        _esmDbContext = esmDbContext;
        _accountStore = accountStore;
        _navigationService = navigationService;
        _openNotifyView = openNotifyView;

        Cities = new CitiesSortCommand(new GetCitiesCommand().GetCitiesList().ToList<City>()).GetSortedCities();
        GetDistricts = new RelayCommand<City>(p => { if (p != null) { Districts = p.level2s; SelectedCity = p; } });
        GetSub_districts = new RelayCommand<District>(p => { if (p != null) { Sub_districts = p.level3s; SelectedDistrict = p; } });
        Sub_districtChanged = new RelayCommand<Sub_district>(p => { if (p != null) SelectedSub_district = p; });
    }
    private void ValidateProperty<T>(T value, string name)
    {
        Validator.ValidateProperty(value, new ValidationContext(this, null, null)
        {
            MemberName = name
        });
    }
}