using ESM.Modules.DataAccess.DTOs;
using ESM.Modules.DataAccess.Infrastructure;
using ESM.Modules.DataAccess.Models;

namespace ESM.Modules.DataAccess.Repositories;
public interface IPccpuRepository : IBaseRepository<PccpuDTO>, IProductRepository {
    string GetSuggestID();
}
public class PccpuRepository : BaseRepository<PccpuDTO>, IPccpuRepository
{
    public PccpuRepository(ESMDbContext context) : base(context)
    {
    }
    public override IEnumerable<PccpuDTO>? GetAll()
    {
        return _context.Pccpus.AsQueryable()
                .Where(p => p.Remain > -1)
                .Select(pccpu => new PccpuDTO()
                {
                    Name = pccpu.Name,
                    ImagePath = @pccpu.ImagePath,
                    Price = pccpu.Price,
                    Company = pccpu.Company,
                    DetailPath = @pccpu.DetailPath,
                    Discount = pccpu.Discount,
                    Id = pccpu.Id,
                    Need = pccpu.Need,
                    Remain = pccpu.Remain,
                    Series = pccpu.Series,
                    Socket = pccpu.Socket,
                    AvatarPath = @pccpu.AvatarPath,
                    Unit = pccpu.Unit
                }).ToList();
    }
    public string GetSuggestID()
    {
        string? NewID = _context.Pccpus.AsQueryable().OrderBy(p => p.Id).LastOrDefault()?.Id;
        if (NewID == null) return StaticData.IdPrefix[ProductType.CPU] + "0000000";
        int counter = Convert.ToInt32(NewID[2..]);
        ++counter;
        return StaticData.IdPrefix[ProductType.CPU] + counter.ToString().PadLeft(7, '0');
    }
    public IEnumerable<ReportMock> GetSoldNumberMonthDuration(DateTime startDate, DateTime endDate)
    {
        return GetSoldNumberMonthDuration(startDate, endDate, ProductType.CPU);
    }

    public IEnumerable<ReportMock> GetSoldNumberQuarterDuration(DateTime startDate, DateTime endDate)
    {
        return GetSoldNumberQuarterDuration(startDate, endDate, ProductType.CPU);
    }

    public IEnumerable<ReportMock> GetSoldNumberWeekDuration(DateTime startDate, DateTime endDate)
    {
        return GetSoldNumberWeekDuration(startDate, endDate, ProductType.CPU);
    }

    public IEnumerable<ReportMock> GetSoldNumberYearDuration(DateTime startDate, DateTime endDate)
    {
        return GetSoldNumberYearDuration(startDate, endDate, ProductType.CPU);
    }
    public IEnumerable<TopSellDTO> GetTopSoldProducts(DateTime startDate, DateTime endDate, int number)
    {
        return GetTopSoldProducts(startDate, endDate, ProductType.CPU, number);
    }
    public IEnumerable<RevenueDTO> GetRevenueWeekDuration(DateTime startDate, DateTime endDate)
    {
        return GetRevenueWeekDuration(startDate, endDate, ProductType.CPU);
    }
}
