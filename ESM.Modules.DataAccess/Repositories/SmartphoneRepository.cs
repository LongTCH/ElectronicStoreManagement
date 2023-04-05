using ESM.Modules.DataAccess.DTOs;
using ESM.Modules.DataAccess.Infrastructure;
using ESM.Modules.DataAccess.Models;

namespace ESM.Modules.DataAccess.Repositories
{
    public interface ISmartphoneRepository : IProductRepository<Smartphone> {
    }
    public class SmartphoneRepository : ProductRepository<Smartphone>,  ISmartphoneRepository
    {
        public SmartphoneRepository(ESMDbContext context) : base(context)
        {
        }
        public override IEnumerable<Smartphone>? GetAll()
        {
            return _context.Smartphones.AsQueryable()
                    .Where(smartphone => smartphone.Remain > -1)
                    .ToList();
        }
        public override object? Add(Smartphone entity)
        {
            _context.Smartphones.Add(entity);
            return null;
        }
        public override object? Update(Smartphone entity)
        {
            var hd = _context.Smartphones.AsQueryable()
                   .First(p => p.Id == entity.Id);
            _context.Entry(hd).CurrentValues.SetValues(entity);
            return null;
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
