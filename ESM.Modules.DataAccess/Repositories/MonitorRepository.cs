using ESM.Modules.DataAccess.DTOs;
using ESM.Modules.DataAccess.Infrastructure;
using ESM.Modules.DataAccess.Models;

namespace ESM.Modules.DataAccess.Repositories;
public interface IMonitorRepository : IBaseRepository<MonitorDTO>, IProductRepository
{
    string GetSuggestID();
}
public class MonitorRepository : BaseRepository<MonitorDTO>, IMonitorRepository
{
    public MonitorRepository(ESMDbContext context) : base(context)
    {
    }
    public override IEnumerable<MonitorDTO>? GetAll()
    {
        return _context.Monitors.AsQueryable()
                .Where(p => p.Remain > -1)
                .Select(monitor => new MonitorDTO()
                {
                    Name = monitor.Name,
                    ImagePath = @monitor.ImagePath,
                    Price = monitor.Price,
                    Company = monitor.Company,
                    DetailPath = @monitor.DetailPath,
                    Discount = monitor.Discount,
                    Id = monitor.Id,
                    Need = monitor.Need,
                    Panel = monitor.Panel,
                    RefreshRate = monitor.RefreshRate,
                    Remain = monitor.Remain,
                    Series = monitor.Series,
                    Size = monitor.Size,
                    AvatarPath = @monitor.AvatarPath,
                    Unit = monitor.Unit
                }).ToList();
    }
    public string GetSuggestID()
    {
        string? NewID = _context.Monitors.OrderBy(p => p.Id).LastOrDefault()?.Id;
        if (NewID == null) return StaticData.IdPrefix[ProductType.MONITOR] +"0000000";
        int counter = Convert.ToInt32(NewID[2..]);
        ++counter;
        return StaticData.IdPrefix[ProductType.MONITOR] + counter.ToString().PadLeft(7, '0');
    }
    public IEnumerable<ReportMock> GetSoldNumberMonthDuration(DateTime startDate, DateTime endDate)
    {
        return GetSoldNumberMonthDuration(startDate, endDate, ProductType.MONITOR);
    }

    public IEnumerable<ReportMock> GetSoldNumberQuarterDuration(DateTime startDate, DateTime endDate)
    {
        return GetSoldNumberQuarterDuration(startDate, endDate, ProductType.MONITOR);
    }

    public IEnumerable<ReportMock> GetSoldNumberWeekDuration(DateTime startDate, DateTime endDate)
    {
        return GetSoldNumberWeekDuration(startDate, endDate, ProductType.MONITOR);
    }

    public IEnumerable<ReportMock> GetSoldNumberYearDuration(DateTime startDate, DateTime endDate)
    {
        return GetSoldNumberYearDuration(startDate, endDate, ProductType.MONITOR);
    }
    public IEnumerable<TopSellDTO> GetTopSoldProducts(DateTime startDate, DateTime endDate, int number)
    {
        return GetTopSoldProducts(startDate, endDate, ProductType.MONITOR, number);
    }
}
