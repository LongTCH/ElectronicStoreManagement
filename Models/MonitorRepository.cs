using Models.DTOs;
using Models.Entities;
using Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models;

public class MonitorRepository : Repository<MonitorDTO>, IMonitorRepository
{
    public MonitorRepository(ESMDbContext context) : base(context)
    {
    }
    public override IEnumerable<MonitorDTO>? GetAll()
    {
        return (from monitor in _context.Monitors
                select new MonitorDTO()
                {
                    Name = monitor.Name,
                    ImagePath = @monitor.ImagePath,
                    Price = monitor.Price,
                    Company = monitor.Company,
                    DetailPath = @monitor.DetailPath,
                    Discount = monitor.Discount,
                    Id = monitor.Id,
                    Need = monitor.Need,
                    Panel = monitor.Panel,
                    RefreshRate = monitor.RefreshRate,
                    Remain = monitor.Remain,
                    Series = monitor.Series,
                    Size = monitor.Size,
                    AvatarPath = @monitor.AvatarPath
                }).ToList();
    }
}
