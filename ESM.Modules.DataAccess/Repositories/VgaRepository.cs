using ESM.Modules.DataAccess.DTOs;
using ESM.Modules.DataAccess.Infrastructure;
using ESM.Modules.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace ESM.Modules.DataAccess.Repositories;

public interface IVgaRepository : IProductRepository<Vga>{
}
public class VgaRepository : ProductRepository<Vga>, IVgaRepository
{
    public VgaRepository(ESMDbContext context) : base(context)
    {
    }
    public override async Task<IEnumerable<Vga>?> GetAll()
    {
        return await _context.Vgas.AsQueryable()
                .Where(vga => vga.Remain > -1)
               .ToListAsync();
    }
    public override async Task<object?> Add(Vga entity)
    {
        await _context.Vgas.AddAsync(entity);
        await _context.SaveChangesAsync();
        return null;
    }
    public override async Task<object?> Update(Vga entity)
    {
        bool res = true;
        try
        {
            var hd = await _context.Vgas.AsQueryable()
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
    public override async Task<bool> IsIdExist(string id)
    {
        return await _context.Vgas.AnyAsync(p => p.Id == id);
    }
    public override async Task<object?> AddList(IEnumerable<Vga> list)
    {
        bool res = true;
        try
        {
            await _context.Vgas.AddRangeAsync(list);
            await _context.SaveChangesAsync();
        }
        catch (Exception ex) { res = false; }
        return res;
    }
    public string GetSuggestID()
    {
        return GetSuggestID(ProductType.VGA);
    }
    public override async Task<object?> Delete(string id)
    {
        var p = await _context.Vgas.SingleAsync(p => p.Id == id);
        p.Remain = -1;
        await _context.SaveChangesAsync();
        return null;
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
    public IEnumerable<TopSellDTO> GetTopSoldProducts(DateTime startDate, DateTime endDate, int number)
    {
        return GetTopSoldProducts(startDate, endDate, ProductType.VGA, number);
    }
    public IEnumerable<RevenueDTO> GetRevenueWeekDuration(DateTime startDate, DateTime endDate)
    {
        return GetRevenueWeekDuration(startDate, endDate, ProductType.VGA);
    }
}
