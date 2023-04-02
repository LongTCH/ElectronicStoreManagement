using ESM.Modules.DataAccess.DTOs;
using ESM.Modules.DataAccess.Infrastructure;
using ESM.Modules.DataAccess.Models;

namespace ESM.Modules.DataAccess.Repositories;
public interface ILaptopRepository : IBaseRepository<LaptopDTO>
{
    string GetSuggestID();
    IEnumerable<ReportMock> GetSoldNumberWeekDuration(DateTime start, DateTime end);
    IEnumerable<ReportMock> GetSoldNumberMonthDuration(DateTime start, DateTime end);
    IEnumerable<ReportMock> GetSoldNumberQuarterDuration(DateTime start, DateTime end);
    IEnumerable<ReportMock> GetSoldNumberYearDuration(DateTime start, DateTime end);
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

    public IEnumerable<ReportMock> GetSoldNumberWeekDuration(DateTime start, DateTime end)
    {
        return (from p in _context.BillProducts
                join q in _context.Bills
                on p.BillId equals q.Id
                where p.ProductId.StartsWith(StaticData.IdPrefix[ProductType.LAPTOP])
                      && q.PurchasedTime.Date >= start && q.PurchasedTime.Date <= end
                group p by new { Year = q.PurchasedTime.Year, Week = StaticData.GetWeekOfYear(q.PurchasedTime) } into g
                select new ReportMock()
                {
                    year = g.Key.Year,
                    sub = g.Key.Week,
                    value = g.Sum(p => p.Number)
                }).ToList();
    }
    public IEnumerable<ReportMock> GetSoldNumberMonthDuration(DateTime start, DateTime end)
    {
        return (from p in _context.BillProducts
                join q in _context.Bills
                on p.BillId equals q.Id
                where p.ProductId.StartsWith(StaticData.IdPrefix[ProductType.LAPTOP])
                      && q.PurchasedTime.Date >= start && q.PurchasedTime.Date <= end
                group p by new { Year = q.PurchasedTime.Year, Month = q.PurchasedTime.Month } into g
                select new ReportMock()
                {
                    year = g.Key.Year,
                    sub = g.Key.Month,
                    value = g.Sum(p => p.Number)
                }).ToList();
    }
    public string GetSuggestID()
    {
        string? NewID = _context.Laptops.OrderBy(p => p.Id).LastOrDefault()?.Id;
        if (NewID == null) return StaticData.IdPrefix[ProductType.LAPTOP] + "0000000";
        int counter = Convert.ToInt32(NewID[2..]);
        ++counter;
        return StaticData.IdPrefix[ProductType.LAPTOP] + counter.ToString().PadLeft(7, '0');
    }

    public IEnumerable<ReportMock> GetSoldNumberQuarterDuration(DateTime start, DateTime end)
    {
        return (from p in _context.BillProducts
                join q in _context.Bills
                on p.BillId equals q.Id
                where p.ProductId.StartsWith(StaticData.IdPrefix[ProductType.LAPTOP])
                      && q.PurchasedTime.Date >= start && q.PurchasedTime.Date <= end
                group p by new { Year = q.PurchasedTime.Year, Quarter = StaticData.GetQuarter(q.PurchasedTime.Month) } into g
                select new ReportMock()
                {
                    year = g.Key.Year,
                    sub = g.Key.Quarter,
                    value = g.Sum(p => p.Number)
                }).ToList();
    }

    public IEnumerable<ReportMock> GetSoldNumberYearDuration(DateTime start, DateTime end)
    {
        return (from p in _context.BillProducts
                join q in _context.Bills
                on p.BillId equals q.Id
                where p.ProductId.StartsWith(StaticData.IdPrefix[ProductType.LAPTOP])
                      && q.PurchasedTime.Date >= start && q.PurchasedTime.Date <= end
                group p by q.PurchasedTime.Year into g
                select new ReportMock()
                {
                    year = g.Key,
                    value = g.Sum(p => p.Number)
                }).ToList();
    }
}
