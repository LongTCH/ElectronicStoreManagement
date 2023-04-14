using ESM.Modules.DataAccess.DTOs;
using ESM.Modules.DataAccess.Infrastructure;
using ESM.Modules.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace ESM.Modules.DataAccess.Repositories;
public interface IPccpuRepository : IProductRepository<Pccpu> {
}
public class PccpuRepository : ProductRepository<Pccpu>, IPccpuRepository
{
    public PccpuRepository(ESMDbContext context) : base(context)
    {
    }
    public override async Task<IEnumerable<Pccpu>?> GetAll()
    {
        return await _context.Pccpus.AsQueryable()
                .Where(p => p.Remain > -1)
                .ToListAsync();
    }
    public override async Task<object?> Add(Pccpu entity)
    {
        await _context.Pccpus.AddAsync(entity);
        await _context.SaveChangesAsync();
        return null;
    }
    public override async Task<object?> Update(Pccpu entity)
    {
        bool res = true;
        try
        {
            var hd = await _context.Pccpus.AsQueryable()
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
    public string GetSuggestID()
    {
        return GetSuggestID(ProductType.CPU);
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
    public IEnumerable<RevenueDTO> GetRevenueMonthDuration(DateTime startDate, DateTime endDate)
    {
        return GetRevenueMonthDuration(startDate, endDate, ProductType.CPU);
    }
    public IEnumerable<RevenueDTO> GetRevenueQuarterDuration(DateTime startDate, DateTime endDate)
    {
        return GetRevenueQuaterDuration(startDate, endDate, ProductType.CPU);
    }
    public IEnumerable<RevenueDTO> GetRevenueYearDuration(DateTime startDate, DateTime endDate)
    {
        return GetRevenueYearDuration(startDate, endDate, ProductType.CPU);
    }
}
