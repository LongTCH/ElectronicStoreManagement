using ESM.Modules.DataAccess.DTOs;
using ESM.Modules.DataAccess.Infrastructure;
using ESM.Modules.DataAccess.Models;

namespace ESM.Modules.DataAccess.Repositories;
public interface IPcRepository : IBaseRepository<PcDTO>, IProductRepository
{
    string GetSuggestID();
}
public class PcRepository : BaseRepository<PcDTO>, IPcRepository
{
    public PcRepository(ESMDbContext context) : base(context)
    {
    }
    public override IEnumerable<PcDTO>? GetAll()
    {
        return _context.Pcs.AsQueryable()
                .Where(pc => pc.Remain > -1)
                .Select(pc => new PcDTO()
                {
                    Name = pc.Name,
                    Company = pc.Company,
                    Cpu = pc.Cpu,
                    DetailPath = @pc.DetailPath,
                    Discount = pc.Discount,
                    Id = pc.Id,
                    ImagePath = @pc.ImagePath,
                    Price = pc.Price,
                    Need = pc.Need,
                    Ram = pc.Ram,
                    Series = pc.Series,
                    AvatarPath = @pc.AvatarPath,
                    Remain = pc.Remain,
                    Unit = pc.Unit
                }).ToList();
    }
    public string GetSuggestID()
    {
        string? NewID = _context.Pcs.OrderBy(p => p.Id).LastOrDefault()?.Id;
        if (NewID == null) return StaticData.IdPrefix[ProductType.PC] + "0000000";
        int counter = Convert.ToInt32(NewID[2..]);
        ++counter;
        return StaticData.IdPrefix[ProductType.PC] + counter.ToString().PadLeft(7, '0');
    }
    public IEnumerable<ReportMock> GetSoldNumberMonthDuration(DateTime startDate, DateTime endDate)
    {
        return GetSoldNumberMonthDuration(startDate, endDate, ProductType.PC);
    }

    public IEnumerable<ReportMock> GetSoldNumberQuarterDuration(DateTime startDate, DateTime endDate)
    {
        return GetSoldNumberQuarterDuration(startDate, endDate, ProductType.PC);
    }

    public IEnumerable<ReportMock> GetSoldNumberWeekDuration(DateTime startDate, DateTime endDate)
    {
        return GetSoldNumberWeekDuration(startDate, endDate, ProductType.PC);
    }

    public IEnumerable<ReportMock> GetSoldNumberYearDuration(DateTime startDate, DateTime endDate)
    {
        return GetSoldNumberYearDuration(startDate, endDate, ProductType.PC);
    }
}
