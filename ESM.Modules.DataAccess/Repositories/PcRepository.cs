using ESM.Modules.DataAccess.DTOs;
using ESM.Modules.DataAccess.Infrastructure;
using ESM.Modules.DataAccess.Models;

namespace ESM.Modules.DataAccess.Repositories;
public interface IPcRepository : IBaseRepository<PcDTO> { }
public class PcRepository : BaseRepository<PcDTO>, IPcRepository
{
    public PcRepository(ESMDbContext context) : base(context)
    {
    }
    public override IEnumerable<PcDTO>? GetAll()
    {
        return _context.Pcs.AsQueryable()
                .Select(pc => new PcDTO()
                {
                    Name = pc.Name,
                    Company = pc.Company,
                    Cpu = pc.Cpu,
                    DetailPath = @pc.DetailPath,
                    Discount = pc.Discount,
                    Id = pc.Id,
                    ImagePath = @pc.ImagePath,
                    Price = pc.Price,
                    Need = pc.Need,
                    Ram = pc.Ram,
                    Series = pc.Series,
                    AvatarPath = @pc.AvatarPath,
                    Remain = pc.Remain,
                    Unit = pc.Unit
                }).ToList();
    }
}
