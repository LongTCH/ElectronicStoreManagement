using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.Admins;

public class ProductInputViewModel : ViewModelBase
{
    public HardDiskNhapLieuViewModel HardDisk { get; }
    public MonitorNhapLieuViewModel Monitor { get; }
    public ProductInputViewModel(
        HardDiskNhapLieuViewModel hardDisk,
        MonitorNhapLieuViewModel monitor)
    {
        HardDisk = hardDisk;
        Monitor = monitor;
    }
}
