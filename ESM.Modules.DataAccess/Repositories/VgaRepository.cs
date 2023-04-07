using ESM.Modules.DataAccess.DTOs;
using ESM.Modules.DataAccess.Infrastructure;
using ESM.Modules.DataAccess.Models;

namespace ESM.Modules.DataAccess.Repositories;

public interface IVgaRepository : IProductRepository<Vga>{
}
public class VgaRepository : ProductRepository<Vga>, IVgaRepository
{
    public VgaRepository(ESMDbContext context) : base(context)
    {
    }
    public override IEnumerable<Vga>? GetAll()
    {
        return _context.Vgas.AsQueryable()
                .Where(vga => vga.Remain > -1)
               .ToList();
    }
    public override object? Add(Vga entity)
    {
        _context.Vgas.Add(entity);
        return null;
    }
    public override object? Update(Vga entity)
    {
        var hd = _context.Vgas.AsQueryable()
               .First(p => p.Id == entity.Id);
        _context.Entry(hd).CurrentValues.SetValues(entity);
        return null;
    }
    public string GetSuggestID()
    {
        return GetSuggestID(ProductType.VGA);
    }
    public override object? Delete(string id)
    {
        var p = _context.Vgas.SingleOrDefault(p => p.Id == id);
        p.Remain = -1;
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
