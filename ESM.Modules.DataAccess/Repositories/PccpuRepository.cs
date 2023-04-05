using ESM.Modules.DataAccess.DTOs;
using ESM.Modules.DataAccess.Infrastructure;
using ESM.Modules.DataAccess.Models;

namespace ESM.Modules.DataAccess.Repositories;
public interface IPccpuRepository : IProductRepository<Pccpu> {
}
public class PccpuRepository : ProductRepository<Pccpu>, IPccpuRepository
{
    public PccpuRepository(ESMDbContext context) : base(context)
    {
    }
    public override IEnumerable<Pccpu>? GetAll()
    {
        return _context.Pccpus.AsQueryable()
                .Where(p => p.Remain > -1)
                .ToList();
    }
    public override object? Add(Pccpu entity)
    {
        _context.Pccpus.Add(entity);
        return null;
    }
    public override object? Update(Pccpu entity)
    {
        var hd = _context.Pccpus.AsQueryable()
               .First(p => p.Id == entity.Id);
        _context.Entry(hd).CurrentValues.SetValues(entity);
        return null;
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
}
