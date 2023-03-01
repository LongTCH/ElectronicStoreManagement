using Models.DTOs;
using Models.Entities;
using Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models;

public class LaptopRepository : Repository<LaptopDTO>, ILaptopRepository
{
    public LaptopRepository(ESMDbContext context) : base(context)
    {
    }
    public override IEnumerable<LaptopDTO>? GetAll()
    {
        return (from laptop in _context.Laptops
                select new LaptopDTO()
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
                    AvatarPath = @laptop.AvatarPath
                }).ToList();
    }
}
