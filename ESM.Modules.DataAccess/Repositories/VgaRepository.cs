using ESM.Modules.DataAccess.DTOs;
using ESM.Modules.DataAccess.Infrastructure;
using ESM.Modules.DataAccess.Models;

namespace ESM.Modules.DataAccess.Repositories;

public interface IVgaRepository : IBaseRepository<VgaDTO>, IProductRepository{
    string GetSuggestID();
}
public class VgaRepository : BaseRepository<VgaDTO>, IVgaRepository
{
    public VgaRepository(ESMDbContext context) : base(context)
    {
    }
    public override IEnumerable<VgaDTO>? GetAll()
    {
        return _context.Vgas.AsQueryable()
                .Where(vga => vga.Remain > -1)
                .Select(vga => new VgaDTO()
                {
                    Name = vga.Name,
                    Chip = vga.Chip,
                    Chipset = vga.Chipset,
                    Company = vga.Company,
                    DetailPath = @vga.DetailPath,
                    Discount = vga.Discount,
                    Gen = vga.Gen,
                    Id = vga.Id,
                    ImagePath = @vga.ImagePath,
                    Need = vga.Need,
                    AvatarPath = @vga.AvatarPath,
                    Series = vga.Series,
                    Unit = vga.Unit
                }).ToList();
    }
    public string GetSuggestID()
    {
        string? NewID = _context.Vgas.OrderBy(p => p.Id).LastOrDefault()?.Id;
        if (NewID == null) return StaticData.IdPrefix[ProductType.VGA] + "0000000";
        int counter = Convert.ToInt32(NewID[2..]);
        ++counter;
        return StaticData.IdPrefix[ProductType.VGA] + counter.ToString().PadLeft(7, '0');
    }
    public IEnumerable<ReportMock> GetSoldNumberMonthDuration(DateTime startDate, DateTime endDate)
    {
        return GetSoldNumberMonthDuration(startDate, endDate, ProductType.VGA);
    }

    public IEnumerable<ReportMock> GetSoldNumberQuarterDuration(DateTime startDate, DateTime endDate)
    {
        return GetSoldNumberQuarterDuration(startDate, endDate, ProductType.VGA);
    }

    public IEnumerable<ReportMock> GetSoldNumberWeekDuration(DateTime startDate, DateTime endDate)
    {
        return GetSoldNumberWeekDuration(startDate, endDate, ProductType.VGA);
    }

    public IEnumerable<ReportMock> GetSoldNumberYearDuration(DateTime startDate, DateTime endDate)
    {
        return GetSoldNumberYearDuration(startDate, endDate, ProductType.VGA);
    }
}
