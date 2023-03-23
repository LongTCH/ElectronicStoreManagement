using ESM.Modules.DataAccess.DTOs;
using ESM.Modules.DataAccess.Infrastructure;
using ESM.Modules.DataAccess.Models;

namespace ESM.Modules.DataAccess.Repositories;
public interface IPccpuRepository : IBaseRepository<PccpuDTO> { }
public class PccpuRepository : BaseRepository<PccpuDTO>, IPccpuRepository
{
    public PccpuRepository(ESMDbContext context) : base(context)
    {
    }
    public override IEnumerable<PccpuDTO>? GetAll()
    {
        return _context.Pccpus.AsQueryable()
                .Select(pccpu => new PccpuDTO()
                {
                    Name = pccpu.Name,
                    ImagePath = @pccpu.ImagePath,
                    Price = pccpu.Price,
                    Company = pccpu.Company,
                    DetailPath = @pccpu.DetailPath,
                    Discount = pccpu.Discount,
                    Id = pccpu.Id,
                    Need = pccpu.Need,
                    Remain = pccpu.Remain,
                    Series = pccpu.Series,
                    Socket = pccpu.Socket,
                    AvatarPath = @pccpu.AvatarPath,
                    Unit = pccpu.Unit
                }).ToList();
    }
}
