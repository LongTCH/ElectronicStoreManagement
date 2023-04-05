using ESM.Modules.DataAccess.DTOs;
using ESM.Modules.DataAccess.Infrastructure;
using ESM.Modules.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace ESM.Modules.DataAccess.Repositories;
public interface ILaptopRepository : IProductRepository<Laptop>
{
}
public class LaptopRepository : ProductRepository<Laptop>, ILaptopRepository
{
    public LaptopRepository(ESMDbContext context) : base(context)
    {
    }
    public override IEnumerable<Laptop>? GetAll()
    {
        return _context.Laptops.AsQueryable()
                .Where(p => p.Remain > -1)
                .ToList();
    }
    public override object? Add(Laptop entity)
    {
        _context.Laptops.Add(entity);
        return null;
    }
    public override object? Update(Laptop entity)
    {
        var hd = _context.Laptops.AsQueryable()
               .First(p => p.Id == entity.Id);
        _context.Entry(hd).CurrentValues.SetValues(entity);
        return null;
    }
    public IEnumerable<RevenueDTO> GetRevenueWeekDuration(DateTime startDate, DateTime endDate)
    {
        return GetRevenueWeekDuration(startDate, endDate, ProductType.LAPTOP);
    }

    public IEnumerable<ReportMock> GetSoldNumberMonthDuration(DateTime startDate, DateTime endDate)
    {
        return GetSoldNumberMonthDuration(startDate, endDate, ProductType.LAPTOP);
    }

    public IEnumerable<ReportMock> GetSoldNumberQuarterDuration(DateTime startDate, DateTime endDate)
    {
        return GetSoldNumberQuarterDuration(startDate, endDate, ProductType.LAPTOP);
    }

    public IEnumerable<ReportMock> GetSoldNumberWeekDuration(DateTime startDate, DateTime endDate)
    {
        return GetSoldNumberWeekDuration(startDate, endDate, ProductType.LAPTOP);
    }

    public IEnumerable<ReportMock> GetSoldNumberYearDuration(DateTime startDate, DateTime endDate)
    {
        return GetSoldNumberYearDuration(startDate, endDate, ProductType.LAPTOP);
    }

    public string GetSuggestID()
    {
        return GetSuggestID(ProductType.LAPTOP);
    }

    public IEnumerable<TopSellDTO> GetTopSoldProducts(DateTime startDate, DateTime endDate, int number)
    {
        return GetTopSoldProducts(startDate, endDate, ProductType.LAPTOP, number);
    }
}
