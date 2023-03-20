using ESM.Modules.DataAccess.DTOs;
using ESM.Modules.DataAccess.Infrastructure;
using ESM.Modules.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace ESM.Modules.DataAccess.Repositories;
public interface IMonitorRepository : IBaseRepository<MonitorDTO> { }
public class MonitorRepository : BaseRepository<MonitorDTO>, IMonitorRepository
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
                    AvatarPath = @monitor.AvatarPath,
                    Unit = monitor.Unit
                }).ToList();
    }
}
