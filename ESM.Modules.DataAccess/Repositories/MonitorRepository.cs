using ESM.Modules.DataAccess.DTOs;
using ESM.Modules.DataAccess.Infrastructure;
using ESM.Modules.DataAccess.Models;

namespace ESM.Modules.DataAccess.Repositories;
public interface IMonitorRepository : IProductRepository<Models.Monitor>
{
}
public class MonitorRepository : ProductRepository<Models.Monitor>, IMonitorRepository
{
    public MonitorRepository(ESMDbContext context) : base(context)
    {
    }
    public override IEnumerable<Models.Monitor>? GetAll()
    {
        return _context.Monitors.AsQueryable()
                .Where(p => p.Remain > -1)
                .ToList();
    }
    public string GetSuggestID()
    {
        return GetSuggestID(ProductType.MONITOR);
    }
    public override object? Add(Models.Monitor entity)
    {
        _context.Monitors.Add(entity);
        return null;
    }
    public override object? Update(Models.Monitor entity)
    {
        var hd = _context.Monitors.AsQueryable()
               .First(p => p.Id == entity.Id);
        _context.Entry(hd).CurrentValues.SetValues(entity);
        return null;
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
    public IEnumerable<RevenueDTO> GetRevenueWeekDuration(DateTime startDate, DateTime endDate)
    {
        return GetRevenueWeekDuration(startDate, endDate, ProductType.MONITOR);
    }
}
