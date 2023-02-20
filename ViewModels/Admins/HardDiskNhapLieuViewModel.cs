using Models.Interfaces;
using System.Windows.Input;

namespace ViewModels.Admins;

public class HardDiskNhapLieuViewModel : ProductInputViewModel
{
    private readonly IUnitOfWork _unitOfWork;

    public HardDiskNhapLieuViewModel(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public ICommand SaveCommand { get; }
}
