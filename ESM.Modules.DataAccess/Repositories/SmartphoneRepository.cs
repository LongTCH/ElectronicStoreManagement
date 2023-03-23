using ESM.Modules.DataAccess.DTOs;
using ESM.Modules.DataAccess.Infrastructure;
using ESM.Modules.DataAccess.Models;

namespace ESM.Modules.DataAccess.Repositories
{
    public interface ISmartphoneRepository : IBaseRepository<SmartphoneDTO> { }
    public class SmartphoneRepository : BaseRepository<SmartphoneDTO>, ISmartphoneRepository
    {
        public SmartphoneRepository(ESMDbContext context) : base(context)
        {
        }
        public override IEnumerable<SmartphoneDTO>? GetAll()
        {
            return _context.Smartphones.AsQueryable()
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

    }
}
