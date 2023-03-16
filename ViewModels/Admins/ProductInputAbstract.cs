using Models.Interfaces;
using System;
using System.ComponentModel.DataAnnotations;
using System.Windows.Input;
using ViewModels.Commands;
using ViewModels.MyMessageBox;
using ViewModels.Validators;

namespace ViewModels.Admins;

public abstract class ProductInputAbstract : ViewModelBase
{
    protected readonly IUnitOfWork _unitOfWork;

    protected ProductInputAbstract(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        SelectFolder = new RelayCommand<object>(_ => getFolderPath());
        SelectDetail = new RelayCommand<object>(_ => getDetailPath());
        AddAvatarCommand = new RelayCommand<object>(_ => addAvatarCommand());
    }

    public abstract string? Id { get; }
    public string? Name { get; set; }
    protected decimal price;
    [Required]
    [PriceValidate(ErrorMessage = "Invalid Input")]
    public decimal Price
    {
        get => price;
        set
        {
            price = value;
            ValidateProperty(value, nameof(Price));
        }
    }
    protected double? discount;
    [Range(0, 100)]
    public double? Discount
    {
        get => discount;
        set
        {
            discount = value;
            ValidateProperty(value, nameof(Discount));
        }
    }
    public string? DetailPath { get; set; }
    public string? AvatarPath { get; set; }
    public string? Company { get; set; }
    public string? Unit { get; set; }
    public bool IsDefault => AvatarPath == null;
    public string? FolderPath { get; set; }
    public ICommand SelectFolder { get; }
    public ICommand SelectDetail { get; }
    public ICommand AddAvatarCommand { get; }
    protected virtual void saveCommand()
    {
        if (Id == null || Company == null || Unit == null)
        {
            ErrorNotifyViewModel.Instance!.Show("Enter all required value", "Warning");
            return;
        }
    }
    private void getFolderPath()
    {
        FolderPath = FolderCommand.Set();
        if (FolderPath != null) 
            OnPropertyChanged(nameof(FolderPath));
    }
    private void getDetailPath()
    {
        DetailPath = FileCommand.Set(FileType.Excel);
        if (DetailPath != null)
            OnPropertyChanged(nameof(DetailPath));
    }
    private void addAvatarCommand()
    {
        AvatarPath = FileCommand.Set(FileType.Image);
        if (AvatarPath != null)
        {
            OnPropertyChanged(nameof(IsDefault));
            OnPropertyChanged(nameof(AvatarPath));
        }
    }
}
