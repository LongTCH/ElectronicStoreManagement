using ESM.Modules.DataAccess.DTOs;
using ESM.Modules.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESM.Modules.DataAccess.Repositories
{
    public interface IReportRepository
    {
        IEnumerable<ReportMock> GetSoldNumberWeekDuration(DateTime startDate, DateTime endDate, ProductType type);
        IEnumerable<ReportMock> GetSoldNumberMonthDuration(DateTime startDate, DateTime endDate, ProductType type);
        IEnumerable<ReportMock> GetSoldNumberQuarterDuration(DateTime startDate, DateTime endDate, ProductType type);
        IEnumerable<ReportMock> GetSoldNumberYearDuration(DateTime startDate, DateTime endDate, ProductType type);
        IEnumerable<TopSellDTO> GetTopSoldProducts(DateTime startDate, DateTime endDate, ProductType type, int number);
        IEnumerable<RevenueDTO> GetRevenueWeekDuration(DateTime startDate, DateTime endDate, ProductType type);
        IEnumerable<RevenueDTO> GetRevenueMonthDuration(DateTime startDate, DateTime endDate, ProductType type);
        IEnumerable<RevenueDTO> GetRevenueQuarterDuration(DateTime startDate, DateTime endDate, ProductType type);
        IEnumerable<RevenueDTO> GetRevenueYearDuration(DateTime startDate, DateTime endDate, ProductType type);
    }
    public class ReportRepository : IReportRepository
    {
        private readonly ESMDbContext _context;

        public ReportRepository(ESMDbContext context)
        {
            _context = context;
        }
        public IEnumerable<ReportMock> GetSoldNumberWeekDuration(DateTime startDate, DateTime endDate, ProductType type)
        {
            var list = new List<ReportMock>();
            if (type == ProductType.COMBO)
            {
                list = _context.BillCombos
                    .Where(x => x.PurchasedTime >= startDate && x.PurchasedTime <= endDate)
                    .AsEnumerable() // switch to client-side evaluation
                    .GroupBy(x => new { Year = x.PurchasedTime.Year, Week = CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(x.PurchasedTime, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday) })
                    .Select(g => new ReportMock
                    {
                        Year = g.Key.Year,
                        Sub = g.Key.Week,
                        Value = g.Count()
                    }).ToList();
            }
            else
            {
                list = _context.BillProducts
                    .Join(_context.Bills, bp => bp.BillId, b => b.Id, (bp, b) => new { BillProduct = bp, Bill = b }).Where(x => x.Bill.PurchasedTime >= startDate && x.Bill.PurchasedTime <= endDate)
                    .Where(x => x.BillProduct.ProductId.StartsWith(DAStaticData.IdPrefix[type]))
                    .AsEnumerable() // switch to client-side evaluation
                    .GroupBy(x => new { Year = x.Bill.PurchasedTime.Year, Week = CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(x.Bill.PurchasedTime, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday) })
                    .Select(g => new ReportMock
                    {
                        Year = g.Key.Year,
                        Sub = g.Key.Week,
                        Value = g.Sum(x => x.BillProduct.Number)
                    }).ToList();
            }
            list.Sort();
            List<ReportMock> listRes = new();
            for (var date = startDate; date <= endDate; date = date.AddDays(1))
            {
                listRes.Add(new()
                {
                    Year = date.Year,
                    Sub = CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(date, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday)
                });
            }
            listRes = listRes.GroupBy(x => new { x.Year, x.Sub })
                    .Select(g => new ReportMock { Year = g.Key.Year, Sub = g.Key.Sub })
                    .ToList();
            if (listRes.Count == 0 || list.Count == 0) return listRes;
            int list_index = 0, listRes_index = 0;
            while (listRes_index < listRes.Count)
            {
                if (listRes[listRes_index].Equals(list[list_index]))
                {
                    listRes[listRes_index] = list[list_index];
                    list_index++;
                    if (list_index == list.Count) break;
                }
                listRes_index++;
            }
            return listRes;
        }
        public IEnumerable<ReportMock> GetSoldNumberMonthDuration(DateTime startDate, DateTime endDate, ProductType type)
        {
            var list = new List<ReportMock>();
            if (type == ProductType.COMBO)
            {
                list = _context.BillCombos
                    .Where(x => x.PurchasedTime >= startDate && x.PurchasedTime <= endDate)
                    .AsEnumerable() // switch to client-side evaluation
                    .GroupBy(x => new { Year = x.PurchasedTime.Year, Month = x.PurchasedTime.Month })
                    .Select(g => new ReportMock
                    {
                        Year = g.Key.Year,
                        Sub = g.Key.Month,
                        Value = g.Count()
                    }).ToList();
            }
            else
            {
                list = _context.BillProducts
                    .Join(_context.Bills, bp => bp.BillId, b => b.Id, (bp, b) => new { BillProduct = bp, Bill = b })
                    .Where(x => x.Bill.PurchasedTime >= startDate && x.Bill.PurchasedTime <= endDate)
                    .Where(x => x.BillProduct.ProductId.StartsWith(DAStaticData.IdPrefix[type]))
                    .AsEnumerable() // switch to client-side evaluation
                    .GroupBy(x => new { Year = x.Bill.PurchasedTime.Year, Month = x.Bill.PurchasedTime.Month })
                    .Select(g => new ReportMock
                    {
                        Year = g.Key.Year,
                        Sub = g.Key.Month,
                        Value = g.Sum(x => x.BillProduct.Number)
                    }).ToList();
            }
            list.Sort();
            List<ReportMock> listRes = new();
            for (var date = startDate; date <= endDate; date = date.AddDays(1))
            {
                listRes.Add(new()
                {
                    Year = date.Year,
                    Sub = date.Month
                });
            }
            listRes = listRes.GroupBy(x => new { x.Year, x.Sub })
                    .Select(g => new ReportMock { Year = g.Key.Year, Sub = g.Key.Sub })
                    .ToList();
            if (listRes.Count == 0 || list.Count == 0) return listRes;
            int list_index = 0, listRes_index = 0;
            while (listRes_index < listRes.Count)
            {
                if (listRes[listRes_index].Equals(list[list_index]))
                {
                    listRes[listRes_index] = list[list_index];
                    list_index++;
                    if (list_index == list.Count) break;
                }
                listRes_index++;
            }
            return listRes;
        }
        public IEnumerable<ReportMock> GetSoldNumberQuarterDuration(DateTime startDate, DateTime endDate, ProductType type)
        {
            var list = new List<ReportMock>();
            if (type == ProductType.COMBO)
            {
                list = _context.BillCombos
                    .Where(x => x.PurchasedTime >= startDate && x.PurchasedTime <= endDate)
                    .AsEnumerable() // switch to client-side evaluation
                    .GroupBy(x => new { Year = x.PurchasedTime.Year, Quarter = ((x.PurchasedTime.Month - 1) / 3) + 1 })
                    .Select(g => new ReportMock
                    {
                        Year = g.Key.Year,
                        Sub = g.Key.Quarter,
                        Value = g.Count()
                    }).ToList();
            }
            else
            {
                list = _context.BillProducts
                    .Join(_context.Bills, bp => bp.BillId, b => b.Id, (bp, b) => new { BillProduct = bp, Bill = b })
                    .Where(x => x.Bill.PurchasedTime >= startDate && x.Bill.PurchasedTime <= endDate)
                    .Where(x => x.BillProduct.ProductId.StartsWith(DAStaticData.IdPrefix[type]))
                    .AsEnumerable() // switch to client-side evaluation
                    .GroupBy(x => new { Year = x.Bill.PurchasedTime.Year, Quarter = ((x.Bill.PurchasedTime.Month - 1) / 3) + 1 })
                    .Select(g => new ReportMock
                    {
                        Year = g.Key.Year,
                        Sub = g.Key.Quarter,
                        Value = g.Sum(x => x.BillProduct.Number)
                    }).ToList();
            }
            list.Sort();
            List<ReportMock> listRes = new();
            for (var date = startDate; date <= endDate; date = date.AddDays(1))
            {
                listRes.Add(new()
                {
                    Year = date.Year,
                    Sub = ((date.Month - 1) / 3) + 1
                });
            }
            listRes = listRes.GroupBy(x => new { x.Year, x.Sub })
                    .Select(g => new ReportMock { Year = g.Key.Year, Sub = g.Key.Sub })
                    .ToList();
            if (listRes.Count == 0 || list.Count == 0) return listRes;
            int list_index = 0, listRes_index = 0;
            while (listRes_index < listRes.Count)
            {
                if (listRes[listRes_index].Equals(list[list_index]))
                {
                    listRes[listRes_index] = list[list_index];
                    list_index++;
                    if (list_index == list.Count) break;
                }
                listRes_index++;
            }
            return listRes;
        }
        public IEnumerable<ReportMock> GetSoldNumberYearDuration(DateTime startDate, DateTime endDate, ProductType type)
        {
            var list = new List<ReportMock>();
            if (type == ProductType.COMBO)
            {
                list = _context.BillCombos
                    .Where(x => x.PurchasedTime >= startDate && x.PurchasedTime <= endDate)
                    .AsEnumerable() // switch to client-side evaluation
                    .GroupBy(x => x.PurchasedTime.Year)
                    .Select(g => new ReportMock
                    {
                        Year = g.Key,
                        Value = g.Count()
                    }).ToList();
            }
            else
            {
                list = _context.BillProducts
                    .Join(_context.Bills, bp => bp.BillId, b => b.Id, (bp, b) => new { BillProduct = bp, Bill = b })
                    .Where(x => x.Bill.PurchasedTime >= startDate && x.Bill.PurchasedTime <= endDate)
                    .Where(x => x.BillProduct.ProductId.StartsWith(DAStaticData.IdPrefix[type]))
                    .AsEnumerable() // switch to client-side evaluation
                    .GroupBy(x => new { x.Bill.PurchasedTime.Year })
                    .Select(g => new ReportMock
                    {
                        Year = g.Key.Year,
                        Value = g.Sum(x => x.BillProduct.Number)
                    }).ToList();
            }
            list.Sort();
            List<ReportMock> listRes = new();
            for (var date = startDate; date <= endDate; date = date.AddDays(1))
            {
                listRes.Add(new()
                {
                    Year = date.Year,
                });
            }
            listRes = listRes.GroupBy(x => x.Year)
                    .Select(g => new ReportMock { Year = g.Key })
                    .ToList();
            if (listRes.Count == 0 || list.Count == 0) return listRes;
            int list_index = 0, listRes_index = 0;
            while (listRes_index < listRes.Count)
            {
                if (listRes[listRes_index].Equals(list[list_index]))
                {
                    listRes[listRes_index] = list[list_index];
                    list_index++;
                    if (list_index == list.Count) break;
                }
                listRes_index++;
            }
            return listRes;
        }
        public IEnumerable<TopSellDTO> GetTopSoldProducts(DateTime startDate, DateTime endDate, ProductType type, int number)
        {
            IEnumerable<TopSellDTO> res = new List<TopSellDTO>();
            if (type == ProductType.COMBO)
            {
                res = (from x in _context.Combos
                       join y in _context.BillCombos
                       on x.Id equals y.ComboId
                       where y.PurchasedTime >= startDate && y.PurchasedTime <= endDate
                       group y by new { y.ComboId, x.Name } into g
                       select new TopSellDTO()
                       {
                           Id = g.Key.ComboId,
                           Name = g.Key.Name,
                           Number = g.Count()
                       });
            }
            else
            {
                var list = (from x in _context.Bills
                            join y in _context.BillProducts
                            on x.Id equals y.BillId
                            where x.PurchasedTime >= startDate && x.PurchasedTime <= endDate && y.ProductId.StartsWith(DAStaticData.IdPrefix[type])
                            group y by y.ProductId into g
                            select new
                            {
                                Id = g.Key,
                                Number = g.Sum(x => x.Number)
                            }).ToList();
                if (type == ProductType.LAPTOP)
                {
                    res = (from x in list
                           join y in _context.Laptops
                           on x.Id equals y.Id
                           select new TopSellDTO()
                           {
                               Id = x.Id,
                               Name = y.Name,
                               Number = x.Number
                           });
                }
                else if (type == ProductType.MONITOR)
                {
                    res = (from x in list
                           join y in _context.Monitors
                           on x.Id equals y.Id
                           select new TopSellDTO()
                           {
                               Id = x.Id,
                               Name = y.Name,
                               Number = x.Number
                           });
                }
                else if (type == ProductType.PC)
                {
                    res = (from x in list
                           join y in _context.Pcs
                           on x.Id equals y.Id
                           select new TopSellDTO()
                           {
                               Id = x.Id,
                               Name = y.Name,
                               Number = x.Number
                           });
                }
                else if (type == ProductType.HARDDISK)
                {
                    res = (from x in list
                           join y in _context.Pcharddisks
                           on x.Id equals y.Id
                           select new TopSellDTO()
                           {
                               Id = x.Id,
                               Name = y.Name,
                               Number = x.Number
                           });
                }
                else if (type == ProductType.CPU)
                {
                    res = (from x in list
                           join y in _context.Pccpus
                           on x.Id equals y.Id
                           select new TopSellDTO()
                           {
                               Id = x.Id,
                               Name = y.Name,
                               Number = x.Number
                           });
                }
                else if (type == ProductType.SMARTPHONE)
                {
                    res = (from x in list
                           join y in _context.Smartphones
                           on x.Id equals y.Id
                           select new TopSellDTO()
                           {
                               Id = x.Id,
                               Name = y.Name,
                               Number = x.Number
                           });
                }
                else if (type == ProductType.VGA)
                {
                    res = (from x in list
                           join y in _context.Vgas
                           on x.Id equals y.Id
                           select new TopSellDTO()
                           {
                               Id = x.Id,
                               Name = y.Name,
                               Number = x.Number
                           });
                }
            }
            return res.OrderBy(x => x.Number).Take(number).ToList();
        }
        public IEnumerable<RevenueDTO> GetRevenueWeekDuration(DateTime startDate, DateTime endDate, ProductType type)
        {
            List<RevenueDTO> list = new();
            if (type == ProductType.COMBO)
            {
                var l = from x in _context.BillCombos
                        where x.PurchasedTime >= startDate && x.PurchasedTime <= endDate
                        select new { Year = x.PurchasedTime.Year, Week = CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(x.PurchasedTime, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday), Revenue = x.TotalAmount };
                list = (from x in l.AsEnumerable()
                        group x by new { x.Year, x.Week } into g
                        select new RevenueDTO()
                        {
                            Year = g.Key.Year,
                            Sub = g.Key.Week,
                            Value = g.Sum(x => x.Revenue)
                        }).ToList();
            }
            else
            {
                var l = from x in _context.Bills
                        join y in _context.BillProducts
                        on x.Id equals y.BillId
                        where x.PurchasedTime >= startDate && x.PurchasedTime <= endDate && y.ProductId.StartsWith(DAStaticData.IdPrefix[type])
                        select new { Year = x.PurchasedTime.Year, Week = CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(x.PurchasedTime, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday), Revenue = y.Amount };
                list = (from x in l.AsEnumerable()
                        group x by new { x.Year, x.Week } into g
                        select new RevenueDTO()
                        {
                            Year = g.Key.Year,
                            Sub = g.Key.Week,
                            Value = g.Sum(x => x.Revenue)
                        }).ToList();
            }
            List<RevenueDTO> listRes = new();
            for (var date = startDate; date <= endDate; date = date.AddDays(1))
            {
                listRes.Add(new()
                {
                    Year = date.Year,
                    Sub = CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(date, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday)
                });
            }
            listRes = listRes.GroupBy(x => new { x.Year, x.Sub })
                    .Select(g => new RevenueDTO { Year = g.Key.Year, Sub = g.Key.Sub })
                    .ToList();
            if (listRes.Count == 0 || list.Count == 0) return listRes;
            int list_index = 0, listRes_index = 0;
            while (listRes_index < listRes.Count)
            {
                if (listRes[listRes_index].Equals(list[list_index]))
                {
                    listRes[listRes_index] = list[list_index];
                    list_index++;
                    if (list_index == list.Count) break;
                }
                listRes_index++;
            }
            return listRes;
        }
        public IEnumerable<RevenueDTO> GetRevenueMonthDuration(DateTime startDate, DateTime endDate, ProductType type)
        {
            List<RevenueDTO> list = new();
            if (type == ProductType.COMBO)
            {
                var l = from x in _context.BillCombos
                        where x.PurchasedTime >= startDate && x.PurchasedTime <= endDate
                        select new { Year = x.PurchasedTime.Year, Month = x.PurchasedTime.Month, Revenue = x.TotalAmount };
                list = (from x in l.AsEnumerable()
                        group x by new { x.Year, x.Month } into g
                        select new RevenueDTO()
                        {
                            Year = g.Key.Year,
                            Sub = g.Key.Month,
                            Value = g.Sum(x => x.Revenue)
                        }).ToList();
            }
            else
            {
                var l = from x in _context.Bills
                        join y in _context.BillProducts
                        on x.Id equals y.BillId
                        where x.PurchasedTime >= startDate && x.PurchasedTime <= endDate && y.ProductId.StartsWith(DAStaticData.IdPrefix[type])
                        select new { Year = x.PurchasedTime.Year, Month = x.PurchasedTime.Month, Revenue = y.Amount };
                list = (from x in l.AsEnumerable()
                        group x by new { x.Year, x.Month } into g
                        select new RevenueDTO()
                        {
                            Year = g.Key.Year,
                            Sub = g.Key.Month,
                            Value = g.Sum(x => x.Revenue)
                        }).ToList();
            }
            List<RevenueDTO> listRes = new();
            for (var date = startDate; date <= endDate; date = date.AddDays(1))
            {
                listRes.Add(new()
                {
                    Year = date.Year,
                    Sub = date.Month
                });
            }
            listRes = listRes.GroupBy(x => new { x.Year, x.Sub })
                    .Select(g => new RevenueDTO { Year = g.Key.Year, Sub = g.Key.Sub })
                    .ToList();
            if (listRes.Count == 0 || list.Count == 0) return listRes;
            int list_index = 0, listRes_index = 0;
            while (listRes_index < listRes.Count)
            {
                if (listRes[listRes_index].Equals(list[list_index]))
                {
                    listRes[listRes_index] = list[list_index];
                    list_index++;
                    if (list_index == list.Count) break;
                }
                listRes_index++;
            }
            return listRes;
        }
        public IEnumerable<RevenueDTO> GetRevenueQuarterDuration(DateTime startDate, DateTime endDate, ProductType type)
        {
            List<RevenueDTO> list = new();
            if (type == ProductType.COMBO)
            {
                var l = from x in _context.BillCombos
                        where x.PurchasedTime >= startDate && x.PurchasedTime <= endDate
                        select new { Year = x.PurchasedTime.Year, Quarter = ((x.PurchasedTime.Month - 1) / 3) + 1, Revenue = x.TotalAmount };
                list = (from x in l.AsEnumerable()
                        group x by new { x.Year, x.Quarter } into g
                        select new RevenueDTO()
                        {
                            Year = g.Key.Year,
                            Sub = g.Key.Quarter,
                            Value = g.Sum(x => x.Revenue)
                        }).ToList();
            }
            else
            {
                var l = from x in _context.Bills
                        join y in _context.BillProducts
                        on x.Id equals y.BillId
                        where x.PurchasedTime >= startDate && x.PurchasedTime <= endDate && y.ProductId.StartsWith(DAStaticData.IdPrefix[type])
                        select new { Year = x.PurchasedTime.Year, Quarter = ((x.PurchasedTime.Month - 1) / 3) + 1, Revenue = y.Amount };
                list = (from x in l.AsEnumerable()
                        group x by new { x.Year, x.Quarter } into g
                        select new RevenueDTO()
                        {
                            Year = g.Key.Year,
                            Sub = g.Key.Quarter,
                            Value = g.Sum(x => x.Revenue)
                        }).ToList();
            }
            List<RevenueDTO> listRes = new();
            for (var date = startDate; date <= endDate; date = date.AddDays(1))
            {
                listRes.Add(new()
                {
                    Year = date.Year,
                    Sub = ((date.Month - 1) / 3) + 1
                });
            }
            listRes = listRes.GroupBy(x => new { x.Year, x.Sub })
                    .Select(g => new RevenueDTO { Year = g.Key.Year, Sub = g.Key.Sub })
                    .ToList();
            if (listRes.Count == 0 || list.Count == 0) return listRes;
            int list_index = 0, listRes_index = 0;
            while (listRes_index < listRes.Count)
            {
                if (listRes[listRes_index].Equals(list[list_index]))
                {
                    listRes[listRes_index] = list[list_index];
                    list_index++;
                    if (list_index == list.Count) break;
                }
                listRes_index++;
            }
            return listRes;
        }
        public IEnumerable<RevenueDTO> GetRevenueYearDuration(DateTime startDate, DateTime endDate, ProductType type)
        {
            List<RevenueDTO> list = new();
            if (type == ProductType.COMBO)
            {
                var l = from x in _context.BillCombos
                        where x.PurchasedTime >= startDate && x.PurchasedTime <= endDate
                        select new { Year = x.PurchasedTime.Year, Quarter = ((x.PurchasedTime.Month - 1) / 3) + 1, Revenue = x.TotalAmount };
                list = (from x in l.AsEnumerable()
                        group x by x.Year into g
                        select new RevenueDTO()
                        {
                            Year = g.Key,
                            Value = g.Sum(x => x.Revenue)
                        }).ToList();
            }
            else
            {
                var l = from x in _context.Bills
                        join y in _context.BillProducts
                        on x.Id equals y.BillId
                        where x.PurchasedTime >= startDate && x.PurchasedTime <= endDate && y.ProductId.StartsWith(DAStaticData.IdPrefix[type])
                        select new { Year = x.PurchasedTime.Year, Revenue = y.Amount };
                list = (from x in l.AsEnumerable()
                        group x by x.Year into g
                        select new RevenueDTO()
                        {
                            Year = g.Key,
                            Value = g.Sum(x => x.Revenue)
                        }).ToList();
            }
            List<RevenueDTO> listRes = new();
            for (var date = startDate; date <= endDate; date = date.AddDays(1))
            {
                listRes.Add(new()
                {
                    Year = date.Year,
                });
            }
            listRes = listRes.GroupBy(x => x.Year)
                    .Select(g => new RevenueDTO { Year = g.Key })
                    .ToList();
            if (listRes.Count == 0 || list.Count == 0) return listRes;
            int list_index = 0, listRes_index = 0;
            while (listRes_index < listRes.Count)
            {
                if (listRes[listRes_index].Equals(list[list_index]))
                {
                    listRes[listRes_index] = list[list_index];
                    list_index++;
                    if (list_index == list.Count) break;
                }
                listRes_index++;
            }
            return listRes;
        }
    }
}
