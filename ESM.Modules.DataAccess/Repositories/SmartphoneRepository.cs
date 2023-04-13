using ESM.Modules.DataAccess.DTOs;
using ESM.Modules.DataAccess.Infrastructure;
using ESM.Modules.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace ESM.Modules.DataAccess.Repositories
{
    public interface ISmartphoneRepository : IProductRepository<Smartphone> {
    }
    public class SmartphoneRepository : ProductRepository<Smartphone>,  ISmartphoneRepository
    {
        public SmartphoneRepository(ESMDbContext context) : base(context)
        {
        }
        public override async Task<IEnumerable<Smartphone>?> GetAll()
        {
            return await _context.Smartphones.AsQueryable()
                    .Where(smartphone => smartphone.Remain > -1)
                    .ToListAsync();
        }
        public override async Task<object?> Add(Smartphone entity)
        {
            await _context.Smartphones.AddAsync(entity);
            await _context.SaveChangesAsync();
            return null;
        }
        public override async Task<object?> Update(Smartphone entity)
        {
            bool res = true;
            try
            {
                var hd = await _context.Smartphones.AsQueryable()
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
        public override async Task<object?> Delete(string id)
        {
            var p = await _context.Smartphones.SingleAsync(p => p.Id == id);
            p.Remain = -1;
            await _context.SaveChangesAsync();
            return null;
        }
        public override async Task<bool> IsIdExist(string id)
        {
            return await _context.Smartphones.AnyAsync(p => p.Id == id);
        }
        public override async Task<object?> AddList(IEnumerable<Smartphone> list)
        {
            bool res = true;
            try
            {
                await _context.Smartphones.AddRangeAsync(list);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex) { res = false; }
            return res;
        }
        public string GetSuggestID()
        {
            return GetSuggestID(ProductType.SMARTPHONE);
        }
        public IEnumerable<ReportMock> GetSoldNumberMonthDuration(DateTime startDate, DateTime endDate)
        {
            return GetSoldNumberMonthDuration(startDate, endDate, ProductType.SMARTPHONE);
        }

        public IEnumerable<ReportMock> GetSoldNumberQuarterDuration(DateTime startDate, DateTime endDate)
        {
            return GetSoldNumberQuarterDuration(startDate, endDate, ProductType.SMARTPHONE);
        }

        public IEnumerable<ReportMock> GetSoldNumberWeekDuration(DateTime startDate, DateTime endDate)
        {
            return GetSoldNumberWeekDuration(startDate, endDate, ProductType.SMARTPHONE);
        }

        public IEnumerable<ReportMock> GetSoldNumberYearDuration(DateTime startDate, DateTime endDate)
        {
            return GetSoldNumberYearDuration(startDate, endDate, ProductType.SMARTPHONE);
        }
        public IEnumerable<TopSellDTO> GetTopSoldProducts(DateTime startDate, DateTime endDate, int number)
        {
            return GetTopSoldProducts(startDate, endDate, ProductType.SMARTPHONE, number);
        }
        public IEnumerable<RevenueDTO> GetRevenueWeekDuration(DateTime startDate, DateTime endDate)
        {
            return GetRevenueWeekDuration(startDate, endDate, ProductType.SMARTPHONE);
        }
    }
}
