using ESM.Modules.DataAccess.DTOs;
using ESM.Modules.DataAccess.Infrastructure;
using ESM.Modules.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace ESM.Modules.DataAccess.Repositories;
public interface ILaptopRepository : IBaseRepository<LaptopDTO>, IProductRepository
{
    string GetSuggestID();
}
public class LaptopRepository : BaseRepository<LaptopDTO>, ILaptopRepository
{
    public LaptopRepository(ESMDbContext context) : base(context)
    {
    }
    public override IEnumerable<LaptopDTO>? GetAll()
    {
        return _context.Laptops.AsQueryable()
                .Where(p => p.Remain > -1)
                .Select(laptop => new LaptopDTO()
                {
                    Name = laptop.Name,
                    ImagePath = @laptop.ImagePath,
                    Price = laptop.Price,
                    Company = laptop.Company,
                    Cpu = laptop.Cpu,
                    DetailPath = @laptop.DetailPath,
                    Discount = laptop.Discount,
                    Graphic = laptop.Graphic,
                    Id = laptop.Id,
                    Need = laptop.Need,
                    Ram = laptop.Ram,
                    Remain = laptop.Remain,
                    Series = laptop.Series,
                    Storage = laptop.Storage,
                    AvatarPath = @laptop.AvatarPath,
                    Unit = laptop.Unit
                }).ToList();
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
        string? NewID = _context.Laptops.OrderBy(p => p.Id).LastOrDefault()?.Id;
        if (NewID == null) return StaticData.IdPrefix[ProductType.LAPTOP] + "0000000";
        int counter = Convert.ToInt32(NewID[2..]);
        ++counter;
        return StaticData.IdPrefix[ProductType.LAPTOP] + counter.ToString().PadLeft(7, '0');
    }

    public IEnumerable<TopSellDTO> GetTopSoldProducts(DateTime startDate, DateTime endDate, int number)
    {
        return GetTopSoldProducts(startDate, endDate, ProductType.LAPTOP, number);
    }
}
