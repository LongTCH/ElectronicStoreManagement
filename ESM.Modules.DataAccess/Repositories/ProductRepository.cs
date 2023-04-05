using ESM.Modules.DataAccess.DTOs;
using ESM.Modules.DataAccess.Infrastructure;
using ESM.Modules.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESM.Modules.DataAccess.Repositories
{
    public class ProductRepository<T> : BaseRepository<T> where T : ProductDTO
    {
        public ProductRepository(ESMDbContext context) : base(context)
        {
        }
        protected string GetSuggestID(ProductType type)
        {
            string? NewID = null;
            if (type == ProductType.LAPTOP) NewID = _context.Laptops.OrderBy(p => p.Id).LastOrDefault()?.Id;
            else if (type == ProductType.MONITOR) NewID = _context.Monitors.OrderBy(p => p.Id).LastOrDefault()?.Id;
            else if (type == ProductType.PC) NewID = _context.Pcs.OrderBy(p => p.Id).LastOrDefault()?.Id;
            else if (type == ProductType.CPU) NewID = _context.Pccpus.OrderBy(p => p.Id).LastOrDefault()?.Id;
            else if (type == ProductType.HARDDISK) NewID = _context.Pcharddisks.OrderBy(p => p.Id).LastOrDefault()?.Id;
            else if (type == ProductType.SMARTPHONE) NewID = _context.Smartphones.OrderBy(p => p.Id).LastOrDefault()?.Id;
            else if (type == ProductType.VGA) NewID = _context.Vgas.OrderBy(p => p.Id).LastOrDefault()?.Id;

            if (NewID == null) return StaticData.IdPrefix[type] + "0000000";
            int counter = Convert.ToInt32(NewID[2..]);
            ++counter;
            return StaticData.IdPrefix[type] + counter.ToString().PadLeft(7, '0');
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
                    select new { Year = x.PurchasedTime.Year, Week = CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(x.PurchasedTime, CalendarWeekRule.FirstDay, DayOfWeek.Monday), Revenue = y.Amount };
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
