using ESM.Modules.DataAccess.DTOs;
using ESM.Modules.DataAccess.Infrastructure;
using ESM.Modules.DataAccess.Models;

namespace ESM.Modules.DataAccess.Repositories
{
    public interface ISmartphoneRepository : IBaseRepository<SmartphoneDTO>, IProductRepository {
        string GetSuggestID();
    }
    public class SmartphoneRepository : BaseRepository<SmartphoneDTO>, ISmartphoneRepository
    {
        public SmartphoneRepository(ESMDbContext context) : base(context)
        {
        }
        public override IEnumerable<SmartphoneDTO>? GetAll()
        {
            return _context.Smartphones.AsQueryable()
                    .Where(smartphone => smartphone.Remain > -1)
                    .Select(smartphone => new SmartphoneDTO()
                    {
                        Name = smartphone.Name,
                        Company = smartphone.Company,
                        Storage = smartphone.Storage,
                        Cpu = smartphone.Cpu,
                        DetailPath = @smartphone.DetailPath,
                        Discount = smartphone.Discount,
                        Id = smartphone.Id,
                        ImagePath = @smartphone.ImagePath,
                        Price = smartphone.Price,
                        Ram = smartphone.Ram,
                        Remain = smartphone.Remain,
                        AvatarPath = @smartphone.AvatarPath,
                        Series = smartphone.Series,
                        Unit = smartphone.Unit
                    }).ToList();
        }
        public string GetSuggestID()
        {
            string? NewID = _context.Smartphones.OrderBy(p => p.Id).LastOrDefault()?.Id;
            if (NewID == null) return StaticData.IdPrefix[ProductType.SMARTPHONE] + "0000000";
            int counter = Convert.ToInt32(NewID[2..]);
            ++counter;
            return StaticData.IdPrefix[ProductType.SMARTPHONE] + counter.ToString().PadLeft(7, '0');
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
    }
}
