using ESM.Modules.DataAccess.DTOs;
using ESM.Modules.DataAccess.Infrastructure;
using ESM.Modules.DataAccess.Models;

namespace ESM.Modules.DataAccess.Repositories;
public interface ILaptopRepository : IBaseRepository<LaptopDTO>
{
}
public class LaptopRepository : BaseRepository<LaptopDTO>, ILaptopRepository
{
    public LaptopRepository(ESMDbContext context) : base(context)
    {
    }
    public override IEnumerable<LaptopDTO>? GetAll()
    {
        return _context.Laptops.AsQueryable()
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
}
