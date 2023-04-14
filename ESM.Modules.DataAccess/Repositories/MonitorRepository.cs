using ESM.Modules.DataAccess.DTOs;
using ESM.Modules.DataAccess.Infrastructure;
using ESM.Modules.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace ESM.Modules.DataAccess.Repositories;
public interface IMonitorRepository : IProductRepository<Models.Monitor>
{
    Task<object?> AddList(IEnumerable<Models.Monitor> list);
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
        bool res = true;
        try
        {
            var hd = await _context.Monitors.AsQueryable()
                   .FirstAsync(p => p.Id == entity.Id);
            _context.Entry(hd).CurrentValues.SetValues(entity);
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            res = false;
        }
        return res;
    }
    public override async Task<object?> AddList(IEnumerable<Models.Monitor> list)
    {
        bool res = true;
        try
        {
            await _context.Monitors.AddRangeAsync(list);
            await _context.SaveChangesAsync();
        }
        catch (Exception ex) { res = false; }
        return res;
    }
    public override async Task<bool> IsIdExist(string id)
    {
        return await _context.Monitors.AnyAsync(p => p.Id == id);
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
    public IEnumerable<RevenueDTO> GetRevenueMonthDuration(DateTime startDate, DateTime endDate)
    {
        return GetRevenueMonthDuration(startDate, endDate, ProductType.MONITOR);
    }
    public IEnumerable<RevenueDTO> GetRevenueQuarterDuration(DateTime startDate, DateTime endDate)
    {
        return GetRevenueQuaterDuration(startDate, endDate, ProductType.MONITOR);
    }
    public IEnumerable<RevenueDTO> GetRevenueYearDuration(DateTime startDate, DateTime endDate)
    {
        return GetRevenueYearDuration(startDate, endDate, ProductType.MONITOR);
    }

}
