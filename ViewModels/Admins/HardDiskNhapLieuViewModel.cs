using Models.DTOs;
using Models.Interfaces;
using System;
using System.Windows.Input;
using ViewModels.Commands;
using ViewModels.MyMessageBox;

namespace ViewModels.Admins;

public class HardDiskNhapLieuViewModel : ProductInputAbstract
{
    public string? Storage { get; set; }
    public string Connect { get; set; }
    public string? Series { get; set; }
    public string? Type { get; set; }

    public HardDiskNhapLieuViewModel(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
        SaveCommand = new RelayCommand<object>(_ => saveCommand());
        Id = _unitOfWork.Pcharddisks.GetLastID();
    }

    public ICommand SaveCommand { get; }

    public override string? Id { get; }

    protected override void saveCommand()
    {
        base.saveCommand();
        if (Name == null || Storage == null || Connect == null || Type == null
            || Company == null)
        {
            ErrorNotifyViewModel.Instance!.Show("Enter all required value", "Warning");
            return;
        }
        PcharddiskDTO pcharddiskDTO = new()
        {
            Name = Name,
            Storage = Storage,
            Connect = Connect,
            Series = Series,
            Type = Type,
            Company = Company,
            DetailPath = DetailPath,
            Discount = Discount,
            Id = Id!,
            ImagePath = FolderPath,
            AvatarPath = AvatarPath,
            Price = Price,
            Unit = Unit,
            Remain = 0
        };
        try
        {
            _unitOfWork.Pcharddisks.Add(pcharddiskDTO);
            _unitOfWork.Complete();
            InformationViewModel.Instance.Show("New Hard Disk Added", "Success");
        }
        catch (Exception)
        {
            ErrorNotifyViewModel.Instance.Show("Add new Hard Disk failed", "Error");
        }
    }
}
