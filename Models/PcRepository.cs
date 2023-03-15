using Models.DTOs;
using Models.Entities;
using Models.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Models;

public class PcRepository : Repository<PcDTO>, IPcRepository
{
    public PcRepository(ESMDbContext context) : base(context)
    {
    }
    public override IEnumerable<PcDTO>? GetAll()
    {
        return (from pc in _context.Pcs
                select new PcDTO()
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
