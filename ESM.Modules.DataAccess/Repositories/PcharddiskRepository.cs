using ESM.Modules.DataAccess.DTOs;
using ESM.Modules.DataAccess.Infrastructure;
using ESM.Modules.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace ESM.Modules.DataAccess.Repositories;

public interface IPcharddiskRepository : IProductRepository<Pcharddisk>
{
    Task<object?> AddList(IEnumerable<Pcharddisk> list);
}
public class PcharddiskRepository : ProductRepository<Pcharddisk>, IPcharddiskRepository
{
    public PcharddiskRepository(ESMDbContext context) : base(context)
    {
    }
    public override async Task<Pcharddisk?> GetById(string id)
    {
        return await _context.Pcharddisks.AsQueryable()
                .Where(p => p.Id == id)
                .FirstOrDefaultAsync();
    }
    public override async Task<object?> Update(Pcharddisk entity)
    {
        var hd = await _context.Pcharddisks.AsQueryable()
               .FirstAsync(p => p.Id == entity.Id);
        _context.Entry(hd).CurrentValues.SetValues(entity);
        await _context.SaveChangesAsync();
        return null;
    }
    public override async Task<IEnumerable<Pcharddisk>?> GetAll()
    {
        return await _context.Pcharddisks.AsQueryable()
                .Where(pcharddisk => pcharddisk.Remain > -1)
                .ToListAsync();
    }
    public override async Task<object?> Add(Pcharddisk entity)
    {
        await _context.Pcharddisks.AddAsync(entity);
        await _context.SaveChangesAsync();
        return null;
    }
    public override async Task<object?> Delete(string id)
    {
        var p = await _context.Pcharddisks.SingleAsync(p => p.Id == id);
        p.Remain = -1;
        await _context.SaveChangesAsync();
        return null;
    }
    public string GetSuggestID()
    {
        return GetSuggestID(ProductType.HARDDISK);
    }
    public IEnumerable<ReportMock> GetSoldNumberMonthDuration(DateTime startDate, DateTime endDate)
    {
        return GetSoldNumberMonthDuration(startDate, endDate, ProductType.HARDDISK);
    }

    public IEnumerable<ReportMock> GetSoldNumberQuarterDuration(DateTime startDate, DateTime endDate)
    {
        return GetSoldNumberQuarterDuration(startDate, endDate, ProductType.HARDDISK);
    }

    public IEnumerable<ReportMock> GetSoldNumberWeekDuration(DateTime startDate, DateTime endDate)
    {
        return GetSoldNumberWeekDuration(startDate, endDate, ProductType.HARDDISK);
    }

    public IEnumerable<ReportMock> GetSoldNumberYearDuration(DateTime startDate, DateTime endDate)
    {
        return GetSoldNumberYearDuration(startDate, endDate, ProductType.HARDDISK);
    }
    public IEnumerable<TopSellDTO> GetTopSoldProducts(DateTime startDate, DateTime endDate, int number)
    {
        return GetTopSoldProducts(startDate, endDate, ProductType.HARDDISK, number);
    }
    public IEnumerable<RevenueDTO> GetRevenueWeekDuration(DateTime startDate, DateTime endDate)
    {
        return GetRevenueWeekDuration(startDate, endDate, ProductType.HARDDISK);
    }

    public async Task<object?> AddList(IEnumerable<Pcharddisk> list)
    {
        await _context.Pcharddisks.AddRangeAsync(list);
        return null;
    }
}
