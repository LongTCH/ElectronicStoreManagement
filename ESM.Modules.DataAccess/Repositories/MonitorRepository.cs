using ESM.Modules.DataAccess.DTOs;
using ESM.Modules.DataAccess.Infrastructure;
using ESM.Modules.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace ESM.Modules.DataAccess.Repositories;
public interface IMonitorRepository : IProductRepository<Models.Monitor>
{
}
public class MonitorRepository : ProductRepository<Models.Monitor>, IMonitorRepository
{
    public MonitorRepository(ESMDbContext context) : base(context)
    {
    }
    public override async Task<IEnumerable<Models.Monitor>?> GetAll()
    {
        return await _context.Monitors.AsQueryable()
                .Where(p => p.Remain > -1)
                .ToListAsync();
    }
    public string GetSuggestID()
    {
        return GetSuggestID(ProductType.MONITOR);
    }
    public override async Task<object?> Add(Models.Monitor entity)
    {
        await _context.Monitors.AddAsync(entity);
        return null;
    }
    public override async Task<object?> Update(Models.Monitor entity)
    {
        var hd = await _context.Monitors.AsQueryable()
               .FirstAsync(p => p.Id == entity.Id);
        _context.Entry(hd).CurrentValues.SetValues(entity);
        await _context.SaveChangesAsync();
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
