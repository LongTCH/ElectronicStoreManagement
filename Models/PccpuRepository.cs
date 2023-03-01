using Models.DTOs;
using Models.Entities;
using Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models;

public class PccpuRepository : Repository<PccpuDTO>, IPccpuRepository
{
    public PccpuRepository(ESMDbContext context) : base(context)
    {
    }
    public override IEnumerable<PccpuDTO>? GetAll()
    {
        return (from pccpu in _context.Pccpus
                select new PccpuDTO()
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
                    AvatarPath = @pccpu.AvatarPath
                }).ToList();
    }
}
