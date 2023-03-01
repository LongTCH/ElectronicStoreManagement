using Models.DTOs;
using Models.Entities;
using Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class SmartphoneRepository : Repository<SmartphoneDTO>, ISmartphoneRepository
    {
        public SmartphoneRepository(ESMDbContext context) : base(context)
        {
        }
        public override IEnumerable<SmartphoneDTO>? GetAll()
        {
            return (from smartphone in _context.Smartphones
                    select new SmartphoneDTO()
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
                        AvatarPath= @smartphone.AvatarPath,
                        Series = smartphone.Series
                    }).ToList();
        }
    }
}
