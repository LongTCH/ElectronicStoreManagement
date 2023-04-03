using ESM.Modules.DataAccess.DTOs;
using ESM.Modules.DataAccess.Models;
using System.Data.Entity.SqlServer;
using System.Globalization;

namespace ESM.Modules.DataAccess.Infrastructure
{
    public abstract class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected readonly ESMDbContext _context;

        protected BaseRepository(ESMDbContext context)
        {
            _context = context;
        }

        public virtual void Add(T entity)
        {
            throw new NotImplementedException();
        }

        public virtual bool Any(string id)
        {
            throw new NotImplementedException();
        }

        public virtual void Delete(string id)
        {
            throw new NotImplementedException();
        }

        public virtual IEnumerable<T>? GetAll()
        {
            throw new NotImplementedException();
        }

        public virtual T? GetById(string id)
        {
            throw new NotImplementedException();
        }


        public virtual void Update(T entity)
        {
            throw new NotImplementedException();
        }
        public static int GetIso8601WeekOfYear(DateTime date)
        {
            // Get the day of week for the specified date
            DayOfWeek day = CultureInfo.InvariantCulture.Calendar.GetDayOfWeek(date);

            // Adjust the date to the closest Thursday
            date = date.AddDays(4 - (int)day).Date;

            // Calculate the week number
            int weekNumber = CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(date, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);

            // Handle the special case of December
            if (weekNumber == 53 && date.Month == 12)
            {
                weekNumber = 1;
            }

            return weekNumber;
        }

        protected IEnumerable<ReportMock> GetSoldNumberWeekDuration(DateTime startDate, DateTime endDate, ProductType type)
        {
            var list = _context.BillProducts
    .Join(_context.Bills, bp => bp.BillId, b => b.Id, (bp, b) => new { BillProduct = bp, Bill = b })
    .Where(x => x.Bill.PurchasedTime >= startDate && x.Bill.PurchasedTime <= endDate)
    .Where(x => x.BillProduct.ProductId.StartsWith(StaticData.IdPrefix[type]))
    .AsEnumerable() // switch to client-side evaluation
    .GroupBy(x => new { Year = x.Bill.PurchasedTime.Year, Week = CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(x.Bill.PurchasedTime, CalendarWeekRule.FirstDay, DayOfWeek.Monday) })
    .Select(g => new ReportMock
    {
        Year = g.Key.Year,
        Sub = g.Key.Week,
        Value = g.Sum(x => x.BillProduct.Number)
    })
    .ToList();
            list.Sort();
            return list;
        }
        protected IEnumerable<ReportMock> GetSoldNumberMonthDuration(DateTime startDate, DateTime endDate, ProductType type)
        {
            var list = _context.BillProducts
    .Join(_context.Bills, bp => bp.BillId, b => b.Id, (bp, b) => new { BillProduct = bp, Bill = b })
    .Where(x => x.Bill.PurchasedTime >= startDate && x.Bill.PurchasedTime <= endDate)
    .Where(x => x.BillProduct.ProductId.StartsWith(StaticData.IdPrefix[type]))
    .AsEnumerable() // switch to client-side evaluation
    .GroupBy(x => new { Year = x.Bill.PurchasedTime.Year, Month = x.Bill.PurchasedTime.Month })
    .Select(g => new ReportMock
    {
        Year = g.Key.Year,
        Sub = g.Key.Month,
        Value = g.Sum(x => x.BillProduct.Number)
    })
    .ToList();
            list.Sort();
            return list;
        }


        protected IEnumerable<ReportMock> GetSoldNumberQuarterDuration(DateTime startDate, DateTime endDate, ProductType type)
        {
            var list = _context.BillProducts
    .Join(_context.Bills, bp => bp.BillId, b => b.Id, (bp, b) => new { BillProduct = bp, Bill = b })
    .Where(x => x.Bill.PurchasedTime >= startDate && x.Bill.PurchasedTime <= endDate)
    .Where(x => x.BillProduct.ProductId.StartsWith(StaticData.IdPrefix[type]))
    .AsEnumerable() // switch to client-side evaluation
    .GroupBy(x => new { Year = x.Bill.PurchasedTime.Year, Quarter = ((x.Bill.PurchasedTime.Month - 1) / 3) + 1 })
    .Select(g => new ReportMock
    {
        Year = g.Key.Year,
        Sub = g.Key.Quarter,
        Value = g.Sum(x => x.BillProduct.Number)
    })
    .ToList();
            list.Sort();
            return list;
        }

        protected IEnumerable<ReportMock> GetSoldNumberYearDuration(DateTime startDate, DateTime endDate, ProductType type)
        {
            var list = _context.BillProducts
      .Join(_context.Bills, bp => bp.BillId, b => b.Id, (bp, b) => new { BillProduct = bp, Bill = b })
      .Where(x => x.Bill.PurchasedTime >= startDate && x.Bill.PurchasedTime <= endDate)
      .Where(x => x.BillProduct.ProductId.StartsWith(StaticData.IdPrefix[type]))
      .AsEnumerable() // switch to client-side evaluation
     .GroupBy(x => new { Year = x.Bill.PurchasedTime.Year })
       .Select(g => new ReportMock
       {
           Year = g.Key.Year,
           Value = g.Sum(x => x.BillProduct.Number)
       })
    .ToList();
            list.Sort();
            return list;
        }
        protected IEnumerable<TopSellDTO> GetTopSoldProducts(DateTime startDate, DateTime endDate, ProductType type, int number)
        {
            return (from x in _context.Bills
                    join y in _context.BillProducts
                    on x.Id equals y.BillId
                    where x.PurchasedTime >= startDate && x.PurchasedTime <= endDate && y.ProductId.StartsWith(StaticData.IdPrefix[type])
                    group y by y.ProductId into g
                    select new TopSellDTO()
                    {
                        Name = g.Key,
                        Number = g.Sum(x => x.Number)
                    }).ToList();
        }
        protected IEnumerable<RevenueDTO> GetRevenueWeekDuration(DateTime startDate, DateTime endDate, ProductType type)
        {
            var l = from x in _context.Bills
                    join y in _context.BillProducts
                    on x.Id equals y.BillId
                    where x.PurchasedTime >= startDate && x.PurchasedTime <= endDate && y.ProductId.StartsWith(StaticData.IdPrefix[type])
                    select new { Year = x.PurchasedTime.Year, Week = CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(x.PurchasedTime, CalendarWeekRule.FirstDay, DayOfWeek.Monday) , Revenue = y.Amount};
            return (from x in l
                   group x by new { x.Year, x.Week } into g
                   select new RevenueDTO()
                   {
                       Year = g.Key.Year,
                       Sub = g.Key.Week,
                       Value = g.Sum(x => x.Revenue)
                   }).ToList();
        }
    }
}
