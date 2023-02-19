using Models.DTOs;
using Models.Entities;
using Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models;

public class PcharddiskRepository : Repository<PcharddiskDTO>, IPcharddiskRepository
{
    public PcharddiskRepository(ESMDbContext context) : base(context)
    {
    }
    public override IEnumerable<PcharddiskDTO>? GetAll()
    {
        return (from pcharddisk in _context.Pcharddisks
                select new PcharddiskDTO()
                {
                    Name = pcharddisk.Name,
                    Company = pcharddisk.Company,
                    Connect = pcharddisk.Connect,
                    DetailPath = @pcharddisk.DetailPath,
                    Discount = pcharddisk.Discount,
                    Id = pcharddisk.Id,
                    ImagePath = @pcharddisk.ImagePath,
                    Price = pcharddisk.Price,
                    Remain = pcharddisk.Remain,
                    Series = pcharddisk.Series,
                    Storage = pcharddisk.Storage,
                    Type = pcharddisk.Type
                }).ToList();
    }
}
